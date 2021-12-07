using System;
using CatlikeCoding.TextBox;
using UnityEngine;

// Token: 0x020003C0 RID: 960
public class DamageText : Suspendable, IPooled
{
	// Token: 0x06001AA6 RID: 6822 RVA: 0x00072D55 File Offset: 0x00070F55
	public new void Awake()
	{
		base.Awake();
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x00072D5D File Offset: 0x00070F5D
	private void Start()
	{
		this.m_speed = new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(4f, 10f));
	}

	// Token: 0x1700046C RID: 1132
	// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x00072D88 File Offset: 0x00070F88
	// (set) Token: 0x06001AA9 RID: 6825 RVA: 0x00072D90 File Offset: 0x00070F90
	public override bool IsSuspended { get; set; }

	// Token: 0x06001AAA RID: 6826 RVA: 0x00072D9C File Offset: 0x00070F9C
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_speed += Vector3.down * Time.deltaTime * 15f;
		base.transform.localPosition += this.m_speed * Time.deltaTime;
		this.m_time += Time.deltaTime;
		if (this.m_time > 1.5f)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001AAB RID: 6827 RVA: 0x00072E34 File Offset: 0x00071034
	public void ChangeText(Damage damage)
	{
		this.m_time = 0f;
		TextBox componentInChildren = base.GetComponentInChildren<TextBox>();
		if (damage.Amount == 0f)
		{
			componentInChildren.SetText("0");
			base.transform.localScale *= this.SizeByDamageAmount.Evaluate(0f);
		}
		else
		{
			componentInChildren.SetText("<red>-" + Mathf.CeilToInt(damage.Amount) + "</>");
			base.transform.localScale *= this.SizeByDamageAmount.Evaluate(damage.Amount);
		}
		componentInChildren.RenderText();
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x00072EEB File Offset: 0x000710EB
	public void OnPoolSpawned()
	{
		this.m_time = 0f;
	}

	// Token: 0x0400171D RID: 5917
	public Color Red = Color.red;

	// Token: 0x0400171E RID: 5918
	public Color Zero = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	// Token: 0x0400171F RID: 5919
	public AnimationCurve SizeByDamageAmount;

	// Token: 0x04001720 RID: 5920
	[PooledSafe]
	private Vector3 m_speed;

	// Token: 0x04001721 RID: 5921
	private float m_time;
}
