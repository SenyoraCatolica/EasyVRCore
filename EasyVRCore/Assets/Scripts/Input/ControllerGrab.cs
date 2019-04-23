using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrab : MonoBehaviour
{
    [SerializeField] GameObject m_handModel;
    [SerializeField] GameObject m_controllerModel;

    Animator m_handAnimator;

    GameObject m_colliderObject;
    GameObject m_grabedObject;
    LineRenderer m_line;

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    bool m_enabled = false;
    bool m_isControllerRight;

    private void Start()
    {
        m_enabled = !ModuleInput.Instance.IsDragEnabled();
        ModuleEvents.Instance.RegisterEventListener(Resources.Load<EVREventBool>("Events/OnInteractionModeChanged"), OnDragModeChanged);
        m_line = GetComponentInChildren<LineRenderer>();

        if (gameObject.name == ModuleInput.Instance.MainController.name)
            m_isControllerRight = true;
        else
            m_isControllerRight = false;

        trackedObj = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObj.index);

        m_handAnimator = m_handModel.GetComponent<Animator>();
        m_handModel.SetActive(true);
    }

    private void Update()
    {
        if(m_enabled)
        {
            if(m_isControllerRight)
            {
                if (ModuleInput.Instance.GetMainTriggerButton(InputButtonStates.UP))
                {
                    if (m_colliderObject && !m_grabedObject)
                    {
                        GrabItem();
                        return;
                    }

                    if (m_grabedObject)
                    {
                        ReleaseItem();
                        return;
                    }
                }
            }

            else
            {
                if (ModuleInput.Instance.GetAuxiliarTriggerButton(InputButtonStates.UP))
                {
                    if (m_colliderObject && !m_grabedObject)
                    {
                        GrabItem();
                        return;
                    }

                    if (m_grabedObject)
                    {
                        ReleaseItem();
                        return;
                    }
                }
            }
        }
    }

    private void SetCollidingObject(Collider col)
    {
        if (m_colliderObject || !col.GetComponent<Rigidbody>() || !col.GetComponent<GrabInteractiveItem>())
            return;
        else
            m_colliderObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!m_colliderObject)
        {
            return;
        }

        m_colliderObject = null;
    }


    void GrabItem()
    {
        m_grabedObject = m_colliderObject;
        m_colliderObject = null;

        Joint joint = AddFixedJoint();
        joint.connectedBody = m_grabedObject.GetComponent<Rigidbody>();

        ModuleEvents.Instance.RaiseEvent(this, Resources.Load<EVREvent>("Events/OnGrabItem"));
    }

    private void ReleaseItem()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            m_grabedObject.GetComponent<Rigidbody>().velocity = device.velocity * 2;
            m_grabedObject.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
        }
        m_grabedObject = null;

        ModuleEvents.Instance.RaiseEvent(this, Resources.Load<EVREvent>("Events/OnUnGrabItem"));
    }



    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    public void OnDragModeChanged(object sender, EventArgs eventArgs)
    {
        m_enabled = !((BoolEventArgs)eventArgs).Bool;

        m_line.enabled = !m_enabled;

        if(m_handModel)
            m_handModel.SetActive(m_enabled);

        if(m_controllerModel)
            m_controllerModel.SetActive(!m_enabled);
    }
}
