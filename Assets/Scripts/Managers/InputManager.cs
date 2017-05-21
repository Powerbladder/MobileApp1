using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static GameObject _tileMap;
    static Battle battle;
    static TileGrid tileGrid;
    static TileCell playerTile;     // Player's current TileCell
    static TileCell currentTile;    // Currently selected TileCell

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

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, floorMask))
        {
            // If the user presses the left mouse button, set the path for the character
            if (Input.GetMouseButtonDown(0))    
            {
                currentTile = tileGrid.GetTile(hitInfo.point);

                if (battle.MoveOrSelectPath(currentTile))   // If the character actually moved
                {
                    playerTile = currentTile;               // Set the character's tile to the currently selected one
                    return;                                 // Then exit to the next update
                }

                if (currentTile != playerTile)   // If the currently selected tile is not the same as the player's tile
                    tileGrid.FindPath(playerTile, currentTile);    // Calculate the distance between the 2

                tileGrid.HighlightTile(currentTile);
            }
        }
        else
        {

        } // end if-else Raycast

        // If the user is finished with their turn (pressed spacebar for now)
        if(Input.GetKeyDown("space"))
        {
            battle.EndTurn();
        } // end if-else end turn
    } // end Update

    public static void SetPlayerTile(TileCell tile)
    {
        playerTile = tile;
    }
}
