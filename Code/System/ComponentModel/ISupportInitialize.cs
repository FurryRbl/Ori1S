using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that this object supports a simple, transacted notification for batch initialization.</summary>
	// Token: 0x0200016E RID: 366
	public interface ISupportInitialize
	{
		/// <summary>Signals the object that initialization is starting.</summary>
		// Token: 0x06000CCF RID: 3279
		void BeginInit();

		/// <summary>Signals the object that initialization is complete.</summary>
		// Token: 0x06000CD0 RID: 3280
		void EndInit();
	}
}
