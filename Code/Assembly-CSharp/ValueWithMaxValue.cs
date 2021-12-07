using System;

// Token: 0x0200062B RID: 1579
public class ValueWithMaxValue : SaveSerialize
{
	// Token: 0x1400003D RID: 61
	// (add) Token: 0x060026E3 RID: 9955 RVA: 0x000A9E36 File Offset: 0x000A8036
	// (remove) Token: 0x060026E4 RID: 9956 RVA: 0x000A9E4F File Offset: 0x000A804F
	public event Action ValueChanged;

	// Token: 0x17000624 RID: 1572
	// (get) Token: 0x060026E5 RID: 9957 RVA: 0x000A9E68 File Offset: 0x000A8068
	// (set) Token: 0x060026E6 RID: 9958 RVA: 0x000A9E70 File Offset: 0x000A8070
	public float Value
	{
		get
		{
			return this.m_value;
		}
		set
		{
			this.m_value = value;
			if (this.m_value < 0f)
			{
				this.m_value = 0f;
			}
			if (this.m_value > this.MaxValue)
			{
				this.m_value = this.MaxValue;
			}
			if (this.ValueChanged != null)
			{
				this.ValueChanged();
			}
		}
	}

	// Token: 0x17000625 RID: 1573
	// (get) Token: 0x060026E7 RID: 9959 RVA: 0x000A9ED2 File Offset: 0x000A80D2
	public bool ValueIsMax
	{
		get
		{
			return this.Value == this.MaxValue;
		}
	}

	// Token: 0x060026E8 RID: 9960 RVA: 0x000A9EE2 File Offset: 0x000A80E2
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_value);
	}

	// Token: 0x060026E9 RID: 9961 RVA: 0x000A9EF0 File Offset: 0x000A80F0
	public new void Awake()
	{
		base.Awake();
		this.Value = this.StartValue;
	}

	// Token: 0x04002176 RID: 8566
	public float MaxValue;

	// Token: 0x04002177 RID: 8567
	private float m_value;

	// Token: 0x04002178 RID: 8568
	public float StartValue;
}
