using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class LinkedList
	{
		public class ListNode {
			public int val;
			public ListNode next;
			public ListNode(int val=0, ListNode next=null) {
				this.val = val;
				this.next = next;
			}
		}

		// #206. Reverse Linked List - Recursive
		// Time: O(n)
		// Space: O(n)
		public ListNode ReverseList(ListNode head) {
			if (head == null || head.next == null)
			{
				return head;
			}

			var res = ReverseList(head.next);
			head.next.next = head;
			head.next = null;
			return res;
		}

		// #206. Reverse Linked List - Iterative
		// Time: O(n)
		// Space: O(1)
		public ListNode ReverseListIterative(ListNode head) {
			ListNode curr = head;
			ListNode prev = null;

			while (curr != null) {
				ListNode temp = curr.next;
				curr.next = prev;
				prev = curr;
				curr = temp;
			}

			return prev;
		}


		// #2095. Delete the Middle Node of a Linked List
		// Time: O(n)
		// Space: O(1)
		public ListNode DeleteMiddle(ListNode head) {

			if (head.next == null)
			{
				return null;
			}

			ListNode slow = head;
			ListNode fast = head.next.next;

			while (fast != null && fast.next != null)
			{
				fast = fast.next.next;
				slow = slow.next;
			}

			slow.next = slow.next.next;
			return head;
		}

		// #328. Odd Even Linked List
		// Time: O(n)
		// Space: O(1)
		public ListNode OddEvenList(ListNode head) {
			if (head == null) {
				return null;
			}

			var odd = head;
			var even = head.next;
			var evenHead = even;

			// Iterate and move even nodes to even list
			while (even != null && even.next != null) {
				odd.next = even.next;
				even.next = even.next.next;
				odd = odd.next;
				even = even.next;
			}

			odd.next = evenHead;
			return head;
		}

		// #2130. Maximum Twin Sum of a Linked List
		// Time: O(n)
		// Space: O(1)
		public int PairSum(ListNode head) {

			// Reverse first half of list
			ListNode slow = head;
			ListNode fast = head;
			ListNode prev = null;
			while (fast != null) {
				fast = fast.next.next;

				// Reverse using iterative approach
				ListNode temp = slow.next;
				slow.next = prev;
				prev = slow;
				slow = temp;
			}

			int max = 0;
			while (slow != null) {
				// prev is node from first half reversed list, slow is node from second half
				int sum = prev.val + slow.val;
				max = Math.Max(sum, max);

				prev = prev.next;
				slow = slow.next;
			}

			return max;
		}

		// #138. Copy List with Random Pointer
		// Time: O(n)
		// Space: O(1)
		public class RandomNode {
			public int val;
			public RandomNode next;
			public RandomNode random;

			public RandomNode(int _val) {
				val = _val;
				next = null;
				random = null;
			}
		}

		public RandomNode CopyRandomList(RandomNode head) {
			if (head == null) { return null; }

			// Create an interweaved list with deep copy nodes
			RandomNode current = head;
			while (current != null) {
				RandomNode temp = current.next;
				current.next = new RandomNode(current.val);
				current.next.next = temp;
				current = current.next.next;
			}
			RandomNode copyHead = head.next;

			// First make random connections from the interweaved list
			current = head;
			while (current != null) {
				if (current.random != null) {
					current.next.random = current.random.next;
				}
				current = current.next.next;
			}

			// Fix connections from the interweaved list
			current = head;
			while (current != null) {
				RandomNode temp = current.next.next;
				current.next.next = temp != null ? temp.next : null;
				current.next = temp;
				current = current.next;
			}

			return copyHead;
		}
	}

	// #146. LRU Cache
	public class LRUCache {
		private Dictionary<int, LRUNode> dict;
		private LRUNode head; // tracks least recently used
		private LRUNode tail; // track most recently used
		private int capacity;

		public LRUCache(int capacity) {
			this.dict = new Dictionary<int, LRUNode>();
			this.capacity = capacity;
			this.head = new LRUNode(-1, -1); // dummy nodes
			this.tail = new LRUNode(-1, -1);
			this.head.next = this.tail;
			this.tail.prev = this.head;
		}

		// Time: O(1)
		public int Get(int key) {
			if (!dict.ContainsKey(key)) {
				return -1;
			}

			// move the used node to tail
			LRUNode node = this.dict[key];
			node.prev.next = node.next;
			node.next.prev = node.prev;
			this.tail.prev.next = node;
			node.prev = this.tail.prev;
			node.next = this.tail;
			this.tail.prev = node;

			return this.dict[key].val;
		}

		// Time: O(1)
		public void Put(int key, int value) {
			LRUNode node = new LRUNode(key, value);

			// replace with new node if it already exists
			if (this.dict.ContainsKey(key)) {
				LRUNode nodeToDel = this.dict[key];
				nodeToDel.prev.next = nodeToDel.next;
				nodeToDel.next.prev = nodeToDel.prev;
				this.dict[key] = node;
			}
			// if reached capacity, remove head node (i.e least recently used) and insert new one
			else if (this.dict.Keys.Count == this.capacity) {
				this.dict.Remove(this.head.next.key);
				this.dict.Add(key, node);
				this.head.next = this.head.next.next;
				this.head.next.prev = this.head;
			}
			else {
				this.dict.Add(key, node);
			}

			// Put new node at the end of list (one before dummy node)
			this.tail.prev.next = node;
			node.prev = this.tail.prev;
			node.next = this.tail;
			this.tail.prev = node;
		}

		// doubly linked list node
		private class LRUNode {
			public int key;
			public int val;
			public LRUNode prev;
			public LRUNode next;

			public LRUNode (int key, int val) {
				this.key = key;
				this.val = val;
			}
		}
	}
}
