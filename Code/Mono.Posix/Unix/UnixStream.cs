using System;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000022 RID: 34
	public sealed class UnixStream : Stream, IDisposable
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00007788 File Offset: 0x00005988
		public UnixStream(int fileDescriptor) : this(fileDescriptor, true)
		{
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007794 File Offset: 0x00005994
		public UnixStream(int fileDescriptor, bool ownsHandle)
		{
			if (fileDescriptor == -1)
			{
				throw new ArgumentException(Locale.GetText("Invalid file descriptor"), "fileDescriptor");
			}
			this.fileDescriptor = fileDescriptor;
			this.owner = ownsHandle;
			long num = Syscall.lseek(fileDescriptor, 0L, SeekFlags.SEEK_CUR);
			if (num != -1L)
			{
				this.canSeek = true;
			}
			long num2 = Syscall.read(fileDescriptor, IntPtr.Zero, 0UL);
			if (num2 != -1L)
			{
				this.canRead = true;
			}
			long num3 = Syscall.write(fileDescriptor, IntPtr.Zero, 0UL);
			if (num3 != -1L)
			{
				this.canWrite = true;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007834 File Offset: 0x00005A34
		void IDisposable.Dispose()
		{
			this.AssertNotDisposed();
			if (this.owner)
			{
				this.Close();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007854 File Offset: 0x00005A54
		private void AssertNotDisposed()
		{
			if (this.fileDescriptor == -1)
			{
				throw new ObjectDisposedException("Invalid File Descriptor");
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00007870 File Offset: 0x00005A70
		public int Handle
		{
			get
			{
				return this.fileDescriptor;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007878 File Offset: 0x00005A78
		public override bool CanRead
		{
			get
			{
				return this.canRead;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00007880 File Offset: 0x00005A80
		public override bool CanSeek
		{
			get
			{
				return this.canSeek;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007888 File Offset: 0x00005A88
		public override bool CanWrite
		{
			get
			{
				return this.canWrite;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00007890 File Offset: 0x00005A90
		public override long Length
		{
			get
			{
				this.AssertNotDisposed();
				if (!this.CanSeek)
				{
					throw new NotSupportedException("File descriptor doesn't support seeking");
				}
				this.RefreshStat();
				return this.stat.st_size;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000078C0 File Offset: 0x00005AC0
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00007908 File Offset: 0x00005B08
		public override long Position
		{
			get
			{
				this.AssertNotDisposed();
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				long num = Syscall.lseek(this.fileDescriptor, 0L, SeekFlags.SEEK_CUR);
				if (num == -1L)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
				return num;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00007914 File Offset: 0x00005B14
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00007928 File Offset: 0x00005B28
		[CLSCompliant(false)]
		public FilePermissions Protection
		{
			get
			{
				this.RefreshStat();
				return this.stat.st_mode;
			}
			set
			{
				value &= ~FilePermissions.S_IFMT;
				int retval = Syscall.fchmod(this.fileDescriptor, value);
				UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00007954 File Offset: 0x00005B54
		public FileTypes FileType
		{
			get
			{
				int protection = (int)this.Protection;
				return (FileTypes)(protection & 61440);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00007970 File Offset: 0x00005B70
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000798C File Offset: 0x00005B8C
		public FileAccessPermissions FileAccessPermissions
		{
			get
			{
				int protection = (int)this.Protection;
				return (FileAccessPermissions)(protection & 511);
			}
			set
			{
				int num = (int)this.Protection;
				num &= -512;
				num |= (int)value;
				this.Protection = (FilePermissions)num;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000079B4 File Offset: 0x00005BB4
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000079D0 File Offset: 0x00005BD0
		public FileSpecialAttributes FileSpecialAttributes
		{
			get
			{
				int protection = (int)this.Protection;
				return (FileSpecialAttributes)(protection & 3584);
			}
			set
			{
				int num = (int)this.Protection;
				num &= -3585;
				num |= (int)value;
				this.Protection = (FilePermissions)num;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000079F8 File Offset: 0x00005BF8
		public UnixUserInfo OwnerUser
		{
			get
			{
				this.RefreshStat();
				return new UnixUserInfo(this.stat.st_uid);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007A10 File Offset: 0x00005C10
		public long OwnerUserId
		{
			get
			{
				this.RefreshStat();
				return (long)((ulong)this.stat.st_uid);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007A24 File Offset: 0x00005C24
		public UnixGroupInfo OwnerGroup
		{
			get
			{
				this.RefreshStat();
				return new UnixGroupInfo((long)((ulong)this.stat.st_gid));
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007A40 File Offset: 0x00005C40
		public long OwnerGroupId
		{
			get
			{
				this.RefreshStat();
				return (long)((ulong)this.stat.st_gid);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007A54 File Offset: 0x00005C54
		private void RefreshStat()
		{
			this.AssertNotDisposed();
			int retval = Syscall.fstat(this.fileDescriptor, out this.stat);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007A80 File Offset: 0x00005C80
		public void AdviseFileAccessPattern(FileAccessPattern pattern, long offset, long len)
		{
			FileHandleOperations.AdviseFileAccessPattern(this.fileDescriptor, pattern, offset, len);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007A90 File Offset: 0x00005C90
		public void AdviseFileAccessPattern(FileAccessPattern pattern)
		{
			this.AdviseFileAccessPattern(pattern, 0L, 0L);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007AA0 File Offset: 0x00005CA0
		public override void Flush()
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007AA4 File Offset: 0x00005CA4
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			if (buffer.Length == 0)
			{
				return 0;
			}
			long num;
			fixed (byte* buf = &buffer[offset])
			{
				do
				{
					num = Syscall.read(this.fileDescriptor, (void*)buf, (ulong)((long)count));
				}
				while (UnixMarshal.ShouldRetrySyscall((int)num));
			}
			if (num == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return (int)num;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007B18 File Offset: 0x00005D18
		private void AssertValidBuffer(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > buffer.Length)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (offset > buffer.Length - count)
			{
				throw new ArgumentException("would overrun buffer");
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007B90 File Offset: 0x00005D90
		public unsafe int ReadAtOffset([In] [Out] byte[] buffer, int offset, int count, long fileOffset)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			if (buffer.Length == 0)
			{
				return 0;
			}
			long num;
			fixed (byte* buf = &buffer[offset])
			{
				do
				{
					num = Syscall.pread(this.fileDescriptor, (void*)buf, (ulong)((long)count), fileOffset);
				}
				while (UnixMarshal.ShouldRetrySyscall((int)num));
			}
			if (num == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return (int)num;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007C08 File Offset: 0x00005E08
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.AssertNotDisposed();
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The File Descriptor does not support seeking");
			}
			SeekFlags whence = SeekFlags.SEEK_CUR;
			switch (origin)
			{
			case SeekOrigin.Begin:
				whence = SeekFlags.SEEK_SET;
				break;
			case SeekOrigin.Current:
				whence = SeekFlags.SEEK_CUR;
				break;
			case SeekOrigin.End:
				whence = SeekFlags.SEEK_END;
				break;
			}
			long num = Syscall.lseek(this.fileDescriptor, offset, whence);
			if (num == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return num;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007C80 File Offset: 0x00005E80
		public override void SetLength(long value)
		{
			this.AssertNotDisposed();
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", "< 0");
			}
			if (!this.CanSeek && !this.CanWrite)
			{
				throw new NotSupportedException("You can't truncating the current file descriptor");
			}
			int num;
			do
			{
				num = Syscall.ftruncate(this.fileDescriptor, value);
			}
			while (UnixMarshal.ShouldRetrySyscall(num));
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007CEC File Offset: 0x00005EEC
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanWrite)
			{
				throw new NotSupportedException("File Descriptor does not support writing");
			}
			if (buffer.Length == 0)
			{
				return;
			}
			long num;
			fixed (byte* buf = &buffer[offset])
			{
				do
				{
					num = Syscall.write(this.fileDescriptor, (void*)buf, (ulong)((long)count));
				}
				while (UnixMarshal.ShouldRetrySyscall((int)num));
			}
			if (num == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007D60 File Offset: 0x00005F60
		public unsafe void WriteAtOffset(byte[] buffer, int offset, int count, long fileOffset)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanWrite)
			{
				throw new NotSupportedException("File Descriptor does not support writing");
			}
			if (buffer.Length == 0)
			{
				return;
			}
			long num;
			fixed (byte* buf = &buffer[offset])
			{
				do
				{
					num = Syscall.pwrite(this.fileDescriptor, (void*)buf, (ulong)((long)count), fileOffset);
				}
				while (UnixMarshal.ShouldRetrySyscall((int)num));
			}
			if (num == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007DD4 File Offset: 0x00005FD4
		public void SendTo(UnixStream output)
		{
			this.SendTo(output, (ulong)output.Length);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007DE4 File Offset: 0x00005FE4
		[CLSCompliant(false)]
		public void SendTo(UnixStream output, ulong count)
		{
			this.SendTo(output.Handle, count);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007DF4 File Offset: 0x00005FF4
		[CLSCompliant(false)]
		public void SendTo(int out_fd, ulong count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Unable to write to the current file descriptor");
			}
			long position = this.Position;
			long num = Syscall.sendfile(out_fd, this.fileDescriptor, ref position, count);
			if (num == -1L)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007E3C File Offset: 0x0000603C
		public void SetOwner(long user, long group)
		{
			this.AssertNotDisposed();
			int retval = Syscall.fchown(this.fileDescriptor, Convert.ToUInt32(user), Convert.ToUInt32(group));
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007E70 File Offset: 0x00006070
		public void SetOwner(string user, string group)
		{
			this.AssertNotDisposed();
			long userId = new UnixUserInfo(user).UserId;
			long groupId = new UnixGroupInfo(group).GroupId;
			this.SetOwner(userId, groupId);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007EA4 File Offset: 0x000060A4
		public void SetOwner(string user)
		{
			this.AssertNotDisposed();
			Passwd passwd = Syscall.getpwnam(user);
			if (passwd == null)
			{
				throw new ArgumentException(Locale.GetText("invalid username"), "user");
			}
			long user2 = (long)((ulong)passwd.pw_uid);
			long group = (long)((ulong)passwd.pw_gid);
			this.SetOwner(user2, group);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007EF8 File Offset: 0x000060F8
		[CLSCompliant(false)]
		public long GetConfigurationValue(PathconfName name)
		{
			this.AssertNotDisposed();
			long num = Syscall.fpathconf(this.fileDescriptor, name);
			if (num == -1L && Stdlib.GetLastError() != (Errno)0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return num;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007F30 File Offset: 0x00006130
		~UnixStream()
		{
			this.Close();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007F6C File Offset: 0x0000616C
		public override void Close()
		{
			if (this.fileDescriptor == -1)
			{
				return;
			}
			this.Flush();
			if (!this.owner)
			{
				return;
			}
			int num;
			do
			{
				num = Syscall.close(this.fileDescriptor);
			}
			while (UnixMarshal.ShouldRetrySyscall(num));
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
			this.fileDescriptor = -1;
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000080 RID: 128
		public const int InvalidFileDescriptor = -1;

		// Token: 0x04000081 RID: 129
		public const int StandardInputFileDescriptor = 0;

		// Token: 0x04000082 RID: 130
		public const int StandardOutputFileDescriptor = 1;

		// Token: 0x04000083 RID: 131
		public const int StandardErrorFileDescriptor = 2;

		// Token: 0x04000084 RID: 132
		private bool canSeek;

		// Token: 0x04000085 RID: 133
		private bool canRead;

		// Token: 0x04000086 RID: 134
		private bool canWrite;

		// Token: 0x04000087 RID: 135
		private bool owner = true;

		// Token: 0x04000088 RID: 136
		private int fileDescriptor = -1;

		// Token: 0x04000089 RID: 137
		private Stat stat;
	}
}
