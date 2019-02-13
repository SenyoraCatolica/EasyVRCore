namespace EVR.UI
{
    public interface IInteractiveItem
    {
        void Enter();
        void Exit();
        void Hover();
        void Select();
        bool IsAutoselect();
        bool IsRepeatSelection();
        bool IsSelectable();
        bool IsSelected();
        void SetSelected(bool isSelected);
        bool StaysSelected();
    }
}
