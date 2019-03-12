using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEVREvent", menuName = "Event/DisableGrabInputEvent", order = 1)]
public class DisableGrabInputEvent : ScriptableObject
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
