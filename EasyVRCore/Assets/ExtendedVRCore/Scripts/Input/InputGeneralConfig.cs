using System.Collections.Generic;
using UnityEngine;
using System;

public enum DeviceSelection
{
    CARDBOARD = 0,
    VIVE = 1,
    RIFT = 2
}

[CreateAssetMenu(fileName = "InputGeneralConfig", menuName = "Input/DeviceConfig", order = 1)]
public class InputGeneralConfig : ScriptableObject
{
    [SerializeField]
    public bool ForceSelectionMethod;
    [SerializeField]
    public DeviceSelection deviceSelection;
    [SerializeField]
    public float UIReticleSize;
    [SerializeField]
    public float SelectionTime;
}
