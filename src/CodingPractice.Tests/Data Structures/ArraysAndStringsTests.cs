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
}
