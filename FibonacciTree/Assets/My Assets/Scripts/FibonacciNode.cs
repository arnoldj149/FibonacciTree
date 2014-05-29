using UnityEngine;
using System.Collections;

/// <summary>
/// A class representing a node in a FibonacciTree.
/// </summary>
public class FibonacciNode : MonoBehaviour {

#region Members & Accessors
	/// <summary>
	/// The integer data held in the node.
	/// </summary>
	private int data;
	/// <summary>
	/// Accessor for data. Upon being set, the text for this node is also set
	/// to the string version of the data.
	/// </summary>
	/// <value>The new data.</value>
	public int Data
	{
		get { return data; }
		set 
		{ 
			data = value;
			Text = data.ToString();
		}
	}
	/// <summary>
	/// The left child node of this node.
	/// </summary>
	private FibonacciNode left = null;
	/// <summary>
	/// Accessor for the left child.
	/// </summary>
	/// <value>The new left child.</value>
	public FibonacciNode Left
	{
		get { return left; }
		set 
		{ 
			left = value;
			left.LineEnd = transform.position;
		}
	}

	/// <summary>
	/// The right child node of this node.
	/// </summary>
	private FibonacciNode right = null;
	/// <summary>
	/// Accessor for the right child.
	/// </summary>
	/// <value>The new right child.</value>
	public FibonacciNode Right
	{
		get { return right; }
		set 
		{ 
			right = value;
			right.LineEnd = transform.position;
		}
	}

	/// <summary>
	/// Gets or sets the first TextMesh component in this GameObject's hierarchy.
	/// </summary>
	/// <value>The new text.</value>
	public string Text
	{
		get { return GetComponentInChildren<TextMesh>().text; }
		set { GetComponentInChildren<TextMesh>().text = value; }
	}

	/// <summary>
	/// Sets the line start point for the first LineRenderer attached to this GameObject.
	/// </summary>
	/// <value>The line start point.</value>
	public Vector3 LineStart
	{
		set { GetComponentInChildren<LineRenderer>().SetPosition(0, value); }
	}
	/// <summary>
	/// Sets the line end point for the first LineRenderer attached to this GameObject.
	/// </summary>
	/// <value>The line end point.</value>
	public Vector3 LineEnd
	{
		set { GetComponentInChildren<LineRenderer>().SetPosition(1, value); }
	}
#endregion

	// Use this for initialization
	void Start () {
		// Start the line so that its start position begins at the center of the node.
		LineStart = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Creates the children of this node with supplied data values.
	/// </summary>
	/// <param name="leftData">Data to give the left child.</param>
	/// <param name="rightData">Data to give the right child.</param>
	void CreateChildren(int leftData, int rightData)
	{
		//will probably take some parts out of the BuildTree function in FibonacciTree
		//and move them here so that child nodes are created by their parents.
	}
}
