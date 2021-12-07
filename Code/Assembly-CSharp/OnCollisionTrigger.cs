using System;
using UnityEngine;

// Token: 0x0200036E RID: 878
public class OnCollisionTrigger : MonoBehaviour
{
	// Token: 0x0600191D RID: 6429 RVA: 0x0006B46C File Offset: 0x0006966C
	public void OnCollisionEnter(Collision collision)
	{
		if (this.OnCollisionEnterAction)
		{
			this.OnCollisionEnterAction.Perform(new CollisionContext(collision, base.GetComponent<Collider>()));
		}
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x0006B495 File Offset: 0x00069695
	public void OnCollisionExit(Collision collision)
	{
		if (this.OnCollisionExitAction)
		{
			this.OnCollisionExitAction.Perform(new CollisionContext(collision, base.GetComponent<Collider>()));
		}
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x0006B4BE File Offset: 0x000696BE
	public void OnTriggerEnter(Collider other)
	{
		if (this.OnTriggerEnterAction)
		{
			this.OnTriggerEnterAction.Perform(new TriggerContext(other, base.GetComponent<Collider>()));
		}
	}

	// Token: 0x06001920 RID: 6432 RVA: 0x0006B4E7 File Offset: 0x000696E7
	public void OnTriggerExit(Collider other)
	{
		if (this.OnTriggerExitAction)
		{
			this.OnTriggerExitAction.Perform(new TriggerContext(other, base.GetComponent<Collider>()));
		}
	}

	// Token: 0x06001921 RID: 6433 RVA: 0x0006B510 File Offset: 0x00069710
	public void OnTriggerStay(Collider other)
	{
		if (this.OnTriggerStayAction)
		{
			this.OnTriggerStayAction.Perform(new TriggerContext(other, base.GetComponent<Collider>()));
		}
	}

	// Token: 0x04001584 RID: 5508
	public ActionMethod OnCollisionEnterAction;

	// Token: 0x04001585 RID: 5509
	public ActionMethod OnCollisionExitAction;

	// Token: 0x04001586 RID: 5510
	public ActionMethod OnTriggerEnterAction;

	// Token: 0x04001587 RID: 5511
	public ActionMethod OnTriggerExitAction;

	// Token: 0x04001588 RID: 5512
	public ActionMethod OnTriggerStayAction;
}
