using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice.Leetcode.Others
{
	public class Math
	{

		// #50. Pow(x, n)
		// Time: O(log n)
		// Space: O(1)
		public double MyPow(double x, int n)
		{
			if (x == 1 || n == 0)
				return 1;

			double res = 1;
			long N = n; // TRICK: use long for overflow of integer minvalue
			if (N < 0)
				N = -N;

			while (N > 0)
			{

				if (N % 2 == 1)
				{
					res *= x;
					N--;
				}

				x *= x;
				N /= 2;
			}

			return n > 0 ? res : (1 / res);
		}
	}
}
