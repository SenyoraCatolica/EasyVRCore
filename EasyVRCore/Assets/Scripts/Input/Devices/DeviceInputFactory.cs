using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DeviceInputFactory
{

    #region Instance
    private static volatile DeviceInputFactory m_instance;
    private static object m_syncRoot = new System.Object();

    public static DeviceInputFactory Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_syncRoot)
                {
                    if (m_instance == null)
                    {
                        m_instance = new DeviceInputFactory();
                    }
                }
            }

            return m_instance;
        }
    }

    #endregion

    private string detectedHMD;

   public IDeviceInput GetDeviceSelection()
   {
        IDeviceInput deviceInput = null;

        InputGeneralConfig config = Resources.Load<InputGeneralConfig>("Input/InputGeneralConfig");

        if(config != null)
        {
            switch(config.deviceSelection)
            {
                case DeviceSelection.CARDBOARD:
                    {
                        deviceInput = new DeviceInputCardboard();
                        ModuleInput.Instance.Device = DeviceType.CARDBOARD;
                        break;
                    }
                case DeviceSelection.VIVEOrOCULUS:
                    {
                        deviceInput = new DeviceInputViveOrRift();
                        break;
                    }
            }
        }

        deviceInput.Init(config);
        DetectConnectedHDM();
        return deviceInput;
   }

    private void DetectConnectedHDM()
    {
        detectedHMD = XRDevice.model;
        if (detectedHMD.ToLower().Contains("vive"))
        {
            // Must be a Vive headset.
            ModuleInput.Instance.Device = DeviceType.VIVE;
        }
        else if (detectedHMD.ToLower().Contains("oculus"))
        {
            // Must be an Oculus headset.
            ModuleInput.Instance.Device = DeviceType.RIFT;
        }
        else
        {
            // Must be a Windows VR headset.
            ModuleInput.Instance.Device = DeviceType.WINDOWS;
        }
    }

}
