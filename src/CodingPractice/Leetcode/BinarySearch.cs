using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice.Leetcode
{
	public class BinarySearch
	{
		// #374. Guess Number Higher or Lower
		// Time: O(log n)
		// Space: O(1)
		public int GuessNumber(int n) {
			int left = 1;
			int right = n;

			while (left <= right)
			{
				// int nextGuess = (int)(((long) left + right) / 2); Watch out for int overflow
				int nextGuess = left + (right - left) / 2;
				switch(guess(nextGuess))
				{
					case -1:
						right = nextGuess - 1;
						break;
					case 1:
						left = nextGuess + 1;
						break;
					default: // 0
						return nextGuess;
				}
			}

			throw new Exception("Number not found!");
		}

		private int guess(int guess) {
			throw new NotImplementedException();
		}

		// #2300. Successful Pairs of Spells and Potions
		// Time: O((n + m) log m)
		// Space: O(log m) - space complexity for sorting
		public int[] SuccessfulPairs(int[] spells, int[] potions, long success) {
			int[] pairs = new int[spells.Length];

			// Sort potions (m log m)
			Array.Sort(potions);

			for (int i = 0; i < spells.Length; i++) {

				// Binary Search of each spell
				int left = 0;
				int right = potions.Length - 1;
				int mid = 0;
				bool found = false; // finding lower bound

				while (left <= right) {
					mid = (left + right) / 2;

					if ((long) spells[i] * potions[mid] >= success) {
						if (mid == 0 || (long) spells[i] * potions[mid - 1] < success) {
							found = true;
							break;
						}
						right = mid - 1;
					}
					else {
						left = mid + 1;
					}
				}

				pairs[i] = found ? potions.Length - mid : 0;
			}

			return pairs;
		}
	}
}
