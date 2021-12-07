using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001CE RID: 462
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DetailPrototype
	{
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x0001A6F8 File Offset: 0x000188F8
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x0001A700 File Offset: 0x00018900
		public GameObject prototype
		{
			get
			{
				return this.m_Prototype;
			}
			set
			{
				this.m_Prototype = value;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x0001A70C File Offset: 0x0001890C
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x0001A714 File Offset: 0x00018914
		public Texture2D prototypeTexture
		{
			get
			{
				return this.m_PrototypeTexture;
			}
			set
			{
				this.m_PrototypeTexture = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0001A720 File Offset: 0x00018920
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x0001A728 File Offset: 0x00018928
		public float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				this.m_MinWidth = value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x0001A734 File Offset: 0x00018934
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x0001A73C File Offset: 0x0001893C
		public float maxWidth
		{
			get
			{
				return this.m_MaxWidth;
			}
			set
			{
				this.m_MaxWidth = value;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x0001A748 File Offset: 0x00018948
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x0001A750 File Offset: 0x00018950
		public float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				this.m_MinHeight = value;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0001A75C File Offset: 0x0001895C
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x0001A764 File Offset: 0x00018964
		public float maxHeight
		{
			get
			{
				return this.m_MaxHeight;
			}
			set
			{
				this.m_MaxHeight = value;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0001A770 File Offset: 0x00018970
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x0001A778 File Offset: 0x00018978
		public float noiseSpread
		{
			get
			{
				return this.m_NoiseSpread;
			}
			set
			{
				this.m_NoiseSpread = value;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0001A784 File Offset: 0x00018984
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x0001A78C File Offset: 0x0001898C
		public float bendFactor
		{
			get
			{
				return this.m_BendFactor;
			}
			set
			{
				this.m_BendFactor = value;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x0001A798 File Offset: 0x00018998
		// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x0001A7A0 File Offset: 0x000189A0
		public Color healthyColor
		{
			get
			{
				return this.m_HealthyColor;
			}
			set
			{
				this.m_HealthyColor = value;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0001A7AC File Offset: 0x000189AC
		// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x0001A7B4 File Offset: 0x000189B4
		public Color dryColor
		{
			get
			{
				return this.m_DryColor;
			}
			set
			{
				this.m_DryColor = value;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0001A7C0 File Offset: 0x000189C0
		// (set) Token: 0x06001BFB RID: 7163 RVA: 0x0001A7C8 File Offset: 0x000189C8
		public DetailRenderMode renderMode
		{
			get
			{
				return (DetailRenderMode)this.m_RenderMode;
			}
			set
			{
				this.m_RenderMode = (int)value;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x0001A7D4 File Offset: 0x000189D4
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x0001A7E4 File Offset: 0x000189E4
		public bool usePrototypeMesh
		{
			get
			{
				return this.m_UsePrototypeMesh != 0;
			}
			set
			{
				this.m_UsePrototypeMesh = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x04000591 RID: 1425
		private GameObject m_Prototype;

		// Token: 0x04000592 RID: 1426
		private Texture2D m_PrototypeTexture;

		// Token: 0x04000593 RID: 1427
		private Color m_HealthyColor = new Color(0.2627451f, 0.9764706f, 0.16470589f, 1f);

		// Token: 0x04000594 RID: 1428
		private Color m_DryColor = new Color(0.8039216f, 0.7372549f, 0.101960786f, 1f);

		// Token: 0x04000595 RID: 1429
		private float m_MinWidth = 1f;

		// Token: 0x04000596 RID: 1430
		private float m_MaxWidth = 2f;

		// Token: 0x04000597 RID: 1431
		private float m_MinHeight = 1f;

		// Token: 0x04000598 RID: 1432
		private float m_MaxHeight = 2f;

		// Token: 0x04000599 RID: 1433
		private float m_NoiseSpread = 0.1f;

		// Token: 0x0400059A RID: 1434
		private float m_BendFactor = 0.1f;

		// Token: 0x0400059B RID: 1435
		private int m_RenderMode = 2;

		// Token: 0x0400059C RID: 1436
		private int m_UsePrototypeMesh;
	}
}
