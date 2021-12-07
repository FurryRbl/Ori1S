using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public struct NetworkInstanceId
	{
		// Token: 0x06000227 RID: 551 RVA: 0x0000BD6C File Offset: 0x00009F6C
		public NetworkInstanceId(uint value)
		{
			this.m_Value = value;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000BD90 File Offset: 0x00009F90
		public bool IsEmpty()
		{
			return this.m_Value == 0U;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public override int GetHashCode()
		{
			return (int)this.m_Value;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		public override bool Equals(object obj)
		{
			return obj is NetworkInstanceId && this == (NetworkInstanceId)obj;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000BDC8 File Offset: 0x00009FC8
		public override string ToString()
		{
			return this.m_Value.ToString();
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		public uint Value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public static bool operator ==(NetworkInstanceId c1, NetworkInstanceId c2)
		{
			return c1.m_Value == c2.m_Value;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000BE00 File Offset: 0x0000A000
		public static bool operator !=(NetworkInstanceId c1, NetworkInstanceId c2)
		{
			return c1.m_Value != c2.m_Value;
		}

		// Token: 0x0400011D RID: 285
		[SerializeField]
		private readonly uint m_Value;

		// Token: 0x0400011E RID: 286
		public static NetworkInstanceId Invalid = new NetworkInstanceId(uint.MaxValue);

		// Token: 0x0400011F RID: 287
		internal static NetworkInstanceId Zero = new NetworkInstanceId(0U);
	}
}
