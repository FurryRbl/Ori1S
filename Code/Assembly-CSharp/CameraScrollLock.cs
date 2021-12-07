using System;
using Game;
using UnityEngine;

// Token: 0x020002CD RID: 717
[ExecuteInEditMode]
public class CameraScrollLock : MonoBehaviour
{
	// Token: 0x170003EF RID: 1007
	// (get) Token: 0x0600163B RID: 5691 RVA: 0x0006227A File Offset: 0x0006047A
	// (set) Token: 0x0600163C RID: 5692 RVA: 0x00062282 File Offset: 0x00060482
	public Vector3 ScrollCenter { get; private set; }

	// Token: 0x170003F0 RID: 1008
	// (get) Token: 0x0600163D RID: 5693 RVA: 0x0006228B File Offset: 0x0006048B
	// (set) Token: 0x0600163E RID: 5694 RVA: 0x00062293 File Offset: 0x00060493
	public Vector3 HalfScrollSize { get; private set; }

	// Token: 0x170003F1 RID: 1009
	// (get) Token: 0x0600163F RID: 5695 RVA: 0x0006229C File Offset: 0x0006049C
	public CameraScrollLock.Type ScrollType
	{
		get
		{
			return (Mathf.Abs(base.transform.localScale.x) <= Mathf.Abs(base.transform.localScale.y)) ? CameraScrollLock.Type.Horizontal : CameraScrollLock.Type.Vertical;
		}
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x000622E5 File Offset: 0x000604E5
	public void Awake()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.ScrollType == CameraScrollLock.Type.Vertical)
		{
			this.ShouldCreateCheckpoint = false;
		}
	}

	// Token: 0x170003F2 RID: 1010
	// (get) Token: 0x06001641 RID: 5697 RVA: 0x00062308 File Offset: 0x00060508
	public Rect BoundingRect
	{
		get
		{
			if (this.Dynamic)
			{
				this.m_boundingRectCalculated = false;
			}
			if (!this.m_boundingRectCalculated)
			{
				this.m_boundingRectCalculated = true;
				this.m_boundingRect = new Rect
				{
					width = base.transform.localScale.x,
					height = base.transform.localScale.y,
					center = base.transform.position
				};
			}
			return this.m_boundingRect;
		}
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x0006239C File Offset: 0x0006059C
	public void OnEnable()
	{
		ScrollLocks.Register(this);
		this.ScrollCenter = base.transform.position;
		this.HalfScrollSize = MoonMath.Vector.Abs(base.transform.localScale * 0.5f);
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x000623E0 File Offset: 0x000605E0
	public void OnDisable()
	{
		ScrollLocks.Unregister(this);
	}

	// Token: 0x0400133B RID: 4923
	[HideInInspector]
	public GameObject Fader;

	// Token: 0x0400133C RID: 4924
	public bool UseFader = true;

	// Token: 0x0400133D RID: 4925
	public AnimationCurve ScrollLockSmooth;

	// Token: 0x0400133E RID: 4926
	public bool UseScrollLockSmooth;

	// Token: 0x0400133F RID: 4927
	public bool ShouldCreateCheckpoint;

	// Token: 0x04001340 RID: 4928
	[Range(0f, 1f)]
	public float WideScreenAdjustment;

	// Token: 0x04001341 RID: 4929
	public bool Dynamic;

	// Token: 0x04001342 RID: 4930
	public CameraScrollLock.ScrollLockMode LockMode;

	// Token: 0x04001343 RID: 4931
	private Rect m_boundingRect;

	// Token: 0x04001344 RID: 4932
	private bool m_boundingRectCalculated;

	// Token: 0x020003CF RID: 975
	public enum ScrollLockMode
	{
		// Token: 0x04001755 RID: 5973
		BothSides,
		// Token: 0x04001756 RID: 5974
		LeftOrBottom,
		// Token: 0x04001757 RID: 5975
		RightOrTop
	}

	// Token: 0x020003D0 RID: 976
	public enum Type
	{
		// Token: 0x04001759 RID: 5977
		Horizontal,
		// Token: 0x0400175A RID: 5978
		Vertical
	}
}
