namespace CodingPractice.Tests.Concepts;
using Xunit;
using CodingPractice;

public class SortAndSearchTests
{
	[Fact]
	public void SortedMerge_BothArraysEmpty_NoChange()
	{
		// Arrange
		int[] A = [];
		int[] B = [];

		// Act
		SortAndSearch.SortedMerge(A, B);

		// Assert
		Assert.Empty(A);
	}

	[Fact]
	public void SortedMerge_BIsEmpty_NoChange()
	{
		// Arrange
		int[] A = [1];
		int[] B = [];

		// Act
		SortAndSearch.SortedMerge(A, B);

		// Assert
		Assert.Single(A);
		Assert.Equal(1, A[0]);
	}

	[Fact]
	public void SortedMerge_BHasSingleElement_AIsSorted()
	{
		// Arrange
		int[] A = [1, 0]; // Assuming 0 is placeholder for B's element
		int[] B = [2];

		// Act
		SortAndSearch.SortedMerge(A, B);

		// Assert
		Assert.Equal(2, A.Length);
		Assert.Equal(1, A[0]);
		Assert.Equal(2, A[1]);
	}

	[Fact]
	public void SortedMerge_BHasMultipleElements_AIsSorted()
	{
		// Arrange
		int[] A = [1, 3, 5, 0, 0]; // Assuming 0s are placeholders for B's elements
		int[] B = [2, 4];

		// Act
		SortAndSearch.SortedMerge(A, B);

		// Assert
		Assert.Equal(5, A.Length);
		Assert.Equal(1, A[0]);
		Assert.Equal(2, A[1]);
		Assert.Equal(3, A[2]);
		Assert.Equal(4, A[3]);
		Assert.Equal(5, A[4]);
	}

	[Fact]
	public void SortedMerge_BHasMoreElements_AIsSorted()
	{
		// Arrange
		int[] A = [1, 3, 5, 0, 0, 0, 0]; // Assuming 0s are placeholders for B's elements
		int[] B = [2, 4, 6, 7];

		// Act
		SortAndSearch.SortedMerge(A, B);

		// Assert
		Assert.Equal(7, A.Length);
		Assert.Equal(1, A[0]);
		Assert.Equal(2, A[1]);
		Assert.Equal(3, A[2]);
		Assert.Equal(4, A[3]);
		Assert.Equal(5, A[4]);
		Assert.Equal(6, A[5]);
		Assert.Equal(7, A[6]);
	}
}
