using System;
using UnityEngine;

// Token: 0x020006CE RID: 1742
public class BreakableObject : MonoBehaviour
{
	// Token: 0x060029CB RID: 10699 RVA: 0x000B3EF0 File Offset: 0x000B20F0
	private void Start()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.GetComponent<Rigidbody>())
			{
				transform.GetComponent<Rigidbody>().isKinematic = true;
			}
		}
		this.BreakTheObject();
	}

	// Token: 0x060029CC RID: 10700 RVA: 0x000B3F70 File Offset: 0x000B2170
	private void Hit()
	{
		this.m_numberOfHits++;
		if (this.m_numberOfHits == this.NumberOfHitsToBreak)
		{
			this.BreakTheObject();
		}
	}

	// Token: 0x060029CD RID: 10701 RVA: 0x000B3F98 File Offset: 0x000B2198
	private void BreakTheObject()
	{
		int num = 0;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.GetComponent<Rigidbody>() && transform.GetComponent<Collider>())
			{
				transform.GetComponent<Rigidbody>().isKinematic = false;
				float magnitude = transform.GetComponent<Collider>().bounds.size.magnitude;
				transform.GetComponent<Rigidbody>().mass *= 0.6f * magnitude + 0.4f;
				float num2 = 1f - Mathf.Clamp01((transform.transform.position - base.transform.position).magnitude / 1.3f);
				Vector3 vector = new Vector3(-1000f * ((FixedRandom.Values[7] + 2f) * num2 + 0.2f), 2000f * num2, 0f) * 0.8f;
				vector = base.transform.TransformDirection(vector);
				vector.z = 0f;
				transform.GetComponent<Rigidbody>().AddForceSafe(vector);
				transform.GetComponent<Rigidbody>().AddTorque(0f, 0f, 4300f * FixedRandom.Values[5]);
				num++;
				if (num % 2 == 0)
				{
					transform.gameObject.layer = LayerMask.NameToLayer("debrisNoCollsion");
				}
				LegacyTransparancyAnimator legacyTransparancyAnimator = transform.gameObject.AddComponent<LegacyTransparancyAnimator>();
				legacyTransparancyAnimator.AnimationCurve = this.ShardsFadeoutCurve;
				legacyTransparancyAnimator.PlayAutomatically = true;
				legacyTransparancyAnimator.DestroyWhenInvisible = true;
				legacyTransparancyAnimator.SampleFirstFrameOnStart = true;
			}
		}
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.ExplosionEffect, base.transform.position, Quaternion.identity);
		gameObject.transform.rotation = base.transform.rotation;
		UnityEngine.Object.DestroyObject(this);
	}

	// Token: 0x060029CE RID: 10702 RVA: 0x000B41C8 File Offset: 0x000B23C8
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name.Contains("spiritFlameBomb"))
		{
			this.Hit();
		}
	}

	// Token: 0x0400253F RID: 9535
	public int NumberOfHitsToBreak = 3;

	// Token: 0x04002540 RID: 9536
	private int m_numberOfHits;

	// Token: 0x04002541 RID: 9537
	public GameObject ExplosionEffect;

	// Token: 0x04002542 RID: 9538
	public AnimationCurve ShardsFadeoutCurve;
}
