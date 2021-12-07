using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.IO.Ports
{
	// Token: 0x020002A2 RID: 674
	internal class WinSerialStream : Stream, IDisposable, ISerialStream
	{
		// Token: 0x06001780 RID: 6016 RVA: 0x00040854 File Offset: 0x0003EA54
		public WinSerialStream(string port_name, int baud_rate, int data_bits, Parity parity, StopBits sb, bool dtr_enable, bool rts_enable, Handshake hs, int read_timeout, int write_timeout, int read_buffer_size, int write_buffer_size)
		{
			this.handle = WinSerialStream.CreateFile(port_name, 3221225472U, 0U, 0U, 3U, 1073741824U, 0U);
			if (this.handle == -1)
			{
				this.ReportIOError(port_name);
			}
			this.SetAttributes(baud_rate, parity, data_bits, sb, hs);
			if (!WinSerialStream.PurgeComm(this.handle, 12U) || !WinSerialStream.SetupComm(this.handle, read_buffer_size, write_buffer_size))
			{
				this.ReportIOError(null);
			}
			this.read_timeout = read_timeout;
			this.write_timeout = write_timeout;
			this.timeouts = new Timeouts(read_timeout, write_timeout);
			if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
			{
				this.ReportIOError(null);
			}
			this.SetSignal(SerialSignal.Dtr, dtr_enable);
			if (hs != Handshake.RequestToSend && hs != Handshake.RequestToSendXOnXOff)
			{
				this.SetSignal(SerialSignal.Rts, rts_enable);
			}
			NativeOverlapped nativeOverlapped = default(NativeOverlapped);
			this.write_event = new ManualResetEvent(false);
			nativeOverlapped.EventHandle = this.write_event.Handle;
			this.write_overlapped = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			Marshal.StructureToPtr(nativeOverlapped, this.write_overlapped, true);
			NativeOverlapped nativeOverlapped2 = default(NativeOverlapped);
			this.read_event = new ManualResetEvent(false);
			nativeOverlapped2.EventHandle = this.read_event.Handle;
			this.read_overlapped = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			Marshal.StructureToPtr(nativeOverlapped2, this.read_overlapped, true);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000409D4 File Offset: 0x0003EBD4
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001782 RID: 6018
		[DllImport("kernel32", SetLastError = true)]
		private static extern int CreateFile(string port_name, uint desired_access, uint share_mode, uint security_attrs, uint creation, uint flags, uint template);

		// Token: 0x06001783 RID: 6019
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetupComm(int handle, int read_buffer_size, int write_buffer_size);

		// Token: 0x06001784 RID: 6020
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool PurgeComm(int handle, uint flags);

		// Token: 0x06001785 RID: 6021
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetCommTimeouts(int handle, Timeouts timeouts);

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x000409E4 File Offset: 0x0003EBE4
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x000409E8 File Offset: 0x0003EBE8
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x000409EC File Offset: 0x0003EBEC
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x000409F0 File Offset: 0x0003EBF0
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x000409F4 File Offset: 0x0003EBF4
		// (set) Token: 0x0600178B RID: 6027 RVA: 0x000409FC File Offset: 0x0003EBFC
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeouts.SetValues(value, this.write_timeout);
				if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
				{
					this.ReportIOError(null);
				}
				this.read_timeout = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x00040A58 File Offset: 0x0003EC58
		// (set) Token: 0x0600178D RID: 6029 RVA: 0x00040A60 File Offset: 0x0003EC60
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeouts.SetValues(this.read_timeout, value);
				if (!WinSerialStream.SetCommTimeouts(this.handle, this.timeouts))
				{
					this.ReportIOError(null);
				}
				this.write_timeout = value;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x00040ABC File Offset: 0x0003ECBC
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x00040AC4 File Offset: 0x0003ECC4
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x00040ACC File Offset: 0x0003ECCC
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001791 RID: 6033
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool CloseHandle(int handle);

		// Token: 0x06001792 RID: 6034 RVA: 0x00040AD4 File Offset: 0x0003ECD4
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			WinSerialStream.CloseHandle(this.handle);
			Marshal.FreeHGlobal(this.write_overlapped);
			Marshal.FreeHGlobal(this.read_overlapped);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00040B0C File Offset: 0x0003ED0C
		public override void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00040B14 File Offset: 0x0003ED14
		~WinSerialStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00040B50 File Offset: 0x0003ED50
		public override void Flush()
		{
			this.CheckDisposed();
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00040B58 File Offset: 0x0003ED58
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00040B60 File Offset: 0x0003ED60
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001798 RID: 6040
		[DllImport("kernel32", SetLastError = true)]
		private unsafe static extern bool ReadFile(int handle, byte* buffer, int bytes_to_read, out int bytes_read, IntPtr overlapped);

		// Token: 0x06001799 RID: 6041
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetOverlappedResult(int handle, IntPtr overlapped, ref int bytes_transfered, bool wait);

		// Token: 0x0600179A RID: 6042 RVA: 0x00040B68 File Offset: 0x0003ED68
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			int num;
			fixed (byte* ptr = ref (buffer != null && buffer.Length != 0) ? ref buffer[0] : ref *null)
			{
				if (WinSerialStream.ReadFile(this.handle, ptr + offset, count, out num, this.read_overlapped))
				{
					return num;
				}
				if ((long)Marshal.GetLastWin32Error() != 997L)
				{
					this.ReportIOError(null);
				}
				if (!WinSerialStream.GetOverlappedResult(this.handle, this.read_overlapped, ref num, true))
				{
					this.ReportIOError(null);
				}
			}
			if (num == 0)
			{
				throw new TimeoutException();
			}
			return num;
		}

		// Token: 0x0600179B RID: 6043
		[DllImport("kernel32", SetLastError = true)]
		private unsafe static extern bool WriteFile(int handle, byte* buffer, int bytes_to_write, out int bytes_written, IntPtr overlapped);

		// Token: 0x0600179C RID: 6044 RVA: 0x00040C44 File Offset: 0x0003EE44
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			int num = 0;
			fixed (byte* ptr = ref (buffer != null && buffer.Length != 0) ? ref buffer[0] : ref *null)
			{
				if (WinSerialStream.WriteFile(this.handle, ptr + offset, count, out num, this.write_overlapped))
				{
					return;
				}
				if ((long)Marshal.GetLastWin32Error() != 997L)
				{
					this.ReportIOError(null);
				}
				if (!WinSerialStream.GetOverlappedResult(this.handle, this.write_overlapped, ref num, true))
				{
					this.ReportIOError(null);
				}
			}
			if (num < count)
			{
				throw new TimeoutException();
			}
		}

		// Token: 0x0600179D RID: 6045
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetCommState(int handle, [Out] DCB dcb);

		// Token: 0x0600179E RID: 6046
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool SetCommState(int handle, DCB dcb);

		// Token: 0x0600179F RID: 6047 RVA: 0x00040D1C File Offset: 0x0003EF1C
		public void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits bits, Handshake hs)
		{
			DCB dcb = new DCB();
			if (!WinSerialStream.GetCommState(this.handle, dcb))
			{
				this.ReportIOError(null);
			}
			dcb.SetValues(baud_rate, parity, data_bits, bits, hs);
			if (!WinSerialStream.SetCommState(this.handle, dcb))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00040D6C File Offset: 0x0003EF6C
		private void ReportIOError(string optional_arg)
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			int num = lastWin32Error;
			string message;
			if (num != 2 && num != 3)
			{
				if (num != 87)
				{
					message = new System.ComponentModel.Win32Exception().Message;
				}
				else
				{
					message = "Parameter is incorrect.";
				}
			}
			else
			{
				message = "The port `" + optional_arg + "' does not exist.";
			}
			throw new IOException(message);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00040DD4 File Offset: 0x0003EFD4
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00040DF4 File Offset: 0x0003EFF4
		public void DiscardInBuffer()
		{
			if (!WinSerialStream.PurgeComm(this.handle, 4U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00040E10 File Offset: 0x0003F010
		public void DiscardOutBuffer()
		{
			if (!WinSerialStream.PurgeComm(this.handle, 4U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x060017A4 RID: 6052
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool ClearCommError(int handle, out uint errors, out CommStat stat);

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x00040E2C File Offset: 0x0003F02C
		public int BytesToRead
		{
			get
			{
				uint num;
				CommStat commStat;
				if (!WinSerialStream.ClearCommError(this.handle, out num, out commStat))
				{
					this.ReportIOError(null);
				}
				return (int)commStat.BytesIn;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00040E5C File Offset: 0x0003F05C
		public int BytesToWrite
		{
			get
			{
				uint num;
				CommStat commStat;
				if (!WinSerialStream.ClearCommError(this.handle, out num, out commStat))
				{
					this.ReportIOError(null);
				}
				return (int)commStat.BytesOut;
			}
		}

		// Token: 0x060017A7 RID: 6055
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool GetCommModemStatus(int handle, out uint flags);

		// Token: 0x060017A8 RID: 6056 RVA: 0x00040E8C File Offset: 0x0003F08C
		public SerialSignal GetSignals()
		{
			uint num;
			if (!WinSerialStream.GetCommModemStatus(this.handle, out num))
			{
				this.ReportIOError(null);
			}
			SerialSignal serialSignal = SerialSignal.None;
			if ((num & 128U) != 0U)
			{
				serialSignal |= SerialSignal.Cd;
			}
			if ((num & 16U) != 0U)
			{
				serialSignal |= SerialSignal.Cts;
			}
			if ((num & 32U) != 0U)
			{
				serialSignal |= SerialSignal.Dsr;
			}
			return serialSignal;
		}

		// Token: 0x060017A9 RID: 6057
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool EscapeCommFunction(int handle, uint flags);

		// Token: 0x060017AA RID: 6058 RVA: 0x00040EE0 File Offset: 0x0003F0E0
		public void SetSignal(SerialSignal signal, bool value)
		{
			if (signal != SerialSignal.Rts && signal != SerialSignal.Dtr)
			{
				throw new Exception("Wrong internal value");
			}
			uint flags;
			if (signal == SerialSignal.Rts)
			{
				if (value)
				{
					flags = 3U;
				}
				else
				{
					flags = 4U;
				}
			}
			else if (value)
			{
				flags = 5U;
			}
			else
			{
				flags = 6U;
			}
			if (!WinSerialStream.EscapeCommFunction(this.handle, flags))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00040F4C File Offset: 0x0003F14C
		public void SetBreakState(bool value)
		{
			if (!WinSerialStream.EscapeCommFunction(this.handle, (!value) ? 9U : 8U))
			{
				this.ReportIOError(null);
			}
		}

		// Token: 0x04000ED0 RID: 3792
		private const uint GenericRead = 2147483648U;

		// Token: 0x04000ED1 RID: 3793
		private const uint GenericWrite = 1073741824U;

		// Token: 0x04000ED2 RID: 3794
		private const uint OpenExisting = 3U;

		// Token: 0x04000ED3 RID: 3795
		private const uint FileFlagOverlapped = 1073741824U;

		// Token: 0x04000ED4 RID: 3796
		private const uint PurgeRxClear = 4U;

		// Token: 0x04000ED5 RID: 3797
		private const uint PurgeTxClear = 8U;

		// Token: 0x04000ED6 RID: 3798
		private const uint WinInfiniteTimeout = 4294967295U;

		// Token: 0x04000ED7 RID: 3799
		private const uint FileIOPending = 997U;

		// Token: 0x04000ED8 RID: 3800
		private const uint SetRts = 3U;

		// Token: 0x04000ED9 RID: 3801
		private const uint ClearRts = 4U;

		// Token: 0x04000EDA RID: 3802
		private const uint SetDtr = 5U;

		// Token: 0x04000EDB RID: 3803
		private const uint ClearDtr = 6U;

		// Token: 0x04000EDC RID: 3804
		private const uint SetBreak = 8U;

		// Token: 0x04000EDD RID: 3805
		private const uint ClearBreak = 9U;

		// Token: 0x04000EDE RID: 3806
		private const uint CtsOn = 16U;

		// Token: 0x04000EDF RID: 3807
		private const uint DsrOn = 32U;

		// Token: 0x04000EE0 RID: 3808
		private const uint RsldOn = 128U;

		// Token: 0x04000EE1 RID: 3809
		private const uint EvRxChar = 1U;

		// Token: 0x04000EE2 RID: 3810
		private const uint EvCts = 8U;

		// Token: 0x04000EE3 RID: 3811
		private const uint EvDsr = 16U;

		// Token: 0x04000EE4 RID: 3812
		private const uint EvRlsd = 32U;

		// Token: 0x04000EE5 RID: 3813
		private const uint EvBreak = 64U;

		// Token: 0x04000EE6 RID: 3814
		private const uint EvErr = 128U;

		// Token: 0x04000EE7 RID: 3815
		private const uint EvRing = 256U;

		// Token: 0x04000EE8 RID: 3816
		private int handle;

		// Token: 0x04000EE9 RID: 3817
		private int read_timeout;

		// Token: 0x04000EEA RID: 3818
		private int write_timeout;

		// Token: 0x04000EEB RID: 3819
		private bool disposed;

		// Token: 0x04000EEC RID: 3820
		private IntPtr write_overlapped;

		// Token: 0x04000EED RID: 3821
		private IntPtr read_overlapped;

		// Token: 0x04000EEE RID: 3822
		private ManualResetEvent read_event;

		// Token: 0x04000EEF RID: 3823
		private ManualResetEvent write_event;

		// Token: 0x04000EF0 RID: 3824
		private Timeouts timeouts;
	}
}
