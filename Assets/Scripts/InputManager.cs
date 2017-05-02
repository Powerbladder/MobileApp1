using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    TGMap _tileMap;
    Battle battle;
    Vector3 currentTileCoord;
    public Transform selectioncube; // Graphic that shows what tile is being selected
    static float tileSize;                 // Size of each tile on the map
    static float halfTile;

    // Use this for initialization
    void Start()
    {
        _tileMap = GameObject.Find("TileMap").GetComponent<TGMap>();    // Get the TGMap component of the TileMap object
        tileSize = _tileMap.tileSize;                                   // Store the tileSize for use later
        halfTile = tileSize / 2.0f;

        battle = _tileMap.GetComponent<Battle>();                       // Get the Battle object
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    // Cast a ray from the camera to the mouse position
        RaycastHit hitInfo;

        if (_tileMap.GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity)) // If it hits our TileMap, do stuff
        {
            Vector3 currentTileCoord = TranslateTileCoords(hitInfo.point.x, 0, hitInfo.point.z);

            selectioncube.transform.position = currentTileCoord;

            if (Input.GetMouseButtonDown(0))    // If the user presses the left mouse button, move the character to that location
            {
                battle.MoveCharacter(currentTileCoord);
            }
        }
        else
        {

        }
    }

    // Translates x,z coords on the map to tile coords
    public static Vector3 TranslateTileCoords(float x, float y, float z)
    {
        Vector3 tileCoords = new Vector3(Mathf.FloorToInt(x / tileSize), y, Mathf.FloorToInt(z / tileSize));
        tileCoords.x = (tileCoords.x * tileSize) + halfTile;
        tileCoords.z = (tileCoords.z * tileSize) + halfTile;

        return tileCoords;
    }
}
