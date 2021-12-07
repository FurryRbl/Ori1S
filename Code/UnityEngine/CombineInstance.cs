using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	public struct CombineInstance
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00002A68 File Offset: 0x00000C68
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00002A78 File Offset: 0x00000C78
		public Mesh mesh
		{
			get
			{
				return this.InternalGetMesh(this.m_MeshInstanceID);
			}
			set
			{
				this.m_MeshInstanceID = ((!(value != null)) ? 0 : value.GetInstanceID());
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00002A98 File Offset: 0x00000C98
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public int subMeshIndex
		{
			get
			{
				return this.m_SubMeshIndex;
			}
			set
			{
				this.m_SubMeshIndex = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00002AAC File Offset: 0x00000CAC
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x0600014A RID: 330
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Mesh InternalGetMesh(int instanceID);

		// Token: 0x04000080 RID: 128
		private int m_MeshInstanceID;

		// Token: 0x04000081 RID: 129
		private int m_SubMeshIndex;

		// Token: 0x04000082 RID: 130
		private Matrix4x4 m_Transform;
	}
}
