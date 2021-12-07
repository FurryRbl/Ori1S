using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000177 RID: 375
	public struct OffMeshLinkData
	{
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x000183FC File Offset: 0x000165FC
		public bool valid
		{
			get
			{
				return this.m_Valid != 0;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x0001840C File Offset: 0x0001660C
		public bool activated
		{
			get
			{
				return this.m_Activated != 0;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0001841C File Offset: 0x0001661C
		public OffMeshLinkType linkType
		{
			get
			{
				return this.m_LinkType;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x00018424 File Offset: 0x00016624
		public Vector3 startPos
		{
			get
			{
				return this.m_StartPos;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0001842C File Offset: 0x0001662C
		public Vector3 endPos
		{
			get
			{
				return this.m_EndPos;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00018434 File Offset: 0x00016634
		public OffMeshLink offMeshLink
		{
			get
			{
				return this.GetOffMeshLinkInternal(this.m_InstanceID);
			}
		}

		// Token: 0x060017EF RID: 6127
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern OffMeshLink GetOffMeshLinkInternal(int instanceID);

		// Token: 0x04000411 RID: 1041
		private int m_Valid;

		// Token: 0x04000412 RID: 1042
		private int m_Activated;

		// Token: 0x04000413 RID: 1043
		private int m_InstanceID;

		// Token: 0x04000414 RID: 1044
		private OffMeshLinkType m_LinkType;

		// Token: 0x04000415 RID: 1045
		private Vector3 m_StartPos;

		// Token: 0x04000416 RID: 1046
		private Vector3 m_EndPos;
	}
}
