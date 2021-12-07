using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mono.Unix
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class AbstractUnixEndPoint : EndPoint
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		public AbstractUnixEndPoint(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Cannot be empty.", "path");
			}
			this.path = path;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002150 File Offset: 0x00000350
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002158 File Offset: 0x00000358
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002164 File Offset: 0x00000364
		public override AddressFamily AddressFamily
		{
			get
			{
				return AddressFamily.Unix;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002168 File Offset: 0x00000368
		public override EndPoint Create(SocketAddress socketAddress)
		{
			byte[] array = new byte[socketAddress.Size - 2 - 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = socketAddress[3 + i];
			}
			string @string = Encoding.Default.GetString(array);
			return new AbstractUnixEndPoint(@string);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B8 File Offset: 0x000003B8
		public override SocketAddress Serialize()
		{
			byte[] bytes = Encoding.Default.GetBytes(this.path);
			SocketAddress socketAddress = new SocketAddress(this.AddressFamily, 3 + bytes.Length);
			socketAddress[2] = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				socketAddress[i + 2 + 1] = bytes[i];
			}
			return socketAddress;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002214 File Offset: 0x00000414
		public override string ToString()
		{
			return this.path;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000221C File Offset: 0x0000041C
		public override int GetHashCode()
		{
			return this.path.GetHashCode();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000222C File Offset: 0x0000042C
		public override bool Equals(object o)
		{
			AbstractUnixEndPoint abstractUnixEndPoint = o as AbstractUnixEndPoint;
			return abstractUnixEndPoint != null && abstractUnixEndPoint.path == this.path;
		}

		// Token: 0x0400001E RID: 30
		private string path;
	}
}
