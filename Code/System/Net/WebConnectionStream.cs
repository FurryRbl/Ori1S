using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000416 RID: 1046
	internal class WebConnectionStream : Stream
	{
		// Token: 0x0600257A RID: 9594 RVA: 0x0007392C File Offset: 0x00071B2C
		public WebConnectionStream(WebConnection cnc)
		{
			this.isRead = true;
			this.pending = new ManualResetEvent(true);
			this.request = cnc.Data.request;
			this.read_timeout = this.request.ReadWriteTimeout;
			this.write_timeout = this.read_timeout;
			this.cnc = cnc;
			string text = cnc.Data.Headers["Transfer-Encoding"];
			bool flag = text != null && text.ToLower().IndexOf("chunked") != -1;
			string text2 = cnc.Data.Headers["Content-Length"];
			if (!flag && text2 != null && text2 != string.Empty)
			{
				try
				{
					this.contentLength = int.Parse(text2);
					if (this.contentLength == 0 && !this.IsNtlmAuth())
					{
						this.ReadAll();
					}
				}
				catch
				{
					this.contentLength = int.MaxValue;
				}
			}
			else
			{
				this.contentLength = int.MaxValue;
			}
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00073A64 File Offset: 0x00071C64
		public WebConnectionStream(WebConnection cnc, HttpWebRequest request)
		{
			this.read_timeout = request.ReadWriteTimeout;
			this.write_timeout = this.read_timeout;
			this.isRead = false;
			this.cnc = cnc;
			this.request = request;
			this.allowBuffering = request.InternalAllowBuffering;
			this.sendChunked = request.SendChunked;
			if (this.sendChunked)
			{
				this.pending = new ManualResetEvent(true);
			}
			else if (this.allowBuffering)
			{
				this.writeBuffer = new MemoryStream();
			}
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x00073B14 File Offset: 0x00071D14
		private bool IsNtlmAuth()
		{
			bool flag = this.request.Proxy != null && !this.request.Proxy.IsBypassed(this.request.Address);
			string name = (!flag) ? "WWW-Authenticate" : "Proxy-Authenticate";
			string text = this.cnc.Data.Headers[name];
			return text != null && text.IndexOf("NTLM") != -1;
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00073B9C File Offset: 0x00071D9C
		internal void CheckResponseInBuffer()
		{
			if (this.contentLength > 0 && this.readBufferSize - this.readBufferOffset >= this.contentLength && !this.IsNtlmAuth())
			{
				this.ReadAll();
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x00073BD4 File Offset: 0x00071DD4
		internal HttpWebRequest Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x00073BDC File Offset: 0x00071DDC
		internal WebConnection Connection
		{
			get
			{
				return this.cnc;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x00073BE4 File Offset: 0x00071DE4
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x00073BE8 File Offset: 0x00071DE8
		// (set) Token: 0x06002583 RID: 9603 RVA: 0x00073BF0 File Offset: 0x00071DF0
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.read_timeout = value;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x00073C0C File Offset: 0x00071E0C
		// (set) Token: 0x06002585 RID: 9605 RVA: 0x00073C14 File Offset: 0x00071E14
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.write_timeout = value;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x00073C30 File Offset: 0x00071E30
		internal bool CompleteRequestWritten
		{
			get
			{
				return this.complete_request_written;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (set) Token: 0x06002587 RID: 9607 RVA: 0x00073C38 File Offset: 0x00071E38
		internal bool SendChunked
		{
			set
			{
				this.sendChunked = value;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (set) Token: 0x06002588 RID: 9608 RVA: 0x00073C44 File Offset: 0x00071E44
		internal byte[] ReadBuffer
		{
			set
			{
				this.readBuffer = value;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (set) Token: 0x06002589 RID: 9609 RVA: 0x00073C50 File Offset: 0x00071E50
		internal int ReadBufferOffset
		{
			set
			{
				this.readBufferOffset = value;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (set) Token: 0x0600258A RID: 9610 RVA: 0x00073C5C File Offset: 0x00071E5C
		internal int ReadBufferSize
		{
			set
			{
				this.readBufferSize = value;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x00073C68 File Offset: 0x00071E68
		internal byte[] WriteBuffer
		{
			get
			{
				return this.writeBuffer.GetBuffer();
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x00073C78 File Offset: 0x00071E78
		internal int WriteBufferLength
		{
			get
			{
				return (this.writeBuffer == null) ? -1 : ((int)this.writeBuffer.Length);
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x00073C98 File Offset: 0x00071E98
		internal void ForceCompletion()
		{
			if (!this.nextReadCalled)
			{
				if (this.contentLength == 2147483647)
				{
					this.contentLength = 0;
				}
				this.nextReadCalled = true;
				this.cnc.NextRead();
			}
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x00073CDC File Offset: 0x00071EDC
		internal void CheckComplete()
		{
			if (!this.nextReadCalled && this.readBufferSize - this.readBufferOffset == this.contentLength)
			{
				this.nextReadCalled = true;
				this.cnc.NextRead();
			}
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x00073D20 File Offset: 0x00071F20
		internal void ReadAll()
		{
			if (!this.isRead || this.read_eof || this.totalRead >= this.contentLength || this.nextReadCalled)
			{
				if (this.isRead && !this.nextReadCalled)
				{
					this.nextReadCalled = true;
					this.cnc.NextRead();
				}
				return;
			}
			this.pending.WaitOne();
			object obj = this.locker;
			lock (obj)
			{
				if (this.totalRead >= this.contentLength)
				{
					return;
				}
				int num = this.readBufferSize - this.readBufferOffset;
				byte[] array2;
				int num2;
				if (this.contentLength == 2147483647)
				{
					MemoryStream memoryStream = new MemoryStream();
					byte[] array = null;
					if (this.readBuffer != null && num > 0)
					{
						memoryStream.Write(this.readBuffer, this.readBufferOffset, num);
						if (this.readBufferSize >= 8192)
						{
							array = this.readBuffer;
						}
					}
					if (array == null)
					{
						array = new byte[8192];
					}
					int count;
					while ((count = this.cnc.Read(this.request, array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, count);
					}
					array2 = memoryStream.GetBuffer();
					num2 = (int)memoryStream.Length;
					this.contentLength = num2;
				}
				else
				{
					num2 = this.contentLength - this.totalRead;
					array2 = new byte[num2];
					if (this.readBuffer != null && num > 0)
					{
						if (num > num2)
						{
							num = num2;
						}
						Buffer.BlockCopy(this.readBuffer, this.readBufferOffset, array2, 0, num);
					}
					int num3 = num2 - num;
					int num4 = -1;
					while (num3 > 0 && num4 != 0)
					{
						num4 = this.cnc.Read(this.request, array2, num, num3);
						num3 -= num4;
						num += num4;
					}
				}
				this.readBuffer = array2;
				this.readBufferOffset = 0;
				this.readBufferSize = num2;
				this.totalRead = 0;
				this.nextReadCalled = true;
			}
			this.cnc.NextRead();
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00073F5C File Offset: 0x0007215C
		private void WriteCallbackWrapper(IAsyncResult r)
		{
			WebAsyncResult webAsyncResult = r as WebAsyncResult;
			if (webAsyncResult != null && webAsyncResult.AsyncWriteAll)
			{
				return;
			}
			if (r.AsyncState != null)
			{
				webAsyncResult = (WebAsyncResult)r.AsyncState;
				webAsyncResult.InnerAsyncResult = r;
				webAsyncResult.DoCallback();
			}
			else
			{
				this.EndWrite(r);
			}
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x00073FB4 File Offset: 0x000721B4
		private void ReadCallbackWrapper(IAsyncResult r)
		{
			if (r.AsyncState != null)
			{
				WebAsyncResult webAsyncResult = (WebAsyncResult)r.AsyncState;
				webAsyncResult.InnerAsyncResult = r;
				webAsyncResult.DoCallback();
			}
			else
			{
				this.EndRead(r);
			}
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x00073FF4 File Offset: 0x000721F4
		public override int Read(byte[] buffer, int offset, int size)
		{
			AsyncCallback cb = new AsyncCallback(this.ReadCallbackWrapper);
			WebAsyncResult webAsyncResult = (WebAsyncResult)this.BeginRead(buffer, offset, size, cb, null);
			if (!webAsyncResult.IsCompleted && !webAsyncResult.WaitUntilComplete(this.ReadTimeout, false))
			{
				this.nextReadCalled = true;
				this.cnc.Close(true);
				throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
			}
			return this.EndRead(webAsyncResult);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x00074064 File Offset: 0x00072264
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			if (!this.isRead)
			{
				throw new NotSupportedException("this stream does not allow reading");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			object obj = this.locker;
			lock (obj)
			{
				this.pendingReads++;
				this.pending.Reset();
			}
			WebAsyncResult webAsyncResult = new WebAsyncResult(cb, state, buffer, offset, size);
			if (this.totalRead >= this.contentLength)
			{
				webAsyncResult.SetCompleted(true, -1);
				webAsyncResult.DoCallback();
				return webAsyncResult;
			}
			int num2 = this.readBufferSize - this.readBufferOffset;
			if (num2 > 0)
			{
				int num3 = (num2 <= size) ? num2 : size;
				Buffer.BlockCopy(this.readBuffer, this.readBufferOffset, buffer, offset, num3);
				this.readBufferOffset += num3;
				offset += num3;
				size -= num3;
				this.totalRead += num3;
				if (size == 0 || this.totalRead >= this.contentLength)
				{
					webAsyncResult.SetCompleted(true, num3);
					webAsyncResult.DoCallback();
					return webAsyncResult;
				}
				webAsyncResult.NBytes = num3;
			}
			if (cb != null)
			{
				cb = new AsyncCallback(this.ReadCallbackWrapper);
			}
			if (this.contentLength != 2147483647 && this.contentLength - this.totalRead < size)
			{
				size = this.contentLength - this.totalRead;
			}
			if (!this.read_eof)
			{
				webAsyncResult.InnerAsyncResult = this.cnc.BeginRead(this.request, buffer, offset, size, cb, webAsyncResult);
			}
			else
			{
				webAsyncResult.SetCompleted(true, webAsyncResult.NBytes);
				webAsyncResult.DoCallback();
			}
			return webAsyncResult;
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0007426C File Offset: 0x0007246C
		public override int EndRead(IAsyncResult r)
		{
			WebAsyncResult webAsyncResult = (WebAsyncResult)r;
			if (webAsyncResult.EndCalled)
			{
				int nbytes = webAsyncResult.NBytes;
				return (nbytes < 0) ? 0 : nbytes;
			}
			webAsyncResult.EndCalled = true;
			if (!webAsyncResult.IsCompleted)
			{
				int num = -1;
				try
				{
					num = this.cnc.EndRead(this.request, webAsyncResult);
				}
				catch (Exception e)
				{
					object obj = this.locker;
					lock (obj)
					{
						this.pendingReads--;
						if (this.pendingReads == 0)
						{
							this.pending.Set();
						}
					}
					this.nextReadCalled = true;
					this.cnc.Close(true);
					webAsyncResult.SetCompleted(false, e);
					webAsyncResult.DoCallback();
					throw;
				}
				if (num < 0)
				{
					num = 0;
					this.read_eof = true;
				}
				this.totalRead += num;
				webAsyncResult.SetCompleted(false, num + webAsyncResult.NBytes);
				webAsyncResult.DoCallback();
				if (num == 0)
				{
					this.contentLength = this.totalRead;
				}
			}
			object obj2 = this.locker;
			lock (obj2)
			{
				this.pendingReads--;
				if (this.pendingReads == 0)
				{
					this.pending.Set();
				}
			}
			if (this.totalRead >= this.contentLength && !this.nextReadCalled)
			{
				this.ReadAll();
			}
			int nbytes2 = webAsyncResult.NBytes;
			return (nbytes2 < 0) ? 0 : nbytes2;
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x00074444 File Offset: 0x00072644
		private void WriteRequestAsyncCB(IAsyncResult r)
		{
			WebAsyncResult webAsyncResult = (WebAsyncResult)r.AsyncState;
			try
			{
				this.cnc.EndWrite2(this.request, r);
				webAsyncResult.SetCompleted(false, 0);
				if (!this.initRead)
				{
					this.initRead = true;
					WebConnection.InitRead(this.cnc);
				}
			}
			catch (Exception ex)
			{
				this.KillBuffer();
				this.nextReadCalled = true;
				this.cnc.Close(true);
				if (ex is System.Net.Sockets.SocketException)
				{
					ex = new IOException("Error writing request", ex);
				}
				webAsyncResult.SetCompleted(false, ex);
			}
			this.complete_request_written = true;
			webAsyncResult.DoCallback();
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x00074500 File Offset: 0x00072700
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			if (this.request.Aborted)
			{
				throw new WebException("The request was canceled.", null, WebExceptionStatus.RequestCanceled);
			}
			if (this.isRead)
			{
				throw new NotSupportedException("this stream does not allow writing");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (this.sendChunked)
			{
				object obj = this.locker;
				lock (obj)
				{
					this.pendingWrites++;
					this.pending.Reset();
				}
			}
			WebAsyncResult webAsyncResult = new WebAsyncResult(cb, state);
			if (!this.sendChunked)
			{
				this.CheckWriteOverflow(this.request.ContentLength, this.totalWritten, (long)size);
			}
			if (this.allowBuffering && !this.sendChunked)
			{
				if (this.writeBuffer == null)
				{
					this.writeBuffer = new MemoryStream();
				}
				this.writeBuffer.Write(buffer, offset, size);
				this.totalWritten += (long)size;
				if (this.request.ContentLength > 0L && this.totalWritten == this.request.ContentLength)
				{
					try
					{
						webAsyncResult.AsyncWriteAll = true;
						webAsyncResult.InnerAsyncResult = this.WriteRequestAsync(new AsyncCallback(this.WriteRequestAsyncCB), webAsyncResult);
						if (webAsyncResult.InnerAsyncResult == null)
						{
							if (!webAsyncResult.IsCompleted)
							{
								webAsyncResult.SetCompleted(true, 0);
							}
							webAsyncResult.DoCallback();
						}
					}
					catch (Exception e)
					{
						webAsyncResult.SetCompleted(true, e);
						webAsyncResult.DoCallback();
					}
				}
				else
				{
					webAsyncResult.SetCompleted(true, 0);
					webAsyncResult.DoCallback();
				}
				return webAsyncResult;
			}
			AsyncCallback cb2 = null;
			if (cb != null)
			{
				cb2 = new AsyncCallback(this.WriteCallbackWrapper);
			}
			if (this.sendChunked)
			{
				this.WriteRequest();
				string s = string.Format("{0:X}\r\n", size);
				byte[] bytes = Encoding.ASCII.GetBytes(s);
				int num2 = 2 + size + bytes.Length;
				byte[] array = new byte[num2];
				Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
				Buffer.BlockCopy(buffer, offset, array, bytes.Length, size);
				Buffer.BlockCopy(WebConnectionStream.crlf, 0, array, bytes.Length + size, WebConnectionStream.crlf.Length);
				buffer = array;
				offset = 0;
				size = num2;
			}
			webAsyncResult.InnerAsyncResult = this.cnc.BeginWrite(this.request, buffer, offset, size, cb2, webAsyncResult);
			this.totalWritten += (long)size;
			return webAsyncResult;
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000747D8 File Offset: 0x000729D8
		private void CheckWriteOverflow(long contentLength, long totalWritten, long size)
		{
			if (contentLength == -1L)
			{
				return;
			}
			long num = contentLength - totalWritten;
			if (size > num)
			{
				this.KillBuffer();
				this.nextReadCalled = true;
				this.cnc.Close(true);
				throw new ProtocolViolationException("The number of bytes to be written is greater than the specified ContentLength.");
			}
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x00074820 File Offset: 0x00072A20
		public override void EndWrite(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			WebAsyncResult webAsyncResult = r as WebAsyncResult;
			if (webAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult");
			}
			if (webAsyncResult.EndCalled)
			{
				return;
			}
			webAsyncResult.EndCalled = true;
			if (webAsyncResult.AsyncWriteAll)
			{
				webAsyncResult.WaitUntilComplete();
				if (webAsyncResult.GotException)
				{
					throw webAsyncResult.Exception;
				}
				return;
			}
			else
			{
				if (this.allowBuffering && !this.sendChunked)
				{
					return;
				}
				if (webAsyncResult.GotException)
				{
					throw webAsyncResult.Exception;
				}
				try
				{
					this.cnc.EndWrite2(this.request, webAsyncResult.InnerAsyncResult);
					webAsyncResult.SetCompleted(false, 0);
					webAsyncResult.DoCallback();
				}
				catch (Exception e)
				{
					webAsyncResult.SetCompleted(false, e);
					webAsyncResult.DoCallback();
					throw;
				}
				finally
				{
					if (this.sendChunked)
					{
						object obj = this.locker;
						lock (obj)
						{
							this.pendingWrites--;
							if (this.pendingWrites == 0)
							{
								this.pending.Set();
							}
						}
					}
				}
				return;
			}
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0007498C File Offset: 0x00072B8C
		public override void Write(byte[] buffer, int offset, int size)
		{
			AsyncCallback cb = new AsyncCallback(this.WriteCallbackWrapper);
			WebAsyncResult webAsyncResult = (WebAsyncResult)this.BeginWrite(buffer, offset, size, cb, null);
			if (!webAsyncResult.IsCompleted && !webAsyncResult.WaitUntilComplete(this.WriteTimeout, false))
			{
				this.KillBuffer();
				this.nextReadCalled = true;
				this.cnc.Close(true);
				throw new IOException("Write timed out.");
			}
			this.EndWrite(webAsyncResult);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x00074A00 File Offset: 0x00072C00
		public override void Flush()
		{
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x00074A04 File Offset: 0x00072C04
		internal void SetHeaders(byte[] buffer)
		{
			if (this.headersSent)
			{
				return;
			}
			this.headers = buffer;
			long num = this.request.ContentLength;
			string method = this.request.Method;
			bool flag = method == "GET" || method == "CONNECT" || method == "HEAD" || method == "TRACE" || method == "DELETE";
			if (this.sendChunked || num > -1L || flag)
			{
				this.WriteHeaders();
				if (!this.initRead)
				{
					this.initRead = true;
					WebConnection.InitRead(this.cnc);
				}
				if (!this.sendChunked && num == 0L)
				{
					this.requestWritten = true;
				}
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x00074AE0 File Offset: 0x00072CE0
		internal bool RequestWritten
		{
			get
			{
				return this.requestWritten;
			}
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x00074AE8 File Offset: 0x00072CE8
		private IAsyncResult WriteRequestAsync(AsyncCallback cb, object state)
		{
			this.requestWritten = true;
			byte[] buffer = this.writeBuffer.GetBuffer();
			int num = (int)this.writeBuffer.Length;
			IAsyncResult result;
			if (num > 0)
			{
				IAsyncResult asyncResult = this.cnc.BeginWrite(this.request, buffer, 0, num, cb, state);
				result = asyncResult;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x00074B3C File Offset: 0x00072D3C
		private void WriteHeaders()
		{
			if (this.headersSent)
			{
				return;
			}
			this.headersSent = true;
			string str = null;
			if (!this.cnc.Write(this.request, this.headers, 0, this.headers.Length, ref str))
			{
				throw new WebException("Error writing request: " + str, null, WebExceptionStatus.SendFailure, null);
			}
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x00074B9C File Offset: 0x00072D9C
		internal void WriteRequest()
		{
			if (this.requestWritten)
			{
				return;
			}
			this.requestWritten = true;
			if (this.sendChunked)
			{
				return;
			}
			if (!this.allowBuffering || this.writeBuffer == null)
			{
				return;
			}
			byte[] buffer = this.writeBuffer.GetBuffer();
			int num = (int)this.writeBuffer.Length;
			if (this.request.ContentLength != -1L && this.request.ContentLength < (long)num)
			{
				this.nextReadCalled = true;
				this.cnc.Close(true);
				throw new WebException("Specified Content-Length is less than the number of bytes to write", null, WebExceptionStatus.ServerProtocolViolation, null);
			}
			if (!this.headersSent)
			{
				string method = this.request.Method;
				if (!(method == "GET") && !(method == "CONNECT") && !(method == "HEAD") && !(method == "TRACE") && !(method == "DELETE"))
				{
					this.request.InternalContentLength = (long)num;
				}
				this.request.SendRequestHeaders(true);
			}
			this.WriteHeaders();
			if (this.cnc.Data.StatusCode != 0 && this.cnc.Data.StatusCode != 100)
			{
				return;
			}
			IAsyncResult result = null;
			if (num > 0)
			{
				result = this.cnc.BeginWrite(this.request, buffer, 0, num, null, null);
			}
			if (!this.initRead)
			{
				this.initRead = true;
				WebConnection.InitRead(this.cnc);
			}
			if (num > 0)
			{
				this.complete_request_written = this.cnc.EndWrite(this.request, result);
			}
			else
			{
				this.complete_request_written = true;
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x00074D64 File Offset: 0x00072F64
		internal void InternalClose()
		{
			this.disposed = true;
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x00074D70 File Offset: 0x00072F70
		public override void Close()
		{
			if (this.sendChunked)
			{
				if (this.disposed)
				{
					return;
				}
				this.disposed = true;
				this.pending.WaitOne();
				byte[] bytes = Encoding.ASCII.GetBytes("0\r\n\r\n");
				string text = null;
				this.cnc.Write(this.request, bytes, 0, bytes.Length, ref text);
				return;
			}
			else
			{
				if (this.isRead)
				{
					if (!this.nextReadCalled)
					{
						this.CheckComplete();
						if (!this.nextReadCalled)
						{
							this.nextReadCalled = true;
							this.cnc.Close(true);
						}
					}
					return;
				}
				if (!this.allowBuffering)
				{
					this.complete_request_written = true;
					if (!this.initRead)
					{
						this.initRead = true;
						WebConnection.InitRead(this.cnc);
					}
					return;
				}
				if (this.disposed || this.requestWritten)
				{
					return;
				}
				long num = this.request.ContentLength;
				if (!this.sendChunked && num != -1L && this.totalWritten != num)
				{
					IOException innerException = new IOException("Cannot close the stream until all bytes are written");
					this.nextReadCalled = true;
					this.cnc.Close(true);
					throw new WebException("Request was cancelled.", innerException, WebExceptionStatus.RequestCanceled);
				}
				this.WriteRequest();
				this.disposed = true;
				return;
			}
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00074EB8 File Offset: 0x000730B8
		internal void KillBuffer()
		{
			this.writeBuffer = null;
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00074EC4 File Offset: 0x000730C4
		public override long Seek(long a, SeekOrigin b)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x00074ECC File Offset: 0x000730CC
		public override void SetLength(long a)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x00074ED4 File Offset: 0x000730D4
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x00074ED8 File Offset: 0x000730D8
		public override bool CanRead
		{
			get
			{
				return !this.disposed && this.isRead;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x00074EF0 File Offset: 0x000730F0
		public override bool CanWrite
		{
			get
			{
				return !this.disposed && !this.isRead;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x00074F0C File Offset: 0x0007310C
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x00074F14 File Offset: 0x00073114
		// (set) Token: 0x060025AA RID: 9642 RVA: 0x00074F1C File Offset: 0x0007311C
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

		// Token: 0x04001733 RID: 5939
		private static byte[] crlf = new byte[]
		{
			13,
			10
		};

		// Token: 0x04001734 RID: 5940
		private bool isRead;

		// Token: 0x04001735 RID: 5941
		private WebConnection cnc;

		// Token: 0x04001736 RID: 5942
		private HttpWebRequest request;

		// Token: 0x04001737 RID: 5943
		private byte[] readBuffer;

		// Token: 0x04001738 RID: 5944
		private int readBufferOffset;

		// Token: 0x04001739 RID: 5945
		private int readBufferSize;

		// Token: 0x0400173A RID: 5946
		private int contentLength;

		// Token: 0x0400173B RID: 5947
		private int totalRead;

		// Token: 0x0400173C RID: 5948
		private long totalWritten;

		// Token: 0x0400173D RID: 5949
		private bool nextReadCalled;

		// Token: 0x0400173E RID: 5950
		private int pendingReads;

		// Token: 0x0400173F RID: 5951
		private int pendingWrites;

		// Token: 0x04001740 RID: 5952
		private ManualResetEvent pending;

		// Token: 0x04001741 RID: 5953
		private bool allowBuffering;

		// Token: 0x04001742 RID: 5954
		private bool sendChunked;

		// Token: 0x04001743 RID: 5955
		private MemoryStream writeBuffer;

		// Token: 0x04001744 RID: 5956
		private bool requestWritten;

		// Token: 0x04001745 RID: 5957
		private byte[] headers;

		// Token: 0x04001746 RID: 5958
		private bool disposed;

		// Token: 0x04001747 RID: 5959
		private bool headersSent;

		// Token: 0x04001748 RID: 5960
		private object locker = new object();

		// Token: 0x04001749 RID: 5961
		private bool initRead;

		// Token: 0x0400174A RID: 5962
		private bool read_eof;

		// Token: 0x0400174B RID: 5963
		private bool complete_request_written;

		// Token: 0x0400174C RID: 5964
		private int read_timeout;

		// Token: 0x0400174D RID: 5965
		private int write_timeout;
	}
}
