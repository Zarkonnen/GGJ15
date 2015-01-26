# Version Number Display for Unity

This code uses 4 components:

- VersionNumberController class: gets the version number from the assembly, pushes it to the label of a ILabelView
- ILabelView interface: provides an interface definition for a class that displays the version number to the user, in whathever form that may be
- VersionNumber class: example implementation of ILabelView - this is a MonoBehaviour than can be dropped onto a game object, and it will show the version number in the bottom left corner of the screen
- AssemblyInformation file: explained below

## AssemblyInformation.cs file

This file needs to be written and can then be dropped anywhere into the codebase, as long as it is part of the CSharp-Assembly.

It contains the following information:

    using System.Reflection;
    
    [assembly:AssemblyVersion ("1.1.*")]
    [assembly:AssemblyCompany ("Your Company")]
    [assembly:AssemblyProduct ("Your Gamename")]
    [assembly:AssemblyInformationalVersion ("1.1.0")]


The first two parts of the AssemblyVersion version number need to set manually, the last part ("*") will be set during the build process and be the build number.

The build number increments daily (see [Microsoft Documentation][1]).

The AssemblyInformationalVersion is free text and can be set to any string.

When building the Unity project, the "Assembly-CSharp.dll" library in the "[Gamename]_Data/Managed/" directory will have the version property set to this version. This information can be used by programs that compile an installer for your game.

[1]:  http://msdn.microsoft.com/en-us/library/system.reflection.assemblyversionattribute(v=vs.110).aspx