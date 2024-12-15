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

		// #649. Dota2 Senate
		// Time: O(n)
		// Space: O(n)
		public string PredictPartyVictory(string senate) {

			// Build senatore queue
			Queue<char> senQueue = new Queue<char>();
			foreach (char c in senate) {
				senQueue.Enqueue(c);
			}

			// Take turn and cast vote from the senator queue
			Queue<char> voteQueue = new Queue<char>();
			while(senQueue.Count > 0) {
				if (voteQueue.Count < 1 || voteQueue.Peek() == senQueue.Peek()) {
					// no senate to cast vote or vote from same party
					voteQueue.Enqueue(senQueue.Dequeue());
				}
				else {
					// vote from different party
					senQueue.Dequeue(); // senator lose his rights
					senQueue.Enqueue(voteQueue.Dequeue()); // senator who casted vote goes back to the queue
				}
			}

			return voteQueue.Peek() == 'R' ? "Radiant" : "Dire";
		}
	}
}
