using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RapidMono;
using RapidMono.DataTypes;
using RapidMono.Services;
using System;
using System.Collections.Generic;

namespace RapidMonoDesktop.GameScreens;

class Menu : IGameScreen
{

    List<string> menuOptions = new(),
                menuHelpers = new();
    Vector2 menuHelperPos = new(100, 440);
    int selectedItem = 0;

    SpriteFont Font;

    Rectangle logoPos = new(400, 40, 400, 400);
    Texture2D texLogo;

    Texture2D smallStar, mediumStar, largeStar;
    List<StarColor> smallStars = new(),
                    mediumStars = new(),
                    largeStars = new();

    public Menu()
    {
        if (GameState.HasLoadState())
        {
            menuOptions.Add("Continue");
            menuHelpers.Add("Continue the game from where you were.");
        }
        menuOptions.Add("New Game");
        menuHelpers.Add("Start a new game.");

        menuOptions.Add("Local Scores");
        menuHelpers.Add("");

        menuOptions.Add("Quit");
        menuHelpers.Add("Exit the game.");
    }


    Random random = new();
    IList<ScoreItem> myScores;
    public override void Load()
    {
        Font = Engine.Content.Load<SpriteFont>("Arial");

        texLogo = Engine.Content.Load<Texture2D>("MenuLogo");

        smallStar = Engine.Content.Load<Texture2D>("Star_Small");
        mediumStar = Engine.Content.Load<Texture2D>("Star_Medium");
        largeStar = Engine.Content.Load<Texture2D>("Star_Large");

        for (int i = 0; i < 40; i++)
            smallStars.Add(new StarColor(random.Next(800), random.Next(480), new Color(random.Next(0, 255), 0, random.Next(0, 255))));
        for (int i = 0; i < 20; i++)
            mediumStars.Add(new StarColor(random.Next(800), random.Next(480), new Color(random.Next(180, 220), random.Next(140, 180), random.Next(0, 40))));
        for (int i = 0; i < 10; i++)
            largeStars.Add(new StarColor(random.Next(800), random.Next(480), new Color(random.Next(175, 255), random.Next(175, 255), 0)));

        myScores = ScoresData.GetScores();
    }

    private float _pad_timeDelay = 0;
    public override void Update()
    {
        if (Engine.Keyboard.KeyPress(Keys.Escape) || Engine.GamePad.BPressed(Controller.One))
            Engine.Exit();

        if (_pad_timeDelay > 0)
            _pad_timeDelay -= Engine.GameTime.ElapsedGameTime.Microseconds;
        var new_direction = _pad_timeDelay > 0 
            ? 0 
            : (float)Math.Atan2(-Engine.GamePad.LeftStick(Controller.One).Y, Engine.GamePad.LeftStick(Controller.One).X);

        if (Engine.Keyboard.KeyPress(Keys.W) || Engine.Keyboard.KeyPress(Keys.Up) || new_direction < 0)
        {
            _pad_timeDelay = 1250;
            selectedItem = selectedItem - 1;
            if (selectedItem < 0)
                selectedItem += menuOptions.Count;
        }
        else if (Engine.Keyboard.KeyPress(Keys.S) || Engine.Keyboard.KeyPress(Keys.Down) || new_direction > 0)
        {
            _pad_timeDelay = 1250;
            selectedItem = (selectedItem + 1) % menuOptions.Count;
        }
        else if (Engine.Keyboard.KeyPress(Keys.Enter) || Engine.GamePad.APressed(Controller.One))
        {
            if (menuOptions[selectedItem] == "Quit")
            {
                Engine.Exit();
            }
            else if (menuOptions[selectedItem] == "New Game")
            {
                GameState.Clear();
                Engine.Screen.PushScreen(new InGame());
            }
            else if (menuOptions[selectedItem] == "Continue")
            {
                Engine.Screen.PushScreen(new InGame());
            }
        }

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
    }


    Vector2 pos = new Vector2(), pos2 = new Vector2(100, 0), v2_helper = new Vector2();
    Color smallColor = new Color(0, 75, 0),
          mediumColor = new Color(0, 150, 0),
          largeColor = new Color(0, 255, 0);
    int OffSet = 0;
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

        pos.Y = 170;
        for (int i = selectedItem - 2; i < selectedItem + 3; i++)
        {
            pos.Y += 14;
            pos2.Y = pos.Y + OffSet;
            if (Math.Abs(selectedItem - i) > 1)
            {
                Engine.SpriteBatch.DrawString(Font, menuOptions[(i + menuOptions.Count) % menuOptions.Count], pos2, smallColor);
            }
            else if (Math.Abs(selectedItem - i) == 1)
            {
                Engine.SpriteBatch.DrawString(Font, menuOptions[(i + menuOptions.Count) % menuOptions.Count], pos2, mediumColor);
                pos.Y += 4;
            }
            else
            {
                Engine.SpriteBatch.DrawString(Font, menuOptions[(i + menuOptions.Count) % menuOptions.Count], pos2, largeColor);
                pos.Y += 8;
            }
        }

        Engine.SpriteBatch.DrawString(Font, menuHelpers[selectedItem], menuHelperPos, mediumColor);

        if (menuOptions[selectedItem] != "Local Scores")
        {
            Engine.SpriteBatch.Draw(texLogo, logoPos, Color.White);
        }
        else
        {
            v2_helper.X = 400;
            v2_helper.Y = 40;
            if (myScores != null)
            {
                for (int i = 0; (i < myScores.Count) && (i < 5); i++)
                {
                    v2_helper.Y += 20;
                    Engine.SpriteBatch.DrawString(Font, $"{myScores[i].Date.ToShortDateString()} - {myScores[i].Score}", v2_helper, Color.White);
                }
            }
            else
            {
                Engine.SpriteBatch.DrawString(Font, "No scores yet!", v2_helper, Color.LightGray);
            }
        }
    }

    public override void OnPop()
    {

    }

    public override void OnPush()
    {
        myScores = ScoresData.GetScores();
    }
}