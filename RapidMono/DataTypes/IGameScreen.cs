﻿namespace RapidMono.DataTypes;

public abstract class IGameScreen
{
    public RapidEngine Engine;
    private bool _IsLoaded = false;
    public bool IsLoaded { get { return _IsLoaded; } }

    /// <summary>
    /// Loading data goes here, it happens in the background
    /// </summary>
    public void BeginLoad()
    {
        ThreadStart ts = new ThreadStart(LoadGameScreenAsync);
        Thread loadThread = new Thread(ts);
        loadThread.Start();
    }
    private void LoadGameScreenAsync()
    {
        this.Load();
        _IsLoaded = true;
    }

    /// <summary>
    /// All functions needed for the GameScreen to function
    /// </summary>
    #region Overrideable Functions
    public abstract void Load();
    public abstract void Update();
    public abstract void Draw();

    /// <summary>
    /// Allows you to do cleaning up when the screen is removed from the stack.
    /// </summary>
    public abstract void OnPop();
    #endregion
}