using System;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.PinChanged" /> event.</summary>
	// Token: 0x0200029C RID: 668
	public class SerialPinChangedEventArgs : EventArgs
	{
		// Token: 0x06001701 RID: 5889 RVA: 0x0003F5C0 File Offset: 0x0003D7C0
		internal SerialPinChangedEventArgs(SerialPinChange eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialPinChange" /> values.</returns>
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x0003F5D0 File Offset: 0x0003D7D0
		public SerialPinChange EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x04000EA3 RID: 3747
		private SerialPinChange eventType;
	}
}
