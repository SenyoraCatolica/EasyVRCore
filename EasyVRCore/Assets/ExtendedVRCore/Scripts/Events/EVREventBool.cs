using System;
using UnityEngine;

public class BoolEventArgs : EventArgs
{
    private bool m_bool;
    public bool Bool
    { get { return m_bool; } private set { } }

    public BoolEventArgs(bool value)
    {
        m_bool = value;
    }

    public BoolEventArgs()
    {
        m_bool = false;
    }
}

[CreateAssetMenu(fileName = "NewEVREventBool", menuName = "Event/EVREventBool", order = 1)]
public class EVREventBool : EVREvent
{
    public EVREventBool()
    {
        EventArgs = new BoolEventArgs();
    }

}
