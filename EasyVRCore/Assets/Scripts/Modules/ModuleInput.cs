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


    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
