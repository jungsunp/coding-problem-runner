namespace CodingPractice.Tests;
using Xunit;
using CodingPractice;

public class StacksAndQueuesTests
{
	[Fact]
	public void StackMin_PushItems_MinValueUpdatesCorrectly()
	{
		var stack = new StackMin();
		stack.Push(5);
		Assert.Equal(5, stack.Min());
		stack.Push(6);
		Assert.Equal(5, stack.Min());
		stack.Push(3);
		Assert.Equal(3, stack.Min());
		stack.Push(7);
		Assert.Equal(3, stack.Min());
	}

	[Fact]
	public void StackMin_PopItems_MinValueUpdatesCorrectly()
	{
		var stack = new StackMin();
		stack.Push(5);
		stack.Push(2);
		stack.Push(4);
		stack.Push(1);
		stack.Push(3);

		stack.Pop();
		Assert.Equal(1, stack.Min());
		stack.Pop();
		Assert.Equal(2, stack.Min());
		stack.Pop();
		Assert.Equal(2, stack.Min());
	}

	[Fact]
	public void StackMin_NonEmptyStack_ReturnsCorrectMin()
	{
		var stack = new StackMin();
		stack.Push(2);
		stack.Push(1);
		stack.Push(3);

		Assert.Equal(1, stack.Min());
	}

	[Fact]
	public void StackMin_EmptyStack_ThrowsMaxInt()
	{
		var stack = new StackMin();

		Assert.Equal(int.MaxValue, stack.Min());
	}
}
