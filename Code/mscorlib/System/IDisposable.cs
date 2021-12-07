using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Defines a method to release allocated resources.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000019 RID: 25
	[ComVisible(true)]
	public interface IDisposable
	{
		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000173 RID: 371
		void Dispose();
	}
}
