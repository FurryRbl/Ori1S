using System;
using Game;
using UnityEngine;

// Token: 0x02000268 RID: 616
public class SeinMaxSpeedBasedOnDistance : MonoBehaviour
{
	// Token: 0x060014B3 RID: 5299 RVA: 0x0005D634 File Offset: 0x0005B834
	public void OnHorizontalInputCalculate()
	{
		Characters.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput *= this.SpeedOverDistance.Evaluate(Vector3.Distance(Characters.Sein.Position, this.Target.position) / this.Distance);
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x0005D688 File Offset: 0x0005B888
	public void OnEnable()
	{
		if (Characters.Sein)
		{
			this.m_registered = true;
			SeinController controller = Characters.Sein.Controller;
			controller.OnHorizontalInputPostCalculate = (Action)Delegate.Combine(controller.OnHorizontalInputPostCalculate, new Action(this.OnHorizontalInputCalculate));
		}
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x0005D6D8 File Offset: 0x0005B8D8
	public void OnDisable()
	{
		if (!Characters.Sein || !Characters.Sein.Controller)
		{
			return;
		}
		if (this.m_registered)
		{
			this.m_registered = false;
			SeinController controller = Characters.Sein.Controller;
			controller.OnHorizontalInputPostCalculate = (Action)Delegate.Remove(controller.OnHorizontalInputPostCalculate, new Action(this.OnHorizontalInputCalculate));
		}
	}

	// Token: 0x04001200 RID: 4608
	private bool m_registered;

	// Token: 0x04001201 RID: 4609
	public Transform Target;

	// Token: 0x04001202 RID: 4610
	public AnimationCurve SpeedOverDistance;

	// Token: 0x04001203 RID: 4611
	public float Distance = 20f;
}
