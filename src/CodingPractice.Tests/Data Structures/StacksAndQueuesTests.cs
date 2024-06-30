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

	 [Fact]
	public void Push_ItemsAddedCorrectly_CreatesMultipleStacksAsNeeded()
	{
		var setOfStacks = new SetOfStacks<int>(2); // Assume each stack can hold 2 items
		setOfStacks.Push(1);
		setOfStacks.Push(2);
		setOfStacks.Push(3); // This should create a new stack
		setOfStacks.Push(4);

		Assert.Equal(4, setOfStacks.Pop()); // Check the top item
		Assert.Equal(2, setOfStacks.Count); // Check the number of stacks
	}

	[Fact]
	public void Pop_ItemsPoppedCorrectly_RemovesStacksAsTheyEmpty()
	{
		var setOfStacks = new SetOfStacks<int>(3);
		setOfStacks.Push(1);
		setOfStacks.Push(2);
		setOfStacks.Push(3);
		setOfStacks.Push(4); // Adds two items to the second stack

		Assert.Equal(4, setOfStacks.Pop()); // Pop the last item
		Assert.Equal(1, setOfStacks.Count); // Should be back to 1 stack after popping enough items
	}

	[Fact]
	public void PopAt_SpecificStackItemsPoppedCorrectly_HandlesStacksIndependently()
	{
		var setOfStacks = new SetOfStacks<int>(2);
		setOfStacks.Push(1);
		setOfStacks.Push(2);
		setOfStacks.Push(3);
		setOfStacks.Push(4);
		setOfStacks.Push(5);
		setOfStacks.Push(6);
		setOfStacks.Push(7);

		Assert.Equal(4, setOfStacks.Count);
		Assert.Equal(4, setOfStacks.PopAt(1)); // Stacks are 0-indexed, pop from the second stack
		Assert.Equal(3, setOfStacks.Count);
		Assert.Equal(7, setOfStacks.Pop());
		Assert.Equal(3, setOfStacks.Count);
	}
}
