using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice.Leetcode.Hash_Map.Custom
{
	public class SparseVector
	{
		// #1570. Dot Product of Two Sparse Vectors
		// Time: O(n)
		// Space: O(m) - m: number of non zero elements
		public Dictionary<int, int> Hash
		{
			get; private set;
		}
		public SparseVector(int[] nums)
		{
			Hash = new();
			for (int i = 0; i < nums.Length; i++)
			{
				if (nums[i] != 0)
				{
					Hash[i] = nums[i];
				}
			}
		}

		// Return the dotProduct of two sparse vectors
		// Time: O(Min(m, l)) - l: number of non zero elements in vec
		// Spce: O(1)
		public int DotProduct(SparseVector vec)
		{
			var keys = Hash.Keys.Count < vec.Hash.Keys.Count ? Hash.Keys : vec.Hash.Keys;
			int res = 0;
			foreach (int key in keys)
			{
				if (Hash.ContainsKey(key) && vec.Hash.ContainsKey(key))
				{
					res += Hash[key] * vec.Hash[key];
				}
			}
			return res;
		}
	}
}
