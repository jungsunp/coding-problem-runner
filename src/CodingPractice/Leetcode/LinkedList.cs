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

		// #206. Reverse Linked List
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
	}
}
