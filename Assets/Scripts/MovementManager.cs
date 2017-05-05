using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementManager : MonoBehaviour {

    public float moveTime = 0.1f;
    public LayerMask blockingLayer; // Collision detection

    private BoxCollider bc;
    private Rigidbody rb;
    private float inverseMoveTime;

    // This class handles all of the movement for the game


	// Use this for initialization
	protected virtual void Start()
    {
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        inverseMoveTime = 1f / moveTime;
	}

    protected bool Move(int xDir, int zDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, zDir);

        bc.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        bc.enabled = true;
        
        if(hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;
    }

    protected IEnumerator SmoothMovement (Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while(sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb.position, end, inverseMoveTime * Time.deltaTime);
            rb.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    protected virtual void AttemptMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if(hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
            OnCantMove(hitComponent);
        
    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}
