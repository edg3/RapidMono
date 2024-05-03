using Microsoft.Xna.Framework.Input;

namespace RapidMono.Services;

public class KeyboardService : IRapidService
{

    private KeyboardState CurrentState, PreviousState;
    private Dictionary<Keys, float>
        PressLengths = new Dictionary<Keys, float>(),
        ReleasedLength = new Dictionary<Keys, float>();
    //keyboardHandler.add(Keys.A);
    private List<Keys> LengthCheckedKeys = new List<Keys>();

    public override void Load()
    {
        CurrentState = Keyboard.GetState();
        PreviousState = Keyboard.GetState();
    }

    public override void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Keyboard.GetState();

        for (int i = 0; i < LengthCheckedKeys.Count; i++)
        {
            if (this.KeyHeld(LengthCheckedKeys[i]))
            {
                PressLengths[LengthCheckedKeys[i]] += Engine.GameTime.ElapsedGameTime.Milliseconds;
            }
            else if (this.KeyLeft(LengthCheckedKeys[i]))
            {
                PressLengths[LengthCheckedKeys[i]] += Engine.GameTime.ElapsedGameTime.Milliseconds;
                ReleasedLength[LengthCheckedKeys[i]] = PressLengths[LengthCheckedKeys[i]];
            }
            else
            {
                PressLengths[LengthCheckedKeys[i]] = 0.0f;
            }
        }
    }

    public override void Draw()
    {

    }

    /// <summary>
    /// Check if a key was pressed, this will trigger exclusively over KeyHeld
    /// </summary>
    /// <param name="k">The key to check</param>
    /// <returns></returns>
    public bool KeyPress(Keys k)
    {
        return ((CurrentState.IsKeyDown(k)) && (!PreviousState.IsKeyDown(k)));
    }

    /// <summary>
    /// Check if a key is currently being held down, this wont trigger at initial press
    /// </summary>
    /// <param name="k">The key to check</param>
    /// <returns></returns>
    public bool KeyHeld(Keys k)
    {
        return ((CurrentState.IsKeyDown(k)) && (PreviousState.IsKeyDown(k)));
    }

    /// <summary>
    /// Check if a key was released
    /// </summary>
    /// <param name="k">The key to check</param>
    /// <returns></returns>
    public bool KeyLeft(Keys k)
    {
        return ((!CurrentState.IsKeyDown(k)) && (PreviousState.IsKeyDown(k)));
    }

    /// <summary>
    /// Add a key to the list of keys you want the pressed length of time for
    /// </summary>
    /// <param name="k">The key to check</param>
    public void AddKey(Keys k)
    {
        if (!LengthCheckedKeys.Contains(k))
        {
            PressLengths.Add(k, 0.0f);
            ReleasedLength.Add(k, 0.0f);
            LengthCheckedKeys.Add(k);
        }
    }

    /// <summary>
    /// Checks how long a key was pressed for so far in millisecond
    /// </summary>
    /// <param name="k">The key to check</param>
    /// <returns></returns>
    public float HeldFor(Keys k)
    {
        if (PressLengths.ContainsKey(k))
        {
            return PressLengths[k];
        }
        return 0f;
    }

    /// <summary>
    /// Check for how long a key was pressed for after release
    /// </summary>
    /// <param name="k">The key to check</param>
    /// <returns></returns>
    public float ReleasedTime(Keys k)
    {
        if (ReleasedLength.ContainsKey(k))
        {
            return ReleasedLength[k];
        }
        return 0f;
    }
}