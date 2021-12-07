using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event.</summary>
	// Token: 0x02000194 RID: 404
	public class PropertyChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property that changed. </param>
		// Token: 0x06000E1B RID: 3611 RVA: 0x00024588 File Offset: 0x00022788
		public PropertyChangedEventArgs(string name)
		{
			this.propertyName = name;
		}

		/// <summary>Gets the name of the property that changed.</summary>
		/// <returns>The name of the property that changed.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00024598 File Offset: 0x00022798
		public virtual string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04000400 RID: 1024
		private string propertyName;
	}
}
