﻿#if WINDOWS
using System.IO;
#endif

#if WINDOWS_PHONE
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
#endif

#if XBOX
#endif

using Microsoft.Xna.Framework.Graphics;

namespace RapidMono.Services;

public class ScreenshotService : IRapidService
{
    public override void Load()
    {
        Engine.OnFinalDraw += new RapidEngine.OnFinalDrawEventHandler(Engine_OnFinalDraw);
    }

    private RenderTarget2D _RenderTarget;
    private GraphicsDevice _graphicsDevice;

    public ScreenshotService(GraphicsDevice graphicsDevice)
    {
        this._graphicsDevice = graphicsDevice;
        _RenderTarget = new RenderTarget2D(_graphicsDevice, 1, 1);
    }

    void Engine_OnFinalDraw(Microsoft.Xna.Framework.Graphics.RenderTarget2D renderTarget)
    {
        lock (_RenderTarget)
        {
            _RenderTarget = renderTarget;
        }
    }

    public override void Update()
    {

    }

    public override void Draw()
    {

    }

    /// <summary>
    /// Take and save a screenshot
    /// </summary>
    public void TakeScreenshot()
    {
        ThreadStart ts = new ThreadStart(AsyncSaveScreenshot);
        Thread saveThread = new Thread(ts);
        saveThread.Start();
    }

    private void AsyncSaveScreenshot()
    {
        lock (_RenderTarget)
        {
#if WINDOWS
                try
                {
                    string targetDirectory = Directory.GetCurrentDirectory() + @"\Screenshots\";
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }
                    Texture2D tex = (Texture2D)_RenderTarget;
                    using (FileStream fs = new FileStream(targetDirectory + "screenshot_" + DateTime.Now.ToString("yyyy_MM_dd_mm_ss") + "_" + DateTime.Now.Millisecond.ToString() + ".jpg", FileMode.CreateNew))
                    {
                        tex.SaveAsJpeg(fs, tex.Width, tex.Height);
                    }
                }
                catch
                {

                }
#endif
#if WINDOWS_PHONE
                try
                {
                    string tempJPG = "tempJPG";
                    var myStore = IsolatedStorageFile.GetUserStoreForApplication();
                    if (myStore.FileExists(tempJPG))
                    {
                        myStore.DeleteFile(tempJPG);
                    }

                    var fs = myStore.CreateFile(tempJPG);
                    Texture2D tex = (Texture2D)_RenderTarget;
                    tex.SaveAsPng(fs, tex.Width, tex.Height);
                    fs.Close();
                    fs = myStore.OpenFile(tempJPG, FileMode.Open, FileAccess.Read);

                    MediaLibrary lib = new MediaLibrary();

                    lib.SavePicture("screenshot_" + DateTime.Now.ToString("yyyy_MM_dd_mm_ss") + "_" + DateTime.Now.Millisecond.ToString() + ".jpg", fs);

                    fs.Close();
                }
                catch
                {

                }
#endif
#if XBOX

#endif
        }
    }
}