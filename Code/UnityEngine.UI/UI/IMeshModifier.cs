using System;

namespace UnityEngine.UI
{
	// Token: 0x020000A4 RID: 164
	public interface IMeshModifier
	{
		// Token: 0x060005C5 RID: 1477
		[Obsolete("use IMeshModifier.ModifyMesh (VertexHelper verts) instead", false)]
		void ModifyMesh(Mesh mesh);

		// Token: 0x060005C6 RID: 1478
		void ModifyMesh(VertexHelper verts);
	}
}
