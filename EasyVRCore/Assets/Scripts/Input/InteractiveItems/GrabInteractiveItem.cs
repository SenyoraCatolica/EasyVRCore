using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInteractiveItem : MonoBehaviour
{
    GameObject m_colliderObject;
    GameObject m_grabedObject;
    Rigidbody m_rigidBody;

    private void Start()
    {
        ModuleInput.Instance.RegisterPhysicItem(this, gameObject);
    }

    private void SetCollidingObject(Collider col)
    {
        if (m_colliderObject || !col.GetComponent<Rigidbody>())
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
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            m_grabedObject.GetComponent<Rigidbody>().velocity = m_rigidBody.velocity;
            m_grabedObject.GetComponent<Rigidbody>().angularVelocity = m_rigidBody.angularVelocity;
        }
        m_grabedObject = null;
    }



    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
}
