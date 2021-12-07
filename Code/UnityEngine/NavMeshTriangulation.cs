using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000170 RID: 368
	[UsedByNativeCode]
	public struct NavMeshTriangulation
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0001826C File Offset: 0x0001646C
		[Obsolete("Use areas instead.")]
		public int[] layers
		{
			get
			{
				return this.areas;
			}
		}

		// Token: 0x04000400 RID: 1024
		public Vector3[] vertices;

		// Token: 0x04000401 RID: 1025
		public int[] indices;

		// Token: 0x04000402 RID: 1026
		public int[] areas;
	}
}
