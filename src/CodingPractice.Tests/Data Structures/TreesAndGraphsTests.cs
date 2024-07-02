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
		Assert.Empty(result.Root.Children);
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
		Assert.Equal(2, result.Root.Children[0].Value);
		Assert.Equal(6, result.Root.Children[1].Value);
		Assert.Equal(1, result.Root.Children[0].Children[0].Value);
		Assert.Equal(3, result.Root.Children[0].Children[1].Value);
		Assert.Equal(5, result.Root.Children[1].Children[0].Value);
		Assert.Equal(7, result.Root.Children[1].Children[1].Value);
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
		Assert.Equal(3, result.Root.Children[0].Value);
		Assert.Equal(7, result.Root.Children[1].Value);
		Assert.Equal(2, result.Root.Children[0].Children[0].Value);
		Assert.Equal(4, result.Root.Children[0].Children[1].Value);
		Assert.Equal(6, result.Root.Children[1].Children[0].Value);
		Assert.Equal(8, result.Root.Children[1].Children[1].Value);
		Assert.Equal(1, result.Root.Children[0].Children[0].Children[0].Value);
	}
}
