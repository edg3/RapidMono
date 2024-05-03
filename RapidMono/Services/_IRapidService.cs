namespace RapidMono.Services;

public abstract class IRapidService
{
    public RapidEngine Engine;
    public abstract void Load();
    public abstract void Update();
    public abstract void Draw();
}