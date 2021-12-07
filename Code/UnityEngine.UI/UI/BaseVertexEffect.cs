using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x020000A1 RID: 161
	[Obsolete("Use BaseMeshEffect instead", true)]
	public abstract class BaseVertexEffect
	{
		// Token: 0x060005BC RID: 1468
		[Obsolete("Use BaseMeshEffect.ModifyMeshes instead", true)]
		public abstract void ModifyVertices(List<UIVertex> vertices);
	}
}
