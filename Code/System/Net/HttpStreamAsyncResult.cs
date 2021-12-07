using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200031F RID: 799
	internal class HttpStreamAsyncResult : IAsyncResult
	{
		// Token: 0x06001BF5 RID: 7157 RVA: 0x00050954 File Offset: 0x0004EB54
		public void Complete(Exception e)
		{
			this.Error = e;
			this.Complete();
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00050964 File Offset: 0x0004EB64
		public void Complete()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (!this.completed)
				{
					this.completed = true;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.Callback != null)
					{
						this.Callback.BeginInvoke(this, null, null);
					}
				}
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x000509F0 File Offset: 0x0004EBF0
		public object AsyncState
		{
			get
			{
				return this.State;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x000509F8 File Offset: 0x0004EBF8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00050A5C File Offset: 0x0004EC5C
		public bool CompletedSynchronously
		{
			get
			{
				return this.SynchRead == this.Count;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x00050A6C File Offset: 0x0004EC6C
		public bool IsCompleted
		{
			get
			{
				object obj = this.locker;
				bool result;
				lock (obj)
				{
					result = this.completed;
				}
				return result;
			}
		}

		// Token: 0x040011B3 RID: 4531
		private object locker = new object();

		// Token: 0x040011B4 RID: 4532
		private ManualResetEvent handle;

		// Token: 0x040011B5 RID: 4533
		private bool completed;

		// Token: 0x040011B6 RID: 4534
		internal byte[] Buffer;

		// Token: 0x040011B7 RID: 4535
		internal int Offset;

		// Token: 0x040011B8 RID: 4536
		internal int Count;

		// Token: 0x040011B9 RID: 4537
		internal AsyncCallback Callback;

		// Token: 0x040011BA RID: 4538
		internal object State;

		// Token: 0x040011BB RID: 4539
		internal int SynchRead;

		// Token: 0x040011BC RID: 4540
		internal Exception Error;
	}
}
