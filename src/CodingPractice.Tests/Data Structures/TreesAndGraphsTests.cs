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
}
