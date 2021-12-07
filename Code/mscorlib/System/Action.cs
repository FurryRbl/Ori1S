using System;

namespace System
{
	/// <summary>Encapsulates a method that takes a single parameter and does not return a value.</summary>
	/// <param name="obj">The parameter of the method that this delegate encapsulates.</param>
	/// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000700 RID: 1792
	// (Invoke) Token: 0x060043F4 RID: 17396
	public delegate void Action<T>(T obj);
}
