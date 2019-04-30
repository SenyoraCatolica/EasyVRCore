using System;
using UnityEngine;

public static class InputStatics
{
    //Controller names from SteamVR camera
    public static readonly string SteamVRMainController = "Controller (right)";
    public static readonly string SteamVRAuxiliarController = "Controller (left)";

    public static readonly string OculusMainController = "RightHandAnchor";
    public static readonly string OculusAuxiliarController = "LeftHandAnchor";

    //right controller
    public static readonly string Horizontal_Main_Axis = "R_Horizontal";
    public static readonly string Vertical_Main_Axis = "R_Vertical";
    public static readonly string Main_Selection = "R_Selection";
    public static readonly string Main_Trigger = "R_Trigger";
    public static readonly string Main_Trigger_Rift = "Oculus_CrossPlatform_SecondaryIndexTrigger";
    public static readonly string Main_TriggerAxis = "R_TriggerAxis";
    public static readonly string Main_Grip = "R_Grip";

    //left controller
    public static readonly string Horizontal_Auxiliar_Axis = "L_Horizontal";
    public static readonly string Vertical_Auxiliar_Axis = "L_Vertical";
    public static readonly string Auxiliar_Selection = "L_Selection";
    public static readonly string Auxiliar_Trigger = "L_Trigger";
    public static readonly string Auxiliar_Grip = "L_Grip";

    //tags
    public static readonly string PlayerHandCollider = "PlayerHand";
}
