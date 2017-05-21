using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    static float tileWidth;         // Width of an individual tile
    static float tileHeight;        // Height of an individual tile

    static GameObject _tileMap;
    static Battle battle;
    static TileGrid tileGrid;

    // Movement variables
    static Stack<TileCell> path;    // Stores the current path
    static bool isMoving;           // Are we not yet done with the current path?

    static float speed;             // Speed characters move
    static float rotationSpeed;     // Speed characters rotate
    static float distCovered;       // Distance the object has moved through current leg
    static float fracJourney;       // Percentage of current leg the character has moved through
    static float journeyLength;     // Total length of the current leg (should be constant for square tiles)
    static TileCell start;
    static TileCell end;
    static Vector3 sp;              // Start point for leg
    static Vector3 ep;              // End point for leg
    static Quaternion sr;           // Start rotation for leg
    static Quaternion er;           // End rotation for leg

    // This class handles all of the movement for the game

    void Start()
    {
        tileWidth = TileCell.width;
        tileHeight = TileCell.height;

        _tileMap = GameObject.Find("TileGrid");   // Get the TileGrid game object
        battle = _tileMap.GetComponent<Battle>();
        tileGrid = _tileMap.GetComponent<TileGrid>();

        isMoving = false;

        speed = 10.0f;
        rotationSpeed = 1f;
        distCovered = 0.0f;
        fracJourney = 0.0f;
        start = null;
        end = null;
        sp = new Vector3();
        ep = new Vector3();
        sr = new Quaternion();
        er = new Quaternion();
    }

    void Update()
    {
        MoveCharacters();
    } // end Update

    // Translates tile coordinates to real game coodinates
    public static Vector3 TranslateTileCoordinates(TileCoordinates coordinates)
    {
        return new Vector3(coordinates.X * tileWidth, 2f, coordinates.Z * tileHeight);    // Will need to change 2 to programmtically generated number to support multiple model heights
    } // end TranslateTileCoordinates

    // Actually moves the game object, this is a straight re-positioning
    public static void MoveObject(GameObject go, TileCoordinates coordinates)
    {
        Vector3 newPosition = TranslateTileCoordinates(coordinates);
        newPosition.y = go.transform.GetChild(0).transform.lossyScale.y / 2.0f;

        go.transform.position = newPosition;
    } // end MoveObject

    public static void MoveCharacters()
    {
        // If the battle has changed to the movement phase
        if (battle.endTurn)
        {
            if (!isMoving && (path == null || path.Count == 0))     // If we haven't yet gotten the path from the TileGrid, get it
            {
                path = tileGrid.GetPath();
                isMoving = true;
            }

            if (fracJourney >= 1.0f && path.Count == 0)    // If we have reached the end of the last leg, we are done moving
            {
                battle.endTurn = false;     // get ready to start a new turn
                tileGrid.ClearPath();       // Clear the current path

                fracJourney = 0.0f;         // Set our journey back to zero
                isMoving = false;           // Character is no longer moving

                InputManager.SetPlayerTile(end);

                return;
            }

            if (fracJourney > 1.0f)
                fracJourney = 0.0f;

            if (fracJourney == 0.0f)
            {
                switch (path.Count)
                {
                    case 0:                         // If our path is empty, nothing else to Pop
                        break;

                    case 2:
                        start = path.Pop();         // Get the last 2 points in the path
                        end = path.Pop();
                        break;

                    default:
                        start = path.Pop();         // Get the next 2 points in the path
                        end = path.Peek();
                        break;
                } // end switch

                sp = TranslateTileCoordinates(start.coordinates);
                ep = TranslateTileCoordinates(end.coordinates);

                sr = battle.players[0].characters[0].charObject.transform.rotation;
                er = end.GetNeighborRotation(start);

                journeyLength = Vector3.Distance(sp, ep);   // Get the distance between endpoints
                distCovered = 0.0f;
            } // end if fracJourney

            MoveAlongPath(battle.players[0].characters[0].charObject, sp, ep, sr, er);
        } // end if endTurn
    } // end MoveCharacters

    public static void MoveAlongPath(GameObject go, Vector3 start, Vector3 end, Quaternion startRot, Quaternion endRot)
    {
        //     startTime += Time.deltaTime;
        //     distCovered = startTime * speed;
        distCovered += Time.deltaTime * speed;
        fracJourney = distCovered / journeyLength;

        go.transform.position = Vector3.Lerp(start, end, fracJourney);
        go.transform.rotation = Quaternion.Slerp(startRot, endRot, Time.time * rotationSpeed);
    } // end MoveAlongPath
}
