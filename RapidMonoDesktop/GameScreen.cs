using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RapidMonoDesktop;

public class GameScreen
{
    // TODO: this might be redundant?
    public GameTime gameTime;
    public SpriteBatch spriteBatch;
    public Game1 Game;
    public virtual void Draw()
    {
    }

    public virtual void Update()
    { 
    }

    public virtual void Load()
    {
    }
}