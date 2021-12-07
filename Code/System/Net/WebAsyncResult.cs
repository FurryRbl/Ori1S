using System;
using System.IO;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200040F RID: 1039
	internal class WebAsyncResult : IAsyncResult
	{
		// Token: 0x060024A2 RID: 9378 RVA: 0x0006E348 File Offset: 0x0006C548
		public WebAsyncResult(AsyncCallback cb, object state)
		{
			this.cb = cb;
			this.state = state;
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0006E36C File Offset: 0x0006C56C
		public WebAsyncResult(HttpWebRequest request, AsyncCallback cb, object state)
		{
			this.cb = cb;
			this.state = state;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x0006E390 File Offset: 0x0006C590
		public WebAsyncResult(AsyncCallback cb, object state, byte[] buffer, int offset, int size)
		{
			this.cb = cb;
			this.state = state;
			this.buffer = buffer;
			this.offset = offset;
			this.size = size;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x0006E3D4 File Offset: 0x0006C5D4
		internal void SetCompleted(bool synch, Exception e)
		{
			this.synch = synch;
			this.exc = e;
			object obj = this.locker;
			lock (obj)
			{
				this.isCompleted = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x0006E444 File Offset: 0x0006C644
		internal void Reset()
		{
			this.callbackDone = false;
			this.exc = null;
			this.response = null;
			this.writeStream = null;
			this.exc = null;
			object obj = this.locker;
			lock (obj)
			{
				this.isCompleted = false;
				if (this.handle != null)
				{
					this.handle.Reset();
				}
			}
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x0006E4C8 File Offset: 0x0006C6C8
		internal void SetCompleted(bool synch, int nbytes)
		{
			this.synch = synch;
			this.nbytes = nbytes;
			this.exc = null;
			object obj = this.locker;
			lock (obj)
			{
				this.isCompleted = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x0006E540 File Offset: 0x0006C740
		internal void SetCompleted(bool synch, Stream writeStream)
		{
			this.synch = synch;
			this.writeStream = writeStream;
			this.exc = null;
			object obj = this.locker;
			lock (obj)
			{
				this.isCompleted = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x0006E5B8 File Offset: 0x0006C7B8
		internal void SetCompleted(bool synch, HttpWebResponse response)
		{
			this.synch = synch;
			this.response = response;
			this.exc = null;
			object obj = this.locker;
			lock (obj)
			{
				this.isCompleted = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x0006E630 File Offset: 0x0006C830
		internal void DoCallback()
		{
			if (!this.callbackDone && this.cb != null)
			{
				this.callbackDone = true;
				this.cb(this);
			}
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x0006E65C File Offset: 0x0006C85C
		internal void WaitUntilComplete()
		{
			if (this.IsCompleted)
			{
				return;
			}
			this.AsyncWaitHandle.WaitOne();
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x0006E678 File Offset: 0x0006C878
		internal bool WaitUntilComplete(int timeout, bool exitContext)
		{
			return this.IsCompleted || this.AsyncWaitHandle.WaitOne(timeout, exitContext);
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x0006E6A0 File Offset: 0x0006C8A0
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x0006E6A8 File Offset: 0x0006C8A8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.isCompleted);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x0006E70C File Offset: 0x0006C90C
		public bool CompletedSynchronously
		{
			get
			{
				return this.synch;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x0006E714 File Offset: 0x0006C914
		public bool IsCompleted
		{
			get
			{
				object obj = this.locker;
				bool result;
				lock (obj)
				{
					result = this.isCompleted;
				}
				return result;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x0006E764 File Offset: 0x0006C964
		internal bool GotException
		{
			get
			{
				return this.exc != null;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x0006E774 File Offset: 0x0006C974
		internal Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x0006E77C File Offset: 0x0006C97C
		// (set) Token: 0x060024B4 RID: 9396 RVA: 0x0006E784 File Offset: 0x0006C984
		internal int NBytes
		{
			get
			{
				return this.nbytes;
			}
			set
			{
				this.nbytes = value;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x0006E790 File Offset: 0x0006C990
		// (set) Token: 0x060024B6 RID: 9398 RVA: 0x0006E798 File Offset: 0x0006C998
		internal IAsyncResult InnerAsyncResult
		{
			get
			{
				return this.innerAsyncResult;
			}
			set
			{
				this.innerAsyncResult = value;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x0006E7A4 File Offset: 0x0006C9A4
		internal Stream WriteStream
		{
			get
			{
				return this.writeStream;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x0006E7AC File Offset: 0x0006C9AC
		internal HttpWebResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x0006E7B4 File Offset: 0x0006C9B4
		internal byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x0006E7BC File Offset: 0x0006C9BC
		internal int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x0006E7C4 File Offset: 0x0006C9C4
		internal int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x040016D8 RID: 5848
		private ManualResetEvent handle;

		// Token: 0x040016D9 RID: 5849
		private bool synch;

		// Token: 0x040016DA RID: 5850
		private bool isCompleted;

		// Token: 0x040016DB RID: 5851
		private AsyncCallback cb;

		// Token: 0x040016DC RID: 5852
		private object state;

		// Token: 0x040016DD RID: 5853
		private int nbytes;

		// Token: 0x040016DE RID: 5854
		private IAsyncResult innerAsyncResult;

		// Token: 0x040016DF RID: 5855
		private bool callbackDone;

		// Token: 0x040016E0 RID: 5856
		private Exception exc;

		// Token: 0x040016E1 RID: 5857
		private HttpWebResponse response;

		// Token: 0x040016E2 RID: 5858
		private Stream writeStream;

		// Token: 0x040016E3 RID: 5859
		private byte[] buffer;

		// Token: 0x040016E4 RID: 5860
		private int offset;

		// Token: 0x040016E5 RID: 5861
		private int size;

		// Token: 0x040016E6 RID: 5862
		private object locker = new object();

		// Token: 0x040016E7 RID: 5863
		public bool EndCalled;

		// Token: 0x040016E8 RID: 5864
		public bool AsyncWriteAll;
	}
}
