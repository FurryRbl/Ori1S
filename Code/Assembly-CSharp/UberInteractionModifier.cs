using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007F6 RID: 2038
[ExecuteInEditMode]
[UberShaderLimitZ(MinZ = -5f, MaxZ = 5f)]
public abstract class UberInteractionModifier : UberShaderModifier, IDynamicGraphic, IAnimationVertex, IInteractable
{
	// Token: 0x17000782 RID: 1922
	// (get) Token: 0x06002ECA RID: 11978 RVA: 0x000C6380 File Offset: 0x000C4580
	protected Bounds Bounds
	{
		get
		{
			if (!this.m_boundsSet)
			{
				this.m_bounds = base.Renderer.bounds;
				this.m_boundsSet = true;
			}
			return this.m_bounds;
		}
	}

	// Token: 0x17000783 RID: 1923
	// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000C63AB File Offset: 0x000C45AB
	private float CurrentTimeA
	{
		get
		{
			return Mathf.Max(0f, UberInteractionManager.Instance.InteractionTime - this.m_prevSetTimeA);
		}
	}

	// Token: 0x17000784 RID: 1924
	// (get) Token: 0x06002ECC RID: 11980 RVA: 0x000C63C8 File Offset: 0x000C45C8
	private float CurrentTimeB
	{
		get
		{
			return Mathf.Max(0f, UberInteractionManager.Instance.InteractionTime - this.m_prevSetTimeB);
		}
	}

	// Token: 0x06002ECD RID: 11981 RVA: 0x000C63E8 File Offset: 0x000C45E8
	public void PlayA(Vector2 pos, Vector2 velocity, float strength, float radius, int flip)
	{
		if (!this.CheckProperties())
		{
			return;
		}
		Material bindMaterial = base.BindMaterial;
		velocity = this.TransformVelocity(velocity);
		Vector4 vector = bindMaterial.GetVector(this.m_posId);
		Vector4 vector2 = bindMaterial.GetVector(this.m_velId);
		Vector4 vector3 = bindMaterial.GetVector(this.m_paramsId);
		Vector4 vector4 = bindMaterial.GetVector(this.m_params2Id);
		bindMaterial.SetVector(this.m_posId, new Vector4(pos.x, pos.y, vector.z, vector.w));
		bindMaterial.SetVector(this.m_velId, new Vector4(velocity.x, velocity.y, vector2.z, vector2.w));
		bindMaterial.SetVector(this.m_paramsId, new Vector4(UberInteractionManager.Instance.InteractionTime, strength * (float)flip, vector3.z, vector3.w));
		bindMaterial.SetVector(this.m_params2Id, new Vector4(radius, -100f, vector4.z, vector4.w));
		this.m_lastStrengthA = strength;
		this.m_prevSetTimeA = UberInteractionManager.Instance.InteractionTime;
	}

	// Token: 0x06002ECE RID: 11982 RVA: 0x000C650C File Offset: 0x000C470C
	public void PlayB(Vector2 pos, Vector2 velocity, float strength, float radius, int flip)
	{
		if (!this.CheckProperties())
		{
			return;
		}
		velocity = this.TransformVelocity(velocity);
		Material bindMaterial = base.BindMaterial;
		Vector4 vector = bindMaterial.GetVector(this.m_posId);
		Vector4 vector2 = bindMaterial.GetVector(this.m_velId);
		Vector4 vector3 = bindMaterial.GetVector(this.m_paramsId);
		Vector4 vector4 = bindMaterial.GetVector(this.m_params2Id);
		bindMaterial.SetVector(this.m_posId, new Vector4(vector.x, vector.y, pos.x, pos.y));
		bindMaterial.SetVector(this.m_velId, new Vector4(vector2.x, vector2.y, velocity.x, velocity.y));
		bindMaterial.SetVector(this.m_paramsId, new Vector4(vector3.x, vector3.y, UberInteractionManager.Instance.InteractionTime, strength * (float)flip));
		bindMaterial.SetVector(this.m_params2Id, new Vector4(vector4.x, vector4.y, radius, -100f));
		this.m_lastStrengthB = strength;
		this.m_prevSetTimeB = UberInteractionManager.Instance.InteractionTime;
	}

	// Token: 0x06002ECF RID: 11983 RVA: 0x000C6630 File Offset: 0x000C4830
	private Vector2 TransformVelocity(Vector2 velocity)
	{
		if (velocity != Vector2.zero)
		{
			velocity = base.transform.InverseTransformDirection(velocity);
		}
		return velocity;
	}

	// Token: 0x06002ED0 RID: 11984 RVA: 0x000C6666 File Offset: 0x000C4866
	private bool CheckProperties()
	{
		return !(base.BindMaterial == null);
	}

	// Token: 0x06002ED1 RID: 11985 RVA: 0x000C667C File Offset: 0x000C487C
	public bool ShouldOverrideA(float calcStr, float difficulty)
	{
		if (this.CurrentTimeA < 0.2f)
		{
			return false;
		}
		float num = 1f - this.CurrentTimeA / this.m_duration;
		float num2 = this.m_lastStrengthA * num;
		float num3 = Mathf.Abs(this.m_lastStrengthA) * num;
		float num4 = Mathf.Abs(calcStr - num2) / num3;
		return num4 > 2.5f * difficulty;
	}

	// Token: 0x06002ED2 RID: 11986 RVA: 0x000C66DC File Offset: 0x000C48DC
	public bool ShouldOverrideB(float calcStr, float difficulty)
	{
		if (this.CurrentTimeB < 0.2f)
		{
			return false;
		}
		float num = 1f - this.CurrentTimeB / this.m_duration;
		float num2 = this.m_lastStrengthB * num;
		float num3 = Mathf.Abs(this.m_lastStrengthB) * num;
		float num4 = Mathf.Abs(calcStr - num2) / num3;
		return num4 > 2.5f * difficulty;
	}

	// Token: 0x17000785 RID: 1925
	// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x000C673B File Offset: 0x000C493B
	// (set) Token: 0x06002ED4 RID: 11988 RVA: 0x000C6743 File Offset: 0x000C4943
	public int Index { get; set; }

	// Token: 0x17000786 RID: 1926
	// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x000C674C File Offset: 0x000C494C
	// (set) Token: 0x06002ED6 RID: 11990 RVA: 0x000C6754 File Offset: 0x000C4954
	public bool IsRegistered { get; set; }

	// Token: 0x17000787 RID: 1927
	// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x000C675D File Offset: 0x000C495D
	// (set) Token: 0x06002ED8 RID: 11992 RVA: 0x000C6765 File Offset: 0x000C4965
	public bool WantsToRegister { get; set; }

	// Token: 0x17000788 RID: 1928
	// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x000C676E File Offset: 0x000C496E
	public virtual string InteractionName
	{
		get
		{
			return string.Empty;
		}
	}

	// Token: 0x06002EDA RID: 11994 RVA: 0x000C6775 File Offset: 0x000C4975
	public override void SetProperties()
	{
	}

	// Token: 0x06002EDB RID: 11995 RVA: 0x000C6777 File Offset: 0x000C4977
	public override bool DoStrip()
	{
		return false;
	}

	// Token: 0x06002EDC RID: 11996 RVA: 0x000C677A File Offset: 0x000C497A
	private void OnDisable()
	{
		if (UberInteractionManager.Instance != null)
		{
			UberInteractionManager.Instance.RemoveInteractor(this);
		}
	}

	// Token: 0x17000789 RID: 1929
	// (get) Token: 0x06002EDD RID: 11997 RVA: 0x000C6797 File Offset: 0x000C4997
	public float Duration
	{
		get
		{
			return UberInteractionManager.Instance.Curves[(int)this.CurveType].Duration;
		}
	}

	// Token: 0x06002EDE RID: 11998 RVA: 0x000C67B4 File Offset: 0x000C49B4
	public override IEnumerable<string> GetBaseVertexTextureNames()
	{
		yield return "_InteractionCurve" + this.CurveType;
		yield break;
	}

	// Token: 0x06002EDF RID: 11999 RVA: 0x000C67D8 File Offset: 0x000C49D8
	public override IEnumerable<string> GetKeywordsForShader()
	{
		yield return "INTERACTION_CURVE_" + this.InteractionName.ToUpper() + "@" + this.CurveType.ToString();
		yield break;
	}

	// Token: 0x06002EE0 RID: 12000 RVA: 0x000C67FB File Offset: 0x000C49FB
	protected virtual float GetStrength(Vector3 velocity, Vector4 strengthVal)
	{
		return 1f;
	}

	// Token: 0x1700078A RID: 1930
	// (get) Token: 0x06002EE1 RID: 12001
	protected abstract UberInteractionManager.PropertyIDCache PropertyCache { get; }

	// Token: 0x06002EE2 RID: 12002 RVA: 0x000C6802 File Offset: 0x000C4A02
	private void OnEnable()
	{
		if (UberInteractionManager.Instance != null)
		{
			UberInteractionManager.Instance.RegisterInteractor(this);
		}
	}

	// Token: 0x06002EE3 RID: 12003 RVA: 0x000C6820 File Offset: 0x000C4A20
	public void OnRegistered()
	{
		this.m_duration = this.Duration;
		this.m_lastStrengthA = (this.m_prevSetTimeA = 0f);
		this.m_lastStrengthB = (this.m_prevSetTimeB = 0f);
		UberInteractionManager.PropertyIDCache propertyCache = this.PropertyCache;
		this.m_posId = propertyCache.PosId;
		this.m_velId = propertyCache.VelId;
		this.m_paramsId = propertyCache.ParamsId;
		this.m_params2Id = propertyCache.Params2Id;
		this.SetBounds();
	}

	// Token: 0x1700078B RID: 1931
	// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x000C689E File Offset: 0x000C4A9E
	protected virtual float OverrideDifficulty
	{
		get
		{
			return 1f;
		}
	}

	// Token: 0x06002EE5 RID: 12005 RVA: 0x000C68A8 File Offset: 0x000C4AA8
	public void SetInteraction(float time, Vector3 pos, Vector3 prevPos, Vector4 strength, Vector3 velocity, float radius, bool explosion)
	{
		Bounds bounds = this.Bounds;
		pos.x = Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
		pos.y = Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);
		float strength2 = this.GetStrength(velocity, strength);
		float sqrMagnitude = (pos - this.m_interactionA.SetPos).sqrMagnitude;
		float sqrMagnitude2 = (pos - this.m_interactionA.SetPos).sqrMagnitude;
		UberInteractionModifier.Interaction interaction = (sqrMagnitude >= sqrMagnitude2) ? this.m_interactionB : this.m_interactionA;
		if (!interaction.ReadyToPlay)
		{
			return;
		}
		int num = -1;
		if (this.CurrentTimeA > this.m_duration)
		{
			num = 0;
		}
		else if (this.CurrentTimeB > this.m_duration)
		{
			num = 1;
		}
		else if (this.ShouldOverrideA(strength2, this.OverrideDifficulty))
		{
			num = 0;
		}
		else if (this.ShouldOverrideB(strength2, this.OverrideDifficulty))
		{
			num = 1;
		}
		if (num == -1)
		{
			return;
		}
		this.OnPlay(strength2, explosion);
		if (Mathf.Abs(strength2) > 0.05f)
		{
			interaction.Play();
			if (num == 0)
			{
				this.PlayA(pos, velocity, strength2, radius * 0.5f, this.GetFlip(pos, explosion));
			}
			if (num == 1)
			{
				this.PlayB(pos, velocity, strength2, radius * 0.5f, this.GetFlip(pos, explosion));
			}
		}
	}

	// Token: 0x06002EE6 RID: 12006 RVA: 0x000C6A70 File Offset: 0x000C4C70
	protected int RandomSign(float val)
	{
		if (val >= 0.1f)
		{
			return 1;
		}
		if (val <= -0.1f)
		{
			return -1;
		}
		return (UnityEngine.Random.value <= 0.5f) ? -1 : 1;
	}

	// Token: 0x06002EE7 RID: 12007 RVA: 0x000C6AAD File Offset: 0x000C4CAD
	protected virtual int GetFlip(Vector3 pos, bool explode)
	{
		return 1;
	}

	// Token: 0x06002EE8 RID: 12008 RVA: 0x000C6AB0 File Offset: 0x000C4CB0
	private void SetBounds()
	{
		this.m_bounds = base.Renderer.bounds;
		Bounds bounds = default(Bounds);
		Bounds bounds2 = default(Bounds);
		bounds.min = this.Bounds.min;
		bounds.max = new Vector3(this.m_bounds.center.x, this.m_bounds.max.y, this.m_bounds.max.z);
		bounds2.min = new Vector3(this.m_bounds.center.x, this.m_bounds.min.y, this.m_bounds.min.z);
		bounds2.max = this.m_bounds.max;
		this.m_interactionA.SetPos = bounds.center;
		this.m_interactionB.SetPos = bounds2.center;
		this.m_maxRadius = Mathf.Max(this.Bounds.extents.x, this.Bounds.extents.y) * 2f;
		this.m_pos = base.transform.position;
	}

	// Token: 0x06002EE9 RID: 12009 RVA: 0x000C6C0A File Offset: 0x000C4E0A
	public float MaxRadius()
	{
		return this.m_maxRadius;
	}

	// Token: 0x06002EEA RID: 12010 RVA: 0x000C6C14 File Offset: 0x000C4E14
	public bool DoesOverlap(Vector3 position, Vector3 velocity, float radius, float zScale)
	{
		float num = velocity.x * 0.016f;
		float num2 = velocity.y * 0.016f;
		float a = position.x - num;
		float x = position.x;
		float a2 = position.y - num2;
		float y = position.y;
		Vector2 vector = new Vector2(Mathf.Min(a, x) - radius + Mathf.Min(num, 0f), Mathf.Min(a2, y) - radius + Mathf.Min(num2, 0f));
		Vector2 vector2 = new Vector2(Mathf.Max(a, x) + radius + Mathf.Max(num, 0f), Mathf.Max(a2, y) + radius + Mathf.Max(num2, 0f));
		this.m_velRect.xMin = vector.x;
		this.m_velRect.yMin = vector.y;
		this.m_velRect.xMax = vector2.x;
		this.m_velRect.yMax = vector2.y;
		float num3 = this.SizeShrink * 0.5f;
		Bounds bounds = this.Bounds;
		float num4 = bounds.extents.x * num3;
		float num5 = bounds.extents.y * num3;
		this.m_rendRect.xMin = bounds.center.x - num4;
		this.m_rendRect.yMin = bounds.center.y - num5;
		this.m_rendRect.xMax = bounds.center.x + num4;
		this.m_rendRect.yMax = bounds.center.y + num5;
		return this.m_velRect.Overlaps(this.m_rendRect) && Mathf.Abs(position.z - bounds.center.z) < radius * zScale;
	}

	// Token: 0x06002EEB RID: 12011 RVA: 0x000C6E16 File Offset: 0x000C5016
	public bool IsWater()
	{
		return false;
	}

	// Token: 0x06002EEC RID: 12012 RVA: 0x000C6E19 File Offset: 0x000C5019
	public Vector3 GetPosition()
	{
		return this.m_pos;
	}

	// Token: 0x06002EED RID: 12013 RVA: 0x000C6E24 File Offset: 0x000C5024
	public Vector3 GetExplodePoint(Vector3 position)
	{
		Bounds bounds = this.Bounds;
		position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
		position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
		return position;
	}

	// Token: 0x06002EEE RID: 12014 RVA: 0x000C6E9C File Offset: 0x000C509C
	protected virtual void OnPlay(float strength, bool explosion)
	{
	}

	// Token: 0x040029FA RID: 10746
	private const float c_signEps = 0.1f;

	// Token: 0x040029FB RID: 10747
	public float SizeShrink = 1f;

	// Token: 0x040029FC RID: 10748
	public UberInteractionManager.InteractionCurveType CurveType;

	// Token: 0x040029FD RID: 10749
	private UberInteractionModifier.Interaction m_interactionA;

	// Token: 0x040029FE RID: 10750
	private UberInteractionModifier.Interaction m_interactionB;

	// Token: 0x040029FF RID: 10751
	private float m_maxRadius;

	// Token: 0x04002A00 RID: 10752
	private Rect m_velRect;

	// Token: 0x04002A01 RID: 10753
	private Rect m_rendRect;

	// Token: 0x04002A02 RID: 10754
	private Vector3 m_pos;

	// Token: 0x04002A03 RID: 10755
	private Bounds m_bounds;

	// Token: 0x04002A04 RID: 10756
	private bool m_boundsSet;

	// Token: 0x04002A05 RID: 10757
	private float m_prevSetTimeA;

	// Token: 0x04002A06 RID: 10758
	private float m_lastStrengthA;

	// Token: 0x04002A07 RID: 10759
	private float m_prevSetTimeB;

	// Token: 0x04002A08 RID: 10760
	private float m_lastStrengthB;

	// Token: 0x04002A09 RID: 10761
	private float m_duration;

	// Token: 0x04002A0A RID: 10762
	private int m_posId;

	// Token: 0x04002A0B RID: 10763
	private int m_velId;

	// Token: 0x04002A0C RID: 10764
	private int m_paramsId;

	// Token: 0x04002A0D RID: 10765
	private int m_params2Id;

	// Token: 0x02000839 RID: 2105
	private struct Interaction
	{
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x000CB7BC File Offset: 0x000C99BC
		public bool ReadyToPlay
		{
			get
			{
				return UberInteractionManager.Instance.InteractionTime - this.m_lastTime > UberInteractionManager.Instance.PlayDelayTime;
			}
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000CB7DB File Offset: 0x000C99DB
		public void Play()
		{
			this.m_lastTime = UberInteractionManager.Instance.InteractionTime;
		}

		// Token: 0x04002B41 RID: 11073
		public Vector3 SetPos;

		// Token: 0x04002B42 RID: 11074
		private float m_lastTime;
	}
}
