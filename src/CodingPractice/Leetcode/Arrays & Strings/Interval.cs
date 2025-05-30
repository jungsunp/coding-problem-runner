using System;
using System.Collections.Generic;

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

		// #56. Merge Intervals
		// Time: O(n log n)
		// Space: O(log n)
		public int[][] Merge(int[][] intervals) {
			// Sort intervals by start value
			Array.Sort(intervals, (itv1, itv2) => itv1[0].CompareTo(itv2[0]));

			// Iterate and build merged intervals
			int index = 0;
			List<int[]> ret = new List<int[]>();
			while (index < intervals.Length) {
				int start = intervals[index][0];
				int end = intervals[index][1];

				while (index + 1 < intervals.Length && end >= intervals[index + 1][0]) {
					end = Math.Max(end, intervals[++index][1]);
				}

				ret.Add([start, end]);
				index++;
			}

			return ret.ToArray();
		}
	}
}
