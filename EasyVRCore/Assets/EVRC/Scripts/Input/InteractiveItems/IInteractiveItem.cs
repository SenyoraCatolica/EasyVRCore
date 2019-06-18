namespace EVR.UI
{
    public interface IInteractiveItem
    {
        void Enter();
        void Exit();
        void Hover();
        void Select();
        void Unselect();
        bool IsAutoselect();
        bool IsRepeatSelection();
        bool IsSelectable();
        bool IsSelected();
        void SetSelected(bool isSelected);
        bool StaysSelected();
        bool IsUnselectable();
        bool IsDragAndDrop();
    }
}
