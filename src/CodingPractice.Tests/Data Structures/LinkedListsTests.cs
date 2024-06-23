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
}
