using System;
using Game;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class SeinMaxSpeedAction : ActionWithDuration
{
	// Token: 0x060014A9 RID: 5289 RVA: 0x0005D4E6 File Offset: 0x0005B6E6
	public void OnHorizontalInputCalculate()
	{
		Characters.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput *= this.SpeedOverTime.Evaluate(this.m_currentTime);
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x0005D514 File Offset: 0x0005B714
	public void OnEnable()
	{
		SeinController controller = Characters.Sein.Controller;
		controller.OnHorizontalInputPostCalculate = (Action)Delegate.Combine(controller.OnHorizontalInputPostCalculate, new Action(this.OnHorizontalInputCalculate));
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x0005D541 File Offset: 0x0005B741
	public void OnDisable()
	{
		SeinController controller = Characters.Sein.Controller;
		controller.OnHorizontalInputPostCalculate = (Action)Delegate.Remove(controller.OnHorizontalInputPostCalculate, new Action(this.OnHorizontalInputCalculate));
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x0005D56E File Offset: 0x0005B76E
	public override void Perform(IContext context)
	{
		this.m_currentTime = 0f;
		this.m_isPerforming = true;
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x0005D584 File Offset: 0x0005B784
	public void FixedUpdate()
	{
		if (this.m_isPerforming)
		{
			this.m_currentTime += Time.deltaTime;
			if (this.m_currentTime > this.ChangeDuration)
			{
				this.m_isPerforming = false;
			}
			Characters.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput *= this.SpeedOverTime.Evaluate(this.m_currentTime);
		}
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x0005D5F2 File Offset: 0x0005B7F2
	public override void Stop()
	{
		this.m_isPerforming = false;
		this.m_currentTime = 0f;
	}

	// Token: 0x170003A9 RID: 937
	// (get) Token: 0x060014AF RID: 5295 RVA: 0x0005D606 File Offset: 0x0005B806
	public override bool IsPerforming
	{
		get
		{
			return this.m_isPerforming;
		}
	}

	// Token: 0x170003AA RID: 938
	// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0005D60E File Offset: 0x0005B80E
	// (set) Token: 0x060014B1 RID: 5297 RVA: 0x0005D616 File Offset: 0x0005B816
	public override float Duration
	{
		get
		{
			return this.ChangeDuration;
		}
		set
		{
			this.ChangeDuration = value;
		}
	}

	// Token: 0x040011FC RID: 4604
	private bool m_isPerforming;

	// Token: 0x040011FD RID: 4605
	private float m_currentTime;

	// Token: 0x040011FE RID: 4606
	public AnimationCurve SpeedOverTime;

	// Token: 0x040011FF RID: 4607
	public float ChangeDuration;
}
