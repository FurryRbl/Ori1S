using System;

// Token: 0x020006A1 RID: 1697
public class AnimatingFloat : ISerializable
{
	// Token: 0x06002917 RID: 10519 RVA: 0x000B190C File Offset: 0x000AFB0C
	public bool Update(float dt)
	{
		float num = this.Value;
		num += this.Speed * dt;
		if (this.Wrap == AnimatingFloat.WrapType.Clamp)
		{
			if (num < this.Min)
			{
				num = this.Min;
			}
			if (num > this.Max)
			{
				num = this.Max;
			}
		}
		if (this.Wrap == AnimatingFloat.WrapType.Repeat)
		{
			if (this.Max == this.Min)
			{
				num = this.Min;
			}
			else
			{
				while (num < this.Min)
				{
					num += this.Max - this.Min;
				}
				while (num > this.Max)
				{
					num -= this.Max - this.Min;
				}
			}
		}
		if (num == this.Value)
		{
			return false;
		}
		this.Value = num;
		return true;
	}

	// Token: 0x1700068C RID: 1676
	// (get) Token: 0x06002918 RID: 10520 RVA: 0x000B19DD File Offset: 0x000AFBDD
	public bool IsValueAtStart
	{
		get
		{
			return this.Value < this.Min + 0.01f;
		}
	}

	// Token: 0x1700068D RID: 1677
	// (get) Token: 0x06002919 RID: 10521 RVA: 0x000B19F3 File Offset: 0x000AFBF3
	public bool IsValueAtEnd
	{
		get
		{
			return this.Value > this.Max - 0.01f;
		}
	}

	// Token: 0x0600291A RID: 10522 RVA: 0x000B1A0C File Offset: 0x000AFC0C
	public void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Max);
		ar.Serialize(ref this.Min);
		ar.Serialize(ref this.Speed);
		ar.Serialize(ref this.Value);
	}

	// Token: 0x040024A2 RID: 9378
	public float Max = 1f;

	// Token: 0x040024A3 RID: 9379
	public float Min;

	// Token: 0x040024A4 RID: 9380
	public float Speed;

	// Token: 0x040024A5 RID: 9381
	public float Value;

	// Token: 0x040024A6 RID: 9382
	private AnimatingFloat.WrapType Wrap;

	// Token: 0x02000938 RID: 2360
	private enum WrapType
	{
		// Token: 0x04002F2A RID: 12074
		Clamp,
		// Token: 0x04002F2B RID: 12075
		Repeat
	}
}
