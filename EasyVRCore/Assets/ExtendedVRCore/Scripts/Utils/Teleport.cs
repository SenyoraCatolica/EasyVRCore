using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    private float previous = 0.0f;

    GameObject target;

    Transform main_ray_origin = null;

    Transform steamCamera;

    private void Start()
    {
        GameObject obj = Resources.Load("Prefabs/Target") as GameObject;
        target = Instantiate(obj);
        transform.position = new Vector3(0, 0, 0);
        steamCamera = FindObjectOfType<SteamVR_Camera>().gameObject.transform;
    }

    void Update()
    {
        if (main_ray_origin == null)
        {
            main_ray_origin = ModuleInput.Instance.GetRayOriginMain();
        }

        previous -= Time.deltaTime;
        Ray ray = new Ray(main_ray_origin.position, main_ray_origin.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //If the ray is pointing at an element tagged as Furniture do not show the movement option

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Teleportable"))
            {
                target.SetActive(true);
                target.transform.position = new Vector3(hit.point.x, hit.point.y + 0.0001f, hit.point.z);
            }

            else
                target.SetActive(false);

        }

        //Only move the user if the target object is active
        if (ModuleInput.Instance.GetMainTriggerButton(InputButtonStates.UP) && target.activeInHierarchy)
        {
            ray = new Ray(main_ray_origin.position, main_ray_origin.forward);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~LayerMask.GetMask("Dragable")))
            {
                transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                ResetPos(transform);
                previous = 1.0f;
            }
        }
    }

    private void ResetPos(Transform desiredHeadPos)
    {
        Vector3 offsetPos = steamCamera.position - transform.position;
        transform.position = (desiredHeadPos.position - offsetPos);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
