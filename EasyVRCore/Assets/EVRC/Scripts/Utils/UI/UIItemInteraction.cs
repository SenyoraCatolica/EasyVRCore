using UnityEngine;
using ExtendedVRUI;
using UnityEngine.UI;

public class UIItemInteraction : MonoBehaviour
{

    SimpleClickInteractiveItem m_item;
    Image m_image;

    Color m_baseColor;
    [SerializeField] Color m_hoverColor;
    [SerializeField] Color m_selectedColor;

    // Start is called before the first frame update
    void Start()
    {
        m_item = GetComponent<SimpleClickInteractiveItem>();

        if(m_item != null)
        {
            m_image = GetComponent<Image>();

            if(m_image != null)
            {
                m_baseColor = m_image.color;

                m_item.RegisterOnEnter(OnEnter);
                m_item.RegisterOnExit(OnExit);
                m_item.RegisterOnSelect(OnSelect);
            }

            else
            {
                Destroy(this);

            }
        }

        else
        {
            Destroy(this);
        }
    }

    public void OnEnter()
    {
        m_image.color = m_hoverColor;
    }

    public void OnExit()
    {
        m_image.color = m_baseColor;
    }

    public void OnSelect()
    {
        m_image.color = m_selectedColor;
    }
}
