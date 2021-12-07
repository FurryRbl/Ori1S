using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public struct NetworkSceneId
	{
		// Token: 0x06000388 RID: 904 RVA: 0x000128D4 File Offset: 0x00010AD4
		public NetworkSceneId(uint value)
		{
			this.m_Value = value;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000128E0 File Offset: 0x00010AE0
		public bool IsEmpty()
		{
			return this.m_Value == 0U;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000128EC File Offset: 0x00010AEC
		public override int GetHashCode()
		{
			return (int)this.m_Value;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000128F4 File Offset: 0x00010AF4
		public override bool Equals(object obj)
		{
			return obj is NetworkSceneId && this == (NetworkSceneId)obj;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00012918 File Offset: 0x00010B18
		public override string ToString()
		{
			return this.m_Value.ToString();
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00012928 File Offset: 0x00010B28
		public uint Value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00012930 File Offset: 0x00010B30
		public static bool operator ==(NetworkSceneId c1, NetworkSceneId c2)
		{
			return c1.m_Value == c2.m_Value;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00012944 File Offset: 0x00010B44
		public static bool operator !=(NetworkSceneId c1, NetworkSceneId c2)
		{
			return c1.m_Value != c2.m_Value;
		}

		// Token: 0x04000190 RID: 400
		[SerializeField]
		private uint m_Value;
	}
}
