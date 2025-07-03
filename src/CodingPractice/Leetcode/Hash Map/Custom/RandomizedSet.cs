using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice.Leetcode.Hash_Map.Custom
{
	// #380. Insert Delete GetRandom O(1)
	// Time: O(1) for all functions
	// Space: O(n)
	public class RandomizedSet
	{
		private Dictionary<int, int> hash; // map item to index in list
		private List<int> list;
		private Random random;

		public RandomizedSet()
		{
			hash = new();
			list = new();
			random = new();
		}

		public bool Insert(int val)
		{
			if (hash.ContainsKey(val))
			{
				return false;
			}

			list.Add(val);
			hash.Add(val, list.Count - 1);
			return true;
		}

		public bool Remove(int val)
		{
			if (!hash.ContainsKey(val))
			{
				return false;
			}

			// Move the last element to index
			int index = hash[val];
			int lastIndex = list.Count - 1;
			if (index < lastIndex)
			{
				int lastVal = list[lastIndex];
				list[index] = lastVal;
				hash[lastVal] = index;
			}

			// Remove with O(1) from both list and hash
			list.RemoveAt(lastIndex);
			hash.Remove(val);
			return true;
		}

		public int GetRandom()
		{
			int rndNumber = random.Next(0, list.Count);
			return list[rndNumber];
		}
	}
}
