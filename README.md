# HDRP Mobile

This project is a Unity package that allows HDRP to build on unsupported targets like Android, IOS, and Switch.

## Installation

From the package manager in Unity, add a new package from git URL and paste `https://github.com/alelievr/HDRP-Mobile.git`.

For more information see the [Unity package manager official documentation](https://docs.unity3d.com/Manual/upm-ui-giturl.html).

## Disclamer

Unity doesn't officially support these platforms, so don't expect it to work on the first try and without any bugs. Also, the API requirement for HDRP is unchanged, which means that for Android, you need to use Vulkan only and remove any other API in the list of supported graphics APIs.

## How does it work?

This package overrides two functions inside the HDRP code: `IsSupportedGraphicDevice` and `IsSupportedBuildTarget`; both of these functions contain a platform check that prevents HDRP from building and running on an unsupported platform even if the hardware could support it. 

The override mechanism uses a combination of [asmref](https://docs.unity3d.com/Manual/class-AssemblyDefinitionReferenceImporter.html) to inject the new code in the HDRP runtime assembly and ILPostProcessor which replaces the body of the two functions. ILPostProcessor is a [Mono.Cecil](https://www.mono-project.com/docs/tools+libraries/libraries/Mono.Cecil/) feature, which permits the modification of the IL code before the assembly is formed. This makes it compatible with all Unity backends including IL2CPP.
