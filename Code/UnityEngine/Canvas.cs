using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E5 RID: 485
	public sealed class Canvas : Behaviour
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001D50 RID: 7504 RVA: 0x0001BB6C File Offset: 0x00019D6C
		// (remove) Token: 0x06001D51 RID: 7505 RVA: 0x0001BB84 File Offset: 0x00019D84
		public static event Canvas.WillRenderCanvases willRenderCanvases;

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001D52 RID: 7506
		// (set) Token: 0x06001D53 RID: 7507
		public extern RenderMode renderMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001D54 RID: 7508
		public extern bool isRootCanvas { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001D55 RID: 7509
		// (set) Token: 0x06001D56 RID: 7510
		public extern Camera worldCamera { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x0001BB9C File Offset: 0x00019D9C
		public Rect pixelRect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_pixelRect(out result);
				return result;
			}
		}

		// Token: 0x06001D58 RID: 7512
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pixelRect(out Rect value);

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001D59 RID: 7513
		// (set) Token: 0x06001D5A RID: 7514
		public extern float scaleFactor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001D5B RID: 7515
		// (set) Token: 0x06001D5C RID: 7516
		public extern float referencePixelsPerUnit { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001D5D RID: 7517
		// (set) Token: 0x06001D5E RID: 7518
		public extern bool overridePixelPerfect { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001D5F RID: 7519
		// (set) Token: 0x06001D60 RID: 7520
		public extern bool pixelPerfect { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001D61 RID: 7521
		// (set) Token: 0x06001D62 RID: 7522
		public extern float planeDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001D63 RID: 7523
		public extern int renderOrder { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001D64 RID: 7524
		// (set) Token: 0x06001D65 RID: 7525
		public extern bool overrideSorting { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001D66 RID: 7526
		// (set) Token: 0x06001D67 RID: 7527
		public extern int sortingOrder { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001D68 RID: 7528
		// (set) Token: 0x06001D69 RID: 7529
		public extern int targetDisplay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001D6A RID: 7530
		// (set) Token: 0x06001D6B RID: 7531
		public extern int sortingLayerID { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001D6C RID: 7532
		public extern int cachedSortingLayerValue { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001D6D RID: 7533
		// (set) Token: 0x06001D6E RID: 7534
		public extern string sortingLayerName { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001D6F RID: 7535
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Material GetDefaultCanvasMaterial();

		// Token: 0x06001D70 RID: 7536
		[Obsolete("Shared default material now used for text and general UI elements, call Canvas.GetDefaultCanvasMaterial()")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Material GetDefaultCanvasTextMaterial();

		// Token: 0x06001D71 RID: 7537 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		[RequiredByNativeCode]
		private static void SendWillRenderCanvases()
		{
			if (Canvas.willRenderCanvases != null)
			{
				Canvas.willRenderCanvases();
			}
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0001BBCC File Offset: 0x00019DCC
		public static void ForceUpdateCanvases()
		{
			Canvas.SendWillRenderCanvases();
		}

		// Token: 0x02000349 RID: 841
		// (Invoke) Token: 0x0600287E RID: 10366
		public delegate void WillRenderCanvases();
	}
}
