using EVR.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceInputCardboard : DeviceInputBase
{
    private Canvas m_inputCanvas = null;
    private Image m_pointerImage = null;
    private Image m_radialImage = null;

    public DeviceInputCardboard()
    {
        MonoBehaviour.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Input/Cardboard/CardboardCamera"));
        GameObject go = Resources.Load<GameObject>("Prefabs/Input/Cardboard/CardboardReticleCanvas");
        m_inputCanvas = MonoBehaviour.Instantiate<GameObject>(go).GetComponent<Canvas>();
        m_inputCanvas.name = "InputCanvas";
        m_inputCanvas.transform.SetParent(Camera.main.transform, false);
        m_pointerImage = m_inputCanvas.transform.Find("Reticle").GetComponent<Image>();
        m_radialImage = m_inputCanvas.transform.Find("Radial").GetComponent<Image>();
        m_rayLength = Camera.main.farClipPlane;
        m_raySetPosition = Camera.main.transform;
    }

    public override void Init(InputGeneralConfig config)
    {
        m_pointerImage.rectTransform.localScale = m_pointerImage.rectTransform.localScale * config.UIReticleSize;
        m_radialImage.rectTransform.localScale = m_radialImage.rectTransform.localScale * config.UIReticleSize;
        m_selectionTime = config.SelectionTime;
        m_radialImage.fillAmount = m_fillValue = 0f;
        m_enabled = true;
    }

    public override void Update(Dictionary<GameObject, IInteractiveItem> interactiveItems)
    {
        base.Update(interactiveItems);
        m_radialImage.fillAmount = m_fillValue / m_selectionTime;
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

    public override bool MainTiggerButton(InputButtonStates state)
    {

        bool ret = false;

        switch (state)
        {
            case InputButtonStates.UP:
                ret = Input.GetButtonUp(InputStatics.Main_Selection);
                break;
            case InputButtonStates.DOWN:
                ret = Input.GetButtonDown(InputStatics.Main_Selection);
                break;
            case InputButtonStates.PRESS:
                ret = Input.GetButton(InputStatics.Main_Selection);
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
                ret = Input.GetButtonUp(InputStatics.Auxiliar_Trigger);
                break;
            case InputButtonStates.DOWN:
                ret = Input.GetButtonDown(InputStatics.Auxiliar_Trigger);
                break;
            case InputButtonStates.PRESS:
                ret = Input.GetButton(InputStatics.Auxiliar_Trigger);
                break;
            case InputButtonStates.NONE:
                break;
        }

        return ret;
    }
}
