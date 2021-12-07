using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class Damage
{
	// Token: 0x06000410 RID: 1040 RVA: 0x00010FC1 File Offset: 0x0000F1C1
	public Damage(float amount, Vector2 force, Vector3 position, DamageType type, GameObject sender)
	{
		this.m_amount = amount;
		this.m_force = force;
		this.m_position = position;
		this.m_type = type;
		this.m_sender = sender;
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000411 RID: 1041 RVA: 0x00010FEE File Offset: 0x0000F1EE
	public float Amount
	{
		get
		{
			return this.m_amount;
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000412 RID: 1042 RVA: 0x00010FF6 File Offset: 0x0000F1F6
	public Vector2 Force
	{
		get
		{
			return this.m_force;
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000413 RID: 1043 RVA: 0x00010FFE File Offset: 0x0000F1FE
	public Vector3 Position
	{
		get
		{
			return this.m_position;
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000414 RID: 1044 RVA: 0x00011006 File Offset: 0x0000F206
	public DamageType Type
	{
		get
		{
			return this.m_type;
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000415 RID: 1045 RVA: 0x0001100E File Offset: 0x0000F20E
	public GameObject Sender
	{
		get
		{
			return this.m_sender;
		}
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00011016 File Offset: 0x0000F216
	public void SetAmount(float amount)
	{
		this.m_amount = amount;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0001101F File Offset: 0x0000F21F
	public void DealToComponents(GameObject target)
	{
		if (target != null)
		{
			target.SendMessage("OnRecieveDamage", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0400034F RID: 847
	private float m_amount;

	// Token: 0x04000350 RID: 848
	private Vector2 m_force;

	// Token: 0x04000351 RID: 849
	private Vector3 m_position;

	// Token: 0x04000352 RID: 850
	private DamageType m_type;

	// Token: 0x04000353 RID: 851
	private GameObject m_sender;
}
