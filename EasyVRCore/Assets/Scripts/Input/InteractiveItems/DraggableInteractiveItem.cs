using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ExtendedVRUI
{
    public class DraggableInteractiveItem : SimpleClickInteractiveItem
    {

        bool isDragging = false;
        float m_distanceToRayOrigin = 0;

        public override void Select()
        {
            base.Select();

            ModuleEvents.Instance.RaiseEvent(gameObject, Resources.Load<EVREvent>("Events/OnDragItem"));

            m_distanceToRayOrigin = (transform.position - ModuleInput.Instance.GetCurrentRay().origin).magnitude;

            isDragging = true;
        }

        public override void Unselect()
        {
            base.Unselect();

            ModuleEvents.Instance.RaiseEvent(gameObject, Resources.Load<EVREvent>("Events/OnUnDragItem"));

            isDragging = false;
        }

        private void Move()
        {
            Ray currentRay = ModuleInput.Instance.GetCurrentRay();
            transform.position = currentRay.origin + currentRay.direction * m_distanceToRayOrigin;
            transform.LookAt(currentRay.origin, -Vector3.up);
        }

        private void Update()
        {
            if(isDragging)
            {
                Move();
            }
        }
    }
}


