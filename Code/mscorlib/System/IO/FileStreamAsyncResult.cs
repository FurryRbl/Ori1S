using System;
using System.Threading;

namespace System.IO
{
	// Token: 0x02000244 RID: 580
	internal class FileStreamAsyncResult : IAsyncResult
	{
		// Token: 0x06001E1F RID: 7711 RVA: 0x0006FB98 File Offset: 0x0006DD98
		public FileStreamAsyncResult(AsyncCallback cb, object state)
		{
			this.state = state;
			this.realcb = cb;
			if (this.realcb != null)
			{
				this.cb = new AsyncCallback(FileStreamAsyncResult.CBWrapper);
			}
			this.wh = new ManualResetEvent(false);
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0006FBD8 File Offset: 0x0006DDD8
		private static void CBWrapper(IAsyncResult ares)
		{
			FileStreamAsyncResult fileStreamAsyncResult = (FileStreamAsyncResult)ares;
			fileStreamAsyncResult.realcb.BeginInvoke(ares, null, null);
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x0006FBFC File Offset: 0x0006DDFC
		public void SetComplete(Exception e)
		{
			this.exc = e;
			this.completed = true;
			this.wh.Set();
			if (this.cb != null)
			{
				this.cb(this);
			}
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0006FC30 File Offset: 0x0006DE30
		public void SetComplete(Exception e, int nbytes)
		{
			this.BytesRead = nbytes;
			this.SetComplete(e);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0006FC40 File Offset: 0x0006DE40
		public void SetComplete(Exception e, int nbytes, bool synch)
		{
			this.completedSynch = synch;
			this.SetComplete(e, nbytes);
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x0006FC54 File Offset: 0x0006DE54
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x0006FC5C File Offset: 0x0006DE5C
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynch;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x0006FC64 File Offset: 0x0006DE64
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.wh;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x0006FC6C File Offset: 0x0006DE6C
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x0006FC74 File Offset: 0x0006DE74
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x0006FC7C File Offset: 0x0006DE7C
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x0006FC84 File Offset: 0x0006DE84
		public bool Done
		{
			get
			{
				return this.done;
			}
			set
			{
				this.done = value;
			}
		}

		// Token: 0x04000B43 RID: 2883
		private object state;

		// Token: 0x04000B44 RID: 2884
		private bool completed;

		// Token: 0x04000B45 RID: 2885
		private bool done;

		// Token: 0x04000B46 RID: 2886
		private Exception exc;

		// Token: 0x04000B47 RID: 2887
		private ManualResetEvent wh;

		// Token: 0x04000B48 RID: 2888
		private AsyncCallback cb;

		// Token: 0x04000B49 RID: 2889
		private bool completedSynch;

		// Token: 0x04000B4A RID: 2890
		public byte[] Buffer;

		// Token: 0x04000B4B RID: 2891
		public int Offset;

		// Token: 0x04000B4C RID: 2892
		public int Count;

		// Token: 0x04000B4D RID: 2893
		public int OriginalCount;

		// Token: 0x04000B4E RID: 2894
		public int BytesRead;

		// Token: 0x04000B4F RID: 2895
		private AsyncCallback realcb;
	}
}
