using System;
using System.Collections.Generic;

namespace CodingPractice
{
	public static class LinkedLists
	{
		// #2.1
		// Time: O(n)
		// Space: O(n)
		public static void RemoveDups(LinkedList<int> list) {

			if (list.Count < 2)
			{
				return;
			}

			var hashSet = new HashSet<int> { list.First.Value };
			var node = list.First.Next;

			while (node != null)
			{
				var nextNode = node.Next;

				if (!hashSet.Contains(node.Value))
				{
					hashSet.Add(node.Value); // keep track of values in hash set
				}
				else
				{
					list.Remove(node);
				}

				node = nextNode;
			}
		}

		// #2.1
		// Time: O(n^2)
		// Space: O(1)
		public static void RemoveDupsNoBuffer(LinkedList<int> list)
		{
			if (list.Count < 2)
			{
				return;
			}

			var nodeOne = list.First;
			var nodeTwo = list.First.Next;

			while (nodeOne != null && nodeTwo != null)
			{
				while (nodeTwo != null)
				{
					if (nodeOne.Value == nodeTwo.Value) {
						var node = nodeTwo.Next;
						list.Remove(nodeTwo);
						nodeTwo = node;
					} else {
						nodeTwo = nodeTwo.Next;
					}
				}

				nodeOne = nodeOne.Next;
				nodeTwo = nodeOne.Next;
			}
		}
	}
}
