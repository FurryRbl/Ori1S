using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class SpiritFlameProjectile : MonoBehaviour, ISuspendable
{
	// Token: 0x1700012C RID: 300
	// (get) Token: 0x0600049D RID: 1181 RVA: 0x00012BD9 File Offset: 0x00010DD9
	// (set) Token: 0x0600049E RID: 1182 RVA: 0x00012BE1 File Offset: 0x00010DE1
	public bool IsSuspended
	{
		get
		{
			return this.m_suspended;
		}
		set
		{
			this.m_suspended = value;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x0600049F RID: 1183 RVA: 0x00012BEA File Offset: 0x00010DEA
	// (set) Token: 0x060004A0 RID: 1184 RVA: 0x00012BF2 File Offset: 0x00010DF2
	public Transform StartTarget { get; set; }

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00012BFB File Offset: 0x00010DFB
	// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00012C03 File Offset: 0x00010E03
	public SeinCharacter Sein { get; set; }

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00012C0C File Offset: 0x00010E0C
	// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00012C14 File Offset: 0x00010E14
	public Transform AttackableTargetTransform { get; set; }

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00012C1D File Offset: 0x00010E1D
	// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00012C25 File Offset: 0x00010E25
	public Vector3 StartPosition { get; set; }

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00012C2E File Offset: 0x00010E2E
	// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00012C36 File Offset: 0x00010E36
	public SpiritFlame SpiritFlame { get; set; }

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00012C3F File Offset: 0x00010E3F
	// (set) Token: 0x060004AA RID: 1194 RVA: 0x00012C47 File Offset: 0x00010E47
	public float Damage { get; set; }

	// Token: 0x060004AB RID: 1195 RVA: 0x00012C50 File Offset: 0x00010E50
	public void PlayThrowSound()
	{
		Sound.Play(this.ThrowSound.GetSound(null), base.transform.position, null);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00012C7C File Offset: 0x00010E7C
	public void PlayHitSound()
	{
		if (this.HitSound && this.DoImpact)
		{
			Sound.Play(this.HitSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00012CC2 File Offset: 0x00010EC2
	public void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00012CCA File Offset: 0x00010ECA
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00012CD4 File Offset: 0x00010ED4
	private void OnPoolSpawned()
	{
		this.m_hitEffect = null;
		this.m_points.Clear();
		this.m_finalPosition = Vector3.zero;
		this.m_arcOffset = 0f;
		this.m_currentTime = 0f;
		this.m_lastTargetPosition = Vector3.zero;
		this.m_currentState = SpiritFlameProjectile.State.Throwing;
		this.HasARealTarget = false;
		this.DisplacementCurveOffset = 0f;
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00012D38 File Offset: 0x00010F38
	public void Start()
	{
		this.m_frame = UnityEngine.Random.Range(0, 5);
		this.m_arcOffset = FixedRandom.Values[6] * 2f;
		this.m_arcOffset = -SpiritFlameProjectile.s_lastFlameArcOffsetSign * Mathf.Pow(this.m_arcOffset, 1f);
		SpiritFlameProjectile.s_lastFlameArcOffsetSign *= -1f;
		this.DisplacementCurveOffset = FixedRandom.Values[0] * 10f;
		if (this.StartTarget)
		{
			this.StartPosition = this.StartTarget.position;
		}
		float x = 1f - 2f * FixedRandom.Values[3];
		float y = 1f - 2f * FixedRandom.Values[5];
		Vector3 finalPosition = this.m_finalPosition;
		Vector3 vector = new Vector3(x, y, 0f);
		this.m_finalPosition = finalPosition + vector.normalized * 0.1f;
		this.LineRenderer.SetVertexCount(this.LineVertexCount);
		for (int i = 0; i < this.LineVertexCount; i++)
		{
			this.LineRenderer.SetPosition(i, Vector3.zero);
		}
		this.PlayThrowSound();
		if (this.AttackableTargetTransform)
		{
			this.m_finalPosition = this.AttackableTargetTransform.position;
			if (this.ThrowEffectGameObject)
			{
				GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.ThrowEffectGameObject, this.StartPosition, Quaternion.identity);
				Vector3 pointOnArc = this.GetPointOnArc(0, 20);
				Vector3 pointOnArc2 = this.GetPointOnArc(4, 20);
				Vector3 normalized = (pointOnArc - pointOnArc2).normalized;
				gameObject.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(normalized));
				FollowPositionRotation component = gameObject.GetComponent<FollowPositionRotation>();
				component.SetTarget(this.StartTarget);
			}
			this.UpdateTargetPosition(this.AttackableTargetTransform.position + this.ImpactOffset);
		}
		for (int j = 0; j < this.LineRenderer.materials.Length; j++)
		{
			Material material = this.LineRenderer.materials[j];
			material.SetTextureOffset("_DistortionTex", new Vector2(Time.time * 0.6f, 0f));
		}
		this.ChangeState(SpiritFlameProjectile.State.Throwing);
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00012F8A File Offset: 0x0001118A
	public void ChangeState(SpiritFlameProjectile.State state)
	{
		this.m_currentState = state;
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00012F93 File Offset: 0x00011193
	public bool TargetHasMovedTooMuch(Vector3 newPos)
	{
		return Vector3.Distance(newPos, this.m_lastTargetPosition) > 1f;
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00012FA8 File Offset: 0x000111A8
	public void UpdateTargetPosition(Vector3 newPos)
	{
		this.m_lastTargetPosition = newPos;
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00012FB4 File Offset: 0x000111B4
	public Vector3 GetPointOnArc(int pointIndex, int numberOfPoints)
	{
		Vector3 a = this.m_lastTargetPosition - this.StartPosition;
		Vector3 a2 = Vector3.Cross(a.normalized, Vector3.forward);
		float num = (float)pointIndex / (float)(numberOfPoints - 1);
		Vector3 result = this.StartPosition + num * a + this.DisplacementCurve.Evaluate(num) * a2 * this.m_arcOffset;
		result.z = base.transform.position.z;
		return result;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00013040 File Offset: 0x00011240
	public Vector3 GetPointOnArc(float r)
	{
		Vector3 a = this.m_lastTargetPosition - this.StartPosition;
		Vector3 a2 = Vector3.Cross(a.normalized, Vector3.forward);
		Vector3 result = this.StartPosition + r * a + this.DisplacementCurve.Evaluate(r) * a2 * this.m_arcOffset;
		result.z = base.transform.position.z;
		return result;
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x000130C4 File Offset: 0x000112C4
	public Vector2 GetHitVector()
	{
		Vector3 pointOnArc = this.GetPointOnArc(16, 20);
		Vector3 pointOnArc2 = this.GetPointOnArc(19, 20);
		return (pointOnArc2 - pointOnArc).normalized;
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x000130FC File Offset: 0x000112FC
	public void UpdateLineRenderer()
	{
		float z = base.transform.position.z;
		Vector3 a = this.m_lastTargetPosition - this.StartPosition;
		Vector3 normalized = a.normalized;
		Vector3 a2 = Vector3.Cross(normalized, Vector3.forward);
		this.m_points.Clear();
		for (int i = 0; i < this.LineVertexCount; i++)
		{
			float num = (float)i / (float)(this.LineVertexCount - 1);
			float num2 = num * Mathf.Min(1f, this.SpeedCurve.Evaluate(this.m_currentTime / this.Duration));
			Vector3 item = this.StartPosition + num2 * a + this.DisplacementCurve.Evaluate(num2) * a2 * this.m_arcOffset;
			item.z = z;
			this.m_points.Add(item);
		}
		for (int j = 1; j < this.LineVertexCount - 1; j++)
		{
			Vector3 position = Vector3.Lerp(Vector3.Lerp(this.m_points[j - 1], this.m_points[j], 0.5f), Vector3.Lerp(this.m_points[j], this.m_points[j + 1], 0.5f), 0.5f);
			this.LineRenderer.SetPosition(this.LineVertexCount - 1 - j, position);
		}
		this.LineRenderer.SetPosition(0, this.m_points[this.LineVertexCount - 1]);
		this.LineRenderer.SetPosition(this.LineVertexCount - 1, this.m_points[0]);
		Material material = this.LineRenderer.material;
		Vector2 textureOffset = material.GetTextureOffset("_MainTex");
		textureOffset.x = this.TextureOffsetCurve.Evaluate(this.m_currentTime / this.Duration);
		material.SetTextureOffset("_MainTex", textureOffset);
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00013300 File Offset: 0x00011500
	public void UpdateState()
	{
		SpiritFlameProjectile.State currentState = this.m_currentState;
		if (currentState != SpiritFlameProjectile.State.Throwing)
		{
			if (currentState == SpiritFlameProjectile.State.Fading)
			{
				if (this.m_currentTime > this.DestroyDelay * 2f + this.Duration)
				{
					InstantiateUtility.Destroy(base.gameObject);
				}
			}
		}
		else
		{
			if (this.AttackableTargetTransform == null || !this.AttackableTargetTransform.gameObject.activeInHierarchy)
			{
				InstantiateUtility.Destroy(base.gameObject);
				return;
			}
			Vector3 newPos = this.AttackableTargetTransform.position + this.ImpactOffset;
			if (this.TargetHasMovedTooMuch(newPos))
			{
				this.Discharge();
			}
			else
			{
				this.UpdateTargetPosition(newPos);
			}
			if (!this.HasARealTarget && this.StartTarget)
			{
				this.StartPosition = this.StartTarget.position;
			}
			if (this.m_currentTime > this.Duration)
			{
				Damage damage = new Damage(this.Damage, this.GetHitVector() * this.Damage / 4f, this.m_lastTargetPosition + this.ImpactOffset, this.DamageType, base.gameObject);
				damage.DealToComponents(this.AttackableTargetTransform.gameObject);
				this.Detonate();
				this.AttackableTargetTransform.gameObject.GetComponents<EntityDamageReciever>(SpiritFlameProjectile.s_enityDamageRecieverList);
				for (int i = 0; i < SpiritFlameProjectile.s_enityDamageRecieverList.Count; i++)
				{
					EntityDamageReciever entityDamageReciever = SpiritFlameProjectile.s_enityDamageRecieverList[i];
					if (entityDamageReciever.Health <= 0f)
					{
						if (this.KillEntitySound)
						{
							Sound.Play(this.KillEntitySound.GetSound(null), this.AttackableTargetTransform.position, null);
						}
						break;
					}
				}
				SpiritFlameProjectile.s_enityDamageRecieverList.Clear();
			}
		}
		this.m_currentTime += Time.deltaTime;
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x00013500 File Offset: 0x00011700
	public void Detonate()
	{
		this.PlayHitSound();
		if (this.ImpactEffectGameObject && this.DoImpact)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.ImpactEffectGameObject, this.m_lastTargetPosition, Quaternion.identity);
			gameObject.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(this.GetHitVector()));
			this.m_hitEffect = gameObject;
			if (UberPoolManager.Instance)
			{
				UberPoolManager.Instance.AddOnDestroyed(this.m_hitEffect, delegate
				{
					this.m_hitEffect = null;
				});
			}
		}
		this.ChangeState(SpiritFlameProjectile.State.Fading);
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x000135A8 File Offset: 0x000117A8
	public void Discharge()
	{
		this.ChangeState(SpiritFlameProjectile.State.Fading);
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x000135B4 File Offset: 0x000117B4
	public bool RayTest(GameObject target)
	{
		Vector3 position = this.AttackableTargetTransform.position;
		Vector3 position2 = target.transform.position;
		Vector3 vector = position2 - position;
		RaycastHit raycastHit;
		return !Physics.Raycast(position, vector.normalized, out raycastHit, vector.magnitude, this.Sein.Controller.RayTestLayerMask) || !(raycastHit.collider.gameObject != target) || raycastHit.distance >= vector.magnitude;
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x00013640 File Offset: 0x00011840
	public ISpiritFlameAttackable IsShootableTarget(Collider target)
	{
		if (!target.gameObject)
		{
			return null;
		}
		ISpiritFlameAttackable spiritFlameAttackable = target.gameObject.FindComponent<ISpiritFlameAttackable>();
		IAttackable attackable = spiritFlameAttackable as IAttackable;
		if (attackable == null)
		{
			return null;
		}
		if (InstantiateUtility.IsDestroyed(spiritFlameAttackable as Component))
		{
			return null;
		}
		if (!attackable.CanBeSpiritFlamed())
		{
			return null;
		}
		if (attackable.IsDead())
		{
			return null;
		}
		if (spiritFlameAttackable.RequiresSpiritFlameAbilityToTarget && !this.Sein.PlayerAbilities.SpiritFlame.HasAbility)
		{
			return null;
		}
		Vector3 position = (spiritFlameAttackable as Component).transform.position;
		float magnitude = (position - this.AttackableTargetTransform.position).magnitude;
		if (magnitude > 10f)
		{
			return null;
		}
		if (magnitude > spiritFlameAttackable.SpiritFlameRange)
		{
			return null;
		}
		if (!this.RayTest((spiritFlameAttackable as Component).gameObject))
		{
			return null;
		}
		return spiritFlameAttackable;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00013730 File Offset: 0x00011930
	public void FixedUpdate()
	{
		if (this.m_suspended)
		{
			return;
		}
		this.m_frame++;
		this.UpdateState();
		if (this.m_hitEffect)
		{
			this.m_hitEffect.transform.position = this.m_lastTargetPosition;
		}
		if (this.m_frame % 2 == 0)
		{
			this.UpdateLineRenderer();
		}
	}

	// Token: 0x040003B7 RID: 951
	public float Duration = 0.2f;

	// Token: 0x040003B8 RID: 952
	public LineRenderer LineRenderer;

	// Token: 0x040003B9 RID: 953
	public int LineVertexCount;

	// Token: 0x040003BA RID: 954
	public AnimationCurve TextureOffsetCurve;

	// Token: 0x040003BB RID: 955
	public AnimationCurve SpeedCurve;

	// Token: 0x040003BC RID: 956
	public float DestroyDelay;

	// Token: 0x040003BD RID: 957
	public SoundProvider HitSound;

	// Token: 0x040003BE RID: 958
	public SoundProvider KillEntitySound;

	// Token: 0x040003BF RID: 959
	public SoundProvider ThrowSound;

	// Token: 0x040003C0 RID: 960
	public GameObject ImpactEffectGameObject;

	// Token: 0x040003C1 RID: 961
	public GameObject ThrowEffectGameObject;

	// Token: 0x040003C2 RID: 962
	public AnimationCurve DisplacementCurve;

	// Token: 0x040003C3 RID: 963
	public float DisplacementCurveOffset;

	// Token: 0x040003C4 RID: 964
	public DamageType DamageType = DamageType.SpiritFlame;

	// Token: 0x040003C5 RID: 965
	public bool HasARealTarget;

	// Token: 0x040003C6 RID: 966
	private bool m_suspended;

	// Token: 0x040003C7 RID: 967
	[PooledSafe]
	public Vector2 ImpactOffset;

	// Token: 0x040003C8 RID: 968
	[PooledSafe]
	[NonSerialized]
	public bool DoImpact = true;

	// Token: 0x040003C9 RID: 969
	private GameObject m_hitEffect;

	// Token: 0x040003CA RID: 970
	private readonly List<Vector3> m_points = new List<Vector3>();

	// Token: 0x040003CB RID: 971
	private Vector3 m_finalPosition;

	// Token: 0x040003CC RID: 972
	private float m_arcOffset;

	// Token: 0x040003CD RID: 973
	private float m_currentTime;

	// Token: 0x040003CE RID: 974
	private Vector3 m_lastTargetPosition;

	// Token: 0x040003CF RID: 975
	private SpiritFlameProjectile.State m_currentState;

	// Token: 0x040003D0 RID: 976
	private int m_frame;

	// Token: 0x040003D1 RID: 977
	private static float s_lastFlameArcOffsetSign = 1f;

	// Token: 0x040003D2 RID: 978
	private static readonly List<EntityDamageReciever> s_enityDamageRecieverList = new List<EntityDamageReciever>();

	// Token: 0x0200008B RID: 139
	public enum State
	{
		// Token: 0x04000495 RID: 1173
		Throwing,
		// Token: 0x04000496 RID: 1174
		Fading
	}
}
