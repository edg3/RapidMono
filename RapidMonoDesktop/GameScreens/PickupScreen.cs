﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RapidMono;
using RapidMono.DataTypes;
using RapidMono.Services;

namespace RapidMonoDesktop.GameScreens;

class PickupScreen : IGameScreen
{
    Texture2D smallTex;
    public int PickupType = 0;
    Rectangle drawRect;

    string PickupInfo = "";
    SpriteFont LargeText;

    public override void Load()
    {
        smallTex = Engine.Content.Load<Texture2D>("blank");
        LargeText = Engine.Content.Load<SpriteFont>("Arial");

        drawRect = new Rectangle(50, 50, 700, 380);

        GameState.PlayerScore += 10;

        switch (PickupType)
        {
            case 0:
                {
                    PickupInfo = "Speed +1" +
                        "\n\nNew augments have been given to your crew.\nThey now pedal faster!";
                    GameState.PlayerSpeed += 1;
                }
                break;
            case 1:
                {
                    PickupInfo = "Speed +2" +
                        "\n\nGenetic enhancement has given your crew more\narms and legs. The can now work faster.";
                    GameState.PlayerSpeed += 2;
                }
                break;
            case 2:
                {
                    PickupInfo = "Speed -1" +
                        "\n\nA rare space flu has slowed your crew down.";
                    GameState.PlayerSpeed -= 1;
                }
                break;
            case 3:
                {
                    PickupInfo = "Health +1" +
                        "\n\nYou find a lost astronaut willing to join\nyour crew.";
                    GameState.PlayerHealth += 1;
                }
                break;
            case 4:
                {
                    PickupInfo = "Health +2" +
                    "\n\nYou find a floating door in space with 2\nstranded people on.";
                    GameState.PlayerHealth += 2;
                }
                break;
            case 5:
                {
                    PickupInfo = "Health -1" +
                        "\n\nA distortion in space and time stole a crew\nmember.";
                    GameState.PlayerHealth -= 1;
                }
                break;
            case 6:
                {
                    PickupInfo = "Super +1" +
                        "\n\nYou find a robot capable of increasing your\nspeed.";
                    GameState.PlayerHealth += 1;
                    GameState.PlayerSpeed += 1;
                }
                break;
            case 7:
                {
                    PickupInfo = "Super -1" +
                        "\n\nA space rock breached your hull, a crew\nmember got sucked into space.";
                    GameState.PlayerSpeed -= 1;
                    GameState.PlayerHealth -= 1;
                }
                break;
            case 8:
                {
                    PickupInfo = "Enemies +1" +
                        "\n\nThe enemies are reinforcing!";
                    GameState.EnemyCounter += 1;
                }
                break;
            case 9:
                {
                    PickupInfo = "Annual Review!" +
                        "\n\nYou review your crew, and all is well!";
                    GameState.PlayerScore += 20;
                }
                break;
            default:
                {
                    PickupInfo = "Nothing." +
                        "\n\nYour space radar must be acting up\nagain...";
                }
                break;
        }
    }

    public override void Update()
    {
        if (Engine.Keyboard.KeyPress(Keys.Escape) || Engine.GamePad.BPressed(Controller.One) || Engine.GamePad.APressed(Controller.One))
            Engine.Screen.PopPopupScreen();
    }

    public override void Draw()
    {
        Engine.SpriteBatch.Draw(smallTex, drawRect, Color.White);
        Engine.SpriteBatch.DrawString(LargeText, PickupInfo + "\n\n<escape to close>", new Vector2(drawRect.X + 32, drawRect.Y + 32), Color.Black);
    }

    public override void OnPop()
    {

    }

    public override void OnPush()
    {

    }
}