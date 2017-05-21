using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileCell : MonoBehaviour
{
    public static float width;
    public static float height;

    int distance;               // Distance between the character and this tile 

    public RectTransform uiRect;
    public Color color;
    public TileCoordinates coordinates;

    [SerializeField]
    TileCell[] neighbors;       // Neighbors of the current tile

    public int Distance
    {
        get { return distance; }
        set { distance = value; /* UpdateDistanceLabel(); */ }
    }

    // Variables to handle pathfinding
    public TileCell PathFrom { get; set; }      // Stores how this TileCell was reached
    public int SearchHeuristic { get; set; }    // For better pathfinding
    public int SearchPriority { get { return distance + SearchHeuristic; } }
    public TileCell NextWithSamePriority { get; set; }

    void Awake()
    {
        width = transform.localScale.x;
        height = transform.localScale.z;
    } // end Awake

    void UpdateDistanceLabel()
    {
        Text label = uiRect.GetComponent<Text>();
        label.text = distance == int.MaxValue ? "" : distance.ToString();
    } // end UpdateDistanceLabel

    public TileCell GetNeighbor(TileDirection direction)
    {
        return neighbors[(int)direction];
    } // end GetNeighbor

    public void SetNeighbor(TileDirection direction, TileCell tile)
    {
        neighbors[(int)direction] = tile;
        tile.neighbors[(int)direction.Opposite()] = this;
    }

    public Quaternion GetNeighborRotation(TileCell tile)
    {
        if(neighbors[0] == tile)
            return Quaternion.Euler(0, (int)CharacterDirection.S, 0);
        if (neighbors[1] == tile)
            return Quaternion.Euler(0, (int)CharacterDirection.E, 0);
        if (neighbors[2] == tile)
            return Quaternion.Euler(0, (int)CharacterDirection.W, 0);
        if (neighbors[3] == tile)
            return Quaternion.Euler(0, (int)CharacterDirection.N, 0);

        return new Quaternion();
    }
}
