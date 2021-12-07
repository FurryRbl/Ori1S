using System;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event.</summary>
	// Token: 0x0200029F RID: 671
	public class SerialDataReceivedEventArgs : EventArgs
	{
		// Token: 0x0600177E RID: 6014 RVA: 0x0004083C File Offset: 0x0003EA3C
		internal SerialDataReceivedEventArgs(SerialData eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialData" /> values.</returns>
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0004084C File Offset: 0x0003EA4C
		public SerialData EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x04000EC3 RID: 3779
		private SerialData eventType;
	}
}
