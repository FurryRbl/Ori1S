using System;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class BabySeinJumpZone : MonoBehaviour
{
	// Token: 0x060001EE RID: 494 RVA: 0x00008360 File Offset: 0x00006560
	public void OnTriggerEnter(Collider collider)
	{
		BabySein component = collider.GetComponent<BabySein>();
		if (component && component.Controller.IgnoreControllerInput)
		{
			component.Controller.Jump();
		}
	}
}
