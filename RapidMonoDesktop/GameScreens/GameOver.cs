using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RapidMono;
using RapidMono.DataTypes;
using RapidMono.Services;
using System;
using System.Collections.Generic;

namespace RapidMonoDesktop.GameScreens;

class GameOver : IGameScreen
{
    Texture2D smallStar, mediumStar, largeStar;
    List<StarColor> smallStars = new(),
                    mediumStars = new(),
                    largeStars = new();
    SpriteFont Font;

    Rectangle logoPos = new(400, 40, 400, 400);
    Texture2D texLogo;

    Vector2 menuHelperPos = new(100, 190);

    Random random = new();

    public int ScoreGained = 0;

    public override void Load()
    {
        Font = Engine.Content.Load<SpriteFont>("Arial");

        smallStar = Engine.Content.Load<Texture2D>("Star_Small");
        mediumStar = Engine.Content.Load<Texture2D>("Star_Medium");
        largeStar = Engine.Content.Load<Texture2D>("Star_Large");

        texLogo = Engine.Content.Load<Texture2D>("MenuLogo");

        for (int i = 0; i < 40; i++)
            smallStars.Add(new StarColor(random.Next(800), random.Next(480), new Color(random.Next(0, 255), 0, random.Next(0, 255))));
        for (int i = 0; i < 20; i++)
            mediumStars.Add(new StarColor(random.Next(800), random.Next(480), new Color(random.Next(180, 220), random.Next(140, 180), random.Next(0, 40))));
        for (int i = 0; i < 10; i++)
            largeStars.Add(new StarColor(random.Next(800), random.Next(480), new Color(random.Next(175, 255), random.Next(175, 255), 0)));

        ScoreItem s = new()
        {
            Score = ScoreGained
        };
        ScoresData.AddScore(s);
    }

    public override void Update()
    {
        foreach (StarColor sc in smallStars)
        {
            sc.Pos.X -= 10;
            if (sc.Pos.X < 0)
            {
                sc.Pos.X += 800;
                sc.Pos.Y = random.Next(480);
            }
        }
        foreach (StarColor sc in mediumStars)
        {
            sc.Pos.X -= 10;
            if (sc.Pos.X < 0)
            {
                sc.Pos.X += 800;
                sc.Pos.Y = random.Next(480);
            }
        }
        foreach (StarColor sc in largeStars)
        {
            sc.Pos.X -= 10;
            if (sc.Pos.X < 0)
            {
                sc.Pos.X += 800;
                sc.Pos.Y = random.Next(480);
            }
        }

        if (Engine.Keyboard.KeyPress(Keys.Escape) || Engine.GamePad.BPressed(Controller.One))
            Engine.Screen.PopScreen();
    }

    public override void Draw()
    {
        foreach (StarColor sc in smallStars)
        {
            Engine.SpriteBatch.Draw(smallStar, sc.Pos, sc.Colour);
        }
        foreach (StarColor sc in mediumStars)
        {
            Engine.SpriteBatch.Draw(mediumStar, sc.Pos, sc.Colour);
        }
        foreach (StarColor sc in largeStars)
        {
            Engine.SpriteBatch.Draw(largeStar, sc.Pos, sc.Colour);
        }

        Engine.SpriteBatch.DrawString(Font, "Game Over!\nScore: " + ScoreGained.ToString() + "\n\n\n<escape to menu>", menuHelperPos, Color.White);
        Engine.SpriteBatch.Draw(texLogo, logoPos, Color.White);
    }

    public override void OnPop()
    {

    }

    public override void OnPush()
    {

    }
}
