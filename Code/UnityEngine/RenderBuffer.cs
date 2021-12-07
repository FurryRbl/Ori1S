using System;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200028E RID: 654
	public struct RenderBuffer
	{
		// Token: 0x060025B4 RID: 9652 RVA: 0x000344C8 File Offset: 0x000326C8
		internal void SetLoadAction(RenderBufferLoadAction action)
		{
			RenderBufferHelper.SetLoadAction(out this, (int)action);
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000344D4 File Offset: 0x000326D4
		internal void SetStoreAction(RenderBufferStoreAction action)
		{
			RenderBufferHelper.SetStoreAction(out this, (int)action);
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x000344E0 File Offset: 0x000326E0
		// (set) Token: 0x060025B7 RID: 9655 RVA: 0x000344E8 File Offset: 0x000326E8
		internal RenderBufferLoadAction loadAction
		{
			get
			{
				return (RenderBufferLoadAction)RenderBufferHelper.GetLoadAction(out this);
			}
			set
			{
				this.SetLoadAction(value);
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x000344F4 File Offset: 0x000326F4
		// (set) Token: 0x060025B9 RID: 9657 RVA: 0x000344FC File Offset: 0x000326FC
		internal RenderBufferStoreAction storeAction
		{
			get
			{
				return (RenderBufferStoreAction)RenderBufferHelper.GetStoreAction(out this);
			}
			set
			{
				this.SetStoreAction(value);
			}
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x00034508 File Offset: 0x00032708
		public IntPtr GetNativeRenderBufferPtr()
		{
			return this.m_BufferPtr;
		}

		// Token: 0x040009E6 RID: 2534
		internal int m_RenderTextureInstanceID;

		// Token: 0x040009E7 RID: 2535
		internal IntPtr m_BufferPtr;
	}
}
