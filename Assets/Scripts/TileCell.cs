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
        set { distance = value; UpdateDistanceLabel(); }
    }

    public TileCell PathFrom { get; set; }

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
}
