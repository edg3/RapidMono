﻿using RapidMono.DataTypes;

namespace RapidMono.Services;

public class ScreenService : IRapidService
{
    /// <summary>
    /// Lists of game screens we have instances of
    /// </summary>
    List<IGameScreen> _GameScreens = new List<IGameScreen>(),
                      _PopupScreens = new List<IGameScreen>();

    /// <summary>
    /// Allows game pausing, change pause state using Pause()
    /// </summary>
    private bool _Pause = false;
    public bool Paused { get { return _Pause; } }
    public bool Pause()
    {
        _Pause = !_Pause;
        return _Pause;
    }

    /// <summary>
    /// Load
    /// </summary>
    public override void Load()
    {

    }

    /// <summary>
    /// Update
    /// </summary>
    public override void Update()
    {
        if (!_Pause)
        {
            if (_PopupScreens.Count > 0)
            {
                if (_PopupScreens[_PopupScreens.Count - 1].IsLoaded)
                    _PopupScreens[_PopupScreens.Count - 1].Update();
            }
            else if (_GameScreens.Count > 0)
            {
                if (_GameScreens[_GameScreens.Count - 1].IsLoaded)
                    _GameScreens[_GameScreens.Count - 1].Update();
            }
        }
    }

    /// <summary>
    /// Draw
    /// </summary>
    public override void Draw()
    {
        Engine.SpriteBatch.Begin();
        if (_GameScreens.Count > 0)
        {
            if (_GameScreens[_GameScreens.Count - 1].IsLoaded)
                _GameScreens[_GameScreens.Count - 1].Draw();
        }
        if (_PopupScreens.Count > 0)
        {
            if (_PopupScreens[_PopupScreens.Count - 1].IsLoaded)
                _PopupScreens[_PopupScreens.Count - 1].Draw();
        }
        Engine.SpriteBatch.End();
    }

    /// <summary>
    /// Add a game screen to the stack
    /// </summary>
    /// <param name="gs">The IGameScreen instance to add</param>
    public void PushScreen(IGameScreen gs)
    {
        if (_GameScreens.Contains(gs))
            return;
        gs.BeginLoad();
        _GameScreens.Add(gs);
    }

    /// <summary>
    /// Remove a game screen from the stack
    /// </summary>
    public void PopScreen()
    {
        if (_GameScreens.Count > 0)
        {
            var gs = _GameScreens[_GameScreens.Count - 1];
            gs.OnPop();
            _GameScreens.Remove(gs);

            if (_GameScreens.Count > 0) _GameScreens.Last().OnPush();
        }
    }

    /// <summary>
    /// Add a pop up screen to the stack (draws over the game screen and pauses the game screen)
    /// </summary>
    /// <param name="gs">The IGameScreen the add</param>
    public void PushPopupScreen(IGameScreen gs)
    {
        if (_PopupScreens.Contains(gs))
            return;
        gs.BeginLoad();
        _PopupScreens.Add(gs);
    }

    /// <summary>
    /// Remove a popup screen from the stack
    /// </summary>
    public void PopPopupScreen()
    {
        if (_PopupScreens.Count > 0)
        {
            var gs = _PopupScreens[_PopupScreens.Count - 1];
            gs.OnPop();
            _PopupScreens.Remove(gs);

            if (_GameScreens.Count > 0) _GameScreens.Last().OnPush();
        }
    }
}