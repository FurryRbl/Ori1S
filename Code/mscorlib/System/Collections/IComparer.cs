using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Exposes a method that compares two objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C2 RID: 450
	[ComVisible(true)]
	public interface IComparer
	{
		/// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
		/// <returns>Value Condition Less than zero <paramref name="x" /> is less than <paramref name="y" />. Zero <paramref name="x" /> equals <paramref name="y" />. Greater than zero <paramref name="x" /> is greater than <paramref name="y" />. </returns>
		/// <param name="x">The first object to compare. </param>
		/// <param name="y">The second object to compare. </param>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="x" /> nor <paramref name="y" /> implements the <see cref="T:System.IComparable" /> interface.-or- <paramref name="x" /> and <paramref name="y" /> are of different types and neither one can handle comparisons with the other. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001768 RID: 5992
		int Compare(object x, object y);
	}
}
