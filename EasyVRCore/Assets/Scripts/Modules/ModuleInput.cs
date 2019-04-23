using EVR.UI;
using System.Collections.Generic;
using UnityEngine;


public enum DeviceType { CARDBOARD, RIFT, VIVE, WINDOWS}

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
    private Dictionary<GameObject, GrabInteractiveItem> m_physicItems = new Dictionary<GameObject, GrabInteractiveItem>();

    bool m_dragInputSystem = true;

    public GameObject MainController;
    public GameObject AuxiliarController;

    public DeviceType Device;

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

        if(m_dragInputSystem)
            m_deviceInput.Update(m_interactiveItems);

        if(Device == DeviceType.RIFT || Device == DeviceType.VIVE)
        if (Input.GetButton(InputStatics.Main_Selection) && Input.GetButton(InputStatics.Auxiliar_Selection))
        {
            ToggleInteractionMode();
        }
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

    public void RegisterPhysicItem(GrabInteractiveItem item, GameObject itemObject)
    {
        if (!m_physicItems.ContainsKey(itemObject))
        {
            m_physicItems[itemObject] = item;
            item.enabled = false;
        }
    }

    public Ray GetCurrentRay()
    {
        return m_deviceInput.GetCurrentPositionRay();
    }

    public Transform GetRayOriginMain()
    {
        Transform ret = null;

        switch (Device)
        {
            case DeviceType.CARDBOARD:
                ret = Camera.main.transform;
                break;

            case DeviceType.RIFT:
                ret = GameObject.Find(InputStatics.OculusMainController).transform;
                break;
            case DeviceType.VIVE:
                ret = GameObject.Find(InputStatics.SteamVRMainController).transform;

                break;
            case DeviceType.WINDOWS:
                ret = ret = Camera.main.transform;
                break;
        }

        return ret;
    }

    public Transform GetRayOriginAuxiliar()
    {
        Transform ret = null;

        switch (Device)
        {
            case DeviceType.CARDBOARD:
                break;

            case DeviceType.RIFT:
                ret = GameObject.Find(InputStatics.OculusAuxiliarController).transform;
                break;
            case DeviceType.VIVE:
                ret = GameObject.Find(InputStatics.SteamVRAuxiliarController).transform;

                break;
            case DeviceType.WINDOWS:
                break;
        }

        return ret;
    }

    private void SetDeviceControllers()
    {
        Transform ret = null;

        switch (Device)
        {
            case DeviceType.CARDBOARD:
                ret = Camera.main.transform;
                break;

            case DeviceType.RIFT:
                MainController = GameObject.Find(InputStatics.OculusMainController);
                AuxiliarController = GameObject.Find(InputStatics.OculusAuxiliarController);
                break;

            case DeviceType.VIVE:
                MainController = GameObject.Find(InputStatics.SteamVRMainController);
                AuxiliarController = GameObject.Find(InputStatics.SteamVRAuxiliarController);

                break;
            case DeviceType.WINDOWS:
                ret = ret = Camera.main.transform;
                break;
        }
    }

    public bool GetDragInputSystem()
    {
        return m_dragInputSystem;
    }

    private void OnEnableDragInputSystem(object sender, System.EventArgs e)
    {
        m_dragInputSystem = true;
    }

    private void OnDisableDragInputSystem(object sender, System.EventArgs e)
    {
        m_dragInputSystem = false;
    }

    private void ToggleInteractionMode()
    {

        m_dragInputSystem = !m_dragInputSystem;



        foreach(GameObject go in m_interactiveItems.Keys)
        {
            go.GetComponent<InteractiveItem>().enabled = m_dragInputSystem;
        }

        foreach (GameObject go in m_physicItems.Keys)
        {
            m_physicItems[go].enabled = !m_dragInputSystem;
        }

        ModuleEvents.Instance.RaiseEvent(null, Resources.Load<EVREventBool>("Events/OnInteractionModeChanged"), new BoolEventArgs(m_dragInputSystem));
    }

    public bool IsDragEnabled()
    {
        return m_dragInputSystem;
    }

    public bool GetMainTriggerButton(InputButtonStates state)
    {
        return m_deviceInput.MainTiggerButton(state);
    }

    public bool GetAuxiliarTriggerButton(InputButtonStates state)
    {
        return m_deviceInput.AuxiliarTiggerButton(state);
    }
}
