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
	}
}
