﻿
using UnityEngine;
using System.Collections;

public class Character
{
    public int currDir;	                // Direction character is currently facing
	
	int maxHP = 100;                    // Character's maximum hit points
    public int currHP;			        // Character's current hit points
	
	int maxMP = 4;                      // Character's maximum movement points
    public int currMP;			        // Character's current movement points

    public string charClass;            // Stores the character's class

    public GameObject charObject;       // Reference to the character's physical game object

	public Character(string charClass, float x, float z)
	{
		currHP = maxHP;                 // Current hp is the max
		currMP = maxMP;                 // Current movement is the max
        this.charClass = charClass;

		currDir = (int)CharacterDirection.E;    // Character is facing E

        string assetPath = "Characters/" + charClass;

        charObject = GameObject.Instantiate(Resources.Load(assetPath)) as GameObject;
        float y = charObject.transform.GetChild(0).transform.lossyScale.y / 2.0f;   // Set character's Y position to half its height
        Quaternion r = Quaternion.Euler(0f, currDir, 0f);                           // Rotation is facing NE
        charObject.transform.position = new Vector3(x, y, z);                       // Initialize the character's position
        charObject.transform.rotation = r;                                          // Initialize the character's rotation
    }
}
