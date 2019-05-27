using ExtendedVRUI;
using UnityEngine;
using UnityEngine.UI;

public class SliderItemInteraction : MonoBehaviour
{
    [SerializeField] Slider slider;

    float m_sliderWidth = 0;
    float currentValue = 0;
    TimeBasedInteractiveItem m_interactiveItem;

    private void Start()
    {
        m_interactiveItem = GetComponent<TimeBasedInteractiveItem>();
    }

    private void Update()
    {
        slider.value = m_interactiveItem.GetCurrentTime();
    }
}
