using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200003B RID: 59
	public sealed class GL
	{
		// Token: 0x060002E9 RID: 745
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Vertex3(float x, float y, float z);

		// Token: 0x060002EA RID: 746 RVA: 0x00003C40 File Offset: 0x00001E40
		public static void Vertex(Vector3 v)
		{
			GL.INTERNAL_CALL_Vertex(ref v);
		}

		// Token: 0x060002EB RID: 747
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Vertex(ref Vector3 v);

		// Token: 0x060002EC RID: 748 RVA: 0x00003C4C File Offset: 0x00001E4C
		public static void Color(Color c)
		{
			GL.INTERNAL_CALL_Color(ref c);
		}

		// Token: 0x060002ED RID: 749
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Color(ref Color c);

		// Token: 0x060002EE RID: 750 RVA: 0x00003C58 File Offset: 0x00001E58
		public static void TexCoord(Vector3 v)
		{
			GL.INTERNAL_CALL_TexCoord(ref v);
		}

		// Token: 0x060002EF RID: 751
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_TexCoord(ref Vector3 v);

		// Token: 0x060002F0 RID: 752
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TexCoord2(float x, float y);

		// Token: 0x060002F1 RID: 753
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TexCoord3(float x, float y, float z);

		// Token: 0x060002F2 RID: 754
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MultiTexCoord2(int unit, float x, float y);

		// Token: 0x060002F3 RID: 755
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MultiTexCoord3(int unit, float x, float y, float z);

		// Token: 0x060002F4 RID: 756 RVA: 0x00003C64 File Offset: 0x00001E64
		public static void MultiTexCoord(int unit, Vector3 v)
		{
			GL.INTERNAL_CALL_MultiTexCoord(unit, ref v);
		}

		// Token: 0x060002F5 RID: 757
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MultiTexCoord(int unit, ref Vector3 v);

		// Token: 0x060002F6 RID: 758
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Begin(int mode);

		// Token: 0x060002F7 RID: 759
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void End();

		// Token: 0x060002F8 RID: 760
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadOrtho();

		// Token: 0x060002F9 RID: 761
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadPixelMatrix();

		// Token: 0x060002FA RID: 762
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LoadPixelMatrixArgs(float left, float right, float bottom, float top);

		// Token: 0x060002FB RID: 763 RVA: 0x00003C70 File Offset: 0x00001E70
		public static void LoadPixelMatrix(float left, float right, float bottom, float top)
		{
			GL.LoadPixelMatrixArgs(left, right, bottom, top);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00003C7C File Offset: 0x00001E7C
		public static void Viewport(Rect pixelRect)
		{
			GL.INTERNAL_CALL_Viewport(ref pixelRect);
		}

		// Token: 0x060002FD RID: 765
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Viewport(ref Rect pixelRect);

		// Token: 0x060002FE RID: 766 RVA: 0x00003C88 File Offset: 0x00001E88
		public static void LoadProjectionMatrix(Matrix4x4 mat)
		{
			GL.INTERNAL_CALL_LoadProjectionMatrix(ref mat);
		}

		// Token: 0x060002FF RID: 767
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_LoadProjectionMatrix(ref Matrix4x4 mat);

		// Token: 0x06000300 RID: 768
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadIdentity();

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00003C94 File Offset: 0x00001E94
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00003CAC File Offset: 0x00001EAC
		public static Matrix4x4 modelview
		{
			get
			{
				Matrix4x4 result;
				GL.INTERNAL_get_modelview(out result);
				return result;
			}
			set
			{
				GL.INTERNAL_set_modelview(ref value);
			}
		}

		// Token: 0x06000303 RID: 771
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_modelview(out Matrix4x4 value);

		// Token: 0x06000304 RID: 772
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_modelview(ref Matrix4x4 value);

		// Token: 0x06000305 RID: 773 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public static void MultMatrix(Matrix4x4 mat)
		{
			GL.INTERNAL_CALL_MultMatrix(ref mat);
		}

		// Token: 0x06000306 RID: 774
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MultMatrix(ref Matrix4x4 mat);

		// Token: 0x06000307 RID: 775
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PushMatrix();

		// Token: 0x06000308 RID: 776
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PopMatrix();

		// Token: 0x06000309 RID: 777 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public static Matrix4x4 GetGPUProjectionMatrix(Matrix4x4 proj, bool renderIntoTexture)
		{
			Matrix4x4 result;
			GL.INTERNAL_CALL_GetGPUProjectionMatrix(ref proj, renderIntoTexture, out result);
			return result;
		}

		// Token: 0x0600030A RID: 778
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetGPUProjectionMatrix(ref Matrix4x4 proj, bool renderIntoTexture, out Matrix4x4 value);

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600030B RID: 779
		// (set) Token: 0x0600030C RID: 780
		public static extern bool wireframe { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600030D RID: 781
		// (set) Token: 0x0600030E RID: 782
		public static extern bool sRGBWrite { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600030F RID: 783
		// (set) Token: 0x06000310 RID: 784
		public static extern bool invertCulling { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000311 RID: 785
		[Obsolete("Use invertCulling property")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetRevertBackfacing(bool revertBackFaces);

		// Token: 0x06000312 RID: 786 RVA: 0x00003CDC File Offset: 0x00001EDC
		[ExcludeFromDocs]
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			float depth = 1f;
			GL.Clear(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor, [DefaultValue("1.0f")] float depth)
		{
			GL.Internal_Clear(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00003D04 File Offset: 0x00001F04
		private static void Internal_Clear(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			GL.INTERNAL_CALL_Internal_Clear(clearDepth, clearColor, ref backgroundColor, depth);
		}

		// Token: 0x06000315 RID: 789
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_Clear(bool clearDepth, bool clearColor, ref Color backgroundColor, float depth);

		// Token: 0x06000316 RID: 790
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearWithSkybox(bool clearDepth, Camera camera);

		// Token: 0x06000317 RID: 791
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InvalidateState();

		// Token: 0x06000318 RID: 792
		[WrapperlessIcall]
		[Obsolete("IssuePluginEvent(eventID) is deprecated. Use IssuePluginEvent(callback, eventID) instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IssuePluginEvent(int eventID);

		// Token: 0x06000319 RID: 793 RVA: 0x00003D10 File Offset: 0x00001F10
		public static void IssuePluginEvent(IntPtr callback, int eventID)
		{
			if (callback == IntPtr.Zero)
			{
				throw new ArgumentException("Null callback specified.");
			}
			GL.IssuePluginEventInternal(callback, eventID);
		}

		// Token: 0x0600031A RID: 794
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IssuePluginEventInternal(IntPtr callback, int eventID);

		// Token: 0x0600031B RID: 795
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RenderTargetBarrier();

		// Token: 0x040000AA RID: 170
		public const int TRIANGLES = 4;

		// Token: 0x040000AB RID: 171
		public const int TRIANGLE_STRIP = 5;

		// Token: 0x040000AC RID: 172
		public const int QUADS = 7;

		// Token: 0x040000AD RID: 173
		public const int LINES = 1;
	}
}
