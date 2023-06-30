using System;
using System.Collections.Generic;
using Godot;

public partial class TerrainDatabase : Node2D
{
    public const float TEXTURE_SCALE = 0.03f;
    public float TextureScale => TEXTURE_SCALE;

    [Export] private PackedScene GroundTile;

    [Export] private Texture2D Dirt;
    [Export] private Texture2D DirtToGrass;
    [Export] private Texture2D Grass;

    public Sprite2D GetSprite(string textureName)
    {
        Sprite2D tile = GroundTile.Instantiate<Sprite2D>();
        if (GroundTile != null)
        {
            switch (textureName)
            {
                case "Dirt":
                    tile.Texture = Dirt;
                    break;
                case "DirtToGrass":
                    tile.Texture = DirtToGrass;
                    break;
                case "GrassToDirt":
                    tile.Texture = DirtToGrass;
                    break;
                case "Grass":
                    tile.Texture = Grass;
                    break;
            }
        }

        return tile;
    }
}