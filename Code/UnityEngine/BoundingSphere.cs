using System;

namespace UnityEngine
{
	// Token: 0x0200004F RID: 79
	public struct BoundingSphere
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x00004768 File Offset: 0x00002968
		public BoundingSphere(Vector3 pos, float rad)
		{
			this.position = pos;
			this.radius = rad;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00004778 File Offset: 0x00002978
		public BoundingSphere(Vector4 packedSphere)
		{
			this.position = new Vector3(packedSphere.x, packedSphere.y, packedSphere.z);
			this.radius = packedSphere.w;
		}

		// Token: 0x040000B7 RID: 183
		public Vector3 position;

		// Token: 0x040000B8 RID: 184
		public float radius;
	}
}
