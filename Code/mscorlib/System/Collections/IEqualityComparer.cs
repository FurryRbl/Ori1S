﻿using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Defines methods to support the comparison of objects for equality.</summary>
	// Token: 0x020001C5 RID: 453
	[ComVisible(true)]
	public interface IEqualityComparer
	{
		/// <summary>Determines whether the specified objects are equal.</summary>
		/// <returns>true if the specified objects are equal; otherwise, false.</returns>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="x" /> and <paramref name="y" /> are of different types and neither one can handle comparisons with the other.</exception>
		// Token: 0x06001777 RID: 6007
		bool Equals(object x, object y);

		/// <summary>Returns a hash code for the specified object.</summary>
		/// <returns>A hash code for the specified object.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
		// Token: 0x06001778 RID: 6008
		int GetHashCode(object obj);
	}
}
