using System;

namespace System
{
	/// <summary>Represents the method that compares two objects of the same type.</summary>
	/// <returns>Value Condition Less than 0 <paramref name="x" /> is less than <paramref name="y" />.0 <paramref name="x" /> equals <paramref name="y" />.Greater than 0 <paramref name="x" /> is greater than <paramref name="y" />.</returns>
	/// <param name="x">The first object to compare.</param>
	/// <param name="y">The second object to compare.</param>
	/// <typeparam name="T">The type of the objects to compare.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000701 RID: 1793
	// (Invoke) Token: 0x060043F8 RID: 17400
	public delegate int Comparison<T>(T x, T y);
}
