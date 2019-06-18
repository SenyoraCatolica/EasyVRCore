using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module : IModule
{
    protected bool m_active;
    protected float m_updateRate;
    protected float m_currentTime;

    protected Module() { }


    public virtual void Init(float updateRate)
    {
        m_updateRate = updateRate * 0.001f;
        m_active = true;
    }

    public abstract void Update();

    public abstract void Clear();

    public bool IsActive()
    {
        return m_active;
    }

    public void Activate()
    {
        m_active = true;
    }

    public void Deactivate()
    {
        m_active = false;
    }

    public virtual float GetUpdateRate()
    {
        return m_updateRate;
    }

    public virtual float LastUpdate()
    {
        return m_currentTime;
    }

    public virtual void SetLastUpdate(float currentTime)
    {
        m_currentTime = currentTime;
    }
}
