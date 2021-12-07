using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200025F RID: 607
public abstract class RespawningPlaceholder : SaveSerialize, ISuspendable, IDynamicGraphic
{
	// Token: 0x0600145D RID: 5213 RVA: 0x0005C50B File Offset: 0x0005A70B
	protected void SetCurrentEntity(Entity entity)
	{
		this.m_currentEntity = entity;
	}

	// Token: 0x1700039B RID: 923
	// (get) Token: 0x0600145E RID: 5214 RVA: 0x0005C514 File Offset: 0x0005A714
	public virtual bool CheckForOverlap
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x0005C517 File Offset: 0x0005A717
	public void DestroyCurrentInstance()
	{
		if (this.m_currentEntity)
		{
			InstantiateUtility.Destroy(this.m_currentEntity.gameObject);
			this.m_currentEntity = null;
		}
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x0005C540 File Offset: 0x0005A740
	public void Spawn()
	{
		this.DestroyCurrentInstance();
		this.m_isKilled = false;
		this.m_currentEntity = this.Instantiate();
		GameObject gameObject = this.m_currentEntity.gameObject;
		if (gameObject.GetComponent<DestroyWhenOutsideActiveBoundaries>() == null)
		{
			gameObject.AddComponent<DestroyWhenOutsideActiveBoundaries>();
			Debug.LogError("The enemy type of " + gameObject.name + " didn't have the DestroyOnOutOfBoundary component on it!");
		}
		this.m_currentEntity.SetSceneRoot(this.m_sceneRootGUID);
		this.m_currentEntity.DamageReciever.OnDeathEvent.Add(new Action<Damage>(this.OnDeath));
		if (this.m_currentEntity && this.WasTimedRespawn)
		{
			gameObject.SendMessage("OnTimedRespawn", SendMessageOptions.DontRequireReceiver);
		}
		this.WasTimedRespawn = false;
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x0005C604 File Offset: 0x0005A804
	public override void Awake()
	{
		SuspensionManager.Register(this);
		Events.Scheduler.OnSceneRootDisabled.Add(new Action<SceneRoot>(this.OnSceneRootDisabled));
		Events.Scheduler.OnGameSerializeLoad.Add(new Action(this.OnGameSerializeLoad));
		base.Awake();
		base.GetComponent<Renderer>().enabled = false;
		this.m_transform = base.transform;
		this.m_sceneRootGUID = SceneRoot.FindFromTransform(base.transform).MetaData.SceneMoonGuid;
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x0005C688 File Offset: 0x0005A888
	public void OnEnable()
	{
		RespawningPlaceholder.All.Add(this);
		this.m_enableTime = Time.time;
		if (this.m_currentEntity)
		{
			this.m_currentEntity.ReclaimOwernship(this);
		}
	}

	// Token: 0x06001463 RID: 5219 RVA: 0x0005C6C7 File Offset: 0x0005A8C7
	public void OnDisable()
	{
		RespawningPlaceholder.All.Remove(this);
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x0005C6D8 File Offset: 0x0005A8D8
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		Events.Scheduler.OnSceneRootDisabled.Remove(new Action<SceneRoot>(this.OnSceneRootDisabled));
		Events.Scheduler.OnGameSerializeLoad.Remove(new Action(this.OnGameSerializeLoad));
		base.OnDestroy();
	}

	// Token: 0x06001465 RID: 5221 RVA: 0x0005C727 File Offset: 0x0005A927
	public void OnGameSerializeLoad()
	{
		this.DestroyCurrentInstance();
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x0005C730 File Offset: 0x0005A930
	public void OnSceneRootDisabled(SceneRoot sceneRoot)
	{
		if (this.m_sceneRootGUID == sceneRoot.MetaData.SceneMoonGuid && this.m_currentEntity)
		{
			if (this.m_currentEntity.IsOnScreen())
			{
				this.m_currentEntity.FreeOwnership(this);
			}
			else
			{
				this.DestroyCurrentInstance();
			}
		}
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x0005C790 File Offset: 0x0005A990
	public void OnDeath(Damage damage)
	{
		if (this.RespawnOnTimeout && this.RespawnTime != 0f)
		{
			this.m_respawnTime = GameController.Instance.GameTime + this.RespawnTime;
			this.m_needsToRespawn = true;
		}
		this.m_isKilled = true;
		this.OnCurrentInstanceDeath(damage);
		this.DestroyCurrentInstance();
	}

	// Token: 0x1700039C RID: 924
	// (get) Token: 0x06001468 RID: 5224 RVA: 0x0005C7EF File Offset: 0x0005A9EF
	public bool EntityIsDead
	{
		get
		{
			return this.m_isKilled;
		}
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x0005C7F7 File Offset: 0x0005A9F7
	public void FixedUpdate()
	{
		this.UpdateSpawnState();
	}

	// Token: 0x1700039D RID: 925
	// (get) Token: 0x0600146A RID: 5226 RVA: 0x0005C7FF File Offset: 0x0005A9FF
	public virtual bool NeedsToRespawn
	{
		get
		{
			return this.m_needsToRespawn;
		}
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x0005C808 File Offset: 0x0005AA08
	public void UpdateSpawnState()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.RespawnOnTimeout && this.NeedsToRespawn && this.m_respawnTime < GameController.Instance.GameTime)
		{
			if (this.IsOnScreen && this.m_enableTime + 0.1f < Time.time)
			{
				if (this.RespawnOnScreen)
				{
					float num = Vector3.Distance(Characters.Sein.PlatformBehaviour.PlatformMovement.Position, this.m_transform.position);
					if (num > this.MinDistanceFromPlayer)
					{
						this.PerformTimedRespawn();
					}
				}
			}
			else
			{
				this.PerformRespawn();
			}
		}
		if (!this.m_isKilled && this.m_currentEntity == null && this.IsOnScreen)
		{
			this.Spawn();
		}
	}

	// Token: 0x1700039E RID: 926
	// (get) Token: 0x0600146C RID: 5228 RVA: 0x0005C8E7 File Offset: 0x0005AAE7
	public bool IsOnScreen
	{
		get
		{
			return UI.Cameras.Current.IsOnScreenPadded(this.m_transform.position, 8f);
		}
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x0005C903 File Offset: 0x0005AB03
	public void PerformRespawn()
	{
		this.m_needsToRespawn = false;
		this.m_isKilled = false;
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x0005C913 File Offset: 0x0005AB13
	public void PerformTimedRespawn()
	{
		this.WasTimedRespawn = true;
		this.PerformRespawn();
	}

	// Token: 0x0600146F RID: 5231
	public abstract Entity Instantiate();

	// Token: 0x1700039F RID: 927
	// (get) Token: 0x06001470 RID: 5232 RVA: 0x0005C922 File Offset: 0x0005AB22
	public Entity CurrentEntity
	{
		get
		{
			return this.m_currentEntity;
		}
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x0005C92C File Offset: 0x0005AB2C
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_isKilled);
		ar.Serialize(ref this.m_needsToRespawn);
		ar.Serialize(ref this.m_respawnTime);
	}

	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x06001472 RID: 5234 RVA: 0x0005C95D File Offset: 0x0005AB5D
	// (set) Token: 0x06001473 RID: 5235 RVA: 0x0005C965 File Offset: 0x0005AB65
	public bool IsSuspended { get; set; }

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x06001474 RID: 5236 RVA: 0x0005C96E File Offset: 0x0005AB6E
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x040011CB RID: 4555
	public static List<RespawningPlaceholder> All = new List<RespawningPlaceholder>();

	// Token: 0x040011CC RID: 4556
	public bool RespawnOnTimeout = true;

	// Token: 0x040011CD RID: 4557
	public bool RespawnOnScreen;

	// Token: 0x040011CE RID: 4558
	public float RespawnTime = 3f;

	// Token: 0x040011CF RID: 4559
	public float MinDistanceFromPlayer = 20f;

	// Token: 0x040011D0 RID: 4560
	private bool m_isKilled;

	// Token: 0x040011D1 RID: 4561
	private float m_respawnTime;

	// Token: 0x040011D2 RID: 4562
	private bool m_needsToRespawn;

	// Token: 0x040011D3 RID: 4563
	private Transform m_transform;

	// Token: 0x040011D4 RID: 4564
	private MoonGuid m_sceneRootGUID;

	// Token: 0x040011D5 RID: 4565
	public Action<Damage> OnCurrentInstanceDeath = delegate(Damage A_0)
	{
	};

	// Token: 0x040011D6 RID: 4566
	protected bool WasTimedRespawn;

	// Token: 0x040011D7 RID: 4567
	private float m_enableTime;

	// Token: 0x040011D8 RID: 4568
	private Entity m_currentEntity;
}
