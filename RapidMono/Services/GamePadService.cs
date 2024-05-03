using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RapidMono.Services;

public enum Controller
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4
}

public class GamePadService : IRapidService
{
    GamePadState[] PreviousState = new GamePadState[4];
    GamePadState[] CurrentState = new GamePadState[4];
    public override void Load()
    {
#if WINDOWS_PHONE
        PreviousState[0] = GamePad.GetState(PlayerIndex.One);
        CurrentState[0] = GamePad.GetState(PlayerIndex.One);
#else
        PreviousState[0] = GamePad.GetState(PlayerIndex.One);
        PreviousState[1] = GamePad.GetState(PlayerIndex.Two);
        PreviousState[2] = GamePad.GetState(PlayerIndex.Three);
        PreviousState[3] = GamePad.GetState(PlayerIndex.Four);

        CurrentState[0] = GamePad.GetState(PlayerIndex.One);
        CurrentState[1] = GamePad.GetState(PlayerIndex.Two);
        CurrentState[2] = GamePad.GetState(PlayerIndex.Three);
        CurrentState[3] = GamePad.GetState(PlayerIndex.Four);
#endif
    }

    public override void Update()
    {
#if WINDOWS_PHONE
        PreviousState[0] = CurrentState[0];
        CurrentState[0] = GamePad.GetState(PlayerIndex.One);
#else
        PreviousState[0] = CurrentState[0];
        PreviousState[1] = CurrentState[1];
        PreviousState[2] = CurrentState[2];
        PreviousState[3] = CurrentState[3];

        CurrentState[0] = GamePad.GetState(PlayerIndex.One);
        CurrentState[1] = GamePad.GetState(PlayerIndex.Two);
        CurrentState[2] = GamePad.GetState(PlayerIndex.Three);
        CurrentState[3] = GamePad.GetState(PlayerIndex.Four);
#endif
    }

    public override void Draw()
    {

    }

    /// <summary>
    /// Triggers functions
    /// </summary>

    public float LeftTriggerValue(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return 0f;
#else
        return CurrentState[(int)ControllerNum - 1].Triggers.Left;
#endif
    }
    public float RightTrigerValue(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return 0f;
#else
        return CurrentState[(int)ControllerNum - 1].Triggers.Right;
#endif
    }
    public bool LeftTriggerPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Triggers.Left == 0.0f) && (CurrentState[(int)ControllerNum - 1].Triggers.Left > 0.0f));
#endif
    }
    public bool RightTriggerPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Triggers.Right == 0.0f) && (CurrentState[(int)ControllerNum - 1].Triggers.Right > 0.0f));
#endif
    }
    public bool LeftTriggerReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Triggers.Left > 0.0f) && (CurrentState[(int)ControllerNum - 1].Triggers.Left == 0.0f));
#endif
    }
    public bool RightTriggerReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Triggers.Right > 0.0f) && (CurrentState[(int)ControllerNum - 1].Triggers.Right == 0.0f));
#endif
    }

    /// <summary>
    /// Bumpers
    /// </summary>

    public bool LeftBumberPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.LeftShoulder == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].Buttons.LeftShoulder == ButtonState.Pressed));
#endif
    }
    public bool RightBumberPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.RightShoulder == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].Buttons.RightShoulder == ButtonState.Pressed));
#endif
    }
    public bool LeftBumberReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.LeftShoulder == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].Buttons.LeftShoulder == ButtonState.Released));
#endif
    }
    public bool RightBumberReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.RightShoulder == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].Buttons.RightShoulder == ButtonState.Released));
#endif
    }
    public bool LeftBumberHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].Buttons.LeftShoulder == ButtonState.Pressed);
#endif
    }
    public bool RightBumberHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].Buttons.RightShoulder == ButtonState.Pressed);
#endif
    }

    /// <summary>
    /// Sticks
    /// </summary>

    public Vector2 LeftStick(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return Vector2.Zero;
#else
        return CurrentState[(int)ControllerNum - 1].ThumbSticks.Left;
#endif
    }
    public Vector2 RightStick(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return Vector2.Zero;
#else
        return CurrentState[(int)ControllerNum - 1].ThumbSticks.Right;
#endif
    }
    public float LeftStickDirection(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return 0f;
#else
        float _x = CurrentState[(int)ControllerNum - 1].ThumbSticks.Left.X, _y = CurrentState[(int)ControllerNum - 1].ThumbSticks.Left.Y;
        return (float)Math.Atan2(_y, _x);
#endif
    }
    public float RightStickDirection(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return 0f;
#else
        float _x = CurrentState[(int)ControllerNum - 1].ThumbSticks.Right.X, _y = CurrentState[(int)ControllerNum - 1].ThumbSticks.Right.Y;
        return (float)Math.Atan2(_y, _x);
#endif
    }
    public bool LeftStickPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.LeftStick == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].Buttons.LeftStick == ButtonState.Pressed));
#endif
    }
    public bool RightStickPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.RightStick == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].Buttons.RightStick == ButtonState.Pressed));
#endif
    }
    public bool LeftStickReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.LeftStick == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].Buttons.LeftStick == ButtonState.Released));
#endif
    }
    public bool RightStickReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.RightStick == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].Buttons.RightStick == ButtonState.Released));
#endif
    }
    public bool LeftStickHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].Buttons.LeftStick == ButtonState.Pressed);
#endif
    }
    public bool RightStickHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].Buttons.RightStick == ButtonState.Pressed);
#endif
    }

    /// <summary>
    /// DPAD
    /// </summary>


    public bool LeftPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].DPad.Left == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].DPad.Left == ButtonState.Pressed));
#endif
    }
    public bool LeftReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].DPad.Left == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].DPad.Left == ButtonState.Released));
#endif
    }
    public bool LeftHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].DPad.Left == ButtonState.Pressed);
#endif
    }
    public bool RightPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].DPad.Right == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].DPad.Right == ButtonState.Pressed));
#endif
    }
    public bool RightReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].DPad.Right == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].DPad.Right == ButtonState.Released));
#endif
    }
    public bool RightHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].DPad.Right == ButtonState.Pressed);
#endif
    }
    public bool UpPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].DPad.Up == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].DPad.Up == ButtonState.Pressed));
#endif
    }
    public bool UpReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].DPad.Up == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].DPad.Up == ButtonState.Released));
#endif
    }
    public bool UpHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].DPad.Up == ButtonState.Pressed);
#endif
    }
    public bool DownPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].DPad.Down == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].DPad.Down == ButtonState.Pressed));
#endif
    }
    public bool DownReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].DPad.Down == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].DPad.Down == ButtonState.Released));
#endif
    }
    public bool DownHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].DPad.Down == ButtonState.Pressed);
#endif
    }

    public bool XPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.X == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].Buttons.X == ButtonState.Pressed));
#endif
    }
    public bool XReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.X == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].Buttons.X == ButtonState.Released));
#endif
    }
    public bool XHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].Buttons.X == ButtonState.Pressed);
#endif
    }
    public bool YPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.Y == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].Buttons.Y == ButtonState.Pressed));
#endif
    }
    public bool YReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.Y == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].Buttons.Y == ButtonState.Released));
#endif
    }
    public bool YHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].Buttons.Y == ButtonState.Pressed);
#endif
    }
    public bool APressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.A == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].Buttons.A == ButtonState.Pressed));
#endif
    }
    public bool AReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.A == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].Buttons.A == ButtonState.Released));
#endif
    }
    public bool AHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].Buttons.A == ButtonState.Pressed);
#endif
    }
    public bool BPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.B == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].Buttons.B == ButtonState.Pressed));
#endif
    }
    public bool BReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.B == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].Buttons.B == ButtonState.Released));
#endif
    }
    public bool BHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].Buttons.B == ButtonState.Pressed);
#endif
    }

    public bool StartPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.Start == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].Buttons.Start == ButtonState.Pressed));
#endif
    }
    public bool StartReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.Start == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].Buttons.Start == ButtonState.Released));
#endif
    }
    public bool StartHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return false;
#else
        return (CurrentState[(int)ControllerNum - 1].Buttons.Start == ButtonState.Pressed);
#endif
    }
    public bool BackPressed(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return ((PreviousState[0].Buttons.Back == ButtonState.Released) && (CurrentState[0].Buttons.Back == ButtonState.Pressed));
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.Back == ButtonState.Released) && (CurrentState[(int)ControllerNum - 1].Buttons.Back == ButtonState.Pressed));
#endif
    }
    public bool BackReleased(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return ((PreviousState[0].Buttons.Back == ButtonState.Pressed) && (CurrentState[0].Buttons.Back == ButtonState.Released));
#else
        return ((PreviousState[(int)ControllerNum - 1].Buttons.Back == ButtonState.Pressed) && (CurrentState[(int)ControllerNum - 1].Buttons.Back == ButtonState.Released));
#endif
    }
    public bool BackHeld(dynamic ControllerNum)
    {
#if WINDOWS_PHONE
        return (CurrentState[0].Buttons.Back == ButtonState.Pressed);
#else
        return (CurrentState[(int)ControllerNum - 1].Buttons.Back == ButtonState.Pressed);
#endif
    }
}
