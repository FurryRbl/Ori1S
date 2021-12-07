using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mono.Posix
{
	// Token: 0x02000076 RID: 118
	[Obsolete("Use Mono.Unix.UnixEndPoint")]
	[Serializable]
	public class UnixEndPoint : EndPoint
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x0000D830 File Offset: 0x0000BA30
		public UnixEndPoint(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			if (filename == string.Empty)
			{
				throw new ArgumentException("Cannot be empty.", "filename");
			}
			this.filename = filename;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x0000D87C File Offset: 0x0000BA7C
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x0000D884 File Offset: 0x0000BA84
		public string Filename
		{
			get
			{
				return this.filename;
			}
			set
			{
				this.filename = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000D890 File Offset: 0x0000BA90
		public override AddressFamily AddressFamily
		{
			get
			{
				return AddressFamily.Unix;
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000D894 File Offset: 0x0000BA94
		public override EndPoint Create(SocketAddress socketAddress)
		{
			byte[] array = new byte[socketAddress.Size - 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = socketAddress[i + 2];
			}
			string @string = Encoding.Default.GetString(array);
			return new UnixEndPoint(@string);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		public override SocketAddress Serialize()
		{
			byte[] bytes = Encoding.Default.GetBytes(this.filename);
			SocketAddress socketAddress = new SocketAddress(this.AddressFamily, bytes.Length + 2);
			for (int i = 0; i < bytes.Length; i++)
			{
				socketAddress[i + 2] = bytes[i];
			}
			return socketAddress;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000D934 File Offset: 0x0000BB34
		public override string ToString()
		{
			return this.filename;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000D93C File Offset: 0x0000BB3C
		public override int GetHashCode()
		{
			return this.filename.GetHashCode();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000D94C File Offset: 0x0000BB4C
		public override bool Equals(object o)
		{
			UnixEndPoint unixEndPoint = o as UnixEndPoint;
			return unixEndPoint != null && unixEndPoint.filename == this.filename;
		}

		// Token: 0x0400041A RID: 1050
		private string filename;
	}
}
