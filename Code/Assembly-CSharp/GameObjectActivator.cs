using System;

// Token: 0x020002B3 RID: 691
public class GameObjectActivator : SaveSerialize, IDynamicGraphicHierarchy
{
	// Token: 0x060015C7 RID: 5575 RVA: 0x00060421 File Offset: 0x0005E621
	private void Start()
	{
		this.m_hasStarted = true;
		if (!this.m_hasSerialized)
		{
			base.gameObject.SetActive(this.ActiveAtStart);
		}
	}

	// Token: 0x060015C8 RID: 5576 RVA: 0x00060448 File Offset: 0x0005E648
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.m_hasSerialized = true;
			base.gameObject.SetActive(ar.Serialize(true));
		}
		else
		{
			ar.Serialize((!this.m_hasStarted && !this.m_hasSerialized) ? this.ActiveAtStart : base.gameObject.activeSelf);
		}
	}

	// Token: 0x040012AF RID: 4783
	public bool ActiveAtStart = true;

	// Token: 0x040012B0 RID: 4784
	private bool m_hasSerialized;

	// Token: 0x040012B1 RID: 4785
	private bool m_hasStarted;
}
