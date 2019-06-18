using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TouchController : MonoBehaviour
{
    private Collider m_indexCollider;
    public XRNode NodeType;

    // Start is called before the first frame update
    void Start()
    {
        XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
        m_indexCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!CheckTriggerButton(InputButtonStates.UP))
            m_indexCollider.enabled = true;

        else
            m_indexCollider.enabled = false;

    }

    public  bool CheckTriggerButton(InputButtonStates state)
    {
        if (NodeType == XRNode.LeftHand)
            return Input.GetButton(InputStatics.Auxiliar_Trigger);

        else if (NodeType == XRNode.RightHand)
            return Input.GetButton(InputStatics.Main_Trigger);


        else
            Debug.Log("XRNode type mus be a Hand!  Current type is" + NodeType.ToString());

        return false;
    }
}
