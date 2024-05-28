using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Unity.CompilationPipeline.Common.Diagnostics;
using Unity.CompilationPipeline.Common.ILPostProcessing;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using ILPPInterface = Unity.CompilationPipeline.Common.ILPostProcessing.ILPostProcessor;
using MethodAttributes = Mono.Cecil.MethodAttributes;

namespace UnityEditor.HighDefinition.CodeGen
{
    class HDRPILPP : ILPostProcessor
    {
        PostProcessorAssemblyResolver m_AssemblyResolver;
        ModuleDefinition m_UnityModule;
        ModuleDefinition m_HDRPModule;
        ModuleDefinition m_MainModule;

        readonly List<DiagnosticMessage> m_Diagnostics = new();

        public override ILPostProcessResult Process(ICompiledAssembly compiledAssembly)
        {
            if (!WillProcess(compiledAssembly))
                return null;

            m_Diagnostics.Clear();

            // read
            var assemblyDefinition = CodeGenHelpers.AssemblyDefinitionFor(compiledAssembly, out m_AssemblyResolver);
            if (assemblyDefinition == null)
            {
                m_Diagnostics.AddError($"Cannot read assembly definition: {compiledAssembly.Name}");
                return new ILPostProcessResult(null, m_Diagnostics);
            }

            (m_UnityModule, m_HDRPModule) = CodeGenHelpers.FindBaseModules(assemblyDefinition, m_AssemblyResolver);

            if (m_UnityModule == null)
            {
                m_Diagnostics.AddError($"Cannot find Unity module: {CodeGenHelpers.UnityModuleName}");
                return new ILPostProcessResult(null, m_Diagnostics);
            }

            // process
            var mainModule = assemblyDefinition.MainModule;
            if (mainModule != null)
            {
                m_MainModule = mainModule;
            }
            else
            {
                m_Diagnostics.AddError("Main module null!");
            }

            PostProcessAllTypes(m_Diagnostics, m_MainModule.GetAllTypes(), compiledAssembly.Defines);

            var pe = new MemoryStream();
            var pdb = new MemoryStream();
            var writerParameters = new WriterParameters
            {
                SymbolWriterProvider = new PortablePdbWriterProvider(), SymbolStream = pdb, WriteSymbols = true
            };

            assemblyDefinition.Write(pe, writerParameters);
            return new ILPostProcessResult(new InMemoryAssembly(pe.ToArray(), pdb.ToArray()), m_Diagnostics);
        }

        public override ILPostProcessor GetInstance() => this;

        public override bool WillProcess(ICompiledAssembly compiledAssembly)
        {
            // We are only interested in changing the code of HDRP
            return compiledAssembly.Name == "Unity.RenderPipelines.HighDefinition.Runtime";
        }

        void ReplaceMethod(List<DiagnosticMessage> messages, List<TypeDefinition> types, string originalMethodName, string newMethodName)
        {
            MethodDefinition originalMethod = null;
            MethodDefinition newMethod = null;

            foreach (var type in types)
            {
                foreach (var method in type.Methods)
                {
                    if (method.Name == originalMethodName)
                        originalMethod = method;
                    if (method.Name == newMethodName)
                        newMethod = method;
                }
            }

            if (originalMethod == null)
            {
                messages.AddError($"Can't find {originalMethodName}!");
                return;
            }

            if (newMethod == null)
            {
                messages.AddError($"Can't find {newMethodName} while trying to replace {originalMethodName}!");
                return;
            }
            
            originalMethod.Body = newMethod.Body;
        }

        void PostProcessAllTypes(List<DiagnosticMessage> messages, IEnumerable<TypeDefinition> types, string[] assemblyDefines)
        {
            var typeList = types.ToList();

            if (assemblyDefines.Contains("UNITY_EDITOR"))
                ReplaceMethod(messages, typeList, "IsSupportedBuildTarget", "NewIsSupportedBuildTarget");
            ReplaceMethod(messages, typeList, "IsSupportedGraphicDevice", "NewIsSupportedGraphicDevice");
        }
    }
}
