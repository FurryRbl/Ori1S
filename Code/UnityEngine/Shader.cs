using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200008C RID: 140
	public sealed class Shader : Object
	{
		// Token: 0x06000835 RID: 2101
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Shader Find(string name);

		// Token: 0x06000836 RID: 2102
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Shader FindBuiltin(string name);

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000837 RID: 2103
		public extern bool isSupported { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000838 RID: 2104
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EnableKeyword(string keyword);

		// Token: 0x06000839 RID: 2105
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisableKeyword(string keyword);

		// Token: 0x0600083A RID: 2106
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsKeywordEnabled(string keyword);

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600083B RID: 2107
		// (set) Token: 0x0600083C RID: 2108
		public extern int maximumLOD { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600083D RID: 2109
		// (set) Token: 0x0600083E RID: 2110
		public static extern int globalMaximumLOD { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600083F RID: 2111
		public extern int renderQueue { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000840 RID: 2112
		internal extern DisableBatchingType disableBatching { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000841 RID: 2113 RVA: 0x0000B864 File Offset: 0x00009A64
		public static void SetGlobalColor(string propertyName, Color color)
		{
			Shader.SetGlobalColor(Shader.PropertyToID(propertyName), color);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0000B874 File Offset: 0x00009A74
		public static void SetGlobalColor(int nameID, Color color)
		{
			Shader.INTERNAL_CALL_SetGlobalColor(nameID, ref color);
		}

		// Token: 0x06000843 RID: 2115
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetGlobalColor(int nameID, ref Color color);

		// Token: 0x06000844 RID: 2116 RVA: 0x0000B880 File Offset: 0x00009A80
		public static void SetGlobalVector(string propertyName, Vector4 vec)
		{
			Shader.SetGlobalColor(propertyName, vec);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0000B890 File Offset: 0x00009A90
		public static void SetGlobalVector(int nameID, Vector4 vec)
		{
			Shader.SetGlobalColor(nameID, vec);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		public static void SetGlobalFloat(string propertyName, float value)
		{
			Shader.SetGlobalFloat(Shader.PropertyToID(propertyName), value);
		}

		// Token: 0x06000847 RID: 2119
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalFloat(int nameID, float value);

		// Token: 0x06000848 RID: 2120 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		public static void SetGlobalInt(string propertyName, int value)
		{
			Shader.SetGlobalFloat(propertyName, (float)value);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0000B8BC File Offset: 0x00009ABC
		public static void SetGlobalInt(int nameID, int value)
		{
			Shader.SetGlobalFloat(nameID, (float)value);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public static void SetGlobalTexture(string propertyName, Texture tex)
		{
			Shader.SetGlobalTexture(Shader.PropertyToID(propertyName), tex);
		}

		// Token: 0x0600084B RID: 2123
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalTexture(int nameID, Texture tex);

		// Token: 0x0600084C RID: 2124 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		public static void SetGlobalMatrix(string propertyName, Matrix4x4 mat)
		{
			Shader.SetGlobalMatrix(Shader.PropertyToID(propertyName), mat);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		public static void SetGlobalMatrix(int nameID, Matrix4x4 mat)
		{
			Shader.INTERNAL_CALL_SetGlobalMatrix(nameID, ref mat);
		}

		// Token: 0x0600084E RID: 2126
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetGlobalMatrix(int nameID, ref Matrix4x4 mat);

		// Token: 0x0600084F RID: 2127
		[Obsolete("SetGlobalTexGenMode is not supported anymore. Use programmable shaders to achieve the same effect.", true)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalTexGenMode(string propertyName, TexGenMode mode);

		// Token: 0x06000850 RID: 2128
		[WrapperlessIcall]
		[Obsolete("SetGlobalTextureMatrixName is not supported anymore. Use programmable shaders to achieve the same effect.", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalTextureMatrixName(string propertyName, string matrixName);

		// Token: 0x06000851 RID: 2129
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalBuffer(string propertyName, ComputeBuffer buffer);

		// Token: 0x06000852 RID: 2130
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int PropertyToID(string name);

		// Token: 0x06000853 RID: 2131
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WarmupAllShaders();
	}
}
