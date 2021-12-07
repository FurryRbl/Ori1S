using System;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200000C RID: 12
	public class StdioFileStream : Stream
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000025C8 File Offset: 0x000007C8
		public StdioFileStream(IntPtr fileStream) : this(fileStream, true)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025D4 File Offset: 0x000007D4
		public StdioFileStream(IntPtr fileStream, bool ownsHandle)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			this.InitStream(fileStream, ownsHandle);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002604 File Offset: 0x00000804
		public StdioFileStream(IntPtr fileStream, FileAccess access) : this(fileStream, access, true)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002610 File Offset: 0x00000810
		public StdioFileStream(IntPtr fileStream, FileAccess access, bool ownsHandle)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			this.InitStream(fileStream, ownsHandle);
			this.InitCanReadWrite(access);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000263C File Offset: 0x0000083C
		public StdioFileStream(string path)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			this.InitStream(StdioFileStream.Fopen(path, "rb"), true);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002674 File Offset: 0x00000874
		public StdioFileStream(string path, string mode)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			this.InitStream(StdioFileStream.Fopen(path, mode), true);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026A8 File Offset: 0x000008A8
		public StdioFileStream(string path, FileMode mode)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			this.InitStream(StdioFileStream.Fopen(path, StdioFileStream.ToFopenMode(path, mode)), true);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026E4 File Offset: 0x000008E4
		public StdioFileStream(string path, FileAccess access)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			this.InitStream(StdioFileStream.Fopen(path, StdioFileStream.ToFopenMode(path, access)), true);
			this.InitCanReadWrite(access);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002724 File Offset: 0x00000924
		public StdioFileStream(string path, FileMode mode, FileAccess access)
		{
			this.owner = true;
			this.file = StdioFileStream.InvalidFileStream;
			base..ctor();
			this.InitStream(StdioFileStream.Fopen(path, StdioFileStream.ToFopenMode(path, mode, access)), true);
			this.InitCanReadWrite(access);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027A0 File Offset: 0x000009A0
		private static IntPtr Fopen(string path, string mode)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("path");
			}
			if (mode == null)
			{
				throw new ArgumentNullException("mode");
			}
			IntPtr intPtr = Stdlib.fopen(path, mode);
			if (intPtr == IntPtr.Zero)
			{
				throw new DirectoryNotFoundException("path", UnixMarshal.CreateExceptionForLastError());
			}
			return intPtr;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002810 File Offset: 0x00000A10
		private void InitStream(IntPtr fileStream, bool ownsHandle)
		{
			if (StdioFileStream.InvalidFileStream == fileStream)
			{
				throw new ArgumentException(Locale.GetText("Invalid file stream"), "fileStream");
			}
			this.file = fileStream;
			this.owner = ownsHandle;
			try
			{
				long num = (long)Stdlib.fseek(this.file, 0L, SeekFlags.SEEK_CUR);
				if (num != -1L)
				{
					this.canSeek = true;
				}
				Stdlib.fread(IntPtr.Zero, 0UL, 0UL, this.file);
				if (Stdlib.ferror(this.file) == 0)
				{
					this.canRead = true;
				}
				Stdlib.fwrite(IntPtr.Zero, 0UL, 0UL, this.file);
				if (Stdlib.ferror(this.file) == 0)
				{
					this.canWrite = true;
				}
				Stdlib.clearerr(this.file);
			}
			catch (Exception)
			{
				throw new ArgumentException(Locale.GetText("Invalid file stream"), "fileStream");
			}
			GC.KeepAlive(this);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002918 File Offset: 0x00000B18
		private void InitCanReadWrite(FileAccess access)
		{
			this.canRead = (this.canRead && (access == FileAccess.Read || access == FileAccess.ReadWrite));
			this.canWrite = (this.canWrite && (access == FileAccess.Write || access == FileAccess.ReadWrite));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000296C File Offset: 0x00000B6C
		private static string ToFopenMode(string file, FileMode mode)
		{
			string result = NativeConvert.ToFopenMode(mode);
			StdioFileStream.AssertFileMode(file, mode);
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000298C File Offset: 0x00000B8C
		private static string ToFopenMode(string file, FileAccess access)
		{
			return NativeConvert.ToFopenMode(access);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002994 File Offset: 0x00000B94
		private static string ToFopenMode(string file, FileMode mode, FileAccess access)
		{
			string result = NativeConvert.ToFopenMode(mode, access);
			bool flag = StdioFileStream.AssertFileMode(file, mode);
			if (mode == FileMode.OpenOrCreate && access == FileAccess.Read && !flag)
			{
				result = "w+b";
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool AssertFileMode(string file, FileMode mode)
		{
			bool flag = StdioFileStream.FileExists(file);
			if (mode == FileMode.CreateNew && flag)
			{
				throw new IOException("File exists and FileMode.CreateNew specified");
			}
			if ((mode == FileMode.Open || mode == FileMode.Truncate) && !flag)
			{
				throw new FileNotFoundException("File doesn't exist and FileMode.Open specified", file);
			}
			return flag;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A1C File Offset: 0x00000C1C
		private static bool FileExists(string file)
		{
			IntPtr intPtr = Stdlib.fopen(file, "r");
			bool result = intPtr != IntPtr.Zero;
			if (intPtr != IntPtr.Zero)
			{
				Stdlib.fclose(intPtr);
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A5C File Offset: 0x00000C5C
		private void AssertNotDisposed()
		{
			if (this.file == StdioFileStream.InvalidFileStream)
			{
				throw new ObjectDisposedException("Invalid File Stream");
			}
			GC.KeepAlive(this);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A90 File Offset: 0x00000C90
		public IntPtr Handle
		{
			get
			{
				this.AssertNotDisposed();
				GC.KeepAlive(this);
				return this.file;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public override bool CanRead
		{
			get
			{
				return this.canRead;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002AAC File Offset: 0x00000CAC
		public override bool CanSeek
		{
			get
			{
				return this.canSeek;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public override bool CanWrite
		{
			get
			{
				return this.canWrite;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002ABC File Offset: 0x00000CBC
		public override long Length
		{
			get
			{
				this.AssertNotDisposed();
				if (!this.CanSeek)
				{
					throw new NotSupportedException("File Stream doesn't support seeking");
				}
				long num = Stdlib.ftell(this.file);
				if (num == -1L)
				{
					throw new NotSupportedException("Unable to obtain current file position");
				}
				int retval = Stdlib.fseek(this.file, 0L, SeekFlags.SEEK_END);
				UnixMarshal.ThrowExceptionForLastErrorIf(retval);
				long num2 = Stdlib.ftell(this.file);
				if (num2 == -1L)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
				retval = Stdlib.fseek(this.file, num, SeekFlags.SEEK_SET);
				UnixMarshal.ThrowExceptionForLastErrorIf(retval);
				GC.KeepAlive(this);
				return num2;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002B50 File Offset: 0x00000D50
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002B9C File Offset: 0x00000D9C
		public override long Position
		{
			get
			{
				this.AssertNotDisposed();
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				long num = Stdlib.ftell(this.file);
				if (num == -1L)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
				GC.KeepAlive(this);
				return num;
			}
			set
			{
				this.AssertNotDisposed();
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public void SaveFilePosition(FilePosition pos)
		{
			this.AssertNotDisposed();
			int retval = Stdlib.fgetpos(this.file, pos);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			GC.KeepAlive(this);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void RestoreFilePosition(FilePosition pos)
		{
			this.AssertNotDisposed();
			if (pos == null)
			{
				throw new ArgumentNullException("value");
			}
			int retval = Stdlib.fsetpos(this.file, pos);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			GC.KeepAlive(this);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002C20 File Offset: 0x00000E20
		public override void Flush()
		{
			this.AssertNotDisposed();
			int num = Stdlib.fflush(this.file);
			if (num != 0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			GC.KeepAlive(this);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C50 File Offset: 0x00000E50
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			ulong num;
			fixed (byte* ptr = &buffer[offset])
			{
				num = Stdlib.fread((void*)ptr, 1UL, (ulong)((long)count), this.file);
			}
			if (num != (ulong)((long)count) && Stdlib.ferror(this.file) != 0)
			{
				throw new IOException();
			}
			GC.KeepAlive(this);
			return (int)num;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CC8 File Offset: 0x00000EC8
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

		// Token: 0x06000041 RID: 65 RVA: 0x00002D40 File Offset: 0x00000F40
		public void Rewind()
		{
			this.AssertNotDisposed();
			Stdlib.rewind(this.file);
			GC.KeepAlive(this);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D5C File Offset: 0x00000F5C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.AssertNotDisposed();
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The File Stream does not support seeking");
			}
			SeekFlags origin2;
			switch (origin)
			{
			case SeekOrigin.Begin:
				origin2 = SeekFlags.SEEK_SET;
				break;
			case SeekOrigin.Current:
				origin2 = SeekFlags.SEEK_CUR;
				break;
			case SeekOrigin.End:
				origin2 = SeekFlags.SEEK_END;
				break;
			default:
				throw new ArgumentException("origin");
			}
			int num = Stdlib.fseek(this.file, offset, origin2);
			if (num != 0)
			{
				throw new IOException("Unable to seek", UnixMarshal.CreateExceptionForLastError());
			}
			long num2 = Stdlib.ftell(this.file);
			if (num2 == -1L)
			{
				throw new IOException("Unable to get current file position", UnixMarshal.CreateExceptionForLastError());
			}
			GC.KeepAlive(this);
			return num2;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002E10 File Offset: 0x00001010
		public override void SetLength(long value)
		{
			throw new NotSupportedException("ANSI C doesn't provide a way to truncate a file");
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002E1C File Offset: 0x0000101C
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			this.AssertNotDisposed();
			this.AssertValidBuffer(buffer, offset, count);
			if (!this.CanWrite)
			{
				throw new NotSupportedException("File Stream does not support writing");
			}
			ulong num;
			fixed (byte* ptr = &buffer[offset])
			{
				num = Stdlib.fwrite((void*)ptr, 1UL, (ulong)((long)count), this.file);
			}
			if (num != (ulong)((long)count))
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			GC.KeepAlive(this);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E80 File Offset: 0x00001080
		~StdioFileStream()
		{
			this.Close();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002EBC File Offset: 0x000010BC
		public override void Close()
		{
			if (this.file == StdioFileStream.InvalidFileStream)
			{
				return;
			}
			if (this.owner)
			{
				int num = Stdlib.fclose(this.file);
				if (num != 0)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
			}
			else
			{
				this.Flush();
			}
			this.file = StdioFileStream.InvalidFileStream;
			this.canRead = false;
			this.canSeek = false;
			this.canWrite = false;
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x04000043 RID: 67
		public static readonly IntPtr InvalidFileStream = IntPtr.Zero;

		// Token: 0x04000044 RID: 68
		public static readonly IntPtr StandardInput = Stdlib.stdin;

		// Token: 0x04000045 RID: 69
		public static readonly IntPtr StandardOutput = Stdlib.stdout;

		// Token: 0x04000046 RID: 70
		public static readonly IntPtr StandardError = Stdlib.stderr;

		// Token: 0x04000047 RID: 71
		private bool canSeek;

		// Token: 0x04000048 RID: 72
		private bool canRead;

		// Token: 0x04000049 RID: 73
		private bool canWrite;

		// Token: 0x0400004A RID: 74
		private bool owner;

		// Token: 0x0400004B RID: 75
		private IntPtr file;
	}
}
