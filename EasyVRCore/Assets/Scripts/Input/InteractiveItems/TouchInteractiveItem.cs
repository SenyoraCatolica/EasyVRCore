using UnityEngine;
using UnityEngine.Events;

public class TouchInteractiveItem : MonoBehaviour
{
    [SerializeField] protected UnityEvent OnSelect;

    public void RegisterOnSelect(UnityAction action)
    {
        OnSelect.AddListener(action);
    }

    public void DeregisterOnSelect(UnityAction action)
    {
        OnSelect.RemoveListener(action);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == InputStatics.PlayerHandCollider)
        {
            if (OnSelect != null)
                OnSelect.Invoke();
        }
    }
}
