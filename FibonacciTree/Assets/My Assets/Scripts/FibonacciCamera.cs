using UnityEngine;
using System.Collections;

public class FibonacciCamera : MonoBehaviour {

	/// <summary>
	/// The speed at which the camera can move.
	/// </summary>
	public float speed = 1.0f;
	/// <summary>
	/// The distance away from the current node the camera tries to view it from.
	/// </summary>
	public float viewDistance = 8.0f;

	/// <summary>
	/// The tree we are looking at and traversing.
	/// </summary>
	public FibonacciTree tree;
	/// <summary>
	/// The current node we are at in the tree.
	/// </summary>
	private FibonacciNode current;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//make sure the current node is not null. If it is, try to make the tree's root the current.
		if (current != null)
		{
			//the target position is where the camera wants to be (viewDistance away from the current node).
			Vector3 target = current.transform.position - new Vector3(0,0,viewDistance);
			//the distance between the camera's current position and the target position (how far it needs to go).
			Vector3 distance = target - transform.position;

			//if it can reach the target right now, move to it.
			if (distance.magnitude < speed * Time.deltaTime)
			{
				transform.position = target;
			}
			//otherwise, use speed and time to move it closer to the target position.
			else
			{
				transform.position += distance.normalized * speed * Time.deltaTime;
			}

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
			current = tree.Root;
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
		
		//otherwise we can and we do.
		current = current.Left;
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

		//otherwise we can and we do.
		current = current.Right;
		return true;
	}

	/// <summary>
	/// Resets the current node to the root of the tree.
	/// </summary>
	void ResetToRoot()
	{
		//sets the current node to the root.
		current = tree.Root;
	}
}
