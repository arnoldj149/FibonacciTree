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
	private FibonacciNode left;
	/// <summary>
	/// Accessor for the left child.
	/// </summary>
	/// <value>The new left child.</value>
	public FibonacciNode Left
	{
		get { return left; }
		set { left = value; }
	}

	/// <summary>
	/// The right child node of this node.
	/// </summary>
	private FibonacciNode right;
	/// <summary>
	/// Accessor for the right child.
	/// </summary>
	/// <value>The new right child.</value>
	public FibonacciNode Right
	{
		get { return right; }
		set { right = value; }
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
#endregion

	// Use this for initialization
	void Start () {
		
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
