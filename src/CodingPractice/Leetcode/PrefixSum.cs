using System;

namespace CodingPractice.Leetcode
{
	public class PrefixSum {

		// Time: O(n)
		// Space: O(1)
		public int LargestAltitude(int[] gain) {
			int height = 0;
			int max = 0;

			foreach (int diff in gain)
			{
				height += diff;
				max = Math.Max(height, max);
			}

			return max;
		}
	}
}
