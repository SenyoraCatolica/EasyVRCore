using System.Collections.Generic;
using UnityEngine;
using EVR.UI;


public enum InputButtonStates { UP, DOWN, PRESS, NONE}

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

    public abstract bool MainTiggerButton(InputButtonStates state);
    public abstract bool MainGripButton(InputButtonStates state);

    public abstract bool AuxiliarTiggerButton(InputButtonStates state);
    public abstract bool AuxiliarGripButton(InputButtonStates state);


    public virtual void Update(Dictionary<GameObject, IInteractiveItem> interactiveItems)
    {
        if (m_currentObject != null)
        {
            foreach (GameObject go in interactiveItems.Keys)
            {
                if (interactiveItems[go].IsSelected())
                {
                    if (interactiveItems[go].IsDragAndDrop())
                    {
                        //Unselect object drag and drop
                        if (MainTiggerButton(InputButtonStates.UP))
                        {
                            interactiveItems[go].Unselect();
                            interactiveItems[go].SetSelected(false);
                            m_currentObject = null;
                            return;
                        }
                    }

                    else
                    {
                        //Unselect object click
                        if (MainTiggerButton(InputButtonStates.DOWN))
                        {
                            interactiveItems[go].Unselect();
                            interactiveItems[go].SetSelected(false);
                            m_currentObject = null;
                            return;
                        }
                    }
                }
            }
        }

        else
        {
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
                            if (MainTiggerButton(InputButtonStates.DOWN))
                            {
                                m_fillValue = 0f;
                                interactiveItems[m_previousObject].Select();
                                if (interactiveItems[m_previousObject].StaysSelected())
                                {
                                    interactiveItems[m_previousObject].SetSelected(true);
                                    m_currentObject = m_previousObject;
                                }
                            }
                            else if ((m_fillValue / m_selectionTime) >= 1.0f)
                            {
                                m_fillValue = 0f;
                                interactiveItems[m_previousObject].Select();
                                if (interactiveItems[m_previousObject].StaysSelected())
                                {
                                    interactiveItems[m_previousObject].SetSelected(true);
                                    m_currentObject = m_previousObject;
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

    }

    public Ray GetCurrentPositionRay()
    {
        Ray ray = new Ray(ModuleInput.Instance.MainController.transform.position, ModuleInput.Instance.MainController.transform.forward);
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
