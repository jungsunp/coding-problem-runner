namespace CodingPractice.Tests.DataStructure;
using Xunit;
using Xunit.Abstractions;
using CodingPractice;

public class LinkedListsTests
{
	[Theory]
	[InlineData(new int[] {}, new int[] {})] // Empty list
	[InlineData(new int[] {1}, new int[] {1})] // Single node
	[InlineData(new int[] {1, 2, 3, 2, 4}, new int[] {1, 2, 3, 4})] // Five nodes with duplicates
	[InlineData(new int[] {1, 2, 3, 4, 5, 3, 2, 6, 7, 5}, new int[] {1, 2, 3, 4, 5, 6, 7})] // Ten nodes with duplicates
	public void RemoveDups_VariousLists_RemovesDuplicatesAsExpected(int[] input, int[] expected)
	{
		var list = new LinkedList<int>(input);
		LinkedLists.RemoveDups(list);
		Assert.Equal(expected, list);
	}

	[Theory]
	[InlineData(new int[] {}, new int[] {})] // Empty list
	[InlineData(new int[] {1}, new int[] {1})] // Single node
	[InlineData(new int[] {1, 2, 3, 2, 4}, new int[] {1, 2, 3, 4})] // Five nodes with duplicates
	[InlineData(new int[] {1, 2, 3, 4, 5, 3, 2, 6, 7, 5}, new int[] {1, 2, 3, 4, 5, 6, 7})] // Ten nodes with duplicates
	public void RemoveDupsNoBuffer_VariousLists_RemovesDuplicatesAsExpected(int[] input, int[] expected)
	{
		var list = new LinkedList<int>(input);
		LinkedLists.RemoveDupsNoBuffer(list);
		Assert.Equal(expected, list);
	}

	[Theory]
	[InlineData(new int[] {}, 0, null)] // Empty list
	[InlineData(new int[] {1}, 1, 1)] // Single element, last
	[InlineData(new int[] {1, 2}, 1, 2)] // Two elements, last
	[InlineData(new int[] {1, 2}, 2, 1)] // Two elements, second to last
	[InlineData(new int[] {1, 2, 3, 4, 5}, 1, 5)] // Five elements, last
	[InlineData(new int[] {1, 2, 3, 4, 5}, 2, 4)] // Five elements, second to last
	[InlineData(new int[] {1, 2, 3, 4, 5}, 3, 3)] // Five elements, third to last
	[InlineData(new int[] {1, 2, 3, 4, 5}, 4, 2)] // Five elements, fourth to last
	[InlineData(new int[] {1, 2, 3, 4, 5}, 5, 1)] // Five elements, fifth to last (first)
	public void KthToLast_VariousLists_ReturnsCorrectElement(int[] input, int k, int? expected)
	{
		var list = new LinkedList<int>(input);
		var result = LinkedLists.KthToLast(list, k);
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Intersection_WithNullNodes_ReturnsNull()
	{
		// Arrange
		CustomLinkedListNode<int>? node1 = null;
		CustomLinkedListNode<int>? node2 = null;

		// Act
		var result = LinkedLists.Intersection(node1, node2);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Intersection_WithEqualSingleNode_ReturnsNode()
	{
		// Arrange
		var node = new CustomLinkedListNode<int>(1);

		// Act
		var result = LinkedLists.Intersection(node, node);

		// Assert
		Assert.Equal(node, result);
	}

	[Fact]
	public void Intersection_WithDifferentSingleNodes_ReturnsNull()
	{
		// Arrange
		var node1 = new CustomLinkedListNode<int>(1);
		var node2 = new CustomLinkedListNode<int>(2);

		// Act
		var result = LinkedLists.Intersection(node1, node2);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Intersection_WithNoIntersection_ReturnsNull()
	{
		// Arrange
		var list1 = new CustomLinkedList<int>();
		var node1 = list1.AddLast(1);
		list1.AddLast(2);
		var list2 = new CustomLinkedList<int>();
		var node2 = list2.AddLast(3);
		list2.AddLast(4);

		// Act
		var result = LinkedLists.Intersection(node1, node2);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Intersection_SameListLength_WithIntersection_ReturnsNode()
	{
		// Arrange
		var list1 = new CustomLinkedList<int>();
		var list2 = new CustomLinkedList<int>();
		var node1 = list1.AddLast(1);
		var sharedNode = list1.AddLast(2);
		var node2 = list2.AddLast(3);
		list2.AddLast(sharedNode);

		// Act
		var result = LinkedLists.Intersection(node1, node2);

		// Assert
		Assert.Equal(sharedNode, result);
	}

	[Fact]
	public void Intersection_DiffListLength_WithIntersection_ReturnsNode()
	{
		// Arrange
		var list1 = new CustomLinkedList<int>();
		var list2 = new CustomLinkedList<int>();
		var node1 = list1.AddLast(1);
		var sharedNode = list1.AddLast(2);
		sharedNode.Next = new CustomLinkedListNode<int>(3);
		var node2 = list2.AddLast(4);
		list2.AddLast(5);
		list2.AddLast(sharedNode);

		// Act
		var result = LinkedLists.Intersection(node1, node2);

		// Assert
		Assert.Equal(sharedNode, result);
	}

	[Fact]
	public void LoopDetection_EmptyList_ReturnsNull()
	{
		var list = new CustomLinkedList<int>();

		var result = LinkedLists.LoopDetection(list);

		Assert.Null(result);
	}

	[Fact]
	public void LoopDetection_SingleNode_ReturnsNull()
	{
		var list = new CustomLinkedList<int>();
		list.AddLast(1);

		var result = LinkedLists.LoopDetection(list);

		Assert.Null(result);
	}

	[Fact]
	public void LoopDetection_TwoNodes_ReturnsCorrectNode()
	{
		var list = new CustomLinkedList<int>();
		var startLoop = new CustomLinkedListNode<int>(1);
		list.AddLast(startLoop);
		list.AddLast(2);
		list.AddLast(startLoop);

		var result = LinkedLists.LoopDetection(list);

		Assert.Equal(startLoop, result);
	}

	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	[InlineData(4)]
	[InlineData(5)]
	[InlineData(6)]
	[InlineData(7)]
	[InlineData(8)]
	[InlineData(9)]
	[InlineData(10)]
	public void LoopDetection_LoopStartsAtVariousNodes_ReturnsCorrectStartingNode(int loopStartIndex)
	{
		// Create a 10-node linked list
		var list = new CustomLinkedList<int>();
		var nodes = new CustomLinkedListNode<int>[10];
		for (int i = 0; i < 10; i++)
		{
			nodes[i] = new CustomLinkedListNode<int>(i + 1);
			list.AddLast(nodes[i]);
		}

		// Create a loop starting at the specified node index (1-indexed for readability)
		list.AddLast(nodes[loopStartIndex - 1]); // Adjust for 0-indexed array

		var result = LinkedLists.LoopDetection(list);

		Assert.Equal(nodes[loopStartIndex - 1], result);
	}
}
