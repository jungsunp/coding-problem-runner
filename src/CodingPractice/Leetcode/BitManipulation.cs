﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice.Leetcode
{
	public class BitManipulation
	{
		// #338. Counting Bits
		// Time: O(n)
		// Space: O(1)
		public int[] CountBits(int n) {
			int[] ret = new int[n + 1];
			ret[0] = 0;

			if (n == 0) { return ret; }
        
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
	}
}