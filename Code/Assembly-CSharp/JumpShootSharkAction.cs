using System;
using UnityEngine;

// Token: 0x020005AF RID: 1455
public class JumpShootSharkAction : ActionMethod
{
	// Token: 0x06002528 RID: 9512 RVA: 0x000A2264 File Offset: 0x000A0464
	public override void Perform(IContext context)
	{
		JumpShootSharkAction.ActionType action = this.Action;
		if (action != JumpShootSharkAction.ActionType.Emerge)
		{
			throw new ArgumentOutOfRangeException();
		}
		(this.JumpShootShark.CurrentEntity as JumpShootShark).SetEmergeLocation(this.Position.position);
	}

	// Token: 0x04001FAC RID: 8108
	public JumpShootSharkPlaceholder JumpShootShark;

	// Token: 0x04001FAD RID: 8109
	public JumpShootSharkAction.ActionType Action;

	// Token: 0x04001FAE RID: 8110
	public Transform Position;

	// Token: 0x020005B0 RID: 1456
	public enum ActionType
	{
		// Token: 0x04001FB0 RID: 8112
		Emerge
	}
}
