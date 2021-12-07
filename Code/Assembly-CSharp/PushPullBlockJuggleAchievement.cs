using System;
using UnityEngine;

// Token: 0x02000903 RID: 2307
public class PushPullBlockJuggleAchievement : MonoBehaviour
{
	// Token: 0x06003340 RID: 13120 RVA: 0x000D7F4A File Offset: 0x000D614A
	public void Awake()
	{
		this.m_pushPullBlock = base.GetComponent<PushPullBlock>();
		this.m_pushPullBlock.OnBashEvent += this.OnPushPullBlockBashed;
	}

	// Token: 0x06003341 RID: 13121 RVA: 0x000D7F70 File Offset: 0x000D6170
	public void OnPushPullBlockBashed()
	{
		this.m_bashedInAirCount++;
		if (this.m_bashedInAirCount == 5)
		{
			AchievementsLogic.Instance.OnJuggledPushBlock();
		}
	}

	// Token: 0x06003342 RID: 13122 RVA: 0x000D7FA4 File Offset: 0x000D61A4
	public void OnCollisionStay(Collision collision)
	{
		Rigidbody component = base.GetComponent<Rigidbody>();
		if (!component.isKinematic && component.velocity.y <= 0.01f)
		{
			foreach (ContactPoint contactPoint in collision.contacts)
			{
				if (Vector3.Dot(Vector3.up, contactPoint.normal) > Mathf.Cos(1.0471976f))
				{
					this.m_bashedInAirCount = 0;
				}
			}
		}
	}

	// Token: 0x04002E2F RID: 11823
	private PushPullBlock m_pushPullBlock;

	// Token: 0x04002E30 RID: 11824
	private int m_bashedInAirCount;
}
