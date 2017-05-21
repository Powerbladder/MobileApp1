using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {

    
    public int numPlayers = 1;      // Number of players in the battle (for now, AI counts as players)

    public Player[] players;        // Contains all of the players in the battle

    GameObject _tileMap;            // The battle map
    GameObject restricted;          // Get the restricted area
    bool isSetup;                   // Is this the setup phase of the battle?
    bool isSelected;                // Do we have a tile selected?
    public bool endTurn;            // Has the player hit the "End Turn" button?

 //   static Dictionary<int, Character> charList;           // Dictionary that stores all of the character objects in the battle

    // Use this for initialization
    void Start ()
    {
        isSetup = true;
 //       isSelected = false;
        endTurn = false;

        restricted = GameObject.Instantiate(Resources.Load("Map/Restricted")) as GameObject;                 // Get the restricted area
        restricted.transform.position = new Vector3(52.5f, 0.01f, 25f);

        //       charList = new Dictionary<int, Character>();

        players = new Player[numPlayers];       // Initialize size based on the number of players in the battle
        
        for(int x=0; x<numPlayers; x++)         // Create a new Player object for each player in the battle
        {
            //         players[x] = new Player(halfTile, halfRowSize + offSet); // Initialize the player
            players[x] = new Player(0, 0);    // Initialize the player


            // For each character attached to this player, add the character to the battle list
            //          for(int i=0; i<players[x].characters.Length; i++)
            //          {
            //               int charID = players[x].characters[i].charObject.GetInstanceID();
            //               charList[charID] = players[x].characters[i];            // Store the Character in the character list using its GameObject ID as the key
            //          } // end inner for
        } // end outer for
	} // end Start
	
    // Moves the character to the selected tile or shows the path from the character to the selected tile
    public bool MoveOrSelectPath(TileCell tile)
    {
        if(isSetup) // If we are still in the setup phase, move the character to the specified location
        {
            //      players[0].MoveCharacter(newPosition);  // Move the character
            MovementManager.MoveObject(players[0].characters[0].charObject, tile.coordinates);
            players[0].coordinates = tile.coordinates;   // Set the player's coordinates on the TileGrid
            isSetup = false;                        // No longer in the setup phase
            Destroy(restricted);                    // Destroy the restricted area

            return true; 
        }
        else                // Show path to selected tile
        {
            /*
            if(isSelected)  // If we already have a path showing, remove it first
            {
                foreach(KeyValuePair<int, GameObject> entry in pathList)    // For all items in the pathlist, deleted the associated game object
                {
                    Destroy(entry.Value);
                }

                pathList.Clear();   // Clear out the pathList
                ShowPath(players[0].characters[0].position, newPosition);   // Now create a new path
            }
            else
            {
                ShowPath(players[0].characters[0].position, newPosition);
                isSelected = true;
            }
            */
            return false;
        }
            
    } // end MoveorSelectCharacter

    public void EndTurn()
    {
        endTurn = true;
    } // end EndTurn
}
