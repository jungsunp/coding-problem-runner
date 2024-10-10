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
	}
}
