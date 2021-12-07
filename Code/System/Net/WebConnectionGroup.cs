using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000415 RID: 1045
	internal class WebConnectionGroup
	{
		// Token: 0x06002573 RID: 9587 RVA: 0x0007355C File Offset: 0x0007175C
		public WebConnectionGroup(ServicePoint sPoint, string name)
		{
			this.sPoint = sPoint;
			this.name = name;
			this.connections = new ArrayList(1);
			this.queue = new Queue();
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x0007358C File Offset: 0x0007178C
		public void Close()
		{
			ArrayList obj = this.connections;
			lock (obj)
			{
				int count = this.connections.Count;
				for (int i = 0; i < count; i++)
				{
					WeakReference weakReference = (WeakReference)this.connections[i];
					WebConnection webConnection = weakReference.Target as WebConnection;
					if (webConnection != null)
					{
						webConnection.Close(false);
					}
				}
				this.connections.Clear();
			}
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x00073630 File Offset: 0x00071830
		public WebConnection GetConnection(HttpWebRequest request)
		{
			WebConnection webConnection = null;
			ArrayList obj = this.connections;
			lock (obj)
			{
				int count = this.connections.Count;
				ArrayList arrayList = null;
				for (int i = 0; i < count; i++)
				{
					WeakReference weakReference = (WeakReference)this.connections[i];
					webConnection = (weakReference.Target as WebConnection);
					if (webConnection == null)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList(1);
						}
						arrayList.Add(i);
					}
				}
				if (arrayList != null)
				{
					for (int j = arrayList.Count - 1; j >= 0; j--)
					{
						this.connections.RemoveAt((int)arrayList[j]);
					}
				}
				webConnection = this.CreateOrReuseConnection(request);
			}
			return webConnection;
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x00073728 File Offset: 0x00071928
		private static void PrepareSharingNtlm(WebConnection cnc, HttpWebRequest request)
		{
			if (!cnc.NtlmAuthenticated)
			{
				return;
			}
			bool flag = false;
			NetworkCredential ntlmCredential = cnc.NtlmCredential;
			NetworkCredential credential = request.Credentials.GetCredential(request.RequestUri, "NTLM");
			if (ntlmCredential.Domain != credential.Domain || ntlmCredential.UserName != credential.UserName || ntlmCredential.Password != credential.Password)
			{
				flag = true;
			}
			if (!flag)
			{
				bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
				bool unsafeAuthenticatedConnectionSharing2 = cnc.UnsafeAuthenticatedConnectionSharing;
				flag = (!unsafeAuthenticatedConnectionSharing || unsafeAuthenticatedConnectionSharing != unsafeAuthenticatedConnectionSharing2);
			}
			if (flag)
			{
				cnc.Close(false);
				cnc.ResetNtlm();
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000737E0 File Offset: 0x000719E0
		private WebConnection CreateOrReuseConnection(HttpWebRequest request)
		{
			int num = this.connections.Count;
			WebConnection webConnection;
			for (int i = 0; i < num; i++)
			{
				WeakReference weakReference = this.connections[i] as WeakReference;
				webConnection = (weakReference.Target as WebConnection);
				if (webConnection == null)
				{
					this.connections.RemoveAt(i);
					num--;
					i--;
				}
				else if (!webConnection.Busy)
				{
					WebConnectionGroup.PrepareSharingNtlm(webConnection, request);
					return webConnection;
				}
			}
			if (this.sPoint.ConnectionLimit > num)
			{
				webConnection = new WebConnection(this, this.sPoint);
				this.connections.Add(new WeakReference(webConnection));
				return webConnection;
			}
			if (this.rnd == null)
			{
				this.rnd = new Random();
			}
			int index = (num <= 1) ? 0 : this.rnd.Next(0, num - 1);
			WeakReference weakReference2 = (WeakReference)this.connections[index];
			webConnection = (weakReference2.Target as WebConnection);
			if (webConnection == null)
			{
				webConnection = new WebConnection(this, this.sPoint);
				this.connections.RemoveAt(index);
				this.connections.Add(new WeakReference(webConnection));
			}
			return webConnection;
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x0007391C File Offset: 0x00071B1C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x00073924 File Offset: 0x00071B24
		internal Queue Queue
		{
			get
			{
				return this.queue;
			}
		}

		// Token: 0x0400172E RID: 5934
		private ServicePoint sPoint;

		// Token: 0x0400172F RID: 5935
		private string name;

		// Token: 0x04001730 RID: 5936
		private ArrayList connections;

		// Token: 0x04001731 RID: 5937
		private Random rnd;

		// Token: 0x04001732 RID: 5938
		private Queue queue;
	}
}
