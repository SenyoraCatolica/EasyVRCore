using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEVREvent", menuName = "Event/EnableGrabInputEvent", order = 1)]
public class EnableGrabInputEvent : ScriptableObject
{
    private EventArgs m_eventArgs;

    public EventArgs EventArgs
    {
        get
        {
            return m_eventArgs;
        }

        set
        {
            m_eventArgs = value;
        }
    }
}
