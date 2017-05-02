
using UnityEngine;
using System.Collections;

public class Character
{
	enum Direction : int {NW=180, NE=-90, SW=90, SE=0};

    public Vector3 position;            // Character's position

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

        position = new Vector3(x, 0, z);       // Initialize the character's position
        // position.y = charObject.transform.lossyScale.y / 2.0f;  // Translate the char model on the Y based on half the model's height
	}

    //  Moves the character to the specified position; translate on the Y based on model's height
    public void MoveCharacter(Vector3 newPosition)
    {
        position.x = newPosition.x;                                            // Set the conceptual position to the new position
        position.y = charObject.transform.GetChild(0).transform.lossyScale.y / 2.0f;  // Set the conceptual Y to half the model's height (have to use the child since the object is inside of an empty wrapper
        position.z = newPosition.z;

        charObject.transform.position = position;               // Set the actual model to the appropriate position
    }
}
