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

		// # 1268. Search Suggestions System
		// Time: O(m) - m: number of total chars in products
		// Space: O(n) - n: number of nodes in trie
		public IList<IList<string>> SuggestedProducts(string[] products, string searchWord) {
			// Build Trie
			var trie = new SuggestionTrie();
			foreach (string product in products) {
				trie.Insert(product);
			}

			// Iteratre through searchWord
			var ret = new List<IList<string>>();
			var node = trie.root;
			var i = 0;
			while (i < searchWord.Length) {
				var ch = searchWord[i];
				if (!node.ContainsKey(ch)) {
					break;
				}
				node = node.Get(ch);
				ret.Add(node.GetTopSuggestions());
				i++;
			}

			while (i++ < searchWord.Length) { // Fill rest for prefixs not found in trie
				ret.Add(new List<string>());
			}

			return ret;
		}

		private class SuggestionTrie {
			public SuggestionTrieNode root;

			public SuggestionTrie() {
				this.root = new SuggestionTrieNode();
			}

			public void Insert(string word) {
				var node = this.root;
				foreach (char ch in word) {
					if (!node.ContainsKey(ch)) {
						node.Insert(ch);
					}
					node = node.Get(ch);
					node.AddSuggestion(word);
				}
				node.IsEnd = true;
			}
		}

		private class SuggestionTrieNode {
			public bool IsEnd = false; // not needed for this problem

			private List<string> topSuggestions;
			private SuggestionTrieNode[] children;

			public SuggestionTrieNode() {
				this.children = new SuggestionTrieNode[26]; // 26 characters
				this.topSuggestions = new List<string>(); // alphabetically sorted string list
			}

			public bool ContainsKey(char ch) {
				return this.children[ch - 'a'] != null;
			}

			public SuggestionTrieNode Get(char ch) {
				return this.children[ch - 'a'];
			}

			public void Insert(char ch) {
				this.children[ch - 'a'] = new SuggestionTrieNode();
			}

			public void AddSuggestion(string word) {
				// Binary search
				int left = 0;
				int right = this.topSuggestions.Count - 1;
				while (left <= right) {
					int mid = (left + right) / 2;
					int comparsion = this.topSuggestions[mid].CompareTo(word);
					if (comparsion == 0) {
						return; // word already exists
					}
					else if (comparsion > 0) {
						right = mid - 1;
					}
					else {
						left = mid + 1;
					}
				}

				this.topSuggestions.Insert(left, word);

				// Keep 3 elements
				if (this.topSuggestions.Count > 3){
					this.topSuggestions.RemoveAt(this.topSuggestions.Count - 1);
				}
			}

			public List<string> GetTopSuggestions() {
				return this.topSuggestions;
			}
		}
	}
}
