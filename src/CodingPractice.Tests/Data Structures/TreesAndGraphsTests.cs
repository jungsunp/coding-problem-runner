namespace CodingPractice.Tests;
using CodingPractice;
using Xunit;

public class TreesAndGraphsTests
{
	[Fact]
	public void RouteExists_DirectRoute_ReturnsTrue()
	{
		// Arrange
		var graph = new Graph(); // Assuming you have a Graph class
		var nodeA = new Node("A");
		var nodeB = new Node("B");
		graph.AddNode(nodeA);
		graph.AddNode(nodeB);
		graph.AddEdge(nodeA, nodeB);

		// Act
		bool result = TreesAndGraphs.RouteBetweenNodes(nodeA, nodeB);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void RouteDoesNotExist_ReturnsFalse()
	{
		// Arrange
		var graph = new Graph();
		var nodeA = new Node("A");
		var nodeC = new Node("C");
		graph.AddNode(nodeA);
		graph.AddNode(nodeC);
		// No edge added between A and C

		// Act
		bool result = TreesAndGraphs.RouteBetweenNodes(nodeA, nodeC);

		// Assert
		Assert.False(result);
	}

	   [Fact]
	public void RouteExists_IndirectRoute_ReturnsTrue()
	{
		// Arrange
		var graph = new Graph();
		var nodeA = new Node("A");
		var nodeB = new Node("B");
		var nodeC = new Node("C");
		graph.AddNode(nodeA);
		graph.AddNode(nodeB);
		graph.AddNode(nodeC);
		graph.AddEdge(nodeA, nodeB);
		graph.AddEdge(nodeB, nodeC);

		// Act
		bool result = TreesAndGraphs.RouteBetweenNodes(nodeA, nodeC);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void EmptyGraph_ReturnsFalse()
	{
		// Arrange
		var graph = new Graph();
		var nodeA = new Node("A");
		var nodeC = new Node("C");

		// Act
		bool result = TreesAndGraphs.RouteBetweenNodes(nodeA, nodeC);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void NodesAreNull_ReturnsFalse()
	{
		// Arrange
		Node nodeA = null;
		Node nodeC = null;

		// Act
		bool result = TreesAndGraphs.RouteBetweenNodes(nodeA, nodeC);

		// Assert
		Assert.False(result);
	}

	   [Fact]
	public void MinimalTree_WithEmptyArray_ReturnsNull()
	{
		// Arrange
		int[] array = new int[0];

		// Act
		var result = TreesAndGraphs.MinimalTree(array);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void MinimalTree_WithSingleElementArray_CreatesCorrectTree()
	{
		// Arrange
		int[] array = [ 1 ];

		// Act
		var result = TreesAndGraphs.MinimalTree(array);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(1, result.Root.Value); // Assuming the tree nodes have a Value property
		Assert.Null(result.Root.Left);
		Assert.Null(result.Root.Right);
	}

	[Fact]
	public void MinimalTree_WithOddElements_CreatesBalancedTree()
	{
		// Arrange
		int[] array = [ 1, 2, 3, 4, 5, 6, 7 ];

		// Act
		var result = TreesAndGraphs.MinimalTree(array);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(4, result.Root.Value);
		Assert.Equal(2, result.Root.Left.Value);
		Assert.Equal(6, result.Root.Right.Value);
		Assert.Equal(1, result.Root.Left.Left.Value);
		Assert.Equal(3, result.Root.Left.Right.Value);
		Assert.Equal(5, result.Root.Right.Left.Value);
		Assert.Equal(7, result.Root.Right.Right.Value);
	}

	[Fact]
	public void MinimalTree_WithEvenElements_CreatesBalancedTree()
	{
		// Arrange
		int[] array = [ 1, 2, 3, 4, 5, 6, 7, 8 ];

		// Act
		var result = TreesAndGraphs.MinimalTree(array);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(5, result.Root.Value);
		Assert.Equal(3, result.Root.Left.Value);
		Assert.Equal(7, result.Root.Right.Value);
		Assert.Equal(2, result.Root.Left.Left.Value);
		Assert.Equal(4, result.Root.Left.Right.Value);
		Assert.Equal(6, result.Root.Right.Left.Value);
		Assert.Equal(8, result.Root.Right.Right.Value);
		Assert.Equal(1, result.Root.Left.Left.Left.Value);
	}

	[Fact]
	public void CheckBalanced_EmptyTree_ReturnsTrue()
	{
		// Arrange
		BinaryTree<int>? tree = null;

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() => TreesAndGraphs.CheckBalanced(tree));

	}

	[Fact]
	public void CheckBalanced_SingleNode_ReturnsTrue()
	{
		// Arrange
		BinaryTree<int> tree = new BinaryTree<int>(1);

		// Act
		bool result = TreesAndGraphs.CheckBalanced(tree);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void CheckBalanced_SameSubTreeHeight_ReturnsTrue()
	{
		// Arrange
		BinaryTree<int> tree = new BinaryTree<int>(1);
		tree.Root.Left = new BinaryTreeNode<int>(2);
		tree.Root.Right = new BinaryTreeNode<int>(3);

		// Act
		bool result = TreesAndGraphs.CheckBalanced(tree);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void CheckBalanced_OneOffSubTreeHeight_ReturnsTrue()
	{
		// Arrange
		BinaryTree<int> tree = new BinaryTree<int>(1);
		tree.Root.Left = new BinaryTreeNode<int>(2);
		tree.Root.Right = new BinaryTreeNode<int>(3);
		tree.Root.Left.Left = new BinaryTreeNode<int>(4);

		// Act
		bool result = TreesAndGraphs.CheckBalanced(tree);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void CheckBalanced_UnbalancedTree_ReturnsFalse()
	{
		// Arrange
		BinaryTree<int> tree = new BinaryTree<int>(1);
		tree.Root.Left = new BinaryTreeNode<int>(2);
		tree.Root.Right = new BinaryTreeNode<int>(3);
		tree.Root.Right.Right = new BinaryTreeNode<int>(4);
		tree.Root.Right.Right.Right = new BinaryTreeNode<int>(5);

		// Act
		bool result = TreesAndGraphs.CheckBalanced(tree);

		// Assert
		Assert.False(result);
	}
}
