using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    public float moveTime = 0.1f;   // Height of an individual tile
    static float tileWidth;                // Width of an individual tile
    static float tileHeight;

    // This class handles all of the movement for the game

    public void Start()
    {
        tileWidth = TileCell.width;
        tileHeight = TileCell.height;
    }

    // Actually moves the game object
    public static void MoveObject(GameObject go, TileCoordinates coordinates)
    {
        Vector3 newPosition = TranslateTileCoordinates(coordinates);
        newPosition.y = go.transform.GetChild(0).transform.lossyScale.y / 2.0f;

        go.transform.position = newPosition;
    }

    // Translates tile coordinates to real game coodinates
    public static Vector3 TranslateTileCoordinates(TileCoordinates coordinates)
    {
        return new Vector3(coordinates.X * tileWidth, 0, coordinates.Z * tileHeight);
    }
}
