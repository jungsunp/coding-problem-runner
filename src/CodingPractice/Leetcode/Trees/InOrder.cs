using CodingPractice.Leetcode.Trees.Custom;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class InOrder
	{
		// #1597. Build Binary Expression Tree From Infix Expression
		// Time: O(n)
		// Space: O(n)
		public BinaryTreeNode<char> ExpTree(string s)
		{
			string postfixStr = ConvertToPostfix(s);

			// Build tree using postfix string
			Stack<BinaryTreeNode<char>> stack = new Stack<BinaryTreeNode<char>>();
			foreach (char c in postfixStr)
			{
				if (Char.IsNumber(c))
				{
					stack.Push(new BinaryTreeNode<char>(c));
				}
				else
				{ // operator
					BinaryTreeNode<char> node = new BinaryTreeNode<char>(c);
					node.Right = stack.Pop();
					node.Left = stack.Pop();
					stack.Push(node);
				}
			}

			return stack.Peek();
		}

		private string ConvertToPostfix(string s)
		{
			StringBuilder postfix = new StringBuilder();
			Stack<char> opStack = new Stack<char>();

			foreach (char c in s)
			{
				if (Char.IsNumber(c))
				{ // operands
					postfix.Append(c);
				}
				else if (c == '(')
				{
					opStack.Push(c);
				}
				else if (c == ')')
				{
					while (opStack.Count > 0 && opStack.Peek() != '(')
					{
						postfix.Append(opStack.Pop());
					}
					opStack.Pop(); // get rid of '(' from stack
				}
				else
				{ // operators
					while (opStack.Count > 0 && OperatorOrder(c) <= OperatorOrder(opStack.Peek()))
					{
						postfix.Append(opStack.Pop());
					}
					opStack.Push(c);
				}
			}

			while (opStack.Count > 0)
			{
				postfix.Append(opStack.Pop()); // put remaining operators
			}

			return postfix.ToString();
		}

		private static int OperatorOrder(char op)
		{
			switch (op)
			{
				case '*':
				case '/':
					return 2;
				case '+':
				case '-':
					return 1;
				default:
					return -1;
			}
		}
	}
}
