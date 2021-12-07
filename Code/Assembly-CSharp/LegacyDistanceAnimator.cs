using System;
using Game;
using UnityEngine;

// Token: 0x020003A8 RID: 936
public abstract class LegacyDistanceAnimator : MonoBehaviour, ISuspendable
{
	// Token: 0x06001A34 RID: 6708 RVA: 0x00070BEB File Offset: 0x0006EDEB
	public virtual void Start()
	{
		if (this.ShouldTargetPlayer)
		{
			this.Target = Characters.Current.Transform;
		}
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x00070C08 File Offset: 0x0006EE08
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.AccountForX && this.AccountForY)
		{
			this.AnimateIt(this.AnimationCurve.Evaluate(Vector3.Distance(this.Target.position, base.transform.position)));
		}
		else if (this.AccountForX)
		{
			this.AnimateIt(this.AnimationCurve.Evaluate(Vector3.Distance(new Vector3(this.Target.position.x, 0f, 0f), new Vector3(base.transform.position.x, 0f, 0f))));
		}
		else if (this.AccountForY)
		{
			this.AnimateIt(this.AnimationCurve.Evaluate(Vector3.Distance(new Vector3(0f, this.Target.position.y, 0f), new Vector3(0f, base.transform.position.y, 0f))));
		}
	}

	// Token: 0x06001A36 RID: 6710
	protected abstract void AnimateIt(float value);

	// Token: 0x1700046A RID: 1130
	// (get) Token: 0x06001A37 RID: 6711 RVA: 0x00070D37 File Offset: 0x0006EF37
	// (set) Token: 0x06001A38 RID: 6712 RVA: 0x00070D3F File Offset: 0x0006EF3F
	public bool IsSuspended { get; set; }

	// Token: 0x0400169E RID: 5790
	public Transform Target;

	// Token: 0x0400169F RID: 5791
	public AnimationCurve AnimationCurve;

	// Token: 0x040016A0 RID: 5792
	public bool ShouldTargetPlayer = true;

	// Token: 0x040016A1 RID: 5793
	public bool AccountForX = true;

	// Token: 0x040016A2 RID: 5794
	public bool AccountForY = true;
}
