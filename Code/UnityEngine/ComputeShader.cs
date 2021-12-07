using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B2 RID: 178
	public sealed class ComputeShader : Object
	{
		// Token: 0x06000AAA RID: 2730
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int FindKernel(string name);

		// Token: 0x06000AAB RID: 2731
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(string name, float val);

		// Token: 0x06000AAC RID: 2732
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInt(string name, int val);

		// Token: 0x06000AAD RID: 2733 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		public void SetVector(string name, Vector4 val)
		{
			ComputeShader.INTERNAL_CALL_SetVector(this, name, ref val);
		}

		// Token: 0x06000AAE RID: 2734
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetVector(ComputeShader self, string name, ref Vector4 val);

		// Token: 0x06000AAF RID: 2735 RVA: 0x0000E9E0 File Offset: 0x0000CBE0
		public void SetFloats(string name, params float[] values)
		{
			this.Internal_SetFloats(name, values);
		}

		// Token: 0x06000AB0 RID: 2736
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetFloats(string name, float[] values);

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		public void SetInts(string name, params int[] values)
		{
			this.Internal_SetInts(name, values);
		}

		// Token: 0x06000AB2 RID: 2738
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetInts(string name, int[] values);

		// Token: 0x06000AB3 RID: 2739
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int kernelIndex, string name, Texture texture);

		// Token: 0x06000AB4 RID: 2740
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBuffer(int kernelIndex, string name, ComputeBuffer buffer);

		// Token: 0x06000AB5 RID: 2741
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispatch(int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);
	}
}
