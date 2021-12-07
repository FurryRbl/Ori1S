using System;
using System.Globalization;

// Token: 0x020006CA RID: 1738
public struct RECT
{
	// Token: 0x0600299E RID: 10654 RVA: 0x000B38E6 File Offset: 0x000B1AE6
	public RECT(int left, int top, int right, int bottom)
	{
		this.Left = left;
		this.Top = top;
		this.Right = right;
		this.Bottom = bottom;
	}

	// Token: 0x1700069C RID: 1692
	// (get) Token: 0x0600299F RID: 10655 RVA: 0x000B3905 File Offset: 0x000B1B05
	// (set) Token: 0x060029A0 RID: 10656 RVA: 0x000B390D File Offset: 0x000B1B0D
	public int X
	{
		get
		{
			return this.Left;
		}
		set
		{
			this.Right -= this.Left - value;
			this.Left = value;
		}
	}

	// Token: 0x1700069D RID: 1693
	// (get) Token: 0x060029A1 RID: 10657 RVA: 0x000B392B File Offset: 0x000B1B2B
	// (set) Token: 0x060029A2 RID: 10658 RVA: 0x000B3933 File Offset: 0x000B1B33
	public int Y
	{
		get
		{
			return this.Top;
		}
		set
		{
			this.Bottom -= this.Top - value;
			this.Top = value;
		}
	}

	// Token: 0x1700069E RID: 1694
	// (get) Token: 0x060029A3 RID: 10659 RVA: 0x000B3951 File Offset: 0x000B1B51
	// (set) Token: 0x060029A4 RID: 10660 RVA: 0x000B3960 File Offset: 0x000B1B60
	public int Height
	{
		get
		{
			return this.Bottom - this.Top;
		}
		set
		{
			this.Bottom = value + this.Top;
		}
	}

	// Token: 0x1700069F RID: 1695
	// (get) Token: 0x060029A5 RID: 10661 RVA: 0x000B3970 File Offset: 0x000B1B70
	// (set) Token: 0x060029A6 RID: 10662 RVA: 0x000B397F File Offset: 0x000B1B7F
	public int Width
	{
		get
		{
			return this.Right - this.Left;
		}
		set
		{
			this.Right = value + this.Left;
		}
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x000B3990 File Offset: 0x000B1B90
	public bool Equals(RECT r)
	{
		return r.Left == this.Left && r.Top == this.Top && r.Right == this.Right && r.Bottom == this.Bottom;
	}

	// Token: 0x060029A8 RID: 10664 RVA: 0x000B39E5 File Offset: 0x000B1BE5
	public override bool Equals(object obj)
	{
		return obj is RECT && this.Equals((RECT)obj);
	}

	// Token: 0x060029A9 RID: 10665 RVA: 0x000B3A00 File Offset: 0x000B1C00
	public override string ToString()
	{
		return string.Format(CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", new object[]
		{
			this.Left,
			this.Top,
			this.Right,
			this.Bottom
		});
	}

	// Token: 0x060029AA RID: 10666 RVA: 0x000B3A5A File Offset: 0x000B1C5A
	public static bool operator ==(RECT r1, RECT r2)
	{
		return r1.Equals(r2);
	}

	// Token: 0x060029AB RID: 10667 RVA: 0x000B3A64 File Offset: 0x000B1C64
	public static bool operator !=(RECT r1, RECT r2)
	{
		return !r1.Equals(r2);
	}

	// Token: 0x0400251E RID: 9502
	public int Left;

	// Token: 0x0400251F RID: 9503
	public int Top;

	// Token: 0x04002520 RID: 9504
	public int Right;

	// Token: 0x04002521 RID: 9505
	public int Bottom;
}
