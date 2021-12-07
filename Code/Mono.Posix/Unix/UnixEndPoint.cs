using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mono.Unix
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class UnixEndPoint : EndPoint
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000050B0 File Offset: 0x000032B0
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

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000050FC File Offset: 0x000032FC
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00005104 File Offset: 0x00003304
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005110 File Offset: 0x00003310
		public override AddressFamily AddressFamily
		{
			get
			{
				return AddressFamily.Unix;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005114 File Offset: 0x00003314
		public override EndPoint Create(SocketAddress socketAddress)
		{
			byte[] array = new byte[socketAddress.Size - 2 - 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = socketAddress[i + 2];
			}
			string @string = Encoding.Default.GetString(array);
			return new UnixEndPoint(@string);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005164 File Offset: 0x00003364
		public override SocketAddress Serialize()
		{
			byte[] bytes = Encoding.Default.GetBytes(this.filename);
			SocketAddress socketAddress = new SocketAddress(this.AddressFamily, 2 + bytes.Length + 1);
			for (int i = 0; i < bytes.Length; i++)
			{
				socketAddress[2 + i] = bytes[i];
			}
			socketAddress[2 + bytes.Length] = 0;
			return socketAddress;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000051C4 File Offset: 0x000033C4
		public override string ToString()
		{
			return this.filename;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000051CC File Offset: 0x000033CC
		public override int GetHashCode()
		{
			return this.filename.GetHashCode();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000051DC File Offset: 0x000033DC
		public override bool Equals(object o)
		{
			UnixEndPoint unixEndPoint = o as UnixEndPoint;
			return unixEndPoint != null && unixEndPoint.filename == this.filename;
		}

		// Token: 0x04000060 RID: 96
		private string filename;
	}
}
