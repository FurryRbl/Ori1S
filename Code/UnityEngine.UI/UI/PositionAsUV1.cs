using System;

namespace UnityEngine.UI
{
	// Token: 0x020000A6 RID: 166
	[AddComponentMenu("UI/Effects/Position As UV1", 16)]
	public class PositionAsUV1 : BaseMeshEffect
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x00019024 File Offset: 0x00017224
		protected PositionAsUV1()
		{
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001902C File Offset: 0x0001722C
		public override void ModifyMesh(VertexHelper vh)
		{
			UIVertex vertex = default(UIVertex);
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				vertex.uv1 = new Vector2(vertex.position.x, vertex.position.y);
				vh.SetUIVertex(vertex, i);
			}
		}
	}
}
