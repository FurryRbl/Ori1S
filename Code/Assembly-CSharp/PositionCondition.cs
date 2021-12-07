using System;
using UnityEngine;

// Token: 0x02000299 RID: 665
public class PositionCondition : Condition
{
	// Token: 0x0600156C RID: 5484 RVA: 0x0005F14C File Offset: 0x0005D34C
	public override bool Validate(IContext context)
	{
		PositionCondition.AxisType axis = this.Axis;
		if (axis != PositionCondition.AxisType.X)
		{
			return axis == PositionCondition.AxisType.Y && LogicUtility.Compare(this.Target.position.y, this.Position.position.y, this.Comparison);
		}
		return LogicUtility.Compare(this.Target.position.x, this.Position.position.x, this.Comparison);
	}

	// Token: 0x04001286 RID: 4742
	public LogicUtility.ComparisonType Comparison;

	// Token: 0x04001287 RID: 4743
	public PositionCondition.AxisType Axis;

	// Token: 0x04001288 RID: 4744
	public Transform Target;

	// Token: 0x04001289 RID: 4745
	public Transform Position;

	// Token: 0x0200029A RID: 666
	public enum AxisType
	{
		// Token: 0x0400128B RID: 4747
		X,
		// Token: 0x0400128C RID: 4748
		Y
	}
}
