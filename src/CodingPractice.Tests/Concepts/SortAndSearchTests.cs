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

	[Fact]
	public void SearchInRotatedArray_SingleElement_Found()
	{
		// Arrange
		int[] arr = { 5 };
		int x = 5;

		// Act
		int result = SortAndSearch.SearchInRotatedArray(arr, x);

		// Assert
		Assert.Equal(0, result);
	}

	[Fact]
	public void SearchInRotatedArray_TwoElements_Found()
	{
		// Arrange
		int[] arr = { 2, 1 };
		int x = 1;

		// Act
		int result = SortAndSearch.SearchInRotatedArray(arr, x);

		// Assert
		Assert.Equal(1, result);
	}

	[Fact]
	public void SearchInRotatedArray_FiveUniqueElements_Found()
	{
		// Arrange
		int[] arr = { 3, 4, 5, 1, 2 };
		int x = 4;

		// Act
		int result = SortAndSearch.SearchInRotatedArray(arr, x);

		// Assert
		Assert.Equal(1, result);
	}

	[Fact]
	public void SearchInRotatedArray_EightElementsWithDuplicates_Found()
	{
		// Arrange
		int[] arr = { 4, 4, 5, 5, 6, 1, 2, 4 };
		int x = 6;

		// Act
		int result = SortAndSearch.SearchInRotatedArray(arr, x);

		// Assert
		Assert.Equal(4, result);
	}

	[Fact]
	public void SearchInRotatedArray_EightElementsAllDuplicates_Found()
	{
		// Arrange
		int[] arr = { 4, 6, 4, 4, 4, 4, 4, 4 };
		int x = 6;

		// Act
		int result = SortAndSearch.SearchInRotatedArray(arr, x);

		// Assert
		Assert.Equal(1, result);
	}

	[Fact]
	public void SearchInRotatedArray_EightElementsWithDuplicates_NotFound()
	{
		// Arrange
		int[] arr = { 4, 4, 5, 5, 6, 1, 2, 4 };
		int x = 3;

		// Act & Assert
		var ex = Assert.Throws<Exception>(() => SortAndSearch.SearchInRotatedArray(arr, x));
		Assert.Equal("value not found in arr", ex.Message);
	}
}
