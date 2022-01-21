using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchState
{
    TAP, SWIPE
}

public class InputDetection : MonoBehaviour
{
    
    public TouchState touchState;

    public float minCuttingVelocity = .001f;

	bool isCutting = false;

	Vector2 previousPosition;

	TrailRenderer trailRenderer;

	Rigidbody2D rb;
	Camera cam;
	CircleCollider2D circleCollider;

	void Start ()
	{
		cam = Camera.main;
		rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
		circleCollider = GetComponent<CircleCollider2D>();
	}

	// Update is called once per frame
	void Update () {
		//if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		if(Input.GetMouseButtonDown(0))
        {
			circleCollider.enabled = true;
			touchState = TouchState.TAP;

			StartCutting();

		} else //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		if(Input.GetMouseButtonUp(0))
        {
			StopCutting();
		}

		if (isCutting)
		{
			UpdateCut();
		}

	}

	void UpdateCut ()
	{
		//Vector2 newPosition = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

		float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
		if (velocity > minCuttingVelocity)
		{
            circleCollider.enabled = true;
            trailRenderer.enabled = true;

            // Debug.Log("Swipe Detect");
            touchState = TouchState.SWIPE;
		} else
		{
            trailRenderer.enabled = false;
		}

		previousPosition = newPosition;
	}

	void StartCutting ()
	{
		isCutting = true;
		//previousPosition = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		circleCollider.enabled = false;
	}

	void StopCutting ()
	{
		isCutting = false;
		circleCollider.enabled = false;
	}


}
