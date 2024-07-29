using System;

namespace CodingPractice
{
	public static class SortAndSearch
	{
		// # 10.1
		// Time: O(m + n)
		// Space: O(1)
		// Assumes A has large enough buffter at then end ot hold B
		public static void SortedMerge(int[] A, int[] B)
		{
			int indexA = A.Length - B.Length - 1; // Last actual element in A
			int indexB = B.Length - 1;
			int mergedIndex = A.Length - 1;

			// Merge B into A, starting from the end
			while (indexB >= 0)
			{
				if (indexA < 0 || A[indexA] < B[indexB])
				{
					A[mergedIndex] = B[indexB];
					indexB--;
				}
				else
				{
					A[mergedIndex] = A[indexA];
					indexA--;
				}
				mergedIndex--;
			}
		}

		// # 10.3
		// Time: O(log n) - worst case (duplicate values) O(n)
		// Space: O(1)
		public static int SearchInRotatedArray(int[] arr, int x)
		{
			int low = 0;
			int high = arr.Length - 1;
			int mid;

			while (low <= high)
			{
				mid = (low + high) / 2;

				if (arr[mid] ==  x)
				{
					return mid;
				}

				if (arr[low] < arr[mid]) // left half is ordered
				{
					if (arr[low] <= x && x < arr[mid])
					{
						high = mid - 1; // search left
					}
					else
					{
						low = mid + 1; // search right
					}
				}
				else if (arr[low] > arr[mid])
				{
					if (arr[mid] < x && x <= arr[high])
					{
						low = mid + 1; // search right
					}
					else
					{
						high = mid -1; // search left
					}
				}
				else // arr[low] == arr[mid] duplicate values
				{
					if (arr[mid] != arr[high]) // left half must be all equal
					{
						low = mid + 1; // search right half
					}
					else
					{
						low++; // may need to look at all elements to search
						high--;
					}

				}
			}

			throw new Exception("value not found in arr");
		}

		// # 10.5
		// Time: O(log n) - worst O(n)
		// Space: O(log n)
		public static int SparseSearch(string input, string[] sortedArr)
		{
			return SparseSearch(input, sortedArr, 0, sortedArr.Length - 1);
		}

		private static int SparseSearch(string input, string[] sortedArr, int low, int high)
		{
			if (low > high) { return -1; } // not found

			int mid = (low + high) / 2;
			while (mid > 0 && sortedArr[mid] == "")
			{
				mid--;
			}

			if (sortedArr[mid] == input)
			{
				return mid;
			}

			if (sortedArr[mid] == "" || string.Compare(sortedArr[mid], input) < 0)
			{
				// searhch right half
				return SparseSearch(input, sortedArr, (low + high) / 2 + 1, high);
			}
			else
			{
				// searhch left half
				return SparseSearch(input, sortedArr, low, (low + high) / 2 - 1);
			}
		}
	}
}
