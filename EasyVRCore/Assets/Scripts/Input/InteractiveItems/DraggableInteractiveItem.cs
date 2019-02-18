using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DragType { MOVE, GRAB, NONE}

namespace EverisUI
{
    public class DraggableInteractiveItem : SimpleClickInteractiveItem
    {
        public DragType TypeOfDragging;

        public override void Select()
        {
            base.Select();

            ModuleEvents.Instance.RaiseEvent(gameObject, Resources.Load<EVREvent>("Events/OnDragItem"));

            Move();
        }

        public override void Unselect()
        {
            base.Select();

            ModuleEvents.Instance.RaiseEvent(gameObject, Resources.Load<EVREvent>("Events/OnUnDragItem"));
        }

        private void Move()
        {
            float distance_to_ray_origin = (transform.position - ModuleInput.Instance.GetCurrentRay().origin).magnitude;
            Ray currentRay = ModuleInput.Instance.GetCurrentRay();
            transform.position = currentRay.origin + currentRay.direction * distance_to_ray_origin;
        }
    }
}


