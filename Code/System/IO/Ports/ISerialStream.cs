using System;

namespace System.IO.Ports
{
	// Token: 0x02000296 RID: 662
	internal interface ISerialStream : IDisposable
	{
		// Token: 0x060016F0 RID: 5872
		int Read(byte[] buffer, int offset, int count);

		// Token: 0x060016F1 RID: 5873
		void Write(byte[] buffer, int offset, int count);

		// Token: 0x060016F2 RID: 5874
		void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits sb, Handshake hs);

		// Token: 0x060016F3 RID: 5875
		void DiscardInBuffer();

		// Token: 0x060016F4 RID: 5876
		void DiscardOutBuffer();

		// Token: 0x060016F5 RID: 5877
		SerialSignal GetSignals();

		// Token: 0x060016F6 RID: 5878
		void SetSignal(SerialSignal signal, bool value);

		// Token: 0x060016F7 RID: 5879
		void SetBreakState(bool value);

		// Token: 0x060016F8 RID: 5880
		void Close();

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060016F9 RID: 5881
		int BytesToRead { get; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060016FA RID: 5882
		int BytesToWrite { get; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060016FB RID: 5883
		// (set) Token: 0x060016FC RID: 5884
		int ReadTimeout { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060016FD RID: 5885
		// (set) Token: 0x060016FE RID: 5886
		int WriteTimeout { get; set; }
	}
}
