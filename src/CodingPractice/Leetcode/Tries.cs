using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice.Leetcode
{
	public class Tries
	{
		
		// #208. Implement Trie (Prefix Tree)
		public class Trie {
			private readonly TrieNode root;

			public Trie() {
				this.root = new TrieNode();
			}
    
			// Time: O(n)
			// Space: O(n) - for new nodes
			public void Insert(string word) {
				TrieNode node = this.root;

				foreach (char ch in word) {
					if (node.Children.ContainsKey(ch)) {
						node = node.Children[ch];
					}
					else {
						TrieNode childNode = new TrieNode(ch);
						node.Children.Add(ch, childNode);
						node = childNode;
					}
				}

				if (!node.Children.ContainsKey('*')) {
					node.Children.Add('*', new TrieNode('*')); // * marks end of word
				}
			}
    

			// Time: O(n)
			// Space: O(1)
			public bool Search(string word) {
				TrieNode node = this.root;

				foreach (char ch in word) {
					if (!node.Children.ContainsKey(ch)) {
						return false;
					}
					node = node.Children[ch];
				}

				return node.Children.ContainsKey('*');
			}
    
			// Time: O(n)
			// Space: O(1)
			public bool StartsWith(string prefix) {
				TrieNode node = this.root;

				foreach (char ch in prefix) {
					if (!node.Children.ContainsKey(ch)) {
						return false;
					}
					node = node.Children[ch];
				}

				return true;
			}

			private class TrieNode {
				public char? Val { get; }
				public Dictionary<char, TrieNode> Children { get; }

				public TrieNode(char? val = null) {
					this.Val = val;
					this.Children = new Dictionary<char, TrieNode>();
				}
			}
		}

	}
}
