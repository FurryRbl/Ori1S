using System;

// Token: 0x0200063C RID: 1596
public class ExtractedIntFromInt64
{
	// Token: 0x0600271D RID: 10013 RVA: 0x000AAF21 File Offset: 0x000A9121
	public ExtractedIntFromInt64(int bits)
	{
		this.Bits = bits;
	}

	// Token: 0x1700062C RID: 1580
	// (get) Token: 0x0600271E RID: 10014 RVA: 0x000AAF3F File Offset: 0x000A913F
	// (set) Token: 0x0600271F RID: 10015 RVA: 0x000AAF47 File Offset: 0x000A9147
	protected int Bits
	{
		get
		{
			return this.m_bits;
		}
		set
		{
			this.m_bits = value;
			this.m_mask = this.Mask;
		}
	}

	// Token: 0x1700062D RID: 1581
	// (get) Token: 0x06002720 RID: 10016 RVA: 0x000AAF5C File Offset: 0x000A915C
	// (set) Token: 0x06002721 RID: 10017 RVA: 0x000AAF64 File Offset: 0x000A9164
	public long Value
	{
		get
		{
			return this.m_value;
		}
		set
		{
			if (this.Validate())
			{
				this.m_value = value;
			}
		}
	}

	// Token: 0x1700062E RID: 1582
	// (get) Token: 0x06002722 RID: 10018 RVA: 0x000AAF7D File Offset: 0x000A917D
	public long MaxValue
	{
		get
		{
			return (1L << this.m_bits) - 1L;
		}
	}

	// Token: 0x1700062F RID: 1583
	// (get) Token: 0x06002723 RID: 10019 RVA: 0x000AAF8E File Offset: 0x000A918E
	public long Mask
	{
		get
		{
			return (1L << this.m_bits) - 1L;
		}
	}

	// Token: 0x06002724 RID: 10020 RVA: 0x000AAF9F File Offset: 0x000A919F
	public void Encode(ref long data)
	{
		data <<= this.m_bits;
		data += this.Value;
	}

	// Token: 0x06002725 RID: 10021 RVA: 0x000AAFBA File Offset: 0x000A91BA
	public void Decode(ref long data)
	{
		this.Value = (data & this.m_mask);
		data >>= this.m_bits;
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x000AAFD9 File Offset: 0x000A91D9
	public bool Validate()
	{
		return (this.Value & ~this.m_mask) == 0L;
	}

	// Token: 0x040021BB RID: 8635
	private int m_bits = 1;

	// Token: 0x040021BC RID: 8636
	private long m_mask = 1L;

	// Token: 0x040021BD RID: 8637
	private long m_value;
}
