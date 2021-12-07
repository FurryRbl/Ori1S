using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanging.PropertyChanging" /> event. </summary>
	// Token: 0x02000195 RID: 405
	public class PropertyChangingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangingEventArgs" /> class. </summary>
		/// <param name="propertyName">The name of the property whose value is changing.</param>
		// Token: 0x06000E1D RID: 3613 RVA: 0x000245A0 File Offset: 0x000227A0
		public PropertyChangingEventArgs(string propertyName)
		{
			this.name = propertyName;
		}

		/// <summary>Gets the name of the property whose value is changing.</summary>
		/// <returns>The name of the property whose value is changing.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x000245B0 File Offset: 0x000227B0
		public virtual string PropertyName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000401 RID: 1025
		private string name;
	}
}
