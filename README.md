# ExtendedVRCore
This project includes an asset package for Unity to implement virtual reality to any Unity aplication for Android and Windows using diferent devices. It also includes a test scene to know how it works.

This is a Virtual Reality core that lets the users develop VR apps in Unity that supports multiplatform.

The devices that the EVRC can handle are any type of cardboard, Oculus Gaze, Oculus Rift and the HTC VIVE.

This core not only is useful to develop virtual reality project very easy but also contains an internat arquitecture based on modules which includes an Event system available from every part of the project.

* Oculus Rift settings 

In order to use Oculus Rift, the Oculus Integration from the Asset Store and the Oculus Avatar SDK and the Oculus Utilities from the Oculus Center website. To include them do the following steps:

Step 1: Download and import the Oculus Integration package from the Asset Store to ypur project. https://assetstore.unity.com/packages/tools/integration/oculus-integration-82022

Step 2: Donload and import the Oculus Utilities for Unity from the oculus official webpage.
https://developer.oculus.com/downloads/package/oculus-utilities-for-unity-5/

Step 3: Donload and import the Oculus Avatar SDK for Unity from the oculus official webpage to be able to use the oculus avatar simulation.
https://developer.oculus.com/downloads/package/oculus-avatar-sdk/



* HTC VIVE settings

In order to use the HTC VIVE device it is required to download and import the SteamVR plugin.
https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647



* Project settings

You will need some options activated to enable virtual reality in your project.
In the player setting, in the XR settings it is needed to include the OpenVR sdk and the Oculus sdk in the virtual reality options.



* Changing device platform.

To change the platform or the device which is wanted to be used for the current project is needed to look for the Scriptable Object called InputGeneralConfig and select the device wanted from the list of the available devices.

Make sure that in the build options, the platform is also corret, for example, vive is for platform windows and cardboard is for the platforms android or ios usually.
