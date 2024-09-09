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
	}
}
