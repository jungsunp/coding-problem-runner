using System;
using System.Collections.Generic;

namespace CodingPractice.Leetcode
{
	public class Queue
	{
		// #933. Number of Recent Calls
		// Time: O(1) (i.e O(3000))
		// Space: O(1)
		public class RecentCounter {
			private readonly Queue<int> queue;

			public RecentCounter() {
				this.queue = new Queue<int> ();
			}

			public int Ping(int t) {
				this.queue.Enqueue(t);

				// Remove expired pings from Queue
				while (this.queue.Peek() < t - 3000)
				{
					this.queue.Dequeue();
				}

				return this.queue.Count;
			}
		}
	}
}
