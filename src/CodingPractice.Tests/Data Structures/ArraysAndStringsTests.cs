namespace CodingPractice.Tests;
using Xunit;
using CodingPractice;

public class ArraysAndStringsTests
{
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
}
