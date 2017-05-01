using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class TGMap : MonoBehaviour {

	public int size_x = 100;
	public int size_z = 50;
	public float tileSize = 5.0f;
	public int tileResolution = 16;
	
	public Texture2D terrainTiles;
	
	Vector2[,] _uv;
	
	void Start () {
		BuildMesh();
	}
	
	public void DefineUV()
	{	
		// Get the number of individual images defined in the main texture
		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;
		
		_uv = new Vector2[numTilesPerRow,numRows*4];
		
		// Loop through and grab the appropiate UV coordinates for each sub-texture
		for(int z=0; z<numRows; z++)
		{
			for(int x=0; x<numTilesPerRow; x++)
			{
				_uv[z,x] = new Vector2((float)x / numTilesPerRow, (float)z / numRows);
			} // end z for
		} // end x for
	} //end DefineUV
	
	public void BuildMesh()
	{
//		TDMap map = new TDMap(size_x,size_z);
		
		int numTiles = size_x * size_z;
		int numTris = numTiles * 2;
		
		int vsize_x = size_x * 2;
		int vsize_z = size_z * 2;
		int numVerts = vsize_x * vsize_z;
		
		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;
		
		// Generate the mesh data
		Vector3[] vertices = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];
		
		int[] triangles = new int[numTris * 3];
		
		int x,z,p=0;
		
		for(z=0; z<=size_z; z++)
		{
			for(x=0; x<=size_x; x++)
			{
				vertices[z * vsize_x + p] = new Vector3(x*tileSize, 0, z*tileSize);					
				normals[z * vsize_x + p] = Vector3.up;
				uv[z * vsize_x + p] = new Vector2((float)x / numTilesPerRow, (float)z / numRows);
				
				if(x>0 && x<size_x)
				{	
					p++;
					vertices[z * vsize_x + p] = new Vector3(x*tileSize, 0, z*tileSize);
					normals[z * vsize_x + p] = Vector3.up;	
					uv[z * vsize_x + p] = new Vector2((float)x / numTilesPerRow, (float)z / numRows);
				}
				p++;
			}
			p=0;
		}

		for(z=0; z<size_z; z++)
		{
			p=0;
			for(x=0; x<size_x; x++)
			{
				int squareIndex = z * size_x + x;
				int triOffset = squareIndex * 6;
				
				triangles[triOffset + 0] = z * vsize_x + p + 0;
				triangles[triOffset + 1] = z * vsize_x + p + vsize_x + 0;
				triangles[triOffset + 2] = z * vsize_x + p + vsize_x + 1;
				
				triangles[triOffset + 3] = z * vsize_x + p + 0;
				triangles[triOffset + 4] = z * vsize_x + p + vsize_x + 1;
				triangles[triOffset + 5] = z * vsize_x + p + 1;
				
				
				p+=2;
			} // end for x
		} // end for z
		
		Mesh mesh = new Mesh();
		mesh.name = "TileMesh";
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
/*		
		uv[0] = new Vector2(0.0f, 0.0f);
		uv[1] = new Vector2(0.25f, 0.0f);
		uv[6] = new Vector2(0.0f, 1.0f);
		uv[7] = new Vector2(0.25f, 1.0f);
*/		
		
		mesh.uv = uv;
		
		MeshFilter mesh_filter = GetComponent<MeshFilter>();
		MeshCollider mesh_collider = GetComponent<MeshCollider>();
		
		mesh_filter.mesh = mesh;
		mesh_collider.sharedMesh = mesh;
		
		BuildTexture();
		
	} // end BuildMesh
	
	void BuildTexture()
	{
		// TDMap map = new TDMap(size_x, size_z);
		
		int texWidth = size_x * tileResolution;
		int texHeight = size_z * tileResolution;
		Texture2D texture = new Texture2D(texWidth, texHeight);
/*		
		// Placeholder
		Color[] p = new Color[tileResolution*tileResolution];
		for(int c=0; c<tileResolution; c++)
			p[c] = Color.black;
		
		for(int z=0; z<size_z; z++)
		{
			for(int x=0; x<size_x; x++)
			{
				texture.SetPixels(x*tileResolution, z*tileResolution, tileResolution, tileResolution, p);
			}
		}
*/		
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();
		
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
//		mesh_renderer.sharedMaterials[0].mainTexture = texture;
		mesh_renderer.sharedMaterials[0].mainTexture = terrainTiles;
	} // end BuildTexture
	
}
