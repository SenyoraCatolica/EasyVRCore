using System.Collections.Generic;
using UnityEngine;
using System;

public enum DeviceSelection
{
    CARDBOARD = 0,
    VIVEorOCULUS = 1,
}

[Serializable]
public struct InputBinding
{
    public DeviceSelection SelectionMethod;
    public int ButtonId;
    public string InputCommand;
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
    [SerializeField]
    List<InputBinding> InputBinding;
}
