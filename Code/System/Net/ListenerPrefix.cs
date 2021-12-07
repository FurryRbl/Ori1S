using System;

namespace System.Net
{
	// Token: 0x02000332 RID: 818
	internal sealed class ListenerPrefix
	{
		// Token: 0x06001D02 RID: 7426 RVA: 0x0005616C File Offset: 0x0005436C
		public ListenerPrefix(string prefix)
		{
			this.original = prefix;
			this.Parse(prefix);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x00056184 File Offset: 0x00054384
		public override string ToString()
		{
			return this.original;
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0005618C File Offset: 0x0005438C
		// (set) Token: 0x06001D05 RID: 7429 RVA: 0x00056194 File Offset: 0x00054394
		public IPAddress[] Addresses
		{
			get
			{
				return this.addresses;
			}
			set
			{
				this.addresses = value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x000561A0 File Offset: 0x000543A0
		public bool Secure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x000561A8 File Offset: 0x000543A8
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x000561B0 File Offset: 0x000543B0
		public int Port
		{
			get
			{
				return (int)this.port;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x000561B8 File Offset: 0x000543B8
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000561C0 File Offset: 0x000543C0
		public override bool Equals(object o)
		{
			ListenerPrefix listenerPrefix = o as ListenerPrefix;
			return listenerPrefix != null && this.original == listenerPrefix.original;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000561F0 File Offset: 0x000543F0
		public override int GetHashCode()
		{
			return this.original.GetHashCode();
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00056200 File Offset: 0x00054400
		private void Parse(string uri)
		{
			int num = (!uri.StartsWith("http://")) ? -1 : 80;
			if (num == -1)
			{
				int num2 = (!uri.StartsWith("https://")) ? -1 : 443;
				this.secure = true;
			}
			int length = uri.Length;
			int num3 = uri.IndexOf(':') + 3;
			if (num3 >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num4 = uri.IndexOf(':', num3, length - num3);
			if (num4 > 0)
			{
				this.host = uri.Substring(num3, num4 - num3);
				int num5 = uri.IndexOf('/', num4, length - num4);
				this.port = (ushort)int.Parse(uri.Substring(num4 + 1, num5 - num4 - 1));
				this.path = uri.Substring(num5);
			}
			else
			{
				int num5 = uri.IndexOf('/', num3, length - num3);
				this.host = uri.Substring(num3, num5 - num3);
				this.path = uri.Substring(num5);
			}
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x00056304 File Offset: 0x00054504
		public static void CheckUri(string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			int num = (!uri.StartsWith("http://")) ? -1 : 80;
			if (num == -1)
			{
				num = ((!uri.StartsWith("https://")) ? -1 : 443);
			}
			if (num == -1)
			{
				throw new ArgumentException("Only 'http' and 'https' schemes are supported.");
			}
			int length = uri.Length;
			int num2 = uri.IndexOf(':') + 3;
			if (num2 >= length)
			{
				throw new ArgumentException("No host specified.");
			}
			int num3 = uri.IndexOf(':', num2, length - num2);
			if (num2 == num3)
			{
				throw new ArgumentException("No host specified.");
			}
			if (num3 > 0)
			{
				int num4 = uri.IndexOf('/', num3, length - num3);
				if (num4 == -1)
				{
					throw new ArgumentException("No path specified.");
				}
				try
				{
					int num5 = int.Parse(uri.Substring(num3 + 1, num4 - num3 - 1));
					if (num5 <= 0 || num5 >= 65536)
					{
						throw new Exception();
					}
				}
				catch
				{
					throw new ArgumentException("Invalid port.");
				}
			}
			else
			{
				int num4 = uri.IndexOf('/', num2, length - num2);
				if (num4 == -1)
				{
					throw new ArgumentException("No path specified.");
				}
			}
			if (uri[uri.Length - 1] != '/')
			{
				throw new ArgumentException("The prefix must end with '/'");
			}
		}

		// Token: 0x04001225 RID: 4645
		private string original;

		// Token: 0x04001226 RID: 4646
		private string host;

		// Token: 0x04001227 RID: 4647
		private ushort port;

		// Token: 0x04001228 RID: 4648
		private string path;

		// Token: 0x04001229 RID: 4649
		private bool secure;

		// Token: 0x0400122A RID: 4650
		private IPAddress[] addresses;

		// Token: 0x0400122B RID: 4651
		public HttpListener Listener;
	}
}
