using System;

// Token: 0x02000364 RID: 868
public class CounterTrigger : Trigger
{
	// Token: 0x1700045A RID: 1114
	// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0006A7F1 File Offset: 0x000689F1
	public int Counter
	{
		get
		{
			return this.m_counter;
		}
	}

	// Token: 0x060018F2 RID: 6386 RVA: 0x0006A7FC File Offset: 0x000689FC
	public void Increment()
	{
		this.m_counter++;
		if (this.m_counter == this.TriggerOnCounter)
		{
			base.DoTrigger(true);
		}
	}

	// Token: 0x060018F3 RID: 6387 RVA: 0x0006A830 File Offset: 0x00068A30
	public void Decrement()
	{
		this.m_counter--;
		if (this.m_counter == this.TriggerOnCounter)
		{
			base.DoTrigger(true);
		}
	}

	// Token: 0x060018F4 RID: 6388 RVA: 0x0006A863 File Offset: 0x00068A63
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_counter);
		base.Serialize(ar);
	}

	// Token: 0x0400155F RID: 5471
	public int TriggerOnCounter;

	// Token: 0x04001560 RID: 5472
	private int m_counter;
}
