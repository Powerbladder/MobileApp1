﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {

    
    public int numPlayers = 1;      // Number of players in the battle (for now, AI counts as players)
    public bool isSetup;             // Is this the setup phase of the battle?

    Player[] players;

    GameObject tileMapGameObject;                           // Stores the map object the player(s) are fighting on
    TGMap _tileMap;                                         // Stores the TileMap "game board"
    float halfTile;                                         // Half of a tile's width
    GameObject restricted;                                  // Get the restricted area

 //   static Dictionary<int, Character> charList;             // Dictionary that stores all of the character objects in the battle

    // Use this for initialization
    void Start ()
    {
        isSetup = true;
        tileMapGameObject = GameObject.Find("TileMap");             // Get the larger map object
        _tileMap = tileMapGameObject.GetComponent<TGMap>();         // Get the TGMap inside of the map object

        restricted = GameObject.Find("Restricted");                 // Get the restricted area

        // Initialize some basic map information
        halfTile = _tileMap.tileSize / 2.0f;                        // Get half of the size of a single tile
        float halfRowSize = _tileMap.size_z * halfTile;             // Get half of the height of the map

        float offSet = 0.0f;
        if (_tileMap.size_z % 2 == 0)                               // If the map height is even, no need to offset by half the tile size
            offSet = halfTile;

 //       charList = new Dictionary<int, Character>();

        players = new Player[numPlayers];       // Initialize size based on the number of players in the battle

        for(int x=0; x<numPlayers; x++)         // Create a new Player object for each player in the battle
        {
            players[x] = new Player(halfTile, halfRowSize + offSet); // Initialize the player

            // For each character attached to this player, add the character to the battle list
            for(int i=0; i<players[x].characters.Length; i++)
            {
 //               int charID = players[x].characters[i].charObject.GetInstanceID();
 //               charList[charID] = players[x].characters[i];            // Store the Character in the character list using its GameObject ID as the key
            } // end inner for
        } // end outer for
	}
	
    // Moves the character to the appropriate position
    public void MoveCharacter(Vector3 newPosition)
    {
        if(isSetup) // If we are still in the setup phase, move the character to the specified location
        {
            players[0].MoveCharacter(newPosition);  // Move the character
            isSetup = false;                        // No longer in the setup phase
            Destroy(restricted);                    // Destroy the restricted area
        }
            
    }

}
