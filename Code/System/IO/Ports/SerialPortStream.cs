using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x0200029E RID: 670
	internal class SerialPortStream : Stream, IDisposable, ISerialStream
	{
		// Token: 0x06001753 RID: 5971 RVA: 0x00040440 File Offset: 0x0003E640
		public SerialPortStream(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits, bool dtrEnable, bool rtsEnable, Handshake handshake, int readTimeout, int writeTimeout, int readBufferSize, int writeBufferSize)
		{
			this.fd = SerialPortStream.open_serial(portName);
			if (this.fd == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			if (!SerialPortStream.set_attributes(this.fd, baudRate, parity, dataBits, stopBits, handshake))
			{
				SerialPortStream.ThrowIOException();
			}
			this.read_timeout = readTimeout;
			this.write_timeout = writeTimeout;
			this.SetSignal(SerialSignal.Dtr, dtrEnable);
			if (handshake != Handshake.RequestToSend && handshake != Handshake.RequestToSendXOnXOff)
			{
				this.SetSignal(SerialSignal.Rts, rtsEnable);
			}
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000404C0 File Offset: 0x0003E6C0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001755 RID: 5973
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int open_serial(string portName);

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x000404D0 File Offset: 0x0003E6D0
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x000404D4 File Offset: 0x0003E6D4
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x000404D8 File Offset: 0x0003E6D8
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x000404DC File Offset: 0x0003E6DC
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x000404E0 File Offset: 0x0003E6E0
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x000404E8 File Offset: 0x0003E6E8
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
				this.read_timeout = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00040518 File Offset: 0x0003E718
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00040520 File Offset: 0x0003E720
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
				this.write_timeout = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00040550 File Offset: 0x0003E750
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x00040558 File Offset: 0x0003E758
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x00040560 File Offset: 0x0003E760
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

		// Token: 0x06001761 RID: 5985 RVA: 0x00040568 File Offset: 0x0003E768
		public override void Flush()
		{
		}

		// Token: 0x06001762 RID: 5986
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int read_serial(int fd, byte[] buffer, int offset, int count);

		// Token: 0x06001763 RID: 5987
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern bool poll_serial(int fd, out int error, int timeout);

		// Token: 0x06001764 RID: 5988 RVA: 0x0004056C File Offset: 0x0003E76C
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
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
			bool flag = SerialPortStream.poll_serial(this.fd, out num, this.read_timeout);
			if (num == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			if (!flag)
			{
				throw new TimeoutException();
			}
			return SerialPortStream.read_serial(this.fd, buffer, offset, count);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00040600 File Offset: 0x0003E800
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00040608 File Offset: 0x0003E808
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001767 RID: 5991
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int write_serial(int fd, byte[] buffer, int offset, int count, int timeout);

		// Token: 0x06001768 RID: 5992 RVA: 0x00040610 File Offset: 0x0003E810
		public override void Write(byte[] buffer, int offset, int count)
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
			if (SerialPortStream.write_serial(this.fd, buffer, offset, count, this.write_timeout) < 0)
			{
				throw new TimeoutException("The operation has timed-out");
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00040688 File Offset: 0x0003E888
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (SerialPortStream.close_serial(this.fd) != 0)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x0600176A RID: 5994
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int close_serial(int fd);

		// Token: 0x0600176B RID: 5995 RVA: 0x000406C0 File Offset: 0x0003E8C0
		public override void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x000406C8 File Offset: 0x0003E8C8
		~SerialPortStream()
		{
			this.Dispose(false);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00040704 File Offset: 0x0003E904
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0600176E RID: 5998
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern bool set_attributes(int fd, int baudRate, Parity parity, int dataBits, StopBits stopBits, Handshake handshake);

		// Token: 0x0600176F RID: 5999 RVA: 0x00040724 File Offset: 0x0003E924
		public void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits sb, Handshake hs)
		{
			if (!SerialPortStream.set_attributes(this.fd, baud_rate, parity, data_bits, sb, hs))
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x06001770 RID: 6000
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int get_bytes_in_buffer(int fd, int input);

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00040744 File Offset: 0x0003E944
		public int BytesToRead
		{
			get
			{
				return SerialPortStream.get_bytes_in_buffer(this.fd, 1);
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x00040754 File Offset: 0x0003E954
		public int BytesToWrite
		{
			get
			{
				return SerialPortStream.get_bytes_in_buffer(this.fd, 0);
			}
		}

		// Token: 0x06001773 RID: 6003
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern void discard_buffer(int fd, bool inputBuffer);

		// Token: 0x06001774 RID: 6004 RVA: 0x00040764 File Offset: 0x0003E964
		public void DiscardInBuffer()
		{
			SerialPortStream.discard_buffer(this.fd, true);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00040774 File Offset: 0x0003E974
		public void DiscardOutBuffer()
		{
			SerialPortStream.discard_buffer(this.fd, false);
		}

		// Token: 0x06001776 RID: 6006
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern SerialSignal get_signals(int fd, out int error);

		// Token: 0x06001777 RID: 6007 RVA: 0x00040784 File Offset: 0x0003E984
		public SerialSignal GetSignals()
		{
			int num;
			SerialSignal result = SerialPortStream.get_signals(this.fd, out num);
			if (num == -1)
			{
				SerialPortStream.ThrowIOException();
			}
			return result;
		}

		// Token: 0x06001778 RID: 6008
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int set_signal(int fd, SerialSignal signal, bool value);

		// Token: 0x06001779 RID: 6009 RVA: 0x000407AC File Offset: 0x0003E9AC
		public void SetSignal(SerialSignal signal, bool value)
		{
			if (signal < SerialSignal.Cd || signal > SerialSignal.Rts || signal == SerialSignal.Cd || signal == SerialSignal.Cts || signal == SerialSignal.Dsr)
			{
				throw new Exception("Invalid internal value");
			}
			if (SerialPortStream.set_signal(this.fd, signal, value) == -1)
			{
				SerialPortStream.ThrowIOException();
			}
		}

		// Token: 0x0600177A RID: 6010
		[DllImport("MonoPosixHelper", SetLastError = true)]
		private static extern int breakprop(int fd);

		// Token: 0x0600177B RID: 6011 RVA: 0x00040800 File Offset: 0x0003EA00
		public void SetBreakState(bool value)
		{
			if (value)
			{
				SerialPortStream.breakprop(this.fd);
			}
		}

		// Token: 0x0600177C RID: 6012
		[DllImport("libc")]
		private static extern IntPtr strerror(int errnum);

		// Token: 0x0600177D RID: 6013 RVA: 0x00040814 File Offset: 0x0003EA14
		private static void ThrowIOException()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			string message = Marshal.PtrToStringAnsi(SerialPortStream.strerror(lastWin32Error));
			throw new IOException(message);
		}

		// Token: 0x04000EBF RID: 3775
		private int fd;

		// Token: 0x04000EC0 RID: 3776
		private int read_timeout;

		// Token: 0x04000EC1 RID: 3777
		private int write_timeout;

		// Token: 0x04000EC2 RID: 3778
		private bool disposed;
	}
}
