using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EverisUI
{
    public class DraggableInteractiveItem : SimpleClickInteractiveItem
    {

        bool isDragging = false;

        public override void Select()
        {
            base.Select();

            ModuleEvents.Instance.RaiseEvent(gameObject, Resources.Load<EVREvent>("Events/OnDragItem"));

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
            float distance_to_ray_origin = (transform.position - ModuleInput.Instance.GetCurrentRay().origin).magnitude;
            Ray currentRay = ModuleInput.Instance.GetCurrentRay();
            transform.position = currentRay.origin + currentRay.direction * distance_to_ray_origin;
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


