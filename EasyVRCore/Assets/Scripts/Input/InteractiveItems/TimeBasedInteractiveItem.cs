using System;
using UnityEngine;

namespace EverisUI
{
    public class TimeBasedInteractiveItem : SimpleClickInteractiveItem
    {
        [SerializeField]
        float m_selectionTime;

        float m_currentTime;

        [SerializeField]
        Action<float> OnHoverValue;

        public void DeregisterOnHoverValue(Action<float> action)
        {
            OnHoverValue -= action;
        }

        public void RegisterOnHoverValue(Action<float> action)
        {
            OnHoverValue += action;
        }

        public override void Exit()
        {
            base.Exit();
            m_currentTime = 0f;
            m_alreadySelect = false;
        }

        public override void Hover()
        {
            base.Hover();

            if (!m_alreadySelect && m_selectionTime != 0)
            {
                m_currentTime += Time.deltaTime;

                if (OnHoverValue != null)
                {
                    OnHoverValue(m_currentTime / m_selectionTime);
                }

                if (m_currentTime >= m_selectionTime)
                {
                    Select();
                    m_currentTime = 0f;
                    OnHoverValue(m_currentTime);
                }
            }
        }
    }
}

