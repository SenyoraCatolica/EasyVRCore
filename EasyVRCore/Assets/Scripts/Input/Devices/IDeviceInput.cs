using EVR.UI;
using System.Collections.Generic;
using UnityEngine;


public interface IDeviceInput
{
    void Init(InputGeneralConfig config);
    void Update(Dictionary<GameObject, IInteractiveItem> interactiveItems);
    void Clear();
    void ShowSelection(bool enabled);
    Ray GetCurrentPositionRay();
}
