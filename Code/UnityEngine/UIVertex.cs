using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E3 RID: 483
	[UsedByNativeCode]
	public struct UIVertex
	{
		// Token: 0x040005FC RID: 1532
		public Vector3 position;

		// Token: 0x040005FD RID: 1533
		public Vector3 normal;

		// Token: 0x040005FE RID: 1534
		public Color32 color;

		// Token: 0x040005FF RID: 1535
		public Vector2 uv0;

		// Token: 0x04000600 RID: 1536
		public Vector2 uv1;

		// Token: 0x04000601 RID: 1537
		public Vector4 tangent;

		// Token: 0x04000602 RID: 1538
		private static readonly Color32 s_DefaultColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x04000603 RID: 1539
		private static readonly Vector4 s_DefaultTangent = new Vector4(1f, 0f, 0f, -1f);

		// Token: 0x04000604 RID: 1540
		public static UIVertex simpleVert = new UIVertex
		{
			position = Vector3.zero,
			normal = Vector3.back,
			tangent = UIVertex.s_DefaultTangent,
			color = UIVertex.s_DefaultColor,
			uv0 = Vector2.zero,
			uv1 = Vector2.zero
		};
	}
}
