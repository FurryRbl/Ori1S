using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for events that can be handled completely in an event handler. </summary>
	// Token: 0x0200014E RID: 334
	public class HandledEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with a default <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property value of false.</summary>
		// Token: 0x06000C48 RID: 3144 RVA: 0x0002018C File Offset: 0x0001E38C
		public HandledEventArgs()
		{
			this.handled = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with the specified default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</summary>
		/// <param name="defaultHandledValue">The default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</param>
		// Token: 0x06000C49 RID: 3145 RVA: 0x0002019C File Offset: 0x0001E39C
		public HandledEventArgs(bool defaultHandledValue)
		{
			this.handled = defaultHandledValue;
		}

		/// <summary>Gets or sets a value that indicates whether the event handler has completely handled the event or whether the system should continue its own processing.</summary>
		/// <returns>true if the event has been completely handled; otherwise, false.</returns>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x000201AC File Offset: 0x0001E3AC
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x000201B4 File Offset: 0x0001E3B4
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x04000372 RID: 882
		private bool handled;
	}
}
