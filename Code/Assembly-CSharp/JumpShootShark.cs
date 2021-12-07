using System;
using System.Collections.Generic;
using fsm;
using fsm.triggers;
using UnityEngine;

// Token: 0x020005AB RID: 1451
public class JumpShootShark : Enemy
{
	// Token: 0x06002511 RID: 9489 RVA: 0x000A1C22 File Offset: 0x0009FE22
	public bool ShouldIgnoreDamage(Damage damage)
	{
		return (damage != null && damage.Type == DamageType.Acid) || this.Controller.StateMachine.CurrentState == this.State.Hidden;
	}

	// Token: 0x06002512 RID: 9490 RVA: 0x000A1C5B File Offset: 0x0009FE5B
	public override void Awake()
	{
		base.Awake();
		JumpShootShark.All.Add(this);
	}

	// Token: 0x06002513 RID: 9491 RVA: 0x000A1C6E File Offset: 0x0009FE6E
	public override void OnDestroy()
	{
		base.OnDestroy();
		JumpShootShark.All.Remove(this);
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x000A1C82 File Offset: 0x0009FE82
	public void HideGraphics()
	{
		this.HideGroup.SetActive(false);
	}

	// Token: 0x06002515 RID: 9493 RVA: 0x000A1C90 File Offset: 0x0009FE90
	public void ShowGraphics()
	{
		this.HideGroup.SetActive(true);
	}

	// Token: 0x06002516 RID: 9494 RVA: 0x000A1CA0 File Offset: 0x0009FEA0
	public new void Start()
	{
		base.Start();
		this.DamageReciever.IgnoreDamageCondition = new Func<Damage, bool>(this.ShouldIgnoreDamage);
		this.State.Hidden = new State();
		this.State.Emerging = new State();
		this.State.Jumping = new State();
		this.State.Submerge = new State();
		this.State.Hidden.OnEnterEvent = delegate()
		{
			base.DeactivateTargetting();
			base.DeactivateDamageDealer();
			this.HideGraphics();
		};
		this.State.Hidden.OnExitEvent = delegate()
		{
			this.ShowGraphics();
		};
		this.State.Emerging.OnEnterEvent = delegate()
		{
			base.transform.position = this.EmergePosition;
			base.DeactivateTargetting();
			base.DeactivateDamageDealer();
			base.SpawnPrefab(this.EmergePrefab);
			if (this.EmergeAnticipation)
			{
				this.EmergeAnticipation.Play();
			}
		};
		this.State.Jumping.OnEnterEvent = delegate()
		{
			base.ActivateTargetting();
			base.ActivateDamageDealer();
			base.PlayAnimationOnce(this.Animations.Jumping, 0);
			base.FacePlayer();
			if (this.Emerge)
			{
				this.Emerge.Play();
			}
		};
		this.State.Submerge.OnEnterEvent = delegate()
		{
			base.DeactivateTargetting();
			base.DeactivateDamageDealer();
			base.SpawnPrefab(this.SubmergePrefab);
			if (this.Submerge)
			{
				this.Submerge.Play();
			}
		};
		this.State.Jumping.UpdateStateEvent = new Action(this.UpdateJumpingState);
		this.State.Jumping.OnExitEvent = new Action(this.ExitJumpingState);
		this.Controller.StateMachine.Configure(this.State.Hidden);
		this.Controller.StateMachine.Configure(this.State.Emerging).AddTransition<OnFixedUpdate>(this.State.Jumping, () => base.AfterTime(this.Settings.EmergingDuration), null);
		this.Controller.StateMachine.Configure(this.State.Jumping).AddTransition<OnFixedUpdate>(this.State.Submerge, () => base.AfterTime(this.Settings.JumpDuration), null);
		this.Controller.StateMachine.Configure(this.State.Submerge).AddTransition<OnFixedUpdate>(this.State.Hidden, () => base.AfterTime(this.Settings.SubmergeDuration), null);
		this.Controller.StateMachine.RegisterStates(new IState[]
		{
			this.State.Hidden,
			this.State.Emerging,
			this.State.Jumping,
			this.State.Submerge
		});
		this.Controller.StateMachine.ChangeState(this.State.Hidden);
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x000A1F00 File Offset: 0x000A0100
	public void UpdateJumpingState()
	{
		if (this.Controller.StateMachine.CurrentStateTime > this.Settings.ShootDelay && !this.m_hasFired)
		{
			this.m_hasFired = true;
			this.ShootProjectileAtPlayer();
		}
		base.transform.position = this.EmergePosition + Vector3.up * this.Settings.JumpHeight * this.JumpCurve.Evaluate(this.Controller.StateMachine.CurrentStateTime);
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x000A1F90 File Offset: 0x000A0190
	public void ExitJumpingState()
	{
		this.m_hasFired = false;
	}

	// Token: 0x06002519 RID: 9497 RVA: 0x000A1F9C File Offset: 0x000A019C
	public void ShootProjectileAtPlayer()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Settings.Projectile, this.ProjectileSpawner.position, Quaternion.identity);
		Projectile component = gameObject.GetComponent<Projectile>();
		component.Direction = base.PositionToPlayerPosition.normalized;
		component.Speed = this.Settings.ProjectileSpeed;
		component.Owner = this.DamageReciever.gameObject;
		if (this.Shoot)
		{
			this.Shoot.Play();
		}
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x000A2027 File Offset: 0x000A0227
	public bool ShouldEmerge()
	{
		return true;
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x000A202C File Offset: 0x000A022C
	public void SetEmergeLocation(Vector3 position)
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (this.Controller.StateMachine.CurrentState != this.State.Hidden)
		{
			return;
		}
		foreach (JumpShootShark jumpShootShark in JumpShootShark.All)
		{
			if (jumpShootShark != this && jumpShootShark.gameObject.activeSelf && Vector3.Distance(jumpShootShark.EmergePosition, position) < 8f)
			{
				return;
			}
		}
		this.EmergePosition = position;
		this.Controller.StateMachine.ChangeState(this.State.Emerging);
	}

	// Token: 0x04001F8E RID: 8078
	public static List<JumpShootShark> All = new List<JumpShootShark>();

	// Token: 0x04001F8F RID: 8079
	public JumpShootSharkAnimations Animations;

	// Token: 0x04001F90 RID: 8080
	public JumpShootSharkSettings Settings;

	// Token: 0x04001F91 RID: 8081
	public Transform ProjectileSpawner;

	// Token: 0x04001F92 RID: 8082
	public PrefabSpawner EmergePrefab;

	// Token: 0x04001F93 RID: 8083
	public PrefabSpawner SubmergePrefab;

	// Token: 0x04001F94 RID: 8084
	public Vector3 EmergePosition;

	// Token: 0x04001F95 RID: 8085
	public GameObject Projectile;

	// Token: 0x04001F96 RID: 8086
	public float ProjectileSpeed;

	// Token: 0x04001F97 RID: 8087
	public JumpShootShark.States State = new JumpShootShark.States();

	// Token: 0x04001F98 RID: 8088
	public SoundSource EmergeAnticipation;

	// Token: 0x04001F99 RID: 8089
	public SoundSource Emerge;

	// Token: 0x04001F9A RID: 8090
	public SoundSource Shoot;

	// Token: 0x04001F9B RID: 8091
	public SoundSource Submerge;

	// Token: 0x04001F9C RID: 8092
	public GameObject HideGroup;

	// Token: 0x04001F9D RID: 8093
	private bool m_hasFired;

	// Token: 0x04001F9E RID: 8094
	public AnimationCurve JumpCurve;

	// Token: 0x020005AC RID: 1452
	public class States
	{
		// Token: 0x04001F9F RID: 8095
		public State Hidden;

		// Token: 0x04001FA0 RID: 8096
		public State Emerging;

		// Token: 0x04001FA1 RID: 8097
		public State Jumping;

		// Token: 0x04001FA2 RID: 8098
		public State Submerge;
	}
}
