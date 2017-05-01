
using UnityEngine;
using System.Collections;

public class Character
{
	enum Direction : int {NW=180, NE=-90, SW=90, SE=0};

	public float positionX;		        // Character's position in the X
	public float positionZ;             // Character's position in the Z

    public int currDir;	                // Direction character is currently facing
	
	int maxHP = 100;                    // Character's maximum hit points
    public int currHP;			        // Character's current hit points
	
	int maxMP = 4;                      // Character's maximum movement points
    public int currMP;			        // Character's current movement points

    public string charClass;            // Stores the character's class

    public GameObject charObject;              // Reference to the character's physical game object

	public Character(string charClass, float x, float z)
	{
		currHP = maxHP;                 // Current hp is the max
		currMP = maxMP;                 // Current movement is the max
        this.charClass = charClass;

		currDir = (int)Direction.NE;    // Character is facing NE
        positionX = x;
        positionZ = z;
	}
}
