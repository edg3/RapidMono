using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RapidMono.Services;

namespace RapidMono;

public static class Engine
{
    private static RapidEngine? _Instance;
    public static RapidEngine Instance
    {
        get => _Instance;
        set
        {
            if (_Instance is not null)
            {
                throw new System.Exception("Engine instance already set");
            }
            _Instance = value;
        }
    }

    public static void Exit() => _Instance?.Exit();
    public static ScreenService Screen => _Instance?.ServiceOf<ScreenService>() ?? throw new System.Exception("ScreenService not found");
    public static ScreenshotService Screenshot => _Instance?.ServiceOf<ScreenshotService>() ?? throw new System.Exception("ScreenshotService not found");
    public static KeyboardService Keyboard => _Instance?.ServiceOf<KeyboardService>() ?? throw new System.Exception("KeyboardService not found");
    public static MouseService Mouse => _Instance?.ServiceOf<MouseService>() ?? throw new System.Exception("MouseService not found");
    public static GamePadService GamePad => _Instance?.ServiceOf<GamePadService>() ?? throw new System.Exception("GamePadService not found");
    public static void AddService(IRapidService service, bool replace) => _Instance?.AddService(service, replace);
    // TODO: consider how to rework this a little
    public static object ServiceOf<T>() => _Instance?.Services.Where(it => typeof(T) == it.GetType()).First() ?? throw new System.Exception("Service not found");
    public static GameTime GameTime => _Instance?.GameTime ?? throw new System.Exception("GameTime not found");
    public static GraphicsDevice GraphicsDevice => _Instance?.GraphicsDevice ?? throw new System.Exception("GraphicsDevice not found");
    public static ContentManager Content => _Instance?.Content ?? throw new System.Exception("ContentManager not found");
    public static SpriteBatch SpriteBatch => _Instance?.SpriteBatch ?? throw new System.Exception("SpriteBatch not found");

}