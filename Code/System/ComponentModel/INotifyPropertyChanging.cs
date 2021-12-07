using System;

namespace System.ComponentModel
{
	/// <summary>Notifies clients that a property value is changing.</summary>
	// Token: 0x02000163 RID: 355
	public interface INotifyPropertyChanging
	{
		/// <summary>Occurs when a property value is changing.</summary>
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000CA6 RID: 3238
		// (remove) Token: 0x06000CA7 RID: 3239
		event PropertyChangingEventHandler PropertyChanging;
	}
}
