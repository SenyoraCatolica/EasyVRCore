using System;
using UnityEngine;

public static class InputStatics
{
    //Controller names from SteamVR camera
    public static readonly string MainController = "Controller (right)";
    public static readonly string AuxiliarController = "Controller (left)";

    //right controller
    public static readonly string Horizontal_Main_Axis = "R_Horizontal";
    public static readonly string Vertical_Main_Axis = "R_Vertical";
    public static readonly string Main_Selection = "R_Selection";
    public static readonly string Main_Trigger = "R_Trigger";
    public static readonly string Main_TriggerAxis = "R_TriggerAxis";
    public static readonly string Main_Grip = "R_Grip";

    //left controller
    public static readonly string Horizontal_Auxiliar_Axis = "L_Horizontal";
    public static readonly string Vertical_Auxiliar_Axis = "L_Vertical";
    public static readonly string Auxiliar_Selection = "L_Selection";
    public static readonly string Auxiliar_Trigger = "L_Trigger";
    public static readonly string Auxiliar_Grip = "L_Grip";



    //Handle Collisions between diferent controllers
    public enum InputStates { None, Down, DownHold, Up };
    public enum InputsStateNames : int { MainTriggerAxis };
    private static InputStates[] InputsState = null;

    static float updateFrame = 0;
    static DeviceType device = DeviceType.WINDOWS;

    public static void UpdateInputs()
    {
        if (updateFrame == Time.frameCount) return;
        updateFrame = Time.frameCount;

        if (InputsState == null)
        {
            InputsState = new InputStates[Enum.GetValues(typeof(InputsStateNames)).Length];
            for (int i = 0; i < InputsState.Length; i++) InputsState[i] = InputStates.None;
        }

        foreach (int input in Enum.GetValues(typeof(InputsStateNames)))
        {
            float val = Input.GetAxis(Enum.GetName(typeof(InputsStateNames), input));
            if (InputsState[input] == InputStates.None && val == 1)
            {
                InputsState[input] = InputStates.Down;
            }
            else if (InputsState[input] == InputStates.Down || InputsState[input] == InputStates.DownHold)
            {
                if (val != 1)
                    InputsState[input] = InputStates.Up;
                else
                    InputsState[input] = InputStates.DownHold;
            }
            else if (InputsState[input] == InputStates.Up)
            {
                if (val != 1)
                {
                    InputsState[input] = InputStates.None;
                }
                else
                {
                    InputsState[input] = InputStates.Down;
                }
            }
        }
    }

    public static bool isInputState(InputsStateNames name, InputStates state) //Se supone que se executa en algun update minimo
    {
        UpdateInputs();
        return InputsState[(int)name] == state;
    }

    public static bool MainTriggerButtonState(InputStates state)
    {
        device = ModuleInput.Instance.Device;

        if (state == InputStates.Down)
        {
            return (device == DeviceType.VIVE && Input.GetButtonDown(InputStatics.Main_Trigger)) || (device == DeviceType.RIFT && isInputState(InputsStateNames.MainTriggerAxis, InputStates.Down));
        }
        if (state == InputStates.Up)
        {
            return (device == DeviceType.VIVE && Input.GetButtonUp(InputStatics.Main_Trigger)) || (device == DeviceType.RIFT && isInputState(InputsStateNames.MainTriggerAxis, InputStates.Up));
        }
        if(state == InputStates.DownHold)
        {
            return (device == DeviceType.VIVE && Input.GetButton(InputStatics.Main_Trigger)) || (device == DeviceType.RIFT && isInputState(InputsStateNames.MainTriggerAxis, InputStates.DownHold));
        }
        return isInputState(InputsStateNames.MainTriggerAxis, state);
    }
}
