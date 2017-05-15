using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


[System.Serializable]
public struct TileCoordinates               // Struct to store where the tile is on the larger grid
{
    [SerializeField]                        // Allso the fields to be shown in the Unity editor
    private int x, z;

    public int X { get { return x; } }

    public int Z { get { return z; } }

    public TileCoordinates(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Z.ToString();
    }

    public static TileCoordinates FromOffsetCoordinates(int x, int z)
    {
        return new TileCoordinates(x, z);
    }

    public static TileCoordinates FromPosition(Vector3 position)
    {
        float x = position.x / TileCell.width;
        float y = position.z / TileCell.height;

        int ix = Mathf.RoundToInt(x);
        int iy = Mathf.RoundToInt(y);

        return new TileCoordinates(ix, iy);
    }
} // end TileCoordinates


public class TileGrid : MonoBehaviour
{
    public int width = 20;
    public int height = 11;
    public int disableLabel = 0;

    public TileCell tilePrefab;                 // Prefab for an individual tile
    public Text tileLabelPrefab;                // Label for the tile

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.magenta;

 //   public Color defaultColor = Color.white;

    Canvas gridCanvas;                          // Place to put the labels
    TileMesh tileMesh;                          // Mesh for the game board

    TileCell[] tiles;                           // Stores all of the tiles

    void Awake()
    {
        if(disableLabel == 0)
            gridCanvas = GetComponentInChildren<Canvas>();

        tileMesh = GetComponentInChildren<TileMesh>();

        tiles = new TileCell[width * height];
        
        for(int z = 0, i = 0; z < height; z++)
        {
            for(int x = 0; x < width; x++)
            {
                CreateTile(x, z, i++);
            }
        }
    } // end Awake

    void Start()
    {
        tileMesh.BuildMesh(tiles);
    } // end Start

    void CreateTile(int x, int z, int i)
    {
        TileCell tile = tiles[i] = Instantiate<TileCell>(tilePrefab);
        Vector3 position = new Vector3(x * tile.transform.lossyScale.x, 0, z * tile.transform.lossyScale.z);

        tile.transform.SetParent(transform, false);
        tile.transform.localPosition = position;
        tile.coordinates = TileCoordinates.FromOffsetCoordinates(x, z);
        tile.color = defaultColor;

        if(x > 0)   // Set the E-W neighbors for each of the tiles
        {
            tile.SetNeighbor(TileDirection.W, tiles[i - 1]);
        }

        if(z > 0)
        {
            tile.SetNeighbor(TileDirection.S, tiles[(z - 1) * width + x]);
        }

        if (disableLabel == 0)
        {
            Text label = Instantiate<Text>(tileLabelPrefab);
            label.rectTransform.SetParent(gridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
      //      label.text = tile.coordinates.ToStringOnSeparateLines();
            tile.uiRect = label.rectTransform;
        }
        
    } // end CreateTile

    public void HighlightTile(TileCoordinates coordinates)
    {
        int index = coordinates.X + coordinates.Z * width;
        TileCell tile = tiles[index];

        for (int i = 0; i < tiles.Length; i++)
            tiles[i].color = defaultColor;

        tile.color = touchedColor;

        tileMesh.BuildMesh(tiles);
    }

    public void CalculateDistance(TileCoordinates startCoords, TileCoordinates endCoords)
    {
        StopAllCoroutines();

        StartCoroutine(Search(endCoords));
    }

    IEnumerator Search (TileCoordinates coordinates)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].Distance = int.MaxValue;
        }

        WaitForSeconds delay = new WaitForSeconds(1 / 60f);
        Queue<TileCell> frontier = new Queue<TileCell>();

        tiles[coordinates.X + Mathf.RoundToInt(coordinates.Z * width)].Distance = 0;

        frontier.Enqueue(tiles[coordinates.X + Mathf.RoundToInt(coordinates.Z * width)]);

        while(frontier.Count > 0)
        {
            yield return delay;
            TileCell current = frontier.Dequeue();

            for(TileDirection d = TileDirection.N; d <= TileDirection.S; d++)
            {
                TileCell neighbor = current.GetNeighbor(d);
                if(neighbor != null && neighbor.Distance == int.MaxValue)
                {
                    neighbor.Distance = current.Distance + 1;
                    frontier.Enqueue(neighbor);
                }
            }
        }
    }


}
