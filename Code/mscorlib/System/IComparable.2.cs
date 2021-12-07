using System;

namespace System
{
	/// <summary>Defines a generalized comparison method that a value type or class implements to create a type-specific comparison method for ordering instances.</summary>
	/// <typeparam name="T">The type of objects to compare.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200000A RID: 10
	public interface IComparable<T>
	{
		/// <summary>Compares the current object with another object of the same type.</summary>
		/// <returns>A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />. </returns>
		/// <param name="other">An object to compare with this object.</param>
		// Token: 0x06000082 RID: 130
		int CompareTo(T other);
	}
}
