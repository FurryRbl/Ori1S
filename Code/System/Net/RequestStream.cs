using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020003DB RID: 987
	internal class RequestStream : Stream
	{
		// Token: 0x060021B2 RID: 8626 RVA: 0x000629C0 File Offset: 0x00060BC0
		internal RequestStream(Stream stream, byte[] buffer, int offset, int length) : this(stream, buffer, offset, length, -1L)
		{
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000629D0 File Offset: 0x00060BD0
		internal RequestStream(Stream stream, byte[] buffer, int offset, int length, long contentlength)
		{
			this.stream = stream;
			this.buffer = buffer;
			this.offset = offset;
			this.length = length;
			this.remaining_body = contentlength;
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x00062A00 File Offset: 0x00060C00
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x00062A04 File Offset: 0x00060C04
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x00062A08 File Offset: 0x00060C08
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x00062A0C File Offset: 0x00060C0C
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x00062A14 File Offset: 0x00060C14
		// (set) Token: 0x060021B9 RID: 8633 RVA: 0x00062A1C File Offset: 0x00060C1C
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

		// Token: 0x060021BA RID: 8634 RVA: 0x00062A24 File Offset: 0x00060C24
		public override void Close()
		{
			this.disposed = true;
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x00062A30 File Offset: 0x00060C30
		public override void Flush()
		{
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x00062A34 File Offset: 0x00060C34
		private int FillFromBuffer(byte[] buffer, int off, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (off < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			int num = buffer.Length;
			if (off > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (off > num - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (this.remaining_body == 0L)
			{
				return -1;
			}
			if (this.length == 0)
			{
				return 0;
			}
			int num2 = Math.Min(this.length, count);
			if (this.remaining_body > 0L)
			{
				num2 = (int)Math.Min((long)num2, this.remaining_body);
			}
			if (this.offset > this.buffer.Length - num2)
			{
				num2 = Math.Min(num2, this.buffer.Length - this.offset);
			}
			if (num2 == 0)
			{
				return 0;
			}
			Buffer.BlockCopy(this.buffer, this.offset, buffer, off, num2);
			this.offset += num2;
			this.length -= num2;
			if (this.remaining_body > 0L)
			{
				this.remaining_body -= (long)num2;
			}
			return num2;
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x00062B70 File Offset: 0x00060D70
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			int num = this.FillFromBuffer(buffer, offset, count);
			if (num == -1)
			{
				return 0;
			}
			if (num > 0)
			{
				return num;
			}
			num = this.stream.Read(buffer, offset, count);
			if (num > 0 && this.remaining_body > 0L)
			{
				this.remaining_body -= (long)num;
			}
			return num;
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x00062BEC File Offset: 0x00060DEC
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			int num = this.FillFromBuffer(buffer, offset, count);
			if (num > 0 || num == -1)
			{
				HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult();
				httpStreamAsyncResult.Buffer = buffer;
				httpStreamAsyncResult.Offset = offset;
				httpStreamAsyncResult.Count = count;
				httpStreamAsyncResult.Callback = cback;
				httpStreamAsyncResult.State = state;
				httpStreamAsyncResult.SynchRead = num;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (this.remaining_body >= 0L && (long)count > this.remaining_body)
			{
				count = (int)Math.Min(2147483647L, this.remaining_body);
			}
			return this.stream.BeginRead(buffer, offset, count, cback, state);
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x00062CAC File Offset: 0x00060EAC
		public override int EndRead(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			if (ares == null)
			{
				throw new ArgumentNullException("async_result");
			}
			if (ares is HttpStreamAsyncResult)
			{
				HttpStreamAsyncResult httpStreamAsyncResult = (HttpStreamAsyncResult)ares;
				if (!ares.IsCompleted)
				{
					ares.AsyncWaitHandle.WaitOne();
				}
				return httpStreamAsyncResult.SynchRead;
			}
			int num = this.stream.EndRead(ares);
			if (this.remaining_body > 0L && num > 0)
			{
				this.remaining_body -= (long)num;
			}
			return num;
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x00062D4C File Offset: 0x00060F4C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x00062D54 File Offset: 0x00060F54
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x00062D5C File Offset: 0x00060F5C
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x00062D64 File Offset: 0x00060F64
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x00062D6C File Offset: 0x00060F6C
		public override void EndWrite(IAsyncResult async_result)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040014E7 RID: 5351
		private byte[] buffer;

		// Token: 0x040014E8 RID: 5352
		private int offset;

		// Token: 0x040014E9 RID: 5353
		private int length;

		// Token: 0x040014EA RID: 5354
		private long remaining_body;

		// Token: 0x040014EB RID: 5355
		private bool disposed;

		// Token: 0x040014EC RID: 5356
		private Stream stream;
	}
}
