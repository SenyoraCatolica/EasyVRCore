using System;
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

        ModuleEvents.Instance.RegisterEventListener(Resources.Load<EVREvent>("Events/OnGrabItem"), OnGrabbed);
        ModuleEvents.Instance.RegisterEventListener(Resources.Load<EVREvent>("Events/OnUnGrabItem"), OnReleased);

    }

    private void Update()
    {
        Debug.Log(GetComponent<Rigidbody>().velocity.ToString());
    }

    public void OnGrabbed(object sender, EventArgs eventArgs)
    {
        Debug.Log(gameObject.name + " Grabbed");
    }

    public void OnReleased(object sender, EventArgs eventArgs)
    {
        Debug.Log(gameObject.name + " Released");
    }
}
