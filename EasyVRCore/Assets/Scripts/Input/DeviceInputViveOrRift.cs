using EVR.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceInputViveOrRift : DeviceInputBase
{
    private Canvas m_inputCanvas = null;

    public DeviceInputViveOrRift()
    {
        GameObject go = Resources.Load("Prefabs/Input/ViveOrRift/ViveOrRiftCamera") as GameObject;
        GameObject tmp = MonoBehaviour.Instantiate<GameObject>(go);
        m_raySetPosition = GetController(tmp);
        m_rayLength = Mathf.Infinity;
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

    public override void Dispose()
    {

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
        Transform ret = go.transform.Find(InputStatics.MainController);
        return ret;
    }
}
