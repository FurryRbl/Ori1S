﻿using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020002C2 RID: 706
	internal class ChunkedInputStream : RequestStream
	{
		// Token: 0x06001857 RID: 6231 RVA: 0x00042DA8 File Offset: 0x00040FA8
		public ChunkedInputStream(HttpListenerContext context, Stream stream, byte[] buffer, int offset, int length) : base(stream, buffer, offset, length)
		{
			this.context = context;
			WebHeaderCollection headers = (WebHeaderCollection)context.Request.Headers;
			this.decoder = new ChunkStream(headers);
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x00042DE8 File Offset: 0x00040FE8
		// (set) Token: 0x06001859 RID: 6233 RVA: 0x00042DF0 File Offset: 0x00040FF0
		public ChunkStream Decoder
		{
			get
			{
				return this.decoder;
			}
			set
			{
				this.decoder = value;
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00042DFC File Offset: 0x00040FFC
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			IAsyncResult ares = this.BeginRead(buffer, offset, count, null, null);
			return this.EndRead(ares);
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00042E1C File Offset: 0x0004101C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || offset > num)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (count < 0 || offset > num - count)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult();
			httpStreamAsyncResult.Callback = cback;
			httpStreamAsyncResult.State = state;
			if (this.no_more_data)
			{
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			int num2 = this.decoder.Read(buffer, offset, count);
			offset += num2;
			count -= num2;
			if (count == 0)
			{
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (!this.decoder.WantMore)
			{
				this.no_more_data = (num2 == 0);
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			httpStreamAsyncResult.Buffer = new byte[8192];
			httpStreamAsyncResult.Offset = 0;
			httpStreamAsyncResult.Count = 8192;
			ChunkedInputStream.ReadBufferState readBufferState = new ChunkedInputStream.ReadBufferState(buffer, offset, count, httpStreamAsyncResult);
			readBufferState.InitialCount += num2;
			base.BeginRead(httpStreamAsyncResult.Buffer, httpStreamAsyncResult.Offset, httpStreamAsyncResult.Count, new AsyncCallback(this.OnRead), readBufferState);
			return httpStreamAsyncResult;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00042F70 File Offset: 0x00041170
		private void OnRead(IAsyncResult base_ares)
		{
			ChunkedInputStream.ReadBufferState readBufferState = (ChunkedInputStream.ReadBufferState)base_ares.AsyncState;
			HttpStreamAsyncResult ares = readBufferState.Ares;
			try
			{
				int num = base.EndRead(base_ares);
				this.decoder.Write(ares.Buffer, ares.Offset, num);
				num = this.decoder.Read(readBufferState.Buffer, readBufferState.Offset, readBufferState.Count);
				readBufferState.Offset += num;
				readBufferState.Count -= num;
				if (readBufferState.Count == 0 || !this.decoder.WantMore || num == 0)
				{
					this.no_more_data = (!this.decoder.WantMore && num == 0);
					ares.Count = readBufferState.InitialCount - readBufferState.Count;
					ares.Complete();
				}
				else
				{
					ares.Offset = 0;
					ares.Count = Math.Min(8192, this.decoder.ChunkLeft + 6);
					base.BeginRead(ares.Buffer, ares.Offset, ares.Count, new AsyncCallback(this.OnRead), readBufferState);
				}
			}
			catch (Exception ex)
			{
				this.context.Connection.SendError(ex.Message, 400);
				ares.Complete(ex);
			}
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x000430DC File Offset: 0x000412DC
		public override int EndRead(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			HttpStreamAsyncResult httpStreamAsyncResult = ares as HttpStreamAsyncResult;
			if (ares == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "ares");
			}
			if (!ares.IsCompleted)
			{
				ares.AsyncWaitHandle.WaitOne();
			}
			if (httpStreamAsyncResult.Error != null)
			{
				throw new HttpListenerException(400, "I/O operation aborted.");
			}
			return httpStreamAsyncResult.Count;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0004315C File Offset: 0x0004135C
		public override void Close()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				base.Close();
			}
		}

		// Token: 0x04000F98 RID: 3992
		private bool disposed;

		// Token: 0x04000F99 RID: 3993
		private ChunkStream decoder;

		// Token: 0x04000F9A RID: 3994
		private HttpListenerContext context;

		// Token: 0x04000F9B RID: 3995
		private bool no_more_data;

		// Token: 0x020002C3 RID: 707
		private class ReadBufferState
		{
			// Token: 0x0600185F RID: 6239 RVA: 0x00043178 File Offset: 0x00041378
			public ReadBufferState(byte[] buffer, int offset, int count, HttpStreamAsyncResult ares)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
				this.InitialCount = count;
				this.Ares = ares;
			}

			// Token: 0x04000F9C RID: 3996
			public byte[] Buffer;

			// Token: 0x04000F9D RID: 3997
			public int Offset;

			// Token: 0x04000F9E RID: 3998
			public int Count;

			// Token: 0x04000F9F RID: 3999
			public int InitialCount;

			// Token: 0x04000FA0 RID: 4000
			public HttpStreamAsyncResult Ares;
		}
	}
}
