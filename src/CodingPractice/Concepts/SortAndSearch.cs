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
	}
}
