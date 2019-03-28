using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 3.5f;
    private float X;
    private float Y;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log(Input.GetAxis(InputStatics.Vertical_Main_Axis).ToString());
            Debug.Log(Input.GetAxis(InputStatics.Horizontal_Main_Axis).ToString());


            transform.Rotate(new Vector3(Input.GetAxis(InputStatics.Vertical_Main_Axis) * speed, Input.GetAxis(InputStatics.Horizontal_Main_Axis) * speed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
            Debug.Log(transform.rotation.ToString());
        }

        if (Input.GetMouseButton(2))
        {
            transform.Translate(new Vector3(Input.GetAxis(InputStatics.Horizontal_Main_Axis) * speed, Input.GetAxis(InputStatics.Vertical_Main_Axis) * speed, 0));
        }
    }
}
