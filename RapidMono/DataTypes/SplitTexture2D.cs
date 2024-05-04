using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RapidMono.DataTypes;

public class SplitTexture2D
{
    public Texture2D Texture { get; private set; }
    
    /// <summary>
    /// Tile Width and Height
    /// </summary>
    public int Width { get; private set; }
    public int Height { get; private set; }

    private Rectangle _sourceTile;

    public SplitTexture2D(string contentFile, int width, int height)
    {
        Texture = Engine.Content.Load<Texture2D>(contentFile);
        Width = width;
        Height = height;
        _sourceTile = new Rectangle(0, 0, width, height);
    }

    public void Draw(int tile_x, int tile_y, int pos_x, int pos_y, int pos_w, int pos_h, Color color)
    {
        _sourceTile.X = tile_x * Width;
        _sourceTile.Y = tile_y * Height;

        Engine.SpriteBatch.Draw(Texture, new Rectangle(pos_x, pos_y, pos_w, pos_h), _sourceTile, color);
    }
}
