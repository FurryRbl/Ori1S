using System;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200028F RID: 655
	public struct RenderTargetSetup
	{
		// Token: 0x060025BB RID: 9659 RVA: 0x00034510 File Offset: 0x00032710
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face, RenderBufferLoadAction[] colorLoad, RenderBufferStoreAction[] colorStore, RenderBufferLoadAction depthLoad, RenderBufferStoreAction depthStore)
		{
			this.color = color;
			this.depth = depth;
			this.mipLevel = mip;
			this.cubemapFace = face;
			this.colorLoad = colorLoad;
			this.colorStore = colorStore;
			this.depthLoad = depthLoad;
			this.depthStore = depthStore;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x00034550 File Offset: 0x00032750
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth)
		{
			this = new RenderTargetSetup(new RenderBuffer[]
			{
				color
			}, depth);
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x0003456C File Offset: 0x0003276C
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel)
		{
			this = new RenderTargetSetup(new RenderBuffer[]
			{
				color
			}, depth, mipLevel);
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0003458C File Offset: 0x0003278C
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel, CubemapFace face)
		{
			this = new RenderTargetSetup(new RenderBuffer[]
			{
				color
			}, depth, mipLevel, face);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000345AC File Offset: 0x000327AC
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth)
		{
			this = new RenderTargetSetup(color, depth, 0, CubemapFace.Unknown);
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000345B8 File Offset: 0x000327B8
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mipLevel)
		{
			this = new RenderTargetSetup(color, depth, mipLevel, CubemapFace.Unknown);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000345C4 File Offset: 0x000327C4
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face)
		{
			this = new RenderTargetSetup(color, depth, mip, face, RenderTargetSetup.LoadActions(color), RenderTargetSetup.StoreActions(color), depth.loadAction, depth.storeAction);
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000345F8 File Offset: 0x000327F8
		internal static RenderBufferLoadAction[] LoadActions(RenderBuffer[] buf)
		{
			RenderBufferLoadAction[] array = new RenderBufferLoadAction[buf.Length];
			for (int i = 0; i < buf.Length; i++)
			{
				array[i] = buf[i].loadAction;
				buf[i].loadAction = RenderBufferLoadAction.Load;
			}
			return array;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x00034640 File Offset: 0x00032840
		internal static RenderBufferStoreAction[] StoreActions(RenderBuffer[] buf)
		{
			RenderBufferStoreAction[] array = new RenderBufferStoreAction[buf.Length];
			for (int i = 0; i < buf.Length; i++)
			{
				array[i] = buf[i].storeAction;
				buf[i].storeAction = RenderBufferStoreAction.Store;
			}
			return array;
		}

		// Token: 0x040009E8 RID: 2536
		public RenderBuffer[] color;

		// Token: 0x040009E9 RID: 2537
		public RenderBuffer depth;

		// Token: 0x040009EA RID: 2538
		public int mipLevel;

		// Token: 0x040009EB RID: 2539
		public CubemapFace cubemapFace;

		// Token: 0x040009EC RID: 2540
		public RenderBufferLoadAction[] colorLoad;

		// Token: 0x040009ED RID: 2541
		public RenderBufferStoreAction[] colorStore;

		// Token: 0x040009EE RID: 2542
		public RenderBufferLoadAction depthLoad;

		// Token: 0x040009EF RID: 2543
		public RenderBufferStoreAction depthStore;
	}
}
