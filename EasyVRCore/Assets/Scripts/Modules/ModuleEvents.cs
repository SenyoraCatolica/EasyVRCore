using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleEvents : Module
{

    #region Instance
    private static volatile ModuleEvents m_instance;
    private static object m_syncRoot = new System.Object();

    public static ModuleEvents Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_syncRoot)
                {
                    if (m_instance == null)
                    {
                        m_instance = new ModuleEvents();
                    }
                }
            }

            return m_instance;
        }
    }
    #endregion

    private Dictionary<EVREvent, List<EventHandler<EventArgs>>> m_listeners;

    private ModuleEvents()
    {
        m_listeners = new Dictionary<EVREvent, List<EventHandler<EventArgs>>>();
    }

    public override void Update()
    {
        
    }

    public override void Clear()
    {
        m_listeners.Clear();
    }

    public bool RegisterEventListener(EVREvent eventType, EventHandler<EventArgs> eventHandler)
    {
        List<EventHandler<EventArgs>> result = null;
        m_listeners.TryGetValue(eventType, out result);
        if (result != null)
        {
            if (!result.Contains(eventHandler))
            {
                m_listeners[eventType].Add(eventHandler);
                return true;
            }
            else
            {
                Debug.LogWarning("Warning! trying to add the same event handler twice to the eventType " + eventType.name);
                return false;
            }
        }
        else
        {
            m_listeners[eventType] = new List<EventHandler<EventArgs>> { eventHandler };
        }
        return true;
    }

    public bool DeregisterEventListener(EVREvent eventType, EventHandler<EventArgs> eventHandler)
    {
        List<EventHandler<EventArgs>> result = null;
        m_listeners.TryGetValue(eventType, out result);
        if (result != null)
        {
            if (result.Contains(eventHandler))
            {
                m_listeners[eventType].Remove(eventHandler);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void RaiseEvent(object sender, EVREvent eventType, EventArgs args = null)
    {
        List<EventHandler<EventArgs>> resultList = null;
        m_listeners.TryGetValue(eventType, out resultList);
        if (resultList != null)
        {
            int count = resultList.Count;
            for (int i = 0; i < count; ++i)
            {
                resultList[i](sender, args);
            }
        }
    }
}
