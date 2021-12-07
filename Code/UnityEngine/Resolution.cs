using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200028D RID: 653
	[UsedByNativeCode]
	public struct Resolution
	{
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x00034444 File Offset: 0x00032644
		// (set) Token: 0x060025AE RID: 9646 RVA: 0x0003444C File Offset: 0x0003264C
		public int width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x00034458 File Offset: 0x00032658
		// (set) Token: 0x060025B0 RID: 9648 RVA: 0x00034460 File Offset: 0x00032660
		public int height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x0003446C File Offset: 0x0003266C
		// (set) Token: 0x060025B2 RID: 9650 RVA: 0x00034474 File Offset: 0x00032674
		public int refreshRate
		{
			get
			{
				return this.m_RefreshRate;
			}
			set
			{
				this.m_RefreshRate = value;
			}
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x00034480 File Offset: 0x00032680
		public override string ToString()
		{
			return UnityString.Format("{0} x {1} @ {2}Hz", new object[]
			{
				this.m_Width,
				this.m_Height,
				this.m_RefreshRate
			});
		}

		// Token: 0x040009E3 RID: 2531
		private int m_Width;

		// Token: 0x040009E4 RID: 2532
		private int m_Height;

		// Token: 0x040009E5 RID: 2533
		private int m_RefreshRate;
	}
}
