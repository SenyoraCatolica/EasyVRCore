
public interface IModule
{
    void Init(float updateRate);
    void Update();
    void Clear();
    bool IsActive();
    float GetUpdateRate();
    float LastUpdate();
    void SetLastUpdate(float currentTime);
}
