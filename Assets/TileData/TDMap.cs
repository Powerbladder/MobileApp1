
using UnityEngine;
using System.Collections;

public class TDMap : MonoBehaviour
{
    TGMap tileMap;              // Instance of the attached TileMap

	int width;                  // Width of the map
	int height;                 // Height of the map
	
	int[,] tiles;               // Stores coords for all of the tiles

    void Start()
    {
        tileMap = GetComponent<TGMap>();
        width = tileMap.size_x;
        height = tileMap.size_z;

        tiles = new int[width, height];     // Initialize a 2D array the width and height of the TileMap
    }

    public int GetTileAt(int x, int y)
	{
		if(x < 0 || x >= width || y < 0 || y >= height)
		{
			return 0;
		}
		
		return tiles[x,y];
	}
}
