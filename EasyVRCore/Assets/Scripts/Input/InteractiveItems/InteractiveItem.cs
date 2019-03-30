using EVR.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct InteractiveItemConfig
{
    public bool BlocksInput;
    public bool Autoselect;
    public bool AllowRepeatSelection;
    public bool AllowUnselect;

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
    public abstract void Unselect();

    [SerializeField] protected UnityEvent OnEnter;
    [SerializeField] protected UnityEvent OnExit;
    [SerializeField] protected UnityEvent OnHover;
    [SerializeField] protected UnityEvent OnSelect;
    [SerializeField] protected UnityEvent OnUnselect;

    public virtual void Awake()
    {
        ModuleInput.Instance.RegisterInteractiveItem(this, gameObject);

        if (OnEnter == null)
            OnEnter = new UnityEvent();

        if (OnExit == null)
            OnExit = new UnityEvent();

        if (OnHover == null)
            OnHover = new UnityEvent();

        if (OnSelect == null)
            OnSelect = new UnityEvent();

        if (OnUnselect == null)
            OnUnselect = new UnityEvent();
    }

    public virtual void Init()
    {
        m_enabled = false;
    }

    public void RegisterOnEnter(UnityAction action)
    {
        OnEnter.AddListener(action);
    }

    public void DeregisterOnEnter(UnityAction action)
    {
        OnEnter.RemoveListener(action);
    }

    public void RegisterOnExit(UnityAction action)
    {
        OnExit.AddListener(action);
    }

    public void DeregisterOnExit(UnityAction action)
    {
        OnExit.RemoveListener(action);
    }

    public void RegisterOnHover(UnityAction action)
    {
        OnHover.AddListener(action);
    }

    public void DeregisterOnHover(UnityAction action)
    {
        OnHover.RemoveListener(action);
    }

    public void RegisterOnSelect(UnityAction action)
    {
        OnSelect.AddListener(action);
    }

    public void DeregisterOnSelect(UnityAction action)
    {
        OnSelect.RemoveListener(action);
    }

    public void RegisterOnUnselect(UnityAction action)
    {
        OnSelect.AddListener(action);
    }

    public void DeregisterOnUnselect(UnityAction action)
    {
        OnSelect.RemoveListener(action);
    }

    public void ClearOnSelect()
    {
        OnSelect.RemoveAllListeners();
    }

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
        return m_config.IsSelected();
    }

    public void SetSelected(bool isSelected)
    {
        m_config.setIsSelected(isSelected);
    }

    public bool StaysSelected()
    {
        return m_config.StaysSelected;
    }

    public bool IsUnselectable()
    {
        return m_config.AllowUnselect;
    }
}
