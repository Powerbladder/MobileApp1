public enum CharacterDirection : int { N = 180, E = -90, W = 90, S = 0 }

public enum TileDirection { N, W, E, S}

public static class TileDirectionExtensions
{
    public static TileDirection Opposite(this TileDirection direction)
    {
        return (TileDirection)3 - (int)direction;
    }
}
