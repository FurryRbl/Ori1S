using System;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanging.PropertyChanging" /> event of an <see cref="T:System.ComponentModel.INotifyPropertyChanging" /> interface. </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangingEventArgs" /> that contains the event data.</param>
	// Token: 0x02000506 RID: 1286
	// (Invoke) Token: 0x06002CC4 RID: 11460
	public delegate void PropertyChangingEventHandler(object sender, PropertyChangingEventArgs e);
}
