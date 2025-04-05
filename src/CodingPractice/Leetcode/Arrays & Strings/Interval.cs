using System;

namespace CodingPractice.Leetcode
{
	public class Interval
	{
		// #435. Non-overlapping Intervals
		// Time: O(n log n)
		// Space: O(log n) - assume quick sort form c#
		public int EraseOverlapIntervals(int[][] intervals) {

			// greedy algorithm (interval scheduling prob)
			Array.Sort(intervals, (int[] a, int[] b) => a[1] - b[1]); // sort by end time

			int count = 0;
			int k = int.MinValue; // most recent end time
			foreach (int[] interval in intervals) {
				if (interval[0] >= k) { // does not overlap
					k = interval[1];
				}
				else { // overlaps
					count++;
				}
			}

			return count;
		}

		// #452. Minimum Number of Arrows to Burst Balloons
		// Time: O(n log n) - quick sort from C#
		// Space: O(log n)
		public int FindMinArrowShots(int[][] points) {
			// Sort points by X-end coordinates
			Array.Sort(points, (int[] a, int[]b) => a[1].CompareTo(b[1])); // Note: watch out for overlfow when doing a[1] - b[1]

			// Perform greedy
			int ret = 0;
			int lastEnd = int.MinValue;
			foreach (int[] point in points) {
				if (lastEnd < point[0] || lastEnd == int.MinValue) { // does not overlap (i.e make a shot)
					ret++;
					lastEnd = point[1];
				}
			}

			return ret;
		}
	}
}
