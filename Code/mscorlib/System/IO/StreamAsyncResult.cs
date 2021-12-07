using System;
using System.Threading;

namespace System.IO
{
	// Token: 0x02000255 RID: 597
	internal class StreamAsyncResult : IAsyncResult
	{
		// Token: 0x06001EE7 RID: 7911 RVA: 0x00072508 File Offset: 0x00070708
		public StreamAsyncResult(object state)
		{
			this.state = state;
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00072520 File Offset: 0x00070720
		public void SetComplete(Exception e)
		{
			this.exc = e;
			this.completed = true;
			lock (this)
			{
				if (this.wh != null)
				{
					this.wh.Set();
				}
			}
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x00072584 File Offset: 0x00070784
		public void SetComplete(Exception e, int nbytes)
		{
			this.nbytes = nbytes;
			this.SetComplete(e);
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x00072594 File Offset: 0x00070794
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0007259C File Offset: 0x0007079C
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle result;
				lock (this)
				{
					if (this.wh == null)
					{
						this.wh = new ManualResetEvent(this.completed);
					}
					result = this.wh;
				}
				return result;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x00072604 File Offset: 0x00070804
		public virtual bool CompletedSynchronously
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x00072608 File Offset: 0x00070808
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x00072610 File Offset: 0x00070810
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x00072618 File Offset: 0x00070818
		public int NBytes
		{
			get
			{
				return this.nbytes;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x00072620 File Offset: 0x00070820
		// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x00072628 File Offset: 0x00070828
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

		// Token: 0x04000BA2 RID: 2978
		private object state;

		// Token: 0x04000BA3 RID: 2979
		private bool completed;

		// Token: 0x04000BA4 RID: 2980
		private bool done;

		// Token: 0x04000BA5 RID: 2981
		private Exception exc;

		// Token: 0x04000BA6 RID: 2982
		private int nbytes = -1;

		// Token: 0x04000BA7 RID: 2983
		private ManualResetEvent wh;
	}
}
