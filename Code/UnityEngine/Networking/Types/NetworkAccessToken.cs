using System;

namespace UnityEngine.Networking.Types
{
	// Token: 0x02000246 RID: 582
	public class NetworkAccessToken
	{
		// Token: 0x06002300 RID: 8960 RVA: 0x0002C054 File Offset: 0x0002A254
		public NetworkAccessToken()
		{
			this.array = new byte[64];
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x0002C06C File Offset: 0x0002A26C
		public NetworkAccessToken(byte[] array)
		{
			this.array = array;
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x0002C07C File Offset: 0x0002A27C
		public NetworkAccessToken(string strArray)
		{
			this.array = Convert.FromBase64String(strArray);
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0002C090 File Offset: 0x0002A290
		public string GetByteString()
		{
			return Convert.ToBase64String(this.array);
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x0002C0A0 File Offset: 0x0002A2A0
		public bool IsValid()
		{
			if (this.array == null || this.array.Length != 64)
			{
				return false;
			}
			bool result = false;
			foreach (byte b in this.array)
			{
				if (b != 0)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x0400093A RID: 2362
		private const int NETWORK_ACCESS_TOKEN_SIZE = 64;

		// Token: 0x0400093B RID: 2363
		public byte[] array;
	}
}
