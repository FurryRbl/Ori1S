using System;

// Token: 0x0200025E RID: 606
public class MakeEnemyAttackPlayerAction : ActionMethod
{
	// Token: 0x0600145A RID: 5210 RVA: 0x0005C464 File Offset: 0x0005A664
	public override void Perform(IContext context)
	{
		if (!this.Placeholder)
		{
			return;
		}
		Entity currentEntity = this.Placeholder.CurrentEntity;
		if (currentEntity)
		{
			currentEntity.Controller.StateMachine.Trigger<AttackTriggered>();
		}
	}

	// Token: 0x040011CA RID: 4554
	public RespawningPlaceholder Placeholder;
}
