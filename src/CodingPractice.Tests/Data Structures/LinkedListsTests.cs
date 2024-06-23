namespace CodingPractice.Tests;
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
}
