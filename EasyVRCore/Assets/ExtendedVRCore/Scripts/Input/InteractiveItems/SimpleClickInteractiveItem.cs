using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtendedVRUI
{
    public class SimpleClickInteractiveItem : InteractiveItem
    {
        public override void Enter()
        {
            if (OnEnter != null)
            {
                OnEnter.Invoke();
            }
        }

        public override void Exit()
        {
            m_alreadySelect = false;
            if (OnExit != null)
            {
                OnExit.Invoke();
            }
        }

        public override void Hover()
        {
            if (OnHover != null)
            {
                OnHover.Invoke();
            }
        }

        public override void Select()
        {
            if (m_config.AllowRepeatSelection)
            {
                m_alreadySelect = false;
            }
            else
            {
                m_alreadySelect = true;
            }
            if (OnSelect != null)
            {
                OnSelect.Invoke();
            }
        }

        public override void Unselect()
        {
            if (OnUnselect != null)
            {
                OnUnselect.Invoke();
            }
        }
    }
}
