public enum CharacterDirection : int { NW = 180, NE = -90, SW = 90, SE = 0 }

public enum TileDirection { N, W, E, S}

public static class TileDirectionExtensions
{
    public static TileDirection Opposite(this TileDirection direction)
    {
        return (TileDirection)3 - (int)direction;
    }
}
