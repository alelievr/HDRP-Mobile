using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
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
                buildTarget == UnityEditor.BuildTarget.VisionOS ||
                buildTarget == UnityEditor.BuildTarget.LinuxHeadlessSimulation);
        }
#endif
    }
}
