using UnityEngine;
using System.Collections;

/// <summary>
/// A tree that follows a set of rules similar to that of the Fibonacci Sequence.
/// </summary>
public class FibonacciTree : MonoBehaviour {

	/// <summary>
	/// The GameObject prefab to use as nodes for the tree. 
	/// PREFAB MUST HAVE A "FibonacciNode.cs" SCRIPT COMPONENT ATTATCHED!!!
	/// </summary>
	public GameObject nodePrefab;

	/// <summary>
	/// The nodes for the tree. Since the nature of this tree is that it is created based on a series
	/// of rules rather than through something more random like user input, the entire tree can be
	/// created as soon as a depth is specified by the user. We can take advantage of that fact and 
	/// maintain all of the tree's data in a structured series of arrays that can be traversed 
	/// </summary>
	private FibonacciNode[][] nodes = null;

	/// <summary>
	/// Gets the root node of the tree.
	/// </summary>
	/// <value>The root.</value>
	public FibonacciNode Root
	{
		get { return nodes[0][0]; }
	}

	// Use this for initialization
	void Start () {
		//Test by building the tree here rather than through user input.
		BuildTree(5);

		//Test DestroyTree by destroying the tree immediately after its creation above.
		DestroyTree();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Builds a tree from a depth supplied by by the user using these rules:
	/// 1)The root has a value of 1. 
	/// 2)Each node has two children. 
	/// 3)If it's the left child, its value will be the sum of the parent's value and the parent's 
	/// left sibling value. If the parent has no left sibling, then the child's value is the 
	/// same as its parent.
	/// 4)If it's the right child, its value will be the sum of the parent's value and the parent's 
	/// right sibling value. If the parent has no right sibling, then the child's value is the 
	/// same as its parent.
	/// </summary>
	/// <param name="depth">The number of generations to create in the tree. A depth of 1 will 
	/// create only the root. If the depth is less than 1, the tree is not constructed.</param>
	void BuildTree(uint depth)
	{
		//if depth is less than 1, no tree is generated.
		if (depth < 1)
			return;

		//the number of siblings at the lowest level of this tree (Used to determine positioning of nodes).
		float maxSiblings = Mathf.Pow (2,depth-1);

		//otherwise create a temporary array of FibonacciNode arrays large enough for 'depth' number of generations.
		nodes = new FibonacciNode[depth][];

		//now generate data for each level of the tree:

		int siblings;
		int parentIndex;
		//for each level of the tree...
		for (int i = 0; i < depth; i++)
		{
			//there are 2^n siblings per generation where n is the generation's level (if root is level 0).
			siblings = (int) Mathf.Pow(2,i);
			//allocate memory for this level of the tree large enough to hold that many siblings.
			nodes[i] = new FibonacciNode[siblings];

			//if this is the root, treat it differently
			if (i == 0)
			{
				//instatiate a node prefab for the root node and cast it as a FibonacciNode.
				nodes[i][0] = ((GameObject)GameObject.Instantiate(nodePrefab, Vector3.zero, Quaternion.identity)).GetComponent<FibonacciNode>();

				//the root is given the value 1.
				nodes[i][0].Data = 1;
			}
			//otherwise...
			else
			{
				FibonacciNode parent;
				FibonacciNode leftChild;
				FibonacciNode rightChild;
				//for each pair of children in this generation...
				for (int j = 0; j < siblings; j += 2)
				{
					//find the index of the parent in generation i - 1 for these two children.
					parentIndex = j / 2;
					//save a reference to the parent node for readability sake.
					parent = nodes[i-1][parentIndex].GetComponent<FibonacciNode>();

					//instatiate a node prefab for the left and right child node and cast it as a FibonacciNode.
					//the left and right nodes' positions are an offset from the parent node.
					nodes[i][j] = ((GameObject)Instantiate(nodePrefab, 
											parent.transform.position + new Vector3(-maxSiblings / siblings,-1,0), 
					                        Quaternion.identity)).GetComponent<FibonacciNode>();

					nodes[i][j+1] = ((GameObject)Instantiate(nodePrefab, 
					                        parent.transform.position + new Vector3(maxSiblings / siblings,-1,0), 
                                            Quaternion.identity)).GetComponent<FibonacciNode>();

					//save a reference to the left and right child for readability sake.
					leftChild = nodes[i][j];
					rightChild = nodes[i][j+1];

					//set the left and right children of the parent to the new node objects.
					parent.Left = leftChild;
					parent.Right = rightChild;

					//find data for left child based on previous generation.
					if (j > 0)
					{
						//if these are not the left most children in the generation, then the left
						//child's data is equal to its parent's data + the parent's left sibling's data.
						leftChild.Data = nodes[i-1][parentIndex-1].Data + parent.Data;
					}
					else
					{
						//otherwise the left child's data is equal to the parent's data.
						leftChild.Data = parent.Data;
					}

					//find data for right child based on previous generation.
					if (j < siblings - 2)
					{
						//if these are not the right most children in the generation, then the right
						//child's data is equal to its parent's data + the parent's right sibling's data.
						rightChild.Data = parent.Data + nodes[i-1][parentIndex+1].Data;
					}
					else
					{
						//otherwise the right child's data is equal to the parent's data.
						rightChild.Data = parent.Data;
					}
				}
			}
		}

	}

	/// <summary>
	/// Destroys each node instance in the tree if it exists and sets nodes to null.
	/// </summary>
	void DestroyTree()
	{
		//check to make sure the there is a tree to delete. If not, return.
		if (nodes == null)
			return;

		//for each level in the tree...
		for (int i = nodes.Length; --i >= 0;)
		{
			//for each sibling in the level...
			for (int j = nodes[i].Length; --j >= 0;)
			{
				//destroy every node GameObject.
				GameObject.Destroy(nodes[i][j].gameObject);
			}
			//set the array to reference null after the level has been destroyed.
			nodes[i] = null;
		}
		//and finally, set the array of arrays to reference null when the tree has been destroyed.
		nodes = null;
	}
}
