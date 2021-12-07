using System;
using UnityEngine;

// Token: 0x020008DC RID: 2268
public class WaterThatRockFallsInto : MonoBehaviour
{
	// Token: 0x0600329B RID: 12955 RVA: 0x000D584C File Offset: 0x000D3A4C
	public void OnTriggerEnter(Collider collider)
	{
		if (collider.GetComponent<Rigidbody>())
		{
			collider.GetComponent<Rigidbody>().velocity -= Vector3.Scale(this.EnterWaterDamp, collider.GetComponent<Rigidbody>().velocity);
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.SplashEffect);
			Vector3 position = new Vector3(collider.transform.position.x, base.transform.position.y + base.transform.localScale.y * 0.5f, -0.2f);
			gameObject.transform.position = position;
		}
	}

	// Token: 0x0600329C RID: 12956 RVA: 0x000D5904 File Offset: 0x000D3B04
	public void OnTriggerStay(Collider collider)
	{
		if (collider.GetComponent<Rigidbody>())
		{
			Rigidbody component = collider.GetComponent<Rigidbody>();
			float num = collider.GetComponent<Rigidbody>().angularVelocity.z;
			num += Mathf.DeltaAngle(collider.transform.eulerAngles.z, 0f) * this.AngularVelocityMultiplier;
			num *= Mathf.Pow(this.AngularFriction, Time.deltaTime * 60f);
			num = Mathf.Clamp(num, -this.MaxAngularVelocity, this.MaxAngularVelocity);
			collider.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, num);
			float num2 = base.transform.position.y + base.transform.localScale.y * 0.5f;
			float num3 = 1f - (collider.transform.position.y - num2) / this.FloatHeight;
			if (num3 > 0f)
			{
				Vector3 v = Physics.gravity * (component.velocity.y * this.BounceDamp - num3);
				component.AddForceSafe(v, ForceMode.Acceleration);
			}
			float num4 = (base.transform.position - collider.GetComponent<Rigidbody>().position).x * this.WaterHorizontalMultiplier;
			num4 = Mathf.Clamp(num4, -this.WaterHorizontalMaxVelocity, this.WaterHorizontalMaxVelocity);
			component.AddForceSafe(Vector3.right * num4, ForceMode.Acceleration);
			component.AddForceSafe(Vector3.left * component.velocity.x * this.WaterHorizontalFriction, ForceMode.Acceleration);
		}
	}

	// Token: 0x04002D7C RID: 11644
	public float AngularVelocityMultiplier;

	// Token: 0x04002D7D RID: 11645
	public float AngularFriction;

	// Token: 0x04002D7E RID: 11646
	public float MaxAngularVelocity;

	// Token: 0x04002D7F RID: 11647
	public float FloatHeight;

	// Token: 0x04002D80 RID: 11648
	public float BounceDamp;

	// Token: 0x04002D81 RID: 11649
	public Vector2 EnterWaterDamp;

	// Token: 0x04002D82 RID: 11650
	public GameObject SplashEffect;

	// Token: 0x04002D83 RID: 11651
	public float WaterHorizontalMaxVelocity;

	// Token: 0x04002D84 RID: 11652
	public float WaterHorizontalMultiplier;

	// Token: 0x04002D85 RID: 11653
	public float WaterHorizontalFriction;
}
