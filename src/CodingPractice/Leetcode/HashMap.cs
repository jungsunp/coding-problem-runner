using System;
using System.Collections.Generic;

namespace CodingPractice.Leetcode
{
	public class HashMap
	{
		// Time: O(m + n)
		// Space: O(m + n)
		public IList<IList<int>> FindDifference(int[] nums1, int[] nums2) {
			var set1 = new HashSet<int>(nums1);
			var set2 = new HashSet<int>(nums2);

			var result1 = new List<int>(set1.Count);
			var result2 = new List<int>(set2.Count);

			foreach (var num in set1) {
				if (!set2.Contains(num)) {
					result1.Add(num);
				}
			}

			foreach (var num in set2) {
				if (!set1.Contains(num)) {
					result2.Add(num);
				}
			}

			return new List<IList<int>> { result1, result2 };
		}
	}
}
