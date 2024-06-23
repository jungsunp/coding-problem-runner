namespace CodingPractice.Tests;
using Xunit;
using Xunit.Abstractions;
using CodingPractice;

public class ArraysAndStringsTests
{
	private readonly ITestOutputHelper outputHelper;

	public ArraysAndStringsTests(ITestOutputHelper outputHelper)
	{
		this.outputHelper = outputHelper;
	}

	[Fact]
	public void IsUnique_WithUniqueChars_ReturnsTrue()
	{
		// Arrange
		var input = "abcde";

		// Act
		var result = ArraysAndStrings.IsUnique(input);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void IsUnique_WithDuplicateChars_ReturnsFalse()
	{
		// Arrange
		var input = "aabcde";

		// Act
		var result = ArraysAndStrings.IsUnique(input);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void IsUnique_OneChar_ReturnsTrue()
	{
		// Arrange
		var input = "a";

		// Act
		var result = ArraysAndStrings.IsUnique(input);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void IsUnique_EmptyStr_ReturnsFalse()
	{
		// Arrange
		var input = "";

		// Act
		var result = ArraysAndStrings.IsUnique(input);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void CheckPermutation_WithPermutation_ReturnsTrue()
	{
		// Arrange
		var str1 = "abc";
		var str2 = "bca";

		// Act
		var result = ArraysAndStrings.CheckPermutation(str1, str2);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void CheckPermutation_WithDifferentLengths_ReturnsFalse()
	{
		// Arrange
		var str1 = "abc";
		var str2 = "abcd";

		// Act
		var result = ArraysAndStrings.CheckPermutation(str1, str2);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void CheckPermutation_WithEmptyStrings_ReturnsTrue()
	{
		// Arrange
		var str1 = "";
		var str2 = "";

		// Act
		var result = ArraysAndStrings.CheckPermutation(str1, str2);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void PalindromePermutation_WithPalindromePermutation_ReturnsTrue()
	{
		// Arrange
		var input = "tact coa"; // Permutation of "taco cat", a palindrome

		// Act
		var result = ArraysAndStrings.PalindromePermutation(input);

		// Assert
		Assert.True(result);
	}

	[Theory]
	[InlineData("abc")]
	[InlineData("abcb")]
	public void PalindromePermutation_WithoutPalindromePermutation_ReturnsFalse(string input)
	{
		// Act
		var result = ArraysAndStrings.PalindromePermutation(input);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void PalindromePermutation_WithEmptyString_ReturnsTrue()
	{
		// Arrange
		var input = "";

		// Act
		var result = ArraysAndStrings.PalindromePermutation(input);

		// Assert
		Assert.True(result);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(3)]
	[InlineData(4)]
	[InlineData(10)]
	public void RotateMatrix_RotatesCorrectly(int size)
	{
		// Arrange
		int[,] input = new int[size, size];
		int[,] expected = new int[size, size];

		// Fill the matrices
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				input[i, j] = i * size + j + 1;
				expected[j, size - i - 1] = i * size + j + 1;
			}
		}

		this.outputHelper.WriteLine("Input Matrix:");
		this.outputHelper.WriteLine(MatrixToString(input));
		this.outputHelper.WriteLine("Expected Matrix:");
		this.outputHelper.WriteLine(MatrixToString(expected));

		// Act
		var result = ArraysAndStrings.RotateMatrix(input);

		this.outputHelper.WriteLine("Result Matrix:");
		this.outputHelper.WriteLine(MatrixToString(result));

		// Assert
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				Assert.True(expected[i, j] == result[i, j], $"Assertion failed at row {i} and column {j}");
			}
		}
	}

	 private string MatrixToString(int[,] matrix)
	{
		int rows = matrix.GetLength(0);
		int cols = matrix.GetLength(1);
		System.Text.StringBuilder sb = new System.Text.StringBuilder();

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				sb.Append(matrix[i, j] + "\t");
			}
			sb.AppendLine();
		}

		return sb.ToString();
	}
}
