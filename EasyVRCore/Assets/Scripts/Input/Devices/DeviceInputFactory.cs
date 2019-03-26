using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        return deviceInput;
   }
}
