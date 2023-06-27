using System;
using System.Collections.Generic;
using Godot;

public partial class TerrainDatabase : Node2D
{
    public const float TEXTURE_SCALE = 0.03f;
    public float TextureScale => TEXTURE_SCALE;

    [Export] private PackedScene GroundTile;

    [Export] private Texture2D _Ground_1;

    public Sprite2D GetSprite(string textureName)
    {
        Sprite2D tile = GroundTile.Instantiate<Sprite2D>();
        if (GroundTile != null)
        {
            switch (textureName)
            {
                case "Ground_1":
                    tile.Texture = _Ground_1;
                    break;
            }
        }

        return tile;
    }
}