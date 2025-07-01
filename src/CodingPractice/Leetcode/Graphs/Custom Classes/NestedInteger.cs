using System.Collections.Generic;

namespace CodingPractice.Leetcode
{
	// Needed for #339. Nested List Weight Sum
	public interface NestedInteger
	{
		// @return true if this NestedInteger holds a single integer, rather than a nested list.
		bool IsInteger();

		// @return the single integer that this NestedInteger holds, if it holds a single integer
		// The result is undefined if this NestedInteger holds a nested list
		int GetInteger();

		// Set this NestedInteger to hold a single integer.
		public void SetInteger(int value);

		// Set this NestedInteger to hold a nested list and adds a nested integer to it.
		public void Add(NestedInteger ni);

		// @return the nested list that this NestedInteger holds, if it holds a nested list
		// The result is undefined if this NestedInteger holds a single integer
		IList<NestedInteger> GetList();
	}
}