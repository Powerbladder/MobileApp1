using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static GameObject _tileMap;
    static Battle battle;
    static TileGrid tileGrid;

    LayerMask floorMask;            // mask of the floor to determine raycast hits
    LayerMask restricted;           // mask of the restricted area during initial character positioning

    // Use this for initialization
    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        restricted = LayerMask.GetMask("Ignore Raycast");     // Do not allow the player to place a character in the restricted area
        _tileMap = GameObject.Find("TileGrid");   // Get the TileGrid component

        battle = _tileMap.GetComponent<Battle>();                       // Get the Battle object
        tileGrid = _tileMap.GetComponent<TileGrid>();                   // Get the TileGrid component
    } // end Start

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    // Cast a ray from the camera to the mouse position
        RaycastHit hitInfo;
       
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, restricted))
            return;

 //      if (_tileMap.GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity)) // If it hits our TileMap, do stuff
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, floorMask))
        {
            TileCoordinates currentTileCoord;

            if (Input.GetMouseButtonDown(0))    // If the user presses the left mouse button, set the path for the character
            {
                currentTileCoord = TouchTile(hitInfo.point);
                battle.MoveOrSelectPath(currentTileCoord);
                tileGrid.HighlightTile(currentTileCoord);
            }
        }
        else
        {

        }
    } // end Update

    // Translates x,z coords on the map to tile coords
    public static TileCoordinates TouchTile(Vector3 position)
    {
        position = _tileMap.transform.InverseTransformPoint(position);
        TileCoordinates tileCoords = TileCoordinates.FromPosition(position);

        return tileCoords;
    }
}
