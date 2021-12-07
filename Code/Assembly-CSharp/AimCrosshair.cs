using System;
using Core;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class AimCrosshair : MonoBehaviour
{
	// Token: 0x060003F9 RID: 1017 RVA: 0x00010BA0 File Offset: 0x0000EDA0
	public void FixedUpdate()
	{
		if (this.AriesIdea)
		{
			Vector3 vector = Core.Input.Axis;
			if (vector.magnitude == 0f)
			{
				this.reset = true;
			}
			base.transform.position += new Vector3(vector.x, vector.y, 0f) * this.Speed.Evaluate(vector.magnitude) * Time.deltaTime;
			Vector3 localPosition = base.transform.localPosition;
			localPosition.Normalize();
			base.transform.localPosition = localPosition;
			if (vector.magnitude > 0f)
			{
				vector.Normalize();
				if (Vector3.Dot(vector, base.transform.localPosition) <= Mathf.Cos(70f) && this.reset)
				{
					base.transform.localPosition = vector;
				}
				this.reset = false;
			}
		}
		else
		{
			Vector3 vector2 = Core.Input.Axis;
			base.transform.position += new Vector3(vector2.x, vector2.y, 0f) * this.Speed.Evaluate(vector2.magnitude) * Time.deltaTime;
		}
	}

	// Token: 0x04000343 RID: 835
	public AnimationCurve Speed;

	// Token: 0x04000344 RID: 836
	public bool reset;

	// Token: 0x04000345 RID: 837
	public bool AriesIdea;
}
