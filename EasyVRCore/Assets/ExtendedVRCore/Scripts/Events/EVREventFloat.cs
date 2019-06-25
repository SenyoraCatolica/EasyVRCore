using System;
using UnityEngine;

public class FloatEventArgs : EventArgs
{
    private float m_float;
    public float Float
    { get { return m_float; } private set { } }

    public FloatEventArgs(float value)
    {
        m_float = value;
    }

    public FloatEventArgs()
    {
        m_float = 0.5f;
    }
}

[CreateAssetMenu(fileName = "NewEVREventFloat", menuName = "Event/EVREventFloat", order = 1)]
public class EVREventFloat : EVREvent
{
    public EVREventFloat()
    {
        EventArgs = new FloatEventArgs();
    }

}
