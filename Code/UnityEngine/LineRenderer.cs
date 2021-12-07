using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	public sealed class LineRenderer : Renderer
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00003064 File Offset: 0x00001264
		public void SetWidth(float start, float end)
		{
			LineRenderer.INTERNAL_CALL_SetWidth(this, start, end);
		}

		// Token: 0x06000228 RID: 552
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetWidth(LineRenderer self, float start, float end);

		// Token: 0x06000229 RID: 553 RVA: 0x00003070 File Offset: 0x00001270
		public void SetColors(Color start, Color end)
		{
			LineRenderer.INTERNAL_CALL_SetColors(this, ref start, ref end);
		}

		// Token: 0x0600022A RID: 554
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetColors(LineRenderer self, ref Color start, ref Color end);

		// Token: 0x0600022B RID: 555 RVA: 0x0000307C File Offset: 0x0000127C
		public void SetVertexCount(int count)
		{
			LineRenderer.INTERNAL_CALL_SetVertexCount(this, count);
		}

		// Token: 0x0600022C RID: 556
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetVertexCount(LineRenderer self, int count);

		// Token: 0x0600022D RID: 557 RVA: 0x00003088 File Offset: 0x00001288
		public void SetPosition(int index, Vector3 position)
		{
			LineRenderer.INTERNAL_CALL_SetPosition(this, index, ref position);
		}

		// Token: 0x0600022E RID: 558
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetPosition(LineRenderer self, int index, ref Vector3 position);

		// Token: 0x0600022F RID: 559
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPositions(Vector3[] positions);

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000230 RID: 560
		// (set) Token: 0x06000231 RID: 561
		public extern bool useWorldSpace { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
