using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using RapidMono;
using RapidMono.Services;
using RapidMonoDesktop.GameScreens;
using RapidMonoDesktop.Helpers;
using RapidMonoDesktop.SamplesCode;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace RapidMonoDesktop;

public static class GameState
{
    public static Vector2 PlayerPosition;
    public static float PlayerDirection;
    public static float PlayerSpeed;
    public static int PlayerFireRate;
    public static int PlayerNextFire;
    public static int PlayerHealth;
    public static int PlayerScore;

    public static List<Pickup> Pickups;
    public static List<Bullet> Bullets;
    public static List<Enemy> Enemies;

    public static int EnemyCounter;


    public static Random random;
    static GameState()
    {
        Clear();
    }

    private static Game1 Game;
    public static GameTime gameTime;
    public static void SetGame(Game1 g)
    {
        Game = g;
        Game.Deactivated += new EventHandler<EventArgs>(Game_Deactivated);
    }

    static void Game_Deactivated(object sender, EventArgs e)
    {
        // TODO: SAVE GAME STATE
    }

    public static IsolatedStorageFile isoFiles;
    public static bool HasLoadState()
    {
        return isoFiles.FileExists("savegame.xml");
    }

    public static void LoadState()
    {

    }

    static float target_direction = 0;
    static int nextScoreUp = 1000;
    internal static void Update()
    {
        nextScoreUp -= gameTime.ElapsedGameTime.Milliseconds;
        if (nextScoreUp <= 0)
        {
            nextScoreUp += 1000;
            PlayerScore += 1;
        }

        #region PLAYER_UPDATE
        float new_direction = 0;
        bool have_new_direction = false;
        if (Engine.Mouse.LeftPressed() || Engine.Mouse.LeftHeld())
        {
            have_new_direction = true;
            new_direction = (float)Math.Atan2(Engine.Mouse.Position().Y - 240, Engine.Mouse.Position().X - 400);
        }
        else if (Engine.Keyboard.KeyPress(Keys.W) || Engine.Keyboard.KeyHeld(Keys.W) 
              || Engine.Keyboard.KeyPress(Keys.Up) || Engine.Keyboard.KeyHeld(Keys.Up))
        {
            have_new_direction = true;
            new_direction = -MathHelper.PiOver2;
        }
        else if (Engine.Keyboard.KeyPress(Keys.A) || Engine.Keyboard.KeyHeld(Keys.A) 
              || Engine.Keyboard.KeyPress(Keys.Left) || Engine.Keyboard.KeyHeld(Keys.Left))
        {
            have_new_direction = true;
            new_direction = -2 * MathHelper.PiOver2;
        }
        else if (Engine.Keyboard.KeyPress(Keys.S) || Engine.Keyboard.KeyHeld(Keys.S) 
              || Engine.Keyboard.KeyPress(Keys.Down) || Engine.Keyboard.KeyHeld(Keys.Down))
        {
            have_new_direction = true;
            new_direction = MathHelper.PiOver2;
        }
        else if (Engine.Keyboard.KeyPress(Keys.D) || Engine.Keyboard.KeyHeld(Keys.D)
              || Engine.Keyboard.KeyPress(Keys.Right) || Engine.Keyboard.KeyHeld(Keys.Right))
        {
            have_new_direction = true;
            new_direction = 0;
        }
        else if (Engine.GamePad.LeftStick(Controller.One).Length() > 0.1f)
        {
            have_new_direction = true;
            new_direction = (float)Math.Atan2(-Engine.GamePad.LeftStick(Controller.One).Y, Engine.GamePad.LeftStick(Controller.One).X);
        }
        
        if (have_new_direction)
        {
            target_direction = new_direction;
        }
        PlayerDirection = ChaseAndEvade.TurnToFace(target_direction, PlayerDirection);
        PlayerPosition.X += (float)(PlayerSpeed * Math.Cos(PlayerDirection));
        PlayerPosition.Y += (float)(PlayerSpeed * Math.Sin(PlayerDirection));

        for (int i = Bullets.Count; i > 0; i--)
        {
            if (Bullets[i - 1].Colour != Color.LimeGreen)
            {
                if (MHelper.Vector2Distance(Bullets[i - 1].Position, PlayerPosition) < 20)
                {
                    PlayerHealth -= 1;
                    Bullets.RemoveAt(i - 1);
                    break;
                }
            }
        }
        #endregion

        #region BULLET_UPDATE
        PlayerNextFire -= gameTime.ElapsedGameTime.Milliseconds;
        if (PlayerNextFire <= 0)
        {
            Bullet b = new Bullet();
            b.Colour = Color.LimeGreen;
            b.Position = new Vector2(PlayerPosition.X, PlayerPosition.Y);
            b.Direction = PlayerDirection;
            b.Speed = PlayerSpeed * 2;
            Bullets.Add(b);
            PlayerNextFire += PlayerFireRate;
        }

        for (int i = 0; i < Bullets.Count; i++)
        {
            Bullet b = Bullets[i];
            b.Position.X += (int)(b.Speed * Math.Cos(b.Direction));
            b.Position.Y += (int)(b.Speed * Math.Sin(b.Direction));

            DoCollisions(b);
        }
        #endregion

        #region PICKUPS_UPDATE
        for (int i = 0; Pickups.Count < 3; i++)
        {
            Pickup p = new Pickup();
            p.WhatAmI = random.Next(20);
            p.Position = new Vector2(PlayerPosition.X + random.Next(-2000, 2000), PlayerPosition.Y + random.Next(-2000, 2000));
            Pickups.Add(p);
        }
        for (int i = Pickups.Count; i > 0; i--)
        {
            if (MHelper.Vector2Distance(PlayerPosition, Pickups[i - 1].Position) > 1500)
            {
                Pickups.RemoveAt(i - 1);
            }
            else if (MHelper.Vector2Distance(PlayerPosition, Pickups[i - 1].Position) < 50)
            {

                PickupScreen ps = new PickupScreen();
                ps.PickupType = Pickups[i - 1].WhatAmI;
                Pickups.RemoveAt(i - 1);
                Engine.Screen.PushPopupScreen(ps);
            }
        }
        #endregion

        #region ENEMIES_UPDATE
        while (Enemies.Count < EnemyCounter)
        {
            Enemy e = new Enemy((int)(PlayerPosition.X + random.Next(-850, 850)), (int)(PlayerPosition.Y + random.Next(-850, 850)), (int)(GameState.PlayerSpeed - 2), 0.5f);
            switch (random.Next(5))
            {
                case 0: e.Colour = Color.Red; break;
                case 1: e.Colour = Color.Magenta; break;
                case 2: e.Colour = Color.DarkViolet; break;
                case 3: e.Colour = Color.DeepPink; break;
                default: e.Colour = Color.DarkOrange; break;
            }
            Enemies.Add(e);
        }

        for (int j = Enemies.Count; j > 0; j--)
        {
            if (MHelper.Vector2Distance(Enemies[j - 1].Position, PlayerPosition) > 2500)
            {
                Enemies.RemoveAt(j - 1);
            }
            else
            {
                for (int i = Bullets.Count; i > 0; i--)
                {
                    if (Bullets[i - 1].Colour != Enemies[j - 1].Colour)
                    {
                        if (MHelper.Vector2Distance(Bullets[i - 1].Position, Enemies[j - 1].Position) < 20)
                        {
                            Enemies.RemoveAt(j - 1);
                            if (Bullets[i - 1].Colour == Color.LimeGreen)
                                PlayerScore += 25;

                            Bullets.RemoveAt(i - 1);
                            break;
                        }
                    }
                }
            }
        }

        foreach (Enemy e in Enemies)
        {
            e.Run(Enemies);
            e.Seek(PlayerPosition);
        }
        #endregion
    }

    public static void DoCollisions(Bullet b)
    {
        for (int i = Bullets.Count; i > 0; i--)
        {
            if (MHelper.Vector2Distance(Bullets[i - 1].Position, PlayerPosition) > 1000)
            {
                Bullets.RemoveAt(i - 1);
            }
        }
    }

    internal static void Clear()
    {
        PlayerPosition = new Vector2();
        PlayerDirection = -MathHelper.PiOver2; //up
        PlayerSpeed = 10;
        PlayerFireRate = 300;
        PlayerNextFire = 300;
        PlayerHealth = 5;
        PlayerScore = 0;

        EnemyCounter = 7;

        Pickups = new List<Pickup>();
        Bullets = new List<Bullet>();
        Enemies = new List<Enemy>();

        isoFiles = IsolatedStorageFile.GetUserStoreForApplication();

        random = new Random();
    }
}