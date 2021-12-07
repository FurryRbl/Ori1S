using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x020000A3 RID: 163
	[Obsolete("Use IMeshModifier instead", true)]
	public interface IVertexModifier
	{
		// Token: 0x060005C4 RID: 1476
		[Obsolete("use IMeshModifier.ModifyMesh (VertexHelper verts)  instead", true)]
		void ModifyVertices(List<UIVertex> verts);
	}
}
