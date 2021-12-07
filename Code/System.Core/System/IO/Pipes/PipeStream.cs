using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000077 RID: 119
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"/>\n")]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public abstract class PipeStream : Stream
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x0001978C File Offset: 0x0001798C
		protected PipeStream(PipeDirection direction, int bufferSize) : this(direction, PipeTransmissionMode.Byte, bufferSize)
		{
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00019798 File Offset: 0x00017998
		protected PipeStream(PipeDirection direction, PipeTransmissionMode transmissionMode, int outBufferSize)
		{
			this.direction = direction;
			this.transmission_mode = transmissionMode;
			this.read_trans_mode = transmissionMode;
			if (outBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize must be greater than 0");
			}
			this.buffer_size = outBufferSize;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000197DC File Offset: 0x000179DC
		internal static bool IsWindows
		{
			get
			{
				return Win32Marshal.IsWindows;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000197E4 File Offset: 0x000179E4
		internal Exception ThrowACLException()
		{
			return new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000197F0 File Offset: 0x000179F0
		internal static PipeAccessRights ToAccessRights(PipeDirection direction)
		{
			switch (direction)
			{
			case PipeDirection.In:
				return PipeAccessRights.ReadData;
			case PipeDirection.Out:
				return PipeAccessRights.WriteData;
			case PipeDirection.InOut:
				return PipeAccessRights.ReadData | PipeAccessRights.WriteData;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00019824 File Offset: 0x00017A24
		internal static PipeDirection ToDirection(PipeAccessRights rights)
		{
			bool flag = (rights & PipeAccessRights.ReadData) != (PipeAccessRights)0;
			bool flag2 = (rights & PipeAccessRights.WriteData) != (PipeAccessRights)0;
			if (flag)
			{
				if (flag2)
				{
					return PipeDirection.InOut;
				}
				return PipeDirection.In;
			}
			else
			{
				if (flag2)
				{
					return PipeDirection.Out;
				}
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00019864 File Offset: 0x00017A64
		public override bool CanRead
		{
			get
			{
				return (this.direction & PipeDirection.In) != (PipeDirection)0;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00019874 File Offset: 0x00017A74
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00019878 File Offset: 0x00017A78
		public override bool CanWrite
		{
			get
			{
				return (this.direction & PipeDirection.Out) != (PipeDirection)0;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00019888 File Offset: 0x00017A88
		public virtual int InBufferSize
		{
			get
			{
				return this.buffer_size;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00019890 File Offset: 0x00017A90
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x00019898 File Offset: 0x00017A98
		public bool IsAsync { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000198A4 File Offset: 0x00017AA4
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x000198AC File Offset: 0x00017AAC
		public bool IsConnected { get; protected set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000198B8 File Offset: 0x00017AB8
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00019934 File Offset: 0x00017B34
		internal Stream Stream
		{
			get
			{
				if (!this.IsConnected)
				{
					throw new InvalidOperationException("Pipe is not connected");
				}
				if (this.stream == null)
				{
					this.stream = new FileStream(this.handle.DangerousGetHandle(), (!this.CanRead) ? FileAccess.Write : ((!this.CanWrite) ? FileAccess.Read : FileAccess.ReadWrite), true, this.buffer_size, this.IsAsync);
				}
				return this.stream;
			}
			set
			{
				this.stream = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00019940 File Offset: 0x00017B40
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00019948 File Offset: 0x00017B48
		private protected bool IsHandleExposed { protected get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00019954 File Offset: 0x00017B54
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x0001995C File Offset: 0x00017B5C
		[MonoTODO]
		public bool IsMessageComplete { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00019968 File Offset: 0x00017B68
		[MonoTODO]
		public virtual int OutBufferSize
		{
			get
			{
				return this.buffer_size;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00019970 File Offset: 0x00017B70
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x00019980 File Offset: 0x00017B80
		public virtual PipeTransmissionMode ReadMode
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.read_trans_mode;
			}
			set
			{
				this.CheckPipePropertyOperations();
				this.read_trans_mode = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00019990 File Offset: 0x00017B90
		public SafePipeHandle SafePipeHandle
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.handle;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x000199A0 File Offset: 0x00017BA0
		public virtual PipeTransmissionMode TransmissionMode
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.transmission_mode;
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000199B0 File Offset: 0x00017BB0
		[MonoTODO]
		protected internal virtual void CheckPipePropertyOperations()
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000199B4 File Offset: 0x00017BB4
		[MonoTODO]
		protected internal void CheckReadOperations()
		{
			if (!this.IsConnected)
			{
				throw new InvalidOperationException("Pipe is not connected");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("The pipe stream does not support read operations");
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000199F0 File Offset: 0x00017BF0
		[MonoTODO]
		protected internal void CheckWriteOperations()
		{
			if (!this.IsConnected)
			{
				throw new InvalidOperationException("Pipe us not connected");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The pipe stream does not support write operations");
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00019A2C File Offset: 0x00017C2C
		protected void InitializeHandle(SafePipeHandle handle, bool isExposed, bool isAsync)
		{
			this.handle = handle;
			this.IsHandleExposed = isExposed;
			this.IsAsync = isAsync;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00019A44 File Offset: 0x00017C44
		protected override void Dispose(bool disposing)
		{
			if (this.handle != null && disposing)
			{
				this.handle.Dispose();
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00019A64 File Offset: 0x00017C64
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00019A6C File Offset: 0x00017C6C
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00019A70 File Offset: 0x00017C70
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00019A78 File Offset: 0x00017C78
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00019A80 File Offset: 0x00017C80
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00019A88 File Offset: 0x00017C88
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeSecurity GetAccessControl()
		{
			throw this.ThrowACLException();
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00019A90 File Offset: 0x00017C90
		[MonoNotSupported("ACL is not supported in Mono")]
		public void SetAccessControl(PipeSecurity pipeSecurity)
		{
			throw this.ThrowACLException();
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00019A98 File Offset: 0x00017C98
		public void WaitForPipeDrain()
		{
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00019A9C File Offset: 0x00017C9C
		[MonoTODO]
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckReadOperations();
			return this.Stream.Read(buffer, offset, count);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00019AC0 File Offset: 0x00017CC0
		[MonoTODO]
		public override int ReadByte()
		{
			this.CheckReadOperations();
			return this.Stream.ReadByte();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00019AD4 File Offset: 0x00017CD4
		[MonoTODO]
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckWriteOperations();
			this.Stream.Write(buffer, offset, count);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00019AF8 File Offset: 0x00017CF8
		[MonoTODO]
		public override void WriteByte(byte value)
		{
			this.CheckWriteOperations();
			this.Stream.WriteByte(value);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00019B0C File Offset: 0x00017D0C
		[MonoTODO]
		public override void Flush()
		{
			this.CheckWriteOperations();
			this.Stream.Flush();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00019B20 File Offset: 0x00017D20
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.read_delegate == null)
			{
				this.read_delegate = new Func<byte[], int, int, int>(this.Read);
			}
			return this.read_delegate.BeginInvoke(buffer, offset, count, callback, state);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00019B60 File Offset: 0x00017D60
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.write_delegate == null)
			{
				this.write_delegate = new Action<byte[], int, int>(this.Write);
			}
			return this.write_delegate.BeginInvoke(buffer, offset, count, callback, state);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00019BA0 File Offset: 0x00017DA0
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.read_delegate.EndInvoke(asyncResult);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00019BB0 File Offset: 0x00017DB0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.write_delegate.EndInvoke(asyncResult);
		}

		// Token: 0x04000198 RID: 408
		internal const int DefaultBufferSize = 1024;

		// Token: 0x04000199 RID: 409
		private PipeDirection direction;

		// Token: 0x0400019A RID: 410
		private PipeTransmissionMode transmission_mode;

		// Token: 0x0400019B RID: 411
		private PipeTransmissionMode read_trans_mode;

		// Token: 0x0400019C RID: 412
		private int buffer_size;

		// Token: 0x0400019D RID: 413
		private SafePipeHandle handle;

		// Token: 0x0400019E RID: 414
		private Stream stream;

		// Token: 0x0400019F RID: 415
		private Func<byte[], int, int, int> read_delegate;

		// Token: 0x040001A0 RID: 416
		private Action<byte[], int, int> write_delegate;
	}
}
