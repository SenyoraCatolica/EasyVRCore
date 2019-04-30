using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTouchEventType", menuName = "Event/Others/TouchEvent", order = 1)]
public class Touch : ScriptableObject
{
    public void OnTouch(String gameObjectName)
    {
        Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));    //todelete

        /*string completePath = "Prefabs/" + gameObjectName;
        GameObject go = Resources.Load<GameObject>(completePath);
        Instantiate(go);*/
    }
}
