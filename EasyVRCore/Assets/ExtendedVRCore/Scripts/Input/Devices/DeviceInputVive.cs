using EVR.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceInputVive : DeviceInputBase
{
    private Canvas m_inputCanvas = null;
    private GameObject m_camera = null;

    public DeviceInputVive()
    {
        GameObject go = Resources.Load("Prefabs/Input/ViveOrRift/SteamVRCamera") as GameObject;
        m_camera = MonoBehaviour.Instantiate<GameObject>(go);
        m_rayLength = Mathf.Infinity;
        SetControllers();
    }

    public override void Init(InputGeneralConfig config)
    {
        m_enabled = true;
        m_selectionTime = Mathf.Infinity;
    }

    public override void Clear()
    {
        m_enabled = false;
    }

    public override void ShowSelection(bool enabled)
    {
        m_enabled = enabled;
        if (!m_enabled)
        {
            m_inputCanvas.enabled = false;
        }
        else if (enabled != m_enabled)
        {
            m_inputCanvas.enabled = true;
        }
    }

    private Transform GetController(GameObject go)
    {
        return ModuleInput.Instance.MainController.transform;
    }

    private void SetControllers()
    {
        ModuleInput.Instance.SetControllers(m_camera.transform.GetChild(1).gameObject, m_camera.transform.GetChild(0).gameObject);
    }

    #region ButtonsMapping

    public override bool MainTiggerButton(InputButtonStates state)
    {

        bool ret = false;

        switch (state)
        {
            case InputButtonStates.UP:
                ret = (Input.GetButtonUp(InputStatics.Main_Trigger));
                break;
            case InputButtonStates.DOWN:
                ret = (Input.GetButtonDown(InputStatics.Main_Trigger));
                break;

            case InputButtonStates.PRESS:
                ret = (Input.GetButton(InputStatics.Main_Trigger));
                break;
            case InputButtonStates.NONE:
                break;
        }

        return ret;
    }

    public override bool AuxiliarTiggerButton(InputButtonStates state)
    {

        bool ret = false;

        switch (state)
        {
            case InputButtonStates.UP:
                ret = (Input.GetButtonUp(InputStatics.Auxiliar_Trigger));
                break;
            case InputButtonStates.DOWN:
                ret = (Input.GetButtonDown(InputStatics.Auxiliar_Trigger));
                break;

            case InputButtonStates.PRESS:
                ret = (Input.GetButton(InputStatics.Auxiliar_Trigger));
                break;
            case InputButtonStates.NONE:
                break;
        }

        return ret;
    }

    public override bool MainGripButton(InputButtonStates state)
    {
        bool ret = false;

        switch (state)
        {
            case InputButtonStates.UP:
                ret = (Input.GetButtonUp(InputStatics.Main_Grip));
                break;
            case InputButtonStates.DOWN:
                ret = (Input.GetButtonDown(InputStatics.Main_Grip));
                break;

            case InputButtonStates.PRESS:
                ret = (Input.GetButton(InputStatics.Main_Grip));
                break;
            case InputButtonStates.NONE:
                break;
        }

        return ret;
    }

    public override bool AuxiliarGripButton(InputButtonStates state)
    {
        bool ret = false;

        switch (state)
        {
            case InputButtonStates.UP:
                ret = (Input.GetButtonUp(InputStatics.Auxiliar_Grip));
                break;
            case InputButtonStates.DOWN:
                ret = (Input.GetButtonDown(InputStatics.Auxiliar_Grip));
                break;

            case InputButtonStates.PRESS:
                ret = (Input.GetButton(InputStatics.Auxiliar_Grip));
                break;
            case InputButtonStates.NONE:
                break;
        }

        return ret;
    }


    #endregion

}
