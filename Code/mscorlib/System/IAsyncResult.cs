using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	/// <summary>Represents the status of an asynchronous operation. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005C RID: 92
	[ComVisible(true)]
	public interface IAsyncResult
	{
		/// <summary>Gets a user-defined object that qualifies or contains information about an asynchronous operation.</summary>
		/// <returns>A user-defined object that qualifies or contains information about an asynchronous operation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000693 RID: 1683
		object AsyncState { get; }

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000694 RID: 1684
		WaitHandle AsyncWaitHandle { get; }

		/// <summary>Gets a value that indicates whether the asynchronous operation completed synchronously.</summary>
		/// <returns>true if the asynchronous operation completed synchronously; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000695 RID: 1685
		bool CompletedSynchronously { get; }

		/// <summary>Gets a value that indicates whether the asynchronous operation has completed.</summary>
		/// <returns>true if the operation is complete; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000696 RID: 1686
		bool IsCompleted { get; }
	}
}
