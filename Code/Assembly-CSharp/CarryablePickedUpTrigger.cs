using System;

// Token: 0x0200035B RID: 859
public class CarryablePickedUpTrigger : Trigger
{
	// Token: 0x06001877 RID: 6263 RVA: 0x00069038 File Offset: 0x00067238
	public new void Awake()
	{
		base.Awake();
		CarryableRigidBody.OnAnyCarryablePickedUpAction += this.OnAnyCarryablePickedUpAction;
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x00069051 File Offset: 0x00067251
	public new void OnDestroy()
	{
		base.OnDestroy();
		CarryableRigidBody.OnAnyCarryablePickedUpAction -= this.OnAnyCarryablePickedUpAction;
	}

	// Token: 0x06001879 RID: 6265 RVA: 0x0006906A File Offset: 0x0006726A
	public void OnAnyCarryablePickedUpAction()
	{
		base.DoTrigger(true);
	}
}
