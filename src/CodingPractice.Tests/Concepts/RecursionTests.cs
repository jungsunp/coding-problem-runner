using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CodingPractice.Concepts;

namespace CodingPractice.Tests.Concepts
{
	public class RecursionTests
	{

		[Theory]
		[InlineData(0, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(3, 4)]
		[InlineData(4, 7)]
		[InlineData(5, 13)]
		[InlineData(10, 274)] // These values assume a specific implementation of TripleStep
		[InlineData(11, 504)]
		public void TripleStep_ValidSteps_ReturnsExpectedResult(int steps, int expected)
		{
			// Act
			int result = Recursion.TripleStep(steps);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void TripleStep_IntegerOverflow_ThrowsOverflowException()
		{
			// Arrange
			int stepsThatCauseOverflow = 37;

			// Act & Assert
			Assert.Throws<OverflowException>(() => Recursion.TripleStep(stepsThatCauseOverflow));
		}

		[Fact]
		public void MagicIndex_EmptyArray_ReturnsNull()
		{
			// Arrange
			int[] arr = new int[0];

			// Act
			var result = Recursion.MagicIndex(arr);

			// Assert
			Assert.Null(result);
		}

		[Theory]
		[InlineData(new int[] {0}, 0)] // Array with 1 element that is a magic index
		[InlineData(new int[] {-1, 0, 0, 3, 5, 6, 7, 8, 9}, 3)] // Array with 9 elements, magic index at position 3
		[InlineData(new int[] {0, 2, 3, 4, 5, 6, 7, 8, 9, 10}, 0)] // Array with 10 elements, magic index at position 0
		[InlineData(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, null)] // Array with 10 elements, no magic index
		[InlineData(new int[] {-10, -5, 2, 2, 2, 3, 4, 8, 9, 12, 13}, 2)] // Array with duplicates, magic index at position 2
		public void MagicIndex_ValidCases_ReturnsExpectedResult(int[] arr, int? expected)
		{
			// Act
			var result = Recursion.MagicIndex(arr);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
