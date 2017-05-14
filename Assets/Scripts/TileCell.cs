using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCell : MonoBehaviour
{
    public static float width;
    public static float height;

    public Color color;

    public TileCoordinates coordinates;

    void Awake()
    {
        width = transform.localScale.x;
        height = transform.localScale.z;
    }
}
