using UnityEngine;
using System.Collections;

public class FibonacciCamera : MonoBehaviour {

	/// <summary>
	/// The amount of time in seconds it takes the camera to move from one node
	/// to the next node when traversing.
	/// </summary>
	public float traversalTime = 0.5f;
	private Vector3 startMove;
	private Vector3 endMove;
	private float lastMoveTime;

	/// <summary>
	/// The distance away from the current node the camera tries to view it from.
	/// </summary>
	public float viewDistance = 8.0f;
	/// <summary>
	/// The amount to scale each node while it is active.
	/// </summary>
	public float inflateValue = 1.3f;

	/// <summary>
	/// The tree we are looking at and traversing.
	/// </summary>
	public FibonacciTree tree;
	/// <summary>
	/// The current node we are at in the tree.
	/// </summary>
	private FibonacciNode current;

	/// <summary>
	/// The message displayed at the bottom of the GUI box
	/// that gives feedback to the user about their last input.
	/// </summary>
	private string errorMsg = "";

	// Use this for initialization
	void Start () {
		startMove = transform.position;
		endMove = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//make sure the current node is not null. If it is, try to make the tree's root the current.
		if (current != null)
		{
			//interpolate the camera's position between the last node and the next node.
			transform.position = Vector3.Lerp(startMove, endMove, (Time.time - lastMoveTime) / traversalTime);

			//some rough input handling for traversal testing. Controls:
			//LeftArrow= traverse left
			//RightArrow= traverse right
			//SpaceBar= reset back to the root of the tree
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				TraverseLeft();
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				TraverseRight();
			}
			else if (Input.GetKeyDown(KeyCode.Space))
			{
				ResetToRoot();
			}
		}
		else if (tree != null)
		{
			ResetToRoot();
		}
	}

	/// <summary>
	/// Traverses to the left child of the current node if possible.
	/// </summary>
	/// <returns><c>true</c>, if left child was traversed, <c>false</c> if child was not.</returns>
	bool TraverseLeft()
	{
		//if the current node does not have a left child, we can't traverse it.
		if (current.Left == null)
			return false;

		//deflate current node before we traverse.
		current.transform.localScale /= inflateValue;
		//otherwise, traverse to the left child.
		current = current.Left;
		//inflate new current node.
		current.transform.localScale *= inflateValue;

		//lastly, setup the camera to interpolate from its current position to the new node.
		startMove = transform.position;
		endMove = current.transform.position - new Vector3(0,0,viewDistance);
		lastMoveTime = Time.time;

		return true;
	}

	/// <summary>
	/// Traverses to the right child of the current node if possible.
	/// </summary>
	/// <returns><c>true</c>, if right child was traversed, <c>false</c> if chidl was not.</returns>
	bool TraverseRight()
	{
		//if the current node does not have a right child, we can't traverse it.
		if (current.Right == null)
			return false;

		//deflate current node before we traverse.
		current.transform.localScale /= inflateValue;
		//otherwise, traverse to the right child.
		current = current.Right;
		//inflate new current node.
		current.transform.localScale *= inflateValue;

		//lastly, setup the camera to interpolate from its current position to the new node.
		startMove = transform.position;
		endMove = current.transform.position - new Vector3(0,0,viewDistance);
		lastMoveTime = Time.time;

		return true;
	}

	/// <summary>
	/// Resets the current node to the root of the tree.
	/// </summary>
	/// <returns><c>true</c>, if the traversal was reset, <c>false</c> if there is no tree to reset.</returns>
	bool ResetToRoot()
	{
		//if the current node is not null, we need to deflate it
		if (current != null)
			current.transform.localScale /= inflateValue;
		//sets the current node to the root.
		current = tree.Root;
		//if the new current node is not null, we need to inflate it
		if (current != null)
			current.transform.localScale *= inflateValue;

		//if the root was null, we don't have a tree to reset on, so return false.
		if (current == null)
			return false;

		//lastly, setup the camera to interpolate from its current position to the new node.
		startMove = transform.position;
		endMove = current.transform.position - new Vector3(0,0,viewDistance);
		lastMoveTime = Time.time;

		return true;
	}

	// Creates the GUI for the camera on the GUI event
	void OnGUI () 
	{
		// Creates a box around the buttons.
		GUI.Box(new Rect(10,220,250,200), "Traversal Controls");

		if(current != null)
		{
			GUI.Label(new Rect(20,250,230,100), "Hotkeys: Left=LeftArrow, Right=RightArrow, Space=Reset");

			// A button that says "Build Tree" that tells the FibonacciTree script to build a tree.
			if(GUI.Button(new Rect(20,330,110,30), "Traverse Left")) 
			{
				if (TraverseLeft())
				{
					errorMsg = "Traversing to left child.";
				}
				else
				{
					errorMsg = "There is no left child!";
				}
			}
			
			// A button that says "Destroy Tree" that tells the FibonacciTree script to destroy the tree.
			if(GUI.Button(new Rect(140,330,110,30), "Traverse Right")) 
			{
				if (TraverseRight())
				{
					errorMsg = "Traversing to right child.";
				}
				else
				{
					errorMsg = "There is no right child!";
				}
			}

			// A button that says "Destroy Tree" that tells the FibonacciTree script to destroy the tree.
			if(GUI.Button(new Rect(20,290,230,30), "Reset")) 
			{
				if (ResetToRoot())
				{
					errorMsg = "Reset to root node.";
				}
				else
				{
					errorMsg = "There is no tree to reset!";
				}
			}
		}
		
		GUI.Label(new Rect(20,370,230,100), errorMsg);
	}
}
