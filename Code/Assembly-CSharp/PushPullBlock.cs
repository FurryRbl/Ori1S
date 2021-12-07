using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200042E RID: 1070
public class PushPullBlock : MonoBehaviour, IDamageReciever, IAttackable, IChargeFlameAttackable, IStompAttackable, IBashAttackable, ISuspendable, IPushable
{
	// Token: 0x14000036 RID: 54
	// (add) Token: 0x06001DD1 RID: 7633 RVA: 0x00083943 File Offset: 0x00081B43
	// (remove) Token: 0x06001DD2 RID: 7634 RVA: 0x0008395C File Offset: 0x00081B5C
	public event Action OnBashEvent = delegate()
	{
	};

	// Token: 0x17000506 RID: 1286
	// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x00083975 File Offset: 0x00081B75
	public Vector3 Position
	{
		get
		{
			return this.m_transform.position;
		}
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x00083982 File Offset: 0x00081B82
	public bool CanBeStomped()
	{
		return true;
	}

	// Token: 0x06001DD5 RID: 7637 RVA: 0x00083985 File Offset: 0x00081B85
	public bool CountsTowardsSuperJumpAchievement()
	{
		return false;
	}

	// Token: 0x06001DD6 RID: 7638 RVA: 0x00083988 File Offset: 0x00081B88
	public bool CanBeChargeFlamed()
	{
		return true;
	}

	// Token: 0x06001DD7 RID: 7639 RVA: 0x0008398B File Offset: 0x00081B8B
	public bool CanBeChargeDashed()
	{
		return false;
	}

	// Token: 0x06001DD8 RID: 7640 RVA: 0x0008398E File Offset: 0x00081B8E
	public bool CanBeGrenaded()
	{
		return true;
	}

	// Token: 0x06001DD9 RID: 7641 RVA: 0x00083991 File Offset: 0x00081B91
	public bool CountsTowardsPowerOfLightAchievement()
	{
		return false;
	}

	// Token: 0x06001DDA RID: 7642 RVA: 0x00083994 File Offset: 0x00081B94
	public bool IsDead()
	{
		return false;
	}

	// Token: 0x06001DDB RID: 7643 RVA: 0x00083997 File Offset: 0x00081B97
	public void OnEnable()
	{
		Targets.Attackables.Add(this);
		PushPullBlock.All.Add(this);
	}

	// Token: 0x06001DDC RID: 7644 RVA: 0x000839AF File Offset: 0x00081BAF
	public void OnDisable()
	{
		Targets.Attackables.Remove(this);
		PushPullBlock.All.Remove(this);
	}

	// Token: 0x06001DDD RID: 7645 RVA: 0x000839C8 File Offset: 0x00081BC8
	public void Awake()
	{
		SuspensionManager.Register(this);
		this.m_transform = base.transform;
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_colliders = base.GetComponentsInChildren<Collider>();
	}

	// Token: 0x06001DDE RID: 7646 RVA: 0x000839FF File Offset: 0x00081BFF
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x17000507 RID: 1287
	// (get) Token: 0x06001DDF RID: 7647 RVA: 0x00083A07 File Offset: 0x00081C07
	public bool IsGrabbed
	{
		get
		{
			return this.m_isGrabbed;
		}
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x00083A10 File Offset: 0x00081C10
	public void OnBashHighlight()
	{
		if (this.BashHighlight)
		{
			this.BashHighlight.AnimatorDriver.ContinueForward();
		}
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x00083A40 File Offset: 0x00081C40
	public void OnBashDehighlight()
	{
		if (this.BashHighlight)
		{
			this.BashHighlight.AnimatorDriver.ContinueBackwards();
		}
	}

	// Token: 0x17000508 RID: 1288
	// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x00083A6D File Offset: 0x00081C6D
	public int BashPriority
	{
		get
		{
			return 50;
		}
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x00083A71 File Offset: 0x00081C71
	public void Start()
	{
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_originalPosition = base.transform.position;
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x00083A90 File Offset: 0x00081C90
	public void FixedUpdate()
	{
		this.m_rigidbody.isKinematic = !Scenes.Manager.IsInsideActiveSceneBoundary(this.m_transform.position + Vector3.down * 2f);
		if (this.KeepAwake)
		{
			this.m_rigidbody.WakeUp();
		}
		if (this.IsSuspended)
		{
			if (!this.m_rigidbody.isKinematic)
			{
				this.m_rigidbody.Sleep();
			}
			return;
		}
		if (this.m_ignoreCollisionRemainingTime > 0f)
		{
			this.m_ignoreCollisionRemainingTime -= Time.deltaTime;
			this.IgnoreCollisionWithPlayer(Characters.Sein.PlatformBehaviour.PlatformMovement, true);
			if (this.m_ignoreCollisionRemainingTime <= 0f)
			{
				this.m_ignoreCollisionRemainingTime = 0f;
				this.IgnoreCollisionWithPlayer(Characters.Sein.PlatformBehaviour.PlatformMovement, false);
			}
		}
		if (!this.m_rigidbody.isKinematic)
		{
			this.ConstrainRotationFix();
			if (this.StrongFrictionWhenReleased && !this.m_isGrabbed)
			{
				Vector3 velocity = this.m_rigidbody.velocity;
				velocity.x *= 0.985f;
				this.m_rigidbody.AddForceSafe(velocity - this.m_rigidbody.velocity, ForceMode.VelocityChange);
			}
		}
		for (int i = 0; i < FloatZone.All.Count; i++)
		{
			FloatZone floatZone = FloatZone.All[i];
			if (floatZone.BoundingRect.Contains(this.m_transform.position))
			{
				Vector3 velocity2 = this.m_rigidbody.velocity;
				Vector3 a = floatZone.DesiredSpeed * Vector3.up;
				Vector3 vector = a - velocity2;
				vector.x = 0f;
				vector = Vector3.ClampMagnitude(vector, floatZone.Acceleration * Time.deltaTime);
				this.m_rigidbody.AddForce(vector, ForceMode.VelocityChange);
				this.m_rigidbody.AddForceSafe(-Physics.gravity, ForceMode.Acceleration);
			}
		}
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x00083C9C File Offset: 0x00081E9C
	private void ConstrainRotationFix()
	{
		this.m_angleFixHack++;
		if (this.m_angleFixHack == 30)
		{
			this.m_rigidbody.angularVelocity = new Vector3(0f, 0f, this.m_rigidbody.angularVelocity.z);
			this.m_rigidbody.rotation = Quaternion.Euler(0f, 0f, this.m_transform.eulerAngles.z);
			this.m_angleFixHack = 0;
		}
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x00083D28 File Offset: 0x00081F28
	public void OnPushOrPull(PlatformMovement platformMovement)
	{
		Vector3 position = platformMovement.Position;
		Vector3 pointVelocity = this.m_rigidbody.GetPointVelocity(position);
		Vector3 vector = platformMovement.WorldToLocal(pointVelocity);
		vector.x = platformMovement.LocalSpeedX;
		vector = platformMovement.LocalToWorld(vector);
		this.m_rigidbody.AddForceSafe(vector - pointVelocity, ForceMode.VelocityChange);
		this.m_isGrabbed = true;
		this.IgnoreCollisionWithPlayer(platformMovement, true);
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x00083DA4 File Offset: 0x00081FA4
	public void OnReleased(PlatformMovement platformMovement)
	{
		Vector3 vector = this.m_rigidbody.velocity;
		vector = platformMovement.WorldToLocal(vector);
		vector.x = 0f;
		vector = platformMovement.LocalToWorld(vector);
		this.m_rigidbody.AddForceSafe(vector - this.m_rigidbody.velocity, ForceMode.VelocityChange);
		this.IgnoreCollisionWithPlayer(platformMovement, false);
		if (platformMovement is PlatformMovementRigidbodyMoonCharacterControllerPenetrate)
		{
			(platformMovement as PlatformMovementRigidbodyMoonCharacterControllerPenetrate).TestAgainstWall();
		}
		this.m_isGrabbed = false;
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x00083E34 File Offset: 0x00082034
	public void OnHighlight()
	{
		foreach (LegacyAnimator legacyAnimator in this.HighlightAnimators)
		{
			legacyAnimator.ContinueForward();
		}
		if (this.HighlightSound)
		{
			this.HighlightSound.Play();
		}
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x00083E84 File Offset: 0x00082084
	public void OnDehighlight()
	{
		foreach (LegacyAnimator legacyAnimator in this.HighlightAnimators)
		{
			legacyAnimator.ContinueBackward();
		}
		if (this.DehighlightSound)
		{
			this.DehighlightSound.Play();
		}
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x00083ED1 File Offset: 0x000820D1
	public void OnGrabbed(PlatformMovement platformMovement)
	{
		this.IgnoreCollisionWithPlayer(platformMovement, true);
		this.m_isGrabbed = true;
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x00083EE4 File Offset: 0x000820E4
	public void IgnoreCollisionWithPlayer(PlatformMovement platformMovement, bool ignore)
	{
		for (int i = 0; i < this.m_colliders.Length; i++)
		{
			Collider collider = this.m_colliders[i];
			if (collider.enabled)
			{
				collider.gameObject.layer = LayerMask.NameToLayer((!ignore) ? "Default" : "pushPullBlock");
				collider.material = ((!ignore) ? this.PushPullMaterial : this.PushPullMaterialMoving);
			}
		}
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x00083F60 File Offset: 0x00082160
	public void OnRecieveDamage(Damage damage)
	{
		if (this.TakesDamage && damage.Amount > 500f)
		{
			base.transform.position = this.m_originalPosition;
		}
		DamageType type = damage.Type;
		if (type == DamageType.Bash)
		{
			this.m_ignoreCollisionRemainingTime = 0.2f;
			this.OnBashEvent();
			if (this.OnBashAction)
			{
				this.OnBashAction.Perform(null);
			}
		}
		this.m_rigidbody.AddForceSafe(damage.Force, ForceMode.Impulse);
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x00083FF6 File Offset: 0x000821F6
	public bool CanBeBashed()
	{
		return this.IsBashable;
	}

	// Token: 0x06001DEE RID: 7662 RVA: 0x00083FFE File Offset: 0x000821FE
	public float PushableSpeedRatio()
	{
		return this.MoveSpeedMultiplier;
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x00084006 File Offset: 0x00082206
	public void OnEnterBash()
	{
		this.m_ignoreCollisionRemainingTime = 20f;
		this.IgnoreCollisionWithPlayer(Characters.Sein.PlatformBehaviour.PlatformMovement, true);
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x00084029 File Offset: 0x00082229
	public bool CanActivateSwitch(GameObject theSwitch)
	{
		return true;
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x0008402C File Offset: 0x0008222C
	public bool CanBeSpiritFlamed()
	{
		return true;
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x0008402F File Offset: 0x0008222F
	public bool IsStompBouncable()
	{
		return false;
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x00084032 File Offset: 0x00082232
	public bool CanBeLevelUpBlasted()
	{
		return false;
	}

	// Token: 0x06001DF4 RID: 7668 RVA: 0x00084035 File Offset: 0x00082235
	public void OnSpiritFlameHighlight()
	{
	}

	// Token: 0x06001DF5 RID: 7669 RVA: 0x00084037 File Offset: 0x00082237
	public void OnSpiritFlameDehighlight()
	{
	}

	// Token: 0x17000509 RID: 1289
	// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x00084039 File Offset: 0x00082239
	public int SpiritFlamePriority
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700050A RID: 1290
	// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x0008403C File Offset: 0x0008223C
	public float OriDistanceFromAttackable
	{
		get
		{
			return 5f;
		}
	}

	// Token: 0x1700050B RID: 1291
	// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x00084043 File Offset: 0x00082243
	public float SpiritFlameRange
	{
		get
		{
			return float.PositiveInfinity;
		}
	}

	// Token: 0x1700050C RID: 1292
	// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x0008404A File Offset: 0x0008224A
	// (set) Token: 0x06001DFA RID: 7674 RVA: 0x00084052 File Offset: 0x00082252
	public bool IsSuspended { get; set; }

	// Token: 0x040019A3 RID: 6563
	public static AllContainer<PushPullBlock> All = new AllContainer<PushPullBlock>();

	// Token: 0x040019A4 RID: 6564
	public bool TakesDamage;

	// Token: 0x040019A5 RID: 6565
	private Rigidbody m_rigidbody;

	// Token: 0x040019A6 RID: 6566
	private Vector3 m_originalPosition;

	// Token: 0x040019A7 RID: 6567
	public PhysicMaterial PushPullMaterial;

	// Token: 0x040019A8 RID: 6568
	public PhysicMaterial PushPullMaterialMoving;

	// Token: 0x040019A9 RID: 6569
	private bool m_isGrabbed;

	// Token: 0x040019AA RID: 6570
	private float m_ignoreCollisionRemainingTime;

	// Token: 0x040019AB RID: 6571
	public LegacyAnimator[] HighlightAnimators;

	// Token: 0x040019AC RID: 6572
	public SoundSource HighlightSound;

	// Token: 0x040019AD RID: 6573
	public SoundSource DehighlightSound;

	// Token: 0x040019AE RID: 6574
	public bool StrongFrictionWhenReleased = true;

	// Token: 0x040019AF RID: 6575
	private Transform m_transform;

	// Token: 0x040019B0 RID: 6576
	private Collider[] m_colliders;

	// Token: 0x040019B1 RID: 6577
	public BaseAnimator BashHighlight;

	// Token: 0x040019B2 RID: 6578
	public bool KeepAwake;

	// Token: 0x040019B3 RID: 6579
	private int m_angleFixHack;

	// Token: 0x040019B4 RID: 6580
	public bool IsBashable = true;

	// Token: 0x040019B5 RID: 6581
	public ActionMethod OnBashAction;

	// Token: 0x040019B6 RID: 6582
	public float MoveSpeedMultiplier = 1f;
}
