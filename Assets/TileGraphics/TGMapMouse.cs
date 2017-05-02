using UnityEngine;
using System.Collections;

public class TGMapMouse : MonoBehaviour {

	TGMap _tileMap;
	Vector3 currentTileCoord;
	public Transform selectioncube; // Graphic that shows what tile is being selected
    static float tileSize;                 // Size of each tile on the map

	// Use this for initialization
	void Start ()
	{
		_tileMap = GetComponent<TGMap>();
        tileSize = _tileMap.tileSize;

    }
	
	// Update is called once per frame
	void Update ()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		
		if(GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
		{
            Vector3 currentTileCoord = TranslateTileCoords(hitInfo.point.x, hitInfo.point.z);

            selectioncube.transform.position = currentTileCoord;
		}
		else
		{
		
		}
	}

    // Translates x,z coords on the map to tile coords
    public static Vector3 TranslateTileCoords(float x, float z)
    {
        Vector3 tileCoords = new Vector3(Mathf.FloorToInt(x / tileSize), 0, Mathf.FloorToInt(z / tileSize));

        return tileCoords * tileSize;
    }

}
