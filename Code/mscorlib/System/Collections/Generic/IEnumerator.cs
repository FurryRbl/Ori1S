using System;

namespace System.Collections.Generic
{
	/// <summary>Supports a simple iteration over a generic collection.</summary>
	/// <typeparam name="T">The type of objects to enumerate.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200001A RID: 26
	public interface IEnumerator<T> : IEnumerator, IDisposable
	{
		/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
		/// <returns>The element in the collection at the current position of the enumerator.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000174 RID: 372
		T Current { get; }
	}
}
