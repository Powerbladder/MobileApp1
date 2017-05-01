using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {

    Player[] players;
    public int numPlayers = 1;

    GameObject tileMapGameObject;                           // Stores the map object the player(s) are fighting on
 //   static Dictionary<int, GameObject> charObjectList;    // Dictionary that stores all of the character GameObjects in the battle (physical objects)
    static Dictionary<int, Character> charList;             // Dictionary that stores all of the character objects in the battle

    // Use this for initialization
    void Start ()
    {
        tileMapGameObject = GameObject.Find("TileMap");             // Get the larger map object
        TGMap _tileMap = tileMapGameObject.GetComponent<TGMap>();   // Get the TGMap inside of the map object

        // Initialize some basic map information
        float halfTile = _tileMap.tileSize / 2.0f;                  // Get half of the size of a single tile
        float halfRowSize = _tileMap.size_z * halfTile;             // Get half of the height of the map

        float offSet = 0.0f;
        if (_tileMap.size_z % 2 == 0)                               // If the map height is even, no need to offset by half the tile size
            offSet = halfTile;

        // Initialize the player/character information
        // charObjectList = new Dictionary<int, GameObject>();
        charList = new Dictionary<int, Character>();

        players = new Player[numPlayers];       // Initialize size based on the number of players in the battle

        for(int x=0; x<numPlayers; x++)         // Create a new Player object for each player in the battle
        {
            players[x] = new Player(halfTile, halfRowSize, offSet); // Initialize the player
    //      Instantiate(players[x].characters[0].charModel, new Vector3(players[x].characters[0].positionX, 2, players[x].characters[0].positionZ), Quaternion.Euler(0, players[x].characters[0].currDir, 0));  // Creates a new instance of the character model

            // For each character attached to this player, create a character model instance and add the character to the battle list
            for(int i=0; i<players[x].characters.Length; i++)
            {
                Vector3 position = new Vector3(players[x].characters[i].positionX, 2, players[x].characters[i].positionZ);  // Set the character's starting position
                Quaternion rotation = Quaternion.Euler(0, players[x].characters[i].currDir, 0);                             // Set the character's starting rotation
                string assetPath = "Characters/" + players[x].characters[i].charClass;                                      // Get the path to the proper prefab object

                GameObject newCharObject = GameObject.Instantiate(Resources.Load(assetPath), position, rotation) as GameObject;   // Instantiate a new character object of the class specified
                int charID = newCharObject.GetInstanceID();         // Get the new character object's ID

                players[x].characters[i].charObject = newCharObject;    // Set the GameObject in the character instance to the newly created GameObject
                charList[charID] = players[x].characters[i];            // Store the Character in the character list using its GameObject ID as the key
            } // end inner for
        } // end outer for
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
