using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001DF RID: 479
	[UsedByNativeCode]
	public struct CharacterInfo
	{
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0001B468 File Offset: 0x00019668
		// (set) Token: 0x06001D0E RID: 7438 RVA: 0x0001B474 File Offset: 0x00019674
		public int advance
		{
			get
			{
				return (int)this.width;
			}
			set
			{
				this.width = (float)value;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0001B480 File Offset: 0x00019680
		// (set) Token: 0x06001D10 RID: 7440 RVA: 0x0001B490 File Offset: 0x00019690
		public int glyphWidth
		{
			get
			{
				return (int)this.vert.width;
			}
			set
			{
				this.vert.width = (float)value;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0001B4A0 File Offset: 0x000196A0
		// (set) Token: 0x06001D12 RID: 7442 RVA: 0x0001B4B0 File Offset: 0x000196B0
		public int glyphHeight
		{
			get
			{
				return (int)(-(int)this.vert.height);
			}
			set
			{
				float height = this.vert.height;
				this.vert.height = (float)(-(float)value);
				this.vert.y = this.vert.y + (height - this.vert.height);
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0001B4F8 File Offset: 0x000196F8
		// (set) Token: 0x06001D14 RID: 7444 RVA: 0x0001B508 File Offset: 0x00019708
		public int bearing
		{
			get
			{
				return (int)this.vert.x;
			}
			set
			{
				this.vert.x = (float)value;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0001B518 File Offset: 0x00019718
		// (set) Token: 0x06001D16 RID: 7446 RVA: 0x0001B534 File Offset: 0x00019734
		public int minY
		{
			get
			{
				return (int)(this.vert.y + this.vert.height);
			}
			set
			{
				this.vert.height = (float)value - this.vert.y;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0001B550 File Offset: 0x00019750
		// (set) Token: 0x06001D18 RID: 7448 RVA: 0x0001B560 File Offset: 0x00019760
		public int maxY
		{
			get
			{
				return (int)this.vert.y;
			}
			set
			{
				float y = this.vert.y;
				this.vert.y = (float)value;
				this.vert.height = this.vert.height + (y - this.vert.y);
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0001B5A8 File Offset: 0x000197A8
		// (set) Token: 0x06001D1A RID: 7450 RVA: 0x0001B5B8 File Offset: 0x000197B8
		public int minX
		{
			get
			{
				return (int)this.vert.x;
			}
			set
			{
				float x = this.vert.x;
				this.vert.x = (float)value;
				this.vert.width = this.vert.width + (x - this.vert.x);
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0001B600 File Offset: 0x00019800
		// (set) Token: 0x06001D1C RID: 7452 RVA: 0x0001B61C File Offset: 0x0001981C
		public int maxX
		{
			get
			{
				return (int)(this.vert.x + this.vert.width);
			}
			set
			{
				this.vert.width = (float)value - this.vert.x;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0001B638 File Offset: 0x00019838
		// (set) Token: 0x06001D1E RID: 7454 RVA: 0x0001B658 File Offset: 0x00019858
		internal Vector2 uvBottomLeftUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x, this.uv.y);
			}
			set
			{
				Vector2 uvTopRightUnFlipped = this.uvTopRightUnFlipped;
				this.uv.x = value.x;
				this.uv.y = value.y;
				this.uv.width = uvTopRightUnFlipped.x - this.uv.x;
				this.uv.height = uvTopRightUnFlipped.y - this.uv.y;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x0001B6CC File Offset: 0x000198CC
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x0001B700 File Offset: 0x00019900
		internal Vector2 uvBottomRightUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x + this.uv.width, this.uv.y);
			}
			set
			{
				Vector2 uvTopRightUnFlipped = this.uvTopRightUnFlipped;
				this.uv.width = value.x - this.uv.x;
				this.uv.y = value.y;
				this.uv.height = uvTopRightUnFlipped.y - this.uv.y;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x0001B764 File Offset: 0x00019964
		// (set) Token: 0x06001D22 RID: 7458 RVA: 0x0001B7A4 File Offset: 0x000199A4
		internal Vector2 uvTopRightUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x + this.uv.width, this.uv.y + this.uv.height);
			}
			set
			{
				this.uv.width = value.x - this.uv.x;
				this.uv.height = value.y - this.uv.y;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0001B7F0 File Offset: 0x000199F0
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x0001B824 File Offset: 0x00019A24
		internal Vector2 uvTopLeftUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x, this.uv.y + this.uv.height);
			}
			set
			{
				Vector2 uvTopRightUnFlipped = this.uvTopRightUnFlipped;
				this.uv.x = value.x;
				this.uv.height = value.y - this.uv.y;
				this.uv.width = uvTopRightUnFlipped.x - this.uv.x;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x0001B888 File Offset: 0x00019A88
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x0001B890 File Offset: 0x00019A90
		public Vector2 uvBottomLeft
		{
			get
			{
				return this.uvBottomLeftUnFlipped;
			}
			set
			{
				this.uvBottomLeftUnFlipped = value;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x0001B89C File Offset: 0x00019A9C
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x0001B8BC File Offset: 0x00019ABC
		public Vector2 uvBottomRight
		{
			get
			{
				return (!this.flipped) ? this.uvBottomRightUnFlipped : this.uvTopLeftUnFlipped;
			}
			set
			{
				if (this.flipped)
				{
					this.uvTopLeftUnFlipped = value;
				}
				else
				{
					this.uvBottomRightUnFlipped = value;
				}
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0001B8DC File Offset: 0x00019ADC
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x0001B8E4 File Offset: 0x00019AE4
		public Vector2 uvTopRight
		{
			get
			{
				return this.uvTopRightUnFlipped;
			}
			set
			{
				this.uvTopRightUnFlipped = value;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x0001B910 File Offset: 0x00019B10
		public Vector2 uvTopLeft
		{
			get
			{
				return (!this.flipped) ? this.uvTopLeftUnFlipped : this.uvBottomRightUnFlipped;
			}
			set
			{
				if (this.flipped)
				{
					this.uvBottomRightUnFlipped = value;
				}
				else
				{
					this.uvTopLeftUnFlipped = value;
				}
			}
		}

		// Token: 0x040005EE RID: 1518
		public int index;

		// Token: 0x040005EF RID: 1519
		[Obsolete("CharacterInfo.uv is deprecated. Use uvBottomLeft, uvBottomRight, uvTopRight or uvTopLeft instead.")]
		public Rect uv;

		// Token: 0x040005F0 RID: 1520
		[Obsolete("CharacterInfo.vert is deprecated. Use minX, maxX, minY, maxY instead.")]
		public Rect vert;

		// Token: 0x040005F1 RID: 1521
		[Obsolete("CharacterInfo.width is deprecated. Use advance instead.")]
		public float width;

		// Token: 0x040005F2 RID: 1522
		public int size;

		// Token: 0x040005F3 RID: 1523
		public FontStyle style;

		// Token: 0x040005F4 RID: 1524
		[Obsolete("CharacterInfo.flipped is deprecated. Use uvBottomLeft, uvBottomRight, uvTopRight or uvTopLeft instead, which will be correct regardless of orientation.")]
		public bool flipped;
	}
}
