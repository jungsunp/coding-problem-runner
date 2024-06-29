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

		// #2.2
		// Time: O(k)
		// Space: O(1)
		// Assumption: Last is not known. k <= list length
		public static int? KthToLast(LinkedList<int> list, int k)
		{
			var p1 = list.First;
			var p2 = list.First;
			for (int i = 0; i < k; i++)
			{
				p2 = p2.Next;
			}

			while (p2 != null)
			{
				p1 = p1.Next;
				p2 = p2.Next;
			}

			return p1?.Value;
		}

		// #2.7
		// Time: O(m + n)
		// Space: O(1)
		public static CustomLinkedListNode<int> Intersection(CustomLinkedListNode<int> n1, CustomLinkedListNode<int> n2)
		{
			// Get length for linked list 1
			int listLength1 = 0;
			var tail1 = n1;
			while (tail1 != null)
			{
				listLength1++;
				tail1 = tail1.Next;
			}

			// Get length for linked list 2
			int listLength2 = 0;
			var tail2 = n2;
			while (tail2 != null)
			{
				listLength2++;
				tail2 = tail2.Next;
			}

			if (tail1 != tail2)
			{
				return null; // if tails are different, they don't have intersection
			}

			// Move pointer on longer list using the difference
			if (listLength1 < listLength2)
			{
				for (int i = 0; i < listLength2 - listLength1; i++)
				{
					n2 = n2.Next;
				}
			}
			else
			{
				for (int i = 0; i < listLength1 - listLength2; i++)
				{
					n1 = n1.Next;
				}
			}

			while (n1 != null)
			{
				if (n1 == n2)
				{
					return n1;
				}

				n1 = n1.Next;
				n2 = n2.Next;
			}

			return null;
		}

		// #2.8
		// Time: O(n)
		// Space: O(1)
		public static CustomLinkedListNode<int> LoopDetection(CustomLinkedList<int> list)
		{
			var slow = list.Head;
			var fast = list.Head;

			while (fast != null)
			{
				slow = slow.Next; // fast runner moves twice as fast
				fast = fast.Next?.Next;

				if (fast == null)
				{
					return null; // no lopp detected
				}

				if (slow == fast)
				{
					break; // exit loop when 2 pointers collide
				}
			}

			slow = list.Head; // move slow runner to head of list
			while (slow != fast)
			{
				slow = slow.Next; // both runners move at same pace
				fast = fast.Next;
			}

			return slow; // runners will collide at start of loop
		}
	}

	// Custom singly linked list to allow shared node in the list
	public class CustomLinkedListNode<T>
	{
		public T Value { get; set; }
		public CustomLinkedListNode<T> Next { get; set; }

		public CustomLinkedListNode(T value)
		{
			Value = value;
			Next = null;
		}
	}

	public class CustomLinkedList<T>
	{

		public CustomLinkedListNode<T> Head { get; private set; }

		public CustomLinkedListNode<T> AddLast(T value)
		{
			CustomLinkedListNode<T> node;
			if (Head == null)
			{
				node = new CustomLinkedListNode<T>(value);
				Head = node;
			}
			else
			{
				CustomLinkedListNode<T> current = Head;
				while (current.Next != null)
				{
					current = current.Next;
				}
				node = new CustomLinkedListNode<T>(value);
				current.Next = node;
			}

			return node;
		}

		public void AddLast(CustomLinkedListNode<T> node)
		{
			if (Head == null)
			{
				Head = node;
			}
			else
			{
				CustomLinkedListNode<T> current = Head;
				while (current.Next != null)
				{
					current = current.Next;
				}
				current.Next = node;
			}
		}
	}
}
