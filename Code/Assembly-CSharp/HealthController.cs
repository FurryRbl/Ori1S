using System;

// Token: 0x0200062A RID: 1578
public class HealthController : ValueWithMaxValue
{
	// Token: 0x1400003C RID: 60
	// (add) Token: 0x060026DD RID: 9949 RVA: 0x000A9DC3 File Offset: 0x000A7FC3
	// (remove) Token: 0x060026DE RID: 9950 RVA: 0x000A9DDC File Offset: 0x000A7FDC
	public event Action OnHealthDepletedEvent = delegate()
	{
	};

	// Token: 0x060026DF RID: 9951 RVA: 0x000A9DF5 File Offset: 0x000A7FF5
	public new void Awake()
	{
		base.Awake();
		base.ValueChanged += this.OnValueChanged;
	}

	// Token: 0x060026E0 RID: 9952 RVA: 0x000A9E0F File Offset: 0x000A800F
	public void OnValueChanged()
	{
		if (base.Value <= 0f)
		{
			this.OnHealthDepletedEvent();
		}
	}
}
