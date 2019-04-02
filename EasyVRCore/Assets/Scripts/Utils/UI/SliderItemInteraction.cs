using ExtendedVRUI;
using UnityEngine;
using UnityEngine.UI;

public class SliderItemInteraction : MonoBehaviour
{
    [SerializeField] Slider slider;

    float m_sliderWidth = 0;
    float currentValue = 0;
    TimeBasedInteractiveItem m_interactiveItem;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnEnter()
    {
        currentValue = 0;
    }

    public void OnHover()
    {
       
    }

    public void OnExit()
    {
        currentValue = 0;
    }
}
