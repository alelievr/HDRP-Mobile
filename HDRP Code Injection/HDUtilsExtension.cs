using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using System;
using System.Linq.Expressions;
using System.Reflection;
using UnityEditor;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace UnityEngine.Rendering.HighDefinition
{
    public static class HDUtilsExtension
    {
        internal static bool NewIsSupportedGraphicDevice(GraphicsDeviceType graphicDevice)
        {
            return (graphicDevice == GraphicsDeviceType.Direct3D11 ||
                    graphicDevice == GraphicsDeviceType.Direct3D12 ||
                    graphicDevice == GraphicsDeviceType.PlayStation4 ||
                    graphicDevice == GraphicsDeviceType.PlayStation5 ||
                    graphicDevice == GraphicsDeviceType.PlayStation5NGGC ||
                    graphicDevice == GraphicsDeviceType.XboxOne ||
                    graphicDevice == GraphicsDeviceType.XboxOneD3D12 ||
                    graphicDevice == GraphicsDeviceType.GameCoreXboxOne ||
                    graphicDevice == GraphicsDeviceType.GameCoreXboxSeries ||
                    graphicDevice == GraphicsDeviceType.Metal ||
                    graphicDevice == GraphicsDeviceType.Vulkan ||
                    graphicDevice == GraphicsDeviceType.Switch);
        }

#if UNITY_EDITOR
        // This function can't be in HDEditorUtils because we need it in HDRenderPipeline.cs (and HDEditorUtils is in an editor asmdef)
        internal static bool NewIsSupportedBuildTarget(UnityEditor.BuildTarget buildTarget)
        {
            Debug.Log(buildTarget);
            return (buildTarget == UnityEditor.BuildTarget.StandaloneWindows ||
                buildTarget == UnityEditor.BuildTarget.StandaloneWindows64 ||
                buildTarget == UnityEditor.BuildTarget.StandaloneLinux64 ||
                buildTarget == UnityEditor.BuildTarget.Stadia ||
                buildTarget == UnityEditor.BuildTarget.StandaloneOSX ||
                buildTarget == UnityEditor.BuildTarget.WSAPlayer ||
                buildTarget == UnityEditor.BuildTarget.XboxOne ||
                buildTarget == UnityEditor.BuildTarget.GameCoreXboxOne ||
                buildTarget == UnityEditor.BuildTarget.GameCoreXboxSeries  ||
                buildTarget == UnityEditor.BuildTarget.PS4 ||
                buildTarget == UnityEditor.BuildTarget.PS5 ||
                buildTarget == UnityEditor.BuildTarget.iOS ||
                buildTarget == UnityEditor.BuildTarget.Switch ||
                buildTarget == UnityEditor.BuildTarget.Android ||
                buildTarget == UnityEditor.BuildTarget.LinuxHeadlessSimulation);
        }

//         internal static bool NewAreGraphicsAPIsSupported(UnityEditor.BuildTarget target, ref GraphicsDeviceType unsupportedGraphicDevice)
//         {
//             bool editor = false;
// #if UNITY_EDITOR
//             editor = !UnityEditor.BuildPipeline.isBuildingPlayer;
// #endif
//
//             if (editor)  // In the editor we use the current graphics device instead of the list to avoid blocking the rendering if an invalid API is added but not enabled.
//             {
//                 return HDUtils.IsSupportedGraphicDevice(SystemInfo.graphicsDeviceType);
//             }
//             else
//             {
//                 foreach (var graphicAPI in UnityEditor.PlayerSettings.GetGraphicsAPIs(target))
//                 {
//                     if (!HDUtils.IsSupportedGraphicDevice(graphicAPI))
//                     {
//                         unsupportedGraphicDevice = graphicAPI;
//                         return false;
//                     }
//                 }
//             }
//             return true;
//         }
//
//         internal static OperatingSystemFamily BuildTargetToOperatingSystemFamily(UnityEditor.BuildTarget target)
//         {
//             switch (target)
//             {
//                 case UnityEditor.BuildTarget.StandaloneOSX:
//                     return OperatingSystemFamily.MacOSX;
//                 case UnityEditor.BuildTarget.StandaloneWindows:
//                 case UnityEditor.BuildTarget.StandaloneWindows64:
//                     return OperatingSystemFamily.Windows;
//                 case UnityEditor.BuildTarget.StandaloneLinux64:
//                 case UnityEditor.BuildTarget.Stadia:
//                     return OperatingSystemFamily.Linux;
//                 default:
//                     return OperatingSystemFamily.Other;
//             }
//         }
//
//         internal static bool IsSupportedBuildTargetAndDevice(UnityEditor.BuildTarget activeBuildTarget, out GraphicsDeviceType unsupportedGraphicDevice)
//         {
//             GraphicsDeviceType systemGraphicsDeviceType = SystemInfo.graphicsDeviceType;
//             unsupportedGraphicDevice = systemGraphicsDeviceType;
//
//             // If the build target matches the operating system of the editor
//             // and if the graphic api is chosen automatically, then only the system's graphic device type matters
//             // otherwise, we need to iterate over every graphic api available in the list to track every non-supported APIs
//             // if the build target does not match the editor OS, then we have to check using the graphic api list
//             bool autoAPI = UnityEditor.PlayerSettings.GetUseDefaultGraphicsAPIs(activeBuildTarget) && (SystemInfo.operatingSystemFamily == HDUtils.BuildTargetToOperatingSystemFamily(activeBuildTarget));
//
//             // If the editor's graphics device type is null though, we still have to iterate the target's graphic api list.
//             bool skipCheckingAPIList = autoAPI && systemGraphicsDeviceType != GraphicsDeviceType.Null;
//
//             if (skipCheckingAPIList ? HDUtils.IsSupportedGraphicDevice(SystemInfo.graphicsDeviceType) : HDUtils.AreGraphicsAPIsSupported(activeBuildTarget, ref unsupportedGraphicDevice)
//                     && HDUtils.IsSupportedBuildTarget(activeBuildTarget)
//                     && HDUtils.IsOperatingSystemSupported(SystemInfo.operatingSystem))
//                 return true;
//
//             return false;
//         }

#endif
    }
}
