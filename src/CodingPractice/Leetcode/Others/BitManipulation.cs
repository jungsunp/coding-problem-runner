using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice.Leetcode
{
	/*
	Bit wise operators
	- ~ : Not (e.x 010 => 101)
	- &
	- |
	- ^ : XOR
	- >> or << : right or left shift
		- e.x) 3 << 2 (i.e 101 => 10100)

	*/
	public class BitManipulation
	{
		// #338. Counting Bits
		// Time: O(n)
		// Space: O(1)
		public int[] CountBits(int n) {
			int[] ret = new int[n + 1];

			int i = 1;
			while (i <= n) {
				ret[i] = 1;

				for (int j = 1; j < i && i + j <= n; j++) {
					ret[i + j] = ret[j] + 1;
				}

				i *= 2;
			}

			return ret;
		}

		// #136. Single Number
		// Time: O(n)
		// Space: O(1)
		public int SingleNumber(int[] nums) {
			int ret = nums[0];
			for(int i = 1; i < nums.Length; i++) {
				ret = ret ^ nums[i]; // use XOR operation
			}
			return ret;
		}

		// #1318. Minimum Flips to Make a OR b Equal to c
		// Time: O(log n)
		// Space: O(1)
		public int MinFlips(int a, int b, int c) {
			int flip = 0;
			while (a > 0 || b > 0 || c > 0) {
				int bitA = a % 2;
				int bitB = b % 2;
				int bitC = c % 2;

				if ((bitA | bitB) != bitC) {
					if (bitC == 1) {
						flip++;
					}
					else { // bitC == 0
						if (bitA == bitB) { // both 1
							flip += 2;
						}
						else {
							flip++;
						}
					}
				}

				a = a / 2; // we can also do a >>= 1;
				b = b / 2;
				c = c / 2;
			}

			return flip;
		}
	}
}
