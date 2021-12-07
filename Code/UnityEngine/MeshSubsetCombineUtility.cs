using System;

namespace UnityEngine
{
	// Token: 0x02000336 RID: 822
	internal class MeshSubsetCombineUtility
	{
		// Token: 0x02000337 RID: 823
		public struct MeshInstance
		{
			// Token: 0x04000C5C RID: 3164
			public int meshInstanceID;

			// Token: 0x04000C5D RID: 3165
			public int rendererInstanceID;

			// Token: 0x04000C5E RID: 3166
			public int additionalVertexStreamsMeshInstanceID;

			// Token: 0x04000C5F RID: 3167
			public Matrix4x4 transform;

			// Token: 0x04000C60 RID: 3168
			public Vector4 lightmapScaleOffset;

			// Token: 0x04000C61 RID: 3169
			public Vector4 realtimeLightmapScaleOffset;
		}

		// Token: 0x02000338 RID: 824
		public struct SubMeshInstance
		{
			// Token: 0x04000C62 RID: 3170
			public int meshInstanceID;

			// Token: 0x04000C63 RID: 3171
			public int vertexOffset;

			// Token: 0x04000C64 RID: 3172
			public int gameObjectInstanceID;

			// Token: 0x04000C65 RID: 3173
			public int subMeshIndex;

			// Token: 0x04000C66 RID: 3174
			public Matrix4x4 transform;
		}
	}
}
