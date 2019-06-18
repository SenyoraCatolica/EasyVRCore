using EVR.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceInputRift : DeviceInputBase
{
    private Canvas m_inputCanvas = null;
    bool m_mainAxisInUse = false;
    bool m_auxAxisInUse = false;
    bool m_mainGripAxisInUse = false;
    bool m_auxGripAxisInUse = false;

    private LineRenderer m_line;


    public DeviceInputRift()
    {
        GameObject go = Resources.Load("Prefabs/Input/ViveOrRift/OculusCamera") as GameObject;
        GameObject tmp = MonoBehaviour.Instantiate<GameObject>(go);
        m_rayLength = Mathf.Infinity;

        SetControllers();
        m_raySetPosition = ModuleInput.Instance.MainController.transform;
        m_line = ModuleInput.Instance.MainController.GetComponent<LineRenderer>();
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

    public override void Update(Dictionary<GameObject, IInteractiveItem> interactiveItems)
    {
        if (MainGripButton(InputButtonStates.PRESS))
        {
            m_line.enabled = false;
            return;
        }

        else
        {
            m_line.enabled = true;
            base.Update(interactiveItems);
        }
    }

    private void SetControllers()
    {
        ModuleInput.Instance.SetControllers(GameObject.Find(InputStatics.OculusMainController), GameObject.Find(InputStatics.OculusAuxiliarController));
    }

    #region ButtonsMapping

    public override bool MainTiggerButton(InputButtonStates state)
    {

        bool ret = false;

        switch (state)
        {
            case InputButtonStates.UP:
                ret = (Input.GetAxis(InputStatics.Main_Trigger_Rift) < 0.2f);
                break;
            case InputButtonStates.DOWN:
                if (Input.GetAxis(InputStatics.Main_Trigger_Rift) != 0)
                {
                    if (!m_mainAxisInUse)
                    {
                        m_mainAxisInUse = true;
                        ret = true;
                    }
                }

                if (Input.GetAxisRaw(InputStatics.Main_Trigger_Rift) == 0)
                {
                    m_mainAxisInUse = false;
                }
                break;

            case InputButtonStates.PRESS:
                ret = (Input.GetAxisRaw(InputStatics.Main_Trigger_Rift) != 0);
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
                ret = (Input.GetAxis(InputStatics.Auxiliar_Trigger) < 0.2f);
                break;
            case InputButtonStates.DOWN:
                if (Input.GetAxis(InputStatics.Main_Trigger) != 0)
                {
                    if (!m_auxAxisInUse)
                    {
                        m_auxAxisInUse = true;
                        ret = true;
                    }
                }

                if (Input.GetAxisRaw(InputStatics.Main_Trigger) == 0)
                {
                    m_auxAxisInUse = false;
                }

                break;

            case InputButtonStates.PRESS:
                ret = (Input.GetAxisRaw(InputStatics.Auxiliar_Trigger) == 1.0f);
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
                ret = (Input.GetAxis(InputStatics.Main_Grip) < 0.2f);
                break;
            case InputButtonStates.DOWN:
                if (Input.GetAxis(InputStatics.Main_Grip) != 0)
                {
                    if (!m_mainGripAxisInUse)
                    {
                        m_auxAxisInUse = true;
                        ret = true;
                    }
                }

                if (Input.GetAxisRaw(InputStatics.Main_Grip) == 0)
                {
                    m_mainGripAxisInUse = false;
                }
                break;

            case InputButtonStates.PRESS:
                ret = (Input.GetAxisRaw(InputStatics.Main_Grip) != 0);
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
                ret = (Input.GetAxis(InputStatics.Auxiliar_Grip) < 0.2f);
                break;
            case InputButtonStates.DOWN:
                if (Input.GetAxis(InputStatics.Auxiliar_Grip) != 0)
                {
                    if (!m_auxGripAxisInUse)
                    {
                        m_auxAxisInUse = true;
                        ret = true;
                    }
                }

                if (Input.GetAxisRaw(InputStatics.Auxiliar_Grip) == 0)
                {
                    m_auxGripAxisInUse = false;
                }

                break;

            case InputButtonStates.PRESS:
                ret = (Input.GetAxisRaw(InputStatics.Auxiliar_Grip) != 0);
                break;
            case InputButtonStates.NONE:
                break;
        }

        return ret;
    }

    #endregion
}
