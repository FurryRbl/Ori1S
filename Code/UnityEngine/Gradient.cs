using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000055 RID: 85
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Gradient
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0000495C File Offset: 0x00002B5C
		[RequiredByNativeCode]
		public Gradient()
		{
			this.Init();
		}

		// Token: 0x06000482 RID: 1154
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x06000483 RID: 1155
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x06000484 RID: 1156 RVA: 0x0000496C File Offset: 0x00002B6C
		~Gradient()
		{
			this.Cleanup();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000049A8 File Offset: 0x00002BA8
		public Color Evaluate(float time)
		{
			Color result;
			Gradient.INTERNAL_CALL_Evaluate(this, time, out result);
			return result;
		}

		// Token: 0x06000486 RID: 1158
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Evaluate(Gradient self, float time, out Color value);

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000487 RID: 1159
		// (set) Token: 0x06000488 RID: 1160
		public extern GradientColorKey[] colorKeys { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000489 RID: 1161
		// (set) Token: 0x0600048A RID: 1162
		public extern GradientAlphaKey[] alphaKeys { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600048B RID: 1163
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys);

		// Token: 0x040000C8 RID: 200
		internal IntPtr m_Ptr;
	}
}
