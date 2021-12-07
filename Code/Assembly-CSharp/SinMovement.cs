using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A5 RID: 933
public class SinMovement : SaveSerialize, ISuspendable
{
	// Token: 0x17000469 RID: 1129
	// (get) Token: 0x06001A1A RID: 6682 RVA: 0x000705D0 File Offset: 0x0006E7D0
	// (set) Token: 0x06001A1B RID: 6683 RVA: 0x000705D8 File Offset: 0x0006E7D8
	public bool IsSuspended { get; set; }

	// Token: 0x06001A1C RID: 6684 RVA: 0x000705E4 File Offset: 0x0006E7E4
	public void OnValidate()
	{
		this.Transform = base.transform;
		this.m_startPosition = this.Transform.localPosition;
		this.m_startRotation = this.Transform.localRotation;
		this.m_startScale = this.Transform.localScale;
	}

	// Token: 0x06001A1D RID: 6685 RVA: 0x00070630 File Offset: 0x0006E830
	public new void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x06001A1E RID: 6686 RVA: 0x0007063E File Offset: 0x0006E83E
	public new void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06001A1F RID: 6687 RVA: 0x0007064C File Offset: 0x0006E84C
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.CurrentTime);
		if (ar.Reading)
		{
			this.UpdateMovement(this.CurrentTime);
		}
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x00070674 File Offset: 0x0006E874
	public void Start()
	{
		foreach (SinMovement.Affect affect in this.Affectors)
		{
			switch (affect.Type)
			{
			case SinMovement.Affect.AffectType.Size:
				this.m_useScale = true;
				break;
			case SinMovement.Affect.AffectType.Width:
				this.m_useScale = true;
				break;
			case SinMovement.Affect.AffectType.Height:
				this.m_useScale = true;
				break;
			case SinMovement.Affect.AffectType.Angle:
				this.m_useRotation = true;
				break;
			case SinMovement.Affect.AffectType.X:
				this.m_usePosition = true;
				break;
			case SinMovement.Affect.AffectType.Y:
				this.m_usePosition = true;
				break;
			case SinMovement.Affect.AffectType.Z:
				this.m_usePosition = true;
				break;
			}
			if (this.DivideByFifty)
			{
				affect.Range /= 50f;
				affect.RangeRandom /= 50f;
			}
			affect.Start(this);
		}
	}

	// Token: 0x06001A21 RID: 6689 RVA: 0x00070784 File Offset: 0x0006E984
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.CurrentTime += Time.fixedDeltaTime;
		this.UpdateMovement(this.CurrentTime);
	}

	// Token: 0x06001A22 RID: 6690 RVA: 0x000707BC File Offset: 0x0006E9BC
	public void UpdateMovement(float time)
	{
		this.LocalPosition = this.m_startPosition;
		this.LocalRotation = this.m_startRotation;
		this.LocalScale = this.m_startScale;
		for (int i = 0; i < this.Affectors.Count; i++)
		{
			SinMovement.Affect affect = this.Affectors[i];
			affect.Apply(this, time);
		}
		if (this.LocalPosition != this.Transform.localPosition && this.m_usePosition)
		{
			this.Transform.localPosition = this.LocalPosition;
		}
		if (this.LocalRotation != this.Transform.localRotation && this.m_useRotation)
		{
			this.Transform.localRotation = this.LocalRotation;
		}
		if (this.LocalScale != this.Transform.localScale && this.m_useScale)
		{
			this.Transform.localScale = this.LocalScale;
		}
	}

	// Token: 0x0400168A RID: 5770
	public List<SinMovement.Affect> Affectors;

	// Token: 0x0400168B RID: 5771
	public bool DivideByFifty = true;

	// Token: 0x0400168C RID: 5772
	[HideInInspector]
	public Vector3 LocalPosition;

	// Token: 0x0400168D RID: 5773
	[HideInInspector]
	public Quaternion LocalRotation;

	// Token: 0x0400168E RID: 5774
	[HideInInspector]
	public Vector3 LocalScale;

	// Token: 0x0400168F RID: 5775
	[HideInInspector]
	public Transform Transform;

	// Token: 0x04001690 RID: 5776
	public float CurrentTime;

	// Token: 0x04001691 RID: 5777
	[HideInInspector]
	[SerializeField]
	private Vector3 m_startPosition;

	// Token: 0x04001692 RID: 5778
	[SerializeField]
	[HideInInspector]
	private Quaternion m_startRotation;

	// Token: 0x04001693 RID: 5779
	[SerializeField]
	[HideInInspector]
	private Vector3 m_startScale;

	// Token: 0x04001694 RID: 5780
	private bool m_usePosition;

	// Token: 0x04001695 RID: 5781
	private bool m_useRotation;

	// Token: 0x04001696 RID: 5782
	private bool m_useScale;

	// Token: 0x020003B6 RID: 950
	[Serializable]
	public class Affect
	{
		// Token: 0x06001A75 RID: 6773 RVA: 0x00071E4C File Offset: 0x0007004C
		public void Start(SinMovement sinMovement)
		{
			this.Period += this.PeriodRandom * FixedRandom.Values[1];
			this.Range += this.RangeRandom * FixedRandom.Values[2];
			this.Offset += this.OffsetRandom * FixedRandom.Values[3];
			this.m_o = sinMovement.Transform.position.x * 0.5f;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00071ECC File Offset: 0x000700CC
		public void Apply(SinMovement sinMovement, float time)
		{
			float num = time / this.Period + this.Offset;
			float d = Mathf.Sin(num * 2f * 3.1415927f + this.m_o) * this.Range;
			switch (this.Type)
			{
			case SinMovement.Affect.AffectType.Size:
				sinMovement.LocalScale += new Vector3(1f, 1f, 0f) * d;
				break;
			case SinMovement.Affect.AffectType.Width:
				sinMovement.LocalScale += new Vector3(1f, 0f, 0f) * d;
				break;
			case SinMovement.Affect.AffectType.Height:
				sinMovement.LocalScale += new Vector3(0f, 1f, 0f) * d;
				break;
			case SinMovement.Affect.AffectType.Angle:
				sinMovement.LocalRotation = Quaternion.Euler(sinMovement.LocalRotation.eulerAngles + new Vector3(0f, 0f, 1f) * d);
				break;
			case SinMovement.Affect.AffectType.X:
				sinMovement.LocalPosition += new Vector3(1f, 0f, 0f) * d;
				break;
			case SinMovement.Affect.AffectType.Y:
				sinMovement.LocalPosition += new Vector3(0f, 1f, 0f) * d;
				break;
			case SinMovement.Affect.AffectType.Z:
				sinMovement.LocalPosition += new Vector3(0f, 0f, 1f) * d;
				break;
			}
		}

		// Token: 0x040016E5 RID: 5861
		public float Offset;

		// Token: 0x040016E6 RID: 5862
		public float OffsetRandom;

		// Token: 0x040016E7 RID: 5863
		public float Period;

		// Token: 0x040016E8 RID: 5864
		public float PeriodRandom;

		// Token: 0x040016E9 RID: 5865
		public float Range;

		// Token: 0x040016EA RID: 5866
		public float RangeRandom;

		// Token: 0x040016EB RID: 5867
		public SinMovement.Affect.AffectType Type;

		// Token: 0x040016EC RID: 5868
		private float m_o;

		// Token: 0x020003B7 RID: 951
		public enum AffectType
		{
			// Token: 0x040016EE RID: 5870
			Size,
			// Token: 0x040016EF RID: 5871
			Width,
			// Token: 0x040016F0 RID: 5872
			Height,
			// Token: 0x040016F1 RID: 5873
			Angle,
			// Token: 0x040016F2 RID: 5874
			Opacity,
			// Token: 0x040016F3 RID: 5875
			X,
			// Token: 0x040016F4 RID: 5876
			Y,
			// Token: 0x040016F5 RID: 5877
			Z
		}
	}
}
