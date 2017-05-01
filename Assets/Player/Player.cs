
using UnityEngine;
using System.Collections;

public class Player
{
    public Character[] characters;  // List of all of the characters belonging to this player
    int numChars = 3;               // Total number of character belonging to the player
    string charClass;               // Main class for the character

	public Player(float halfTile, float halfRowSize, float offSet)
	{
        charClass = "Mage";         // Placeholder, change later for real class setup

        characters = new Character[numChars];

        for(int i=0; i<numChars; i++)
            characters[i] = new Character(charClass, halfTile, halfRowSize + offSet);        // Create a new character model on the first column, halfway up the map
    }
}
