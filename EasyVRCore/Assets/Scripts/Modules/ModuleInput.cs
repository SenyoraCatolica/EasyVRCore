using EVR.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleInput : Module
{
    #region Instance
    private static volatile ModuleInput m_instance;
    private static object m_syncRoot = new System.Object();

    public static ModuleInput Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_syncRoot)
                {
                    if (m_instance == null)
                    {
                        m_instance = new ModuleInput();
                    }
                }
            }

            return m_instance;
        }
    }
    #endregion

    private IDeviceInput m_deviceInput;

    private Dictionary<GameObject, IInteractiveItem> m_interactiveItems = new Dictionary<GameObject, IInteractiveItem>();


    public GameObject MainController;
    public GameObject AuxiliarController;

    public override void Init(float updateRate)
    {
        base.Init(updateRate);
        CreateSelectionMethod();
        SetDeviceControllers();
    }

    public override void Update()
    {
        if (!m_active)
        {
            return;
        }

        m_deviceInput.Update(m_interactiveItems);
    }

    public override void Clear()
    {
        m_deviceInput.Clear();
    }

    private static void CreateSelectionMethod()
    {
        m_instance.m_deviceInput = DeviceInputFactory.Instance.GetDeviceSelection();
    }

    public void RegisterInteractiveItem(IInteractiveItem interactiveItem, GameObject interactiveObject)
    {
        if (!m_interactiveItems.ContainsKey(interactiveObject))
        {
            m_interactiveItems[interactiveObject] = interactiveItem;
        }
    }

    public Ray GetCurrentRay()
    {
        return m_deviceInput.GetCurrentPositionRay();
    }

    private void SetDeviceControllers()
    {
        MainController = GameObject.Find(InputStatics.MainController);
        AuxiliarController = GameObject.Find(InputStatics.MainController);
    }
}
