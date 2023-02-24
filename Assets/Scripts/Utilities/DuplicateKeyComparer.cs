using System;
using System.Collections.Generic;

// For SortedList. This doesn't let you remove anything from the list
public class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable {
	public int Compare(TKey x, TKey y) {
		int result = x.CompareTo(y);
		return result == 0 ? 1 : result;
	}
}