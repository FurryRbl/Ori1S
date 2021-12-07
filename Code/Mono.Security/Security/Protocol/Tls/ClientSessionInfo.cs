using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000085 RID: 133
	internal class ClientSessionInfo : IDisposable
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0001D54C File Offset: 0x0001B74C
		public ClientSessionInfo(string hostname, byte[] id)
		{
			this.host = hostname;
			this.sid = id;
			this.KeepAlive();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001D568 File Offset: 0x0001B768
		static ClientSessionInfo()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_TLS_SESSION_CACHE_TIMEOUT");
			if (environmentVariable == null)
			{
				ClientSessionInfo.ValidityInterval = 180;
			}
			else
			{
				try
				{
					ClientSessionInfo.ValidityInterval = int.Parse(environmentVariable);
				}
				catch
				{
					ClientSessionInfo.ValidityInterval = 180;
				}
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
		~ClientSessionInfo()
		{
			this.Dispose(false);
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0001D610 File Offset: 0x0001B810
		public string HostName
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0001D618 File Offset: 0x0001B818
		public byte[] Id
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0001D620 File Offset: 0x0001B820
		public bool Valid
		{
			get
			{
				return this.masterSecret != null && this.validuntil > DateTime.UtcNow;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001D640 File Offset: 0x0001B840
		public void GetContext(Context context)
		{
			this.CheckDisposed();
			if (context.MasterSecret != null)
			{
				this.masterSecret = (byte[])context.MasterSecret.Clone();
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001D674 File Offset: 0x0001B874
		public void SetContext(Context context)
		{
			this.CheckDisposed();
			if (this.masterSecret != null)
			{
				context.MasterSecret = (byte[])this.masterSecret.Clone();
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001D6A0 File Offset: 0x0001B8A0
		public void KeepAlive()
		{
			this.CheckDisposed();
			this.validuntil = DateTime.UtcNow.AddSeconds((double)ClientSessionInfo.ValidityInterval);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.validuntil = DateTime.MinValue;
				this.host = null;
				this.sid = null;
				if (this.masterSecret != null)
				{
					Array.Clear(this.masterSecret, 0, this.masterSecret.Length);
					this.masterSecret = null;
				}
			}
			this.disposed = true;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001D73C File Offset: 0x0001B93C
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				string text = Locale.GetText("Cache session information were disposed.");
				throw new ObjectDisposedException(text);
			}
		}

		// Token: 0x04000255 RID: 597
		private const int DefaultValidityInterval = 180;

		// Token: 0x04000256 RID: 598
		private static readonly int ValidityInterval;

		// Token: 0x04000257 RID: 599
		private bool disposed;

		// Token: 0x04000258 RID: 600
		private DateTime validuntil;

		// Token: 0x04000259 RID: 601
		private string host;

		// Token: 0x0400025A RID: 602
		private byte[] sid;

		// Token: 0x0400025B RID: 603
		private byte[] masterSecret;
	}
}
