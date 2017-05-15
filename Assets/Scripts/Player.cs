
using UnityEngine;
using System.Collections;

public class Player
{
    public Character[] characters;  // List of all of the characters belonging to this player
    public int team;                // ID of the Player's "team" for use in determining a winner

    int numChars = 1;               // Total number of character belonging to the player
    string charClass;               // Main class for the character

    public TileCoordinates coordinates;        // Main character's location on the game board

    public Player(float x, float z)
	{
        charClass = "Mage";         // Placeholder, change later for real class setup

        characters = new Character[numChars];

        for(int i=0; i<numChars; i++)
            characters[i] = new Character(charClass, x, z);        // Create a new character model on the first column, halfway up the map
    }
}
