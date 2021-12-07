using System;

namespace System.ComponentModel
{
	/// <summary>Notifies clients that a property value has changed.</summary>
	// Token: 0x02000162 RID: 354
	public interface INotifyPropertyChanged
	{
		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06000CA4 RID: 3236
		// (remove) Token: 0x06000CA5 RID: 3237
		event PropertyChangedEventHandler PropertyChanged;
	}
}
