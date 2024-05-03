using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using RapidMono;
using RapidMonoDesktop.GameScreens;
using System;
using System.Collections.Generic;

namespace RapidMonoDesktop;

public class Game1 : Microsoft.Xna.Framework.Game
{
    private RapidEngine _rapidEngine;
    private GraphicsDeviceManager _graphics;

    //Input
    public List<GestureSample> gestureSamples = new List<GestureSample>();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        TargetElapsedTime = TimeSpan.FromTicks(333333);

        InactiveSleepTime = TimeSpan.FromSeconds(1);

        ScoresData.CreateDatabase();
    }

    protected override void Initialize()
    {
        TouchPanel.EnabledGestures = GestureType.Tap | GestureType.FreeDrag | GestureType.DragComplete | GestureType.Flick | GestureType.DoubleTap;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        GameState.SetGame(this);
        _rapidEngine = new RapidEngine(this, _graphics.GraphicsDevice, Content);
        Engine.Screen.PushScreen(new Menu());
    }

    protected override void UnloadContent() { }

    protected override void Update(GameTime gameTime)
    {

        gestureSamples.Clear();

        while (TouchPanel.IsGestureAvailable)
        {
            GestureSample gs = TouchPanel.ReadGesture();
            gestureSamples.Add(gs);
        }

        _rapidEngine.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _rapidEngine.Draw(gameTime, Color.Black);

        base.Draw(gameTime);
    }
}