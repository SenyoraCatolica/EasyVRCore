using System.Collections.Generic;
using UnityEngine;
using EVR.UI;

public abstract class DeviceInputBase : IDeviceInput
{
    protected bool m_enabled = false;
    protected float m_fillValue = 0;
    protected float m_selectionTime = 0f;
    protected Transform m_raySetPosition;
    protected int m_ignoreChannel = 0;
    protected float m_rayLength;
    protected GameObject m_previousObject = null;
    protected GameObject m_currentObject = null;


    public abstract void Init(InputGeneralConfig config);
    public abstract void Clear();
    public abstract void ShowSelection(bool enabled);

    public virtual void Update(Dictionary<GameObject, IInteractiveItem> interactiveItems)
    {
        if (!m_enabled)
        {
            return;
        }

        if (m_currentObject != null && interactiveItems[m_currentObject].StaysSelected())
        {
            if (Input.GetButtonDown(InputStatics.Main_Selection))
            {
                interactiveItems[m_currentObject].Select();
                interactiveItems[m_currentObject].SetSelected(false);
                m_currentObject = null;
            }
            interactiveItems[m_currentObject].Hover();
            return;
        }


        Ray ray = GetCurrentPositionRay();

        RaycastHit[] raycastHit = Physics.RaycastAll(ray, m_rayLength, ~m_ignoreChannel);
        if (raycastHit.Length == 0)
        {
            if (m_previousObject != null)
            {
                interactiveItems[m_previousObject].Exit();
                m_previousObject = null;
                m_fillValue = 0f;
            }
        }
        else
        {
            int length = raycastHit.Length;

            int closest_object_index = 0;
            float closest_object_distance = Mathf.Infinity;
            for (int i = 0; i < raycastHit.Length; ++i)
            {
                if (raycastHit[i].distance < closest_object_distance)
                {
                    closest_object_index = i;
                    closest_object_distance = raycastHit[i].distance;
                }
            }

            GameObject hitObject = raycastHit[closest_object_index].collider.gameObject;

            //if the object is not in the dictionary let's check if it has an interactive item attached
            if (interactiveItems.ContainsKey(hitObject))
            {
                if (hitObject == m_previousObject)
                {
                    if (interactiveItems[m_previousObject].IsSelectable())
                    {
                        interactiveItems[m_previousObject].Hover();

                        if (interactiveItems[m_previousObject].IsAutoselect())
                        {
                            m_fillValue += Time.deltaTime;
                        }
                        if (Input.GetButtonDown("R_Selection"))
                        {
                            m_fillValue = 0f;
                            interactiveItems[m_previousObject].Select();
                            if (interactiveItems[m_previousObject].StaysSelected())
                            {
                                m_currentObject = hitObject;
                                interactiveItems[m_previousObject].SetSelected(true);
                            }
                        }
                        else if ((m_fillValue / m_selectionTime) >= 1.0f)
                        {
                            m_fillValue = 0f;
                            interactiveItems[m_previousObject].Select();
                            if (interactiveItems[m_previousObject].StaysSelected())
                            {
                                m_currentObject = hitObject;
                                interactiveItems[m_previousObject].SetSelected(true);
                            }
                        }
                    }
                }
                else
                {
                    m_fillValue = 0f;
                    if (m_previousObject != null)
                    {
                        interactiveItems[m_previousObject].Exit();
                    }
                    m_previousObject = hitObject;
                    interactiveItems[m_previousObject].Enter();
                }
            }

        }
    }

    public Ray GetCurrentPositionRay()
    {
        Ray ray = new Ray(m_raySetPosition.position, m_raySetPosition.forward);
        return ray;
    }

    [System.Diagnostics.Conditional("EDITOR")]
    private void DrawDebug(Ray ray)
    {
        if (Application.isEditor)
        {
            Debug.DrawRay(ray.origin, ray.direction * m_rayLength, Color.red);
        }
    }
}
