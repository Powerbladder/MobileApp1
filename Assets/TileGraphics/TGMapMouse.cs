using UnityEngine;
using System.Collections;

public class TGMapMouse : MonoBehaviour {

	TGMap _tileMap;
	Vector3 currentTileCoord;
	public Transform selectioncube;

	// Use this for initialization
	void Start ()
	{
		_tileMap = GetComponent<TGMap>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		
		if(GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
		{
			int x = Mathf.FloorToInt(hitInfo.point.x / _tileMap.tileSize);
			int z = Mathf.FloorToInt(hitInfo.point.z / _tileMap.tileSize);
			
			currentTileCoord.x = x;
			currentTileCoord.z = z;
			
			selectioncube.transform.position = currentTileCoord * _tileMap.tileSize;
		}
		else
		{
		
		}
	}
}
