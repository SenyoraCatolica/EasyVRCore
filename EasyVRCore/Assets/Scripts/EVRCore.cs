using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVRCore : MonoBehaviour
{

    #region Instance
    private static volatile EVRCore m_instance;
    private static object m_syncRoot = new System.Object();

    public static EVRCore Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_syncRoot)
                {
                    if (m_instance == null)
                    {
                        m_instance = new EVRCore();
                    }
                }
            }

            return m_instance;
        }
    }

    #endregion

    private List<IModule> m_modules;                     //List with all the modules


    private void Awake()
    {
        //Create all modules and add the to the list

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        int count = m_modules.Count;
        for (int i = 0; i < count; ++i)
        {
            if (m_modules[i].IsActive())
            {
                if (Time.time > m_modules[i].LastUpdate() + m_modules[i].GetUpdateRate())
                {
                    m_modules[i].Update();
                    m_modules[i].SetLastUpdate(Time.time - m_modules[i].GetUpdateRate());
                }
            }
        }
    }
}
   
