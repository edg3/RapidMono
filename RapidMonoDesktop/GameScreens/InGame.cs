using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RapidMono;
using RapidMono.DataTypes;
using RapidMonoDesktop.GameScreens;
using RapidMonoDesktop.Helpers;
using System;
using System.Collections.Generic;

namespace RapidMonoDesktop;

class StarColor
{
    public Vector2 Pos;
    public Color Colour;
    public StarColor(int x, int y, Color c)
    {
        Pos = new(x, y);
        Colour = c;
    }
}

class InGame : IGameScreen
{
    Texture2D texShip, texIndicator, texBullet, texLife, texPickup;
    SpriteFont Font;

    // Stars
    Texture2D smallStar, mediumStar, largeStar;
    List<StarColor> smallStars = new(),
                    mediumStars = new(),
                    largeStars = new();

    // RandomGen
    Random random = new();

    public InGame()
    {

    }

    public override void Load()
    {
        texShip = Engine.Content.Load<Texture2D>("Ship");
        texIndicator = Engine.Content.Load<Texture2D>("Indicator");
        texBullet = Engine.Content.Load<Texture2D>("Bullet");
        texLife = Engine.Content.Load<Texture2D>("Heart");
        texPickup = Engine.Content.Load<Texture2D>("Pickup");

        Font = Engine.Content.Load<SpriteFont>("Arial");

        smallStar = Engine.Content.Load<Texture2D>("Star_Small");
        mediumStar = Engine.Content.Load<Texture2D>("Star_Medium");
        largeStar = Engine.Content.Load<Texture2D>("Star_Large");

        for (int i = 0; i < 200; i++)
            smallStars.Add(new StarColor(random.Next(-600, 600), random.Next(-600, 600), new Color(random.Next(0, 255), 0, random.Next(0, 255))));
        for (int i = 0; i < 100; i++)
            mediumStars.Add(new StarColor(random.Next(-600, 600), random.Next(-600, 600), new Color(random.Next(180, 220), random.Next(140, 180), random.Next(0, 40))));
        for (int i = 0; i < 50; i++)
            largeStars.Add(new StarColor(random.Next(-600, 600), random.Next(-600, 600), new Color(random.Next(175, 255), random.Next(175, 255), 0)));
    }

    public override void Update()
    {
        UpdateStars();
        GameState.gameTime = Engine.GameTime;
        GameState.Update();

        if (GameState.PlayerHealth <= 0)
        {
            //Game over
            Engine.Screen.PopScreen();
            GameOver go = new GameOver();
            go.ScoreGained = GameState.PlayerScore;
            Engine.Screen.PushScreen(go);
        }
        else if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            Engine.Screen.PopScreen(); //Note: do something fancy to preserve game state
    }

    Rectangle PlayerAreaRect = new(-600, -600, 1200, 1200);
    private void UpdateStars()
    {
        PlayerAreaRect.X = (int)(GameState.PlayerPosition.X - 600);
        PlayerAreaRect.Y = (int)(GameState.PlayerPosition.Y - 600);
        foreach (StarColor sc in smallStars)
        {
            if (!PlayerAreaRect.Contains((int)sc.Pos.X, (int)sc.Pos.Y))
            {
                double angle = Math.Atan2(GameState.PlayerPosition.Y - sc.Pos.Y, GameState.PlayerPosition.X - sc.Pos.X);
                double length = MHelper.Vector2Distance(GameState.PlayerPosition, sc.Pos) - 100;
                sc.Pos.X = GameState.PlayerPosition.X + (float)(length * Math.Cos(angle));
                sc.Pos.Y = GameState.PlayerPosition.Y + (float)(length * Math.Sin(angle));
            }
        }
        foreach (StarColor sc in mediumStars)
        {
            if (!PlayerAreaRect.Contains((int)sc.Pos.X, (int)sc.Pos.Y))
            {
                double angle = Math.Atan2(GameState.PlayerPosition.Y - sc.Pos.Y, GameState.PlayerPosition.X - sc.Pos.X);
                double length = MHelper.Vector2Distance(GameState.PlayerPosition, sc.Pos) - 100;
                sc.Pos.X = GameState.PlayerPosition.X + (float)(length * Math.Cos(angle));
                sc.Pos.Y = GameState.PlayerPosition.Y + (float)(length * Math.Sin(angle));
            }
        }
        foreach (StarColor sc in largeStars)
        {
            if (!PlayerAreaRect.Contains((int)sc.Pos.X, (int)sc.Pos.Y))
            {
                double angle = Math.Atan2(GameState.PlayerPosition.Y - sc.Pos.Y, GameState.PlayerPosition.X - sc.Pos.X);
                double length = MHelper.Vector2Distance(GameState.PlayerPosition, sc.Pos) - 100;
                sc.Pos.X = GameState.PlayerPosition.X + (float)(length * Math.Cos(angle));
                sc.Pos.Y = GameState.PlayerPosition.Y + (float)(length * Math.Sin(angle));
            }
        }
    }

    Vector2 v2_helper = new();
    Vector2 v2_bulletOrigin = new(10, 10),
            v2_pickupOrigin = new(25, 25);
    Vector2 v2_score = new(8, 8);
    public override void Draw()
    {
        // Draw all stars
        foreach (StarColor sc in smallStars)
        {
            v2_helper.X = sc.Pos.X - GameState.PlayerPosition.X + 400;
            v2_helper.Y = sc.Pos.Y - GameState.PlayerPosition.Y + 240;
            Engine.SpriteBatch.Draw(smallStar, v2_helper, sc.Colour);
        }
        foreach (StarColor sc in mediumStars)
        {
            v2_helper.X = sc.Pos.X - GameState.PlayerPosition.X + 400;
            v2_helper.Y = sc.Pos.Y - GameState.PlayerPosition.Y + 240;
            Engine.SpriteBatch.Draw(mediumStar, v2_helper, sc.Colour);
        }
        foreach (StarColor sc in largeStars)
        {
            v2_helper.X = sc.Pos.X - GameState.PlayerPosition.X + 400;
            v2_helper.Y = sc.Pos.Y - GameState.PlayerPosition.Y + 240;
            Engine.SpriteBatch.Draw(largeStar, v2_helper, sc.Colour);
        }

        // Draw all enemies
        foreach (Enemy e in GameState.Enemies)
        {
            v2_helper.X = e.Position.X - GameState.PlayerPosition.X + 400;
            v2_helper.Y = e.Position.Y - GameState.PlayerPosition.Y + 240;
            Engine.SpriteBatch.Draw(texShip, v2_helper, null, e.Colour, e.Direction, v2_bulletOrigin, 1.0f, SpriteEffects.None, 0.1f);
        }

        // Draw all bullets
        foreach (Bullet b in GameState.Bullets)
        {
            v2_helper.X = b.Position.X - GameState.PlayerPosition.X + 400;
            v2_helper.Y = b.Position.Y - GameState.PlayerPosition.Y + 240;
            Engine.SpriteBatch.Draw(texBullet, v2_helper, null, b.Colour, 0.0f, v2_bulletOrigin, 1.0f, SpriteEffects.None, 0.1f);
        }

        // Draw Pickup markers
        foreach (Pickup p in GameState.Pickups)
        {
            double direction = Math.Atan2(p.Position.Y - GameState.PlayerPosition.Y, p.Position.X - GameState.PlayerPosition.X);
            v2_helper.X = 400 + (float)(200 * Math.Cos(direction));
            v2_helper.Y = 240 + (float)(200 * Math.Sin(direction));
            Engine.SpriteBatch.Draw(texIndicator, v2_helper, null, Color.White, (float)direction, v2_bulletOrigin, 1.0f, SpriteEffects.None, 0.05f);

            v2_helper.X = -GameState.PlayerPosition.X + p.Position.X + 400;
            v2_helper.Y = -GameState.PlayerPosition.Y + p.Position.Y + 240;
            Engine.SpriteBatch.Draw(texPickup, v2_helper, null, Color.White, 0.0f, v2_pickupOrigin, 1.0f, SpriteEffects.None, 0.05f);
        }

        // Draw player
        Engine.SpriteBatch.Draw(texShip, new Vector2(400, 240), null, Color.LimeGreen, GameState.PlayerDirection, new Vector2(20, 20), 1.0f, SpriteEffects.None, 0);

        // Draw health
        v2_helper.Y = 12;
        for (int i = 0; i < GameState.PlayerHealth; ++i)
        {
            v2_helper.X = 400 - GameState.PlayerHealth * 10 + i * 20;
            Engine.SpriteBatch.Draw(texLife, v2_helper, Color.MediumVioletRed);
        }

        // Draw score
        Engine.SpriteBatch.DrawString(Font, "Score: " + GameState.PlayerScore.ToString(), v2_score, Color.White);
    }

    public override void OnPop()
    {

    }

    public override void OnPush()
    {

    }
}