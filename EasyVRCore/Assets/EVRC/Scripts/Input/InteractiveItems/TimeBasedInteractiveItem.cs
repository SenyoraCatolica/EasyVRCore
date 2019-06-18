using System;
using UnityEngine;

namespace ExtendedVRUI
{
    public class TimeBasedInteractiveItem : SimpleClickInteractiveItem
    {
        public float SelectionTime;

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

            if (!m_alreadySelect && SelectionTime != 0)
            {
                m_currentTime += Time.deltaTime;

                OnHoverValue?.Invoke(m_currentTime / SelectionTime);

                if (m_currentTime >= SelectionTime)
                {
                    Select();
                    m_currentTime = 0f;

                    OnHoverValue?.Invoke(m_currentTime);
                }
            }
        }

        public float GetCurrentTime()
        {
            return m_currentTime;
        }
    }
}

