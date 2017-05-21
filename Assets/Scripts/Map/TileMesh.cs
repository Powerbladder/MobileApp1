using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TileMesh : MonoBehaviour
{
    Mesh tileMesh;                  // The mesh to display
    MeshCollider meshCollider;      // Collider to register player actions

    int size_x;                     // Stores the width of the TielGrid
    int size_z;                     // Stores the height of the TileGrid
    int tileSize;                   // Size of each individual tile

    List<Vector3> vertices;         // All of the mesh vertices
    List<int> triangles;            // All of the triangle vertices

    List<Color> colors;             // Stores the colors of all of the tiles

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = tileMesh = new Mesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();

        tileMesh.name = "Tile Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        colors = new List<Color>();
    }

    public void BuildMesh(TileCell[] tiles)
    {
        tileMesh.Clear();       // Clear out the lists just in case
        vertices.Clear();
        triangles.Clear();
        colors.Clear();

        for(int i=0; i<tiles.Length; i++)   // For each TileCell, create the 2 triangles that make up the tile
        {
            Triangulate(tiles[i]);
        }

        tileMesh.vertices = vertices.ToArray();
        tileMesh.colors = colors.ToArray();
        tileMesh.triangles = triangles.ToArray();
        tileMesh.RecalculateNormals();

        meshCollider.sharedMesh = tileMesh;

    } // end BuildMesh

    void Triangulate(TileCell tile)
    {
        Vector3 bottomLeft = tile.transform.localPosition;  // Gets the center of the tile, will need to calculate the bottom left
        float lx = tile.transform.localScale.x;
        float lz = tile.transform.localScale.z;

        bottomLeft.x -= (lx / 2);
        bottomLeft.z -= (lz / 2);

        AddTriangle(    // Add the first triangle
            bottomLeft,
            bottomLeft + new Vector3(lx, 0, lz),
            bottomLeft + new Vector3(lx, 0, 0)
            
        );
        AddTriangleColor(tile.color);

        AddTriangle(    // Add the second triangle
            bottomLeft,
            bottomLeft + new Vector3(0, 0, lz),
            bottomLeft + new Vector3(lz, 0, lz)
        );
        AddTriangleColor(tile.color);


    } // end Triangulate

    // Adds each triangle to our list
    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;

        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);

        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    } // end Add Triangle

    void AddTriangleColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }
}
