using EVR.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct InteractiveItemConfig
{
    public bool BlocksInput;
    public bool Autoselect;
    public bool AllowRepeatSelection;

    public bool StaysSelected;
    private bool m_isSelected_;

    public bool IsSelected()
    {
        return m_isSelected_;
    }

    public void setIsSelected(bool isSelected)
    {
        m_isSelected_ = isSelected;
    }
}

public abstract class InteractiveItem : MonoBehaviour, IInteractiveItem
{
    [SerializeField] protected InteractiveItemConfig m_config;
    [SerializeField] protected bool m_enabled = false;
    protected bool m_alreadySelect = false;

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Hover();
    public abstract void Select();

    public bool IsAutoselect()
    {
        return m_config.Autoselect;
    }

    public bool IsRepeatSelection()
    {
        return m_config.AllowRepeatSelection;
    }

    public bool IsSelectable()
    {
        return !m_alreadySelect && m_enabled;
    }

    public bool IsSelected()
    {
        return m_config.StaysSelected;
    }

    public void SetSelected(bool isSelected)
    {
        m_config.setIsSelected(isSelected);
    }

    public bool StaysSelected()
    {
        return m_config.StaysSelected;
    }
}
