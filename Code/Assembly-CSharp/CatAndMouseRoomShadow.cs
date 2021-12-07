using System;
using Game;
using UnityEngine;

// Token: 0x020009B8 RID: 2488
public class CatAndMouseRoomShadow : MonoBehaviour
{
	// Token: 0x06003646 RID: 13894 RVA: 0x000E3C58 File Offset: 0x000E1E58
	public void Awake()
	{
		this.ScaleAnimator.Initialize();
		this.TransparencyAnimator.Initialize();
		this.m_bounds = new Rect
		{
			width = this.Zone.lossyScale.x,
			height = this.Zone.lossyScale.y,
			center = this.Zone.position
		};
	}

	// Token: 0x06003647 RID: 13895 RVA: 0x000E3CD8 File Offset: 0x000E1ED8
	public void FixedUpdate()
	{
		if (!this.Timer.Active)
		{
			this.TransparencyAnimator.AnimatorDriver.ContinueBackwards();
			return;
		}
		this.ScaleAnimator.SampleValue(this.Timer.TimeNormalized, true);
		Vector3 position = Characters.Current.Position;
		if (this.m_bounds.Contains(position))
		{
			this.TransparencyAnimator.AnimatorDriver.ContinueForward();
			RaycastHit raycastHit;
			if (Characters.Sein.Controller.RayTest(position, Vector3.down * 30f, out raycastHit))
			{
				base.transform.position = raycastHit.point;
			}
			else
			{
				Vector3 position2 = base.transform.position;
				position2.x = position.x;
				base.transform.position = position2;
			}
		}
		else
		{
			this.TransparencyAnimator.AnimatorDriver.ContinueBackwards();
		}
	}

	// Token: 0x040030E5 RID: 12517
	public Transform Zone;

	// Token: 0x040030E6 RID: 12518
	private Rect m_bounds;

	// Token: 0x040030E7 RID: 12519
	public TransparencyAnimator TransparencyAnimator;

	// Token: 0x040030E8 RID: 12520
	public ScaleAnimator ScaleAnimator;

	// Token: 0x040030E9 RID: 12521
	public CatAndMouseRoomTimerController Timer;
}
