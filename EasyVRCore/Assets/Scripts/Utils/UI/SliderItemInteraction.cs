using EverisUI;
using UnityEngine;
using UnityEngine.UI;

public class SliderItemInteraction : MonoBehaviour
{
    [SerializeField] Color m_backgroundColor;
    [SerializeField] Color m_silderColor;
    [SerializeField] Image m_sliderImage;
    [SerializeField] Image m_backgroundImage;

    float m_sliderWidth = 0;
    float percentage = 0;
    float currentValue = 0;
    TimeBasedInteractiveItem m_interactiveItem;

    // Start is called before the first frame update
    void Start()
    {
        m_sliderWidth = m_sliderImage.rectTransform.rect.width;
        m_interactiveItem = GetComponent<TimeBasedInteractiveItem>();

        m_interactiveItem.RegisterOnEnter(OnEnter);
        m_interactiveItem.RegisterOnHover(OnHover);
        m_interactiveItem.RegisterOnExit(OnExit);

        m_sliderImage.color = m_silderColor;
        m_backgroundImage.color = m_backgroundColor;

        percentage = m_interactiveItem.SelectionTime / m_sliderWidth;
    }

    public void OnEnter()
    {
        currentValue = 0;
    }

    public void OnHover()
    {
        if(currentValue < m_interactiveItem.SelectionTime)
        {
            currentValue += percentage;
            Rect rect = m_sliderImage.rectTransform.rect;
            m_sliderImage.rectTransform.rect.Set(rect.x, rect.y, currentValue, rect.height);
        }
    }

    public void OnExit()
    {
        currentValue = 0;
    }
}
