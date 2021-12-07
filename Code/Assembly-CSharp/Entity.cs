using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class Entity : SaveSerialize, IRespawnReciever, IFrustumOptimizable, ISuspendable
{
	// Token: 0x060013A3 RID: 5027 RVA: 0x0005A784 File Offset: 0x00058984
	public Entity()
	{
		this.IsSuspended = false;
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x0005A7D8 File Offset: 0x000589D8
	public void OnSceneUnloaded(SceneRoot sceneRoot)
	{
		if (!Scenes.Manager.IsInsideActiveSceneBoundary(base.transform.position))
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x0005A80C File Offset: 0x00058A0C
	public void ReclaimOwernship(RespawningPlaceholder placeholder)
	{
		base.transform.parent = placeholder.transform.parent;
		Events.Scheduler.OnSceneRootDisabled.Remove(new Action<SceneRoot>(this.OnSceneUnloaded));
		this.m_registeredToSceneRootDisabled = false;
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x0005A854 File Offset: 0x00058A54
	public void FreeOwnership(RespawningPlaceholder placeholder)
	{
		base.transform.parent = null;
		Events.Scheduler.OnSceneRootDisabled.Add(new Action<SceneRoot>(this.OnSceneUnloaded));
		this.m_registeredToSceneRootDisabled = true;
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x0005A88F File Offset: 0x00058A8F
	public virtual bool CanBeOptimized()
	{
		return true;
	}

	// Token: 0x1700037C RID: 892
	// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0005A892 File Offset: 0x00058A92
	public bool IsInWater
	{
		get
		{
			return WaterZone.PositionInWater(this.Position);
		}
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x0005A8A0 File Offset: 0x00058AA0
	public void Drown()
	{
		Damage damage = new Damage(1000f, Vector3.zero, this.Position, DamageType.Water, base.gameObject);
		this.DamageReciever.OnRecieveDamage(damage);
	}

	// Token: 0x060013AA RID: 5034 RVA: 0x0005A8DC File Offset: 0x00058ADC
	public bool IsOnScreen()
	{
		return UI.Cameras.Current == null || UI.Cameras.Current.IsOnScreen(base.transform.position);
	}

	// Token: 0x060013AB RID: 5035 RVA: 0x0005A910 File Offset: 0x00058B10
	public override void Awake()
	{
		SuspensionManager.Register(this);
		if (this.FrustrumOptimized)
		{
			CameraFrustumOptimizer.Register(this);
		}
		SceneRoot sceneRoot = SceneRoot.FindFromTransform(base.transform);
		if (sceneRoot != null)
		{
			this.SceneRootGUID = sceneRoot.MetaData.SceneMoonGuid;
		}
		base.Awake();
	}

	// Token: 0x060013AC RID: 5036 RVA: 0x0005A963 File Offset: 0x00058B63
	public void SetSceneRoot(MoonGuid sceneRoot)
	{
		this.SceneRootGUID = sceneRoot;
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x0005A96C File Offset: 0x00058B6C
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		if (this.FrustrumOptimized)
		{
			CameraFrustumOptimizer.Unregister(this);
		}
		if (this.m_registeredToSceneRootDisabled)
		{
			Events.Scheduler.OnSceneRootDisabled.Remove(new Action<SceneRoot>(this.OnSceneUnloaded));
		}
		base.OnDestroy();
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x0005A9BC File Offset: 0x00058BBC
	public override void Serialize(Archive ar)
	{
		this.Position = ar.Serialize(this.Position);
		this.Rotation = ar.Serialize(this.Rotation);
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x0005A9ED File Offset: 0x00058BED
	public void Start()
	{
		this.StartPosition = base.transform.position;
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x0005AA00 File Offset: 0x00058C00
	public void FixedUpdate()
	{
		if (this.FrustrumOptimized && !this.m_insideFrustum && this.CanBeOptimized())
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x1700037D RID: 893
	// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0005AA30 File Offset: 0x00058C30
	public bool PlayerIsToLeft
	{
		get
		{
			return this.PositionToPlayerPosition.x < 0f;
		}
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0005AA52 File Offset: 0x00058C52
	public Vector3 PlayerPosition
	{
		get
		{
			return Characters.Sein.PlatformBehaviour.PlatformMovement.Position;
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0005AA68 File Offset: 0x00058C68
	// (set) Token: 0x060013B4 RID: 5044 RVA: 0x0005AA75 File Offset: 0x00058C75
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
		set
		{
			base.transform.position = value;
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0005AA83 File Offset: 0x00058C83
	// (set) Token: 0x060013B6 RID: 5046 RVA: 0x0005AA90 File Offset: 0x00058C90
	public Quaternion Rotation
	{
		get
		{
			return base.transform.rotation;
		}
		set
		{
			base.transform.rotation = value;
		}
	}

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0005AAA0 File Offset: 0x00058CA0
	public Vector3 PositionToPlayerPosition
	{
		get
		{
			return base.transform.InverseTransformDirection(this.PlayerPosition - this.Position);
		}
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x060013B8 RID: 5048 RVA: 0x0005AAC9 File Offset: 0x00058CC9
	public Vector3 StartPositionToPlayerPosition
	{
		get
		{
			return this.PlayerPosition - this.StartPosition;
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0005AADC File Offset: 0x00058CDC
	public bool LeftOfStartPosition
	{
		get
		{
			return this.StartPositionToPlayerPosition.x < 0f;
		}
	}

	// Token: 0x17000384 RID: 900
	// (get) Token: 0x060013BA RID: 5050 RVA: 0x0005AAFE File Offset: 0x00058CFE
	public Vector3 PositionToStartPosition
	{
		get
		{
			return this.StartPosition - this.Position;
		}
	}

	// Token: 0x17000385 RID: 901
	// (get) Token: 0x060013BB RID: 5051 RVA: 0x0005AB11 File Offset: 0x00058D11
	// (set) Token: 0x060013BC RID: 5052 RVA: 0x0005AB19 File Offset: 0x00058D19
	public Vector3 StartPosition { get; set; }

	// Token: 0x060013BD RID: 5053 RVA: 0x0005AB22 File Offset: 0x00058D22
	public bool AfterTime(float duration)
	{
		return this.Controller.StateMachine.CurrentStateTime > duration;
	}

	// Token: 0x17000386 RID: 902
	// (get) Token: 0x060013BE RID: 5054 RVA: 0x0005AB37 File Offset: 0x00058D37
	// (set) Token: 0x060013BF RID: 5055 RVA: 0x0005AB3F File Offset: 0x00058D3F
	public bool IsSuspended { get; set; }

	// Token: 0x060013C0 RID: 5056 RVA: 0x0005AB48 File Offset: 0x00058D48
	public void OnTimedRespawn()
	{
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x0005AB4C File Offset: 0x00058D4C
	public void RegisterRespawnDelegate(Action onRespawn)
	{
		this.DamageReciever.OnDeathEvent.Add(delegate(Damage a)
		{
			onRespawn();
		});
	}

	// Token: 0x060013C2 RID: 5058 RVA: 0x0005AB82 File Offset: 0x00058D82
	public void PlaySound(SoundSource sound)
	{
		if (sound != null)
		{
			sound.Play();
		}
	}

	// Token: 0x060013C3 RID: 5059 RVA: 0x0005AB96 File Offset: 0x00058D96
	public void StopSound(SoundSource sound)
	{
		if (sound != null)
		{
			sound.Stop();
		}
	}

	// Token: 0x060013C4 RID: 5060 RVA: 0x0005ABAC File Offset: 0x00058DAC
	public void PlaySound(SoundProvider sound)
	{
		if (sound != null)
		{
			Sound.Play(sound.GetSound(null), this.Position, null);
		}
	}

	// Token: 0x060013C5 RID: 5061 RVA: 0x0005ABD9 File Offset: 0x00058DD9
	public void SpawnPrefab(PrefabSpawner prefabSpawner)
	{
		if (prefabSpawner != null)
		{
			prefabSpawner.Spawn(null);
		}
	}

	// Token: 0x060013C6 RID: 5062 RVA: 0x0005ABF0 File Offset: 0x00058DF0
	public void SpawnPrefab(GameObject prefab)
	{
		if (prefab != null)
		{
			InstantiateUtility.Instantiate(prefab, this.Position, base.transform.rotation);
		}
	}

	// Token: 0x060013C7 RID: 5063 RVA: 0x0005AC21 File Offset: 0x00058E21
	public void DestroyPrefab(PrefabSpawner prefabSpawner)
	{
		if (prefabSpawner != null)
		{
			prefabSpawner.DestroyInstance();
		}
	}

	// Token: 0x060013C8 RID: 5064 RVA: 0x0005AC35 File Offset: 0x00058E35
	public void ActivateDamageDealer()
	{
		this.DamageDealer.Activated = true;
	}

	// Token: 0x060013C9 RID: 5065 RVA: 0x0005AC43 File Offset: 0x00058E43
	public void DeactivateDamageDealer()
	{
		this.DamageDealer.Activated = false;
	}

	// Token: 0x060013CA RID: 5066 RVA: 0x0005AC51 File Offset: 0x00058E51
	public void ActivateTargetting()
	{
		this.Targetting.Activated = true;
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x0005AC5F File Offset: 0x00058E5F
	public void DeactivateTargetting()
	{
		this.Targetting.Activated = false;
	}

	// Token: 0x060013CC RID: 5068 RVA: 0x0005AC6D File Offset: 0x00058E6D
	public void OnFrustumEnter()
	{
		this.m_insideFrustum = true;
		if (!this.DamageReciever || !this.DamageReciever.NoHealthLeft)
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x060013CD RID: 5069 RVA: 0x0005ACA2 File Offset: 0x00058EA2
	public void OnFrustumExit()
	{
		this.m_insideFrustum = false;
	}

	// Token: 0x17000387 RID: 903
	// (get) Token: 0x060013CE RID: 5070 RVA: 0x0005ACAB File Offset: 0x00058EAB
	public bool InsideFrustum
	{
		get
		{
			return this.m_insideFrustum;
		}
	}

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x060013CF RID: 5071 RVA: 0x0005ACB4 File Offset: 0x00058EB4
	public Bounds Bounds
	{
		get
		{
			Vector3 size = new Vector3(this.BoundingBox.width, this.BoundingBox.height, 0f);
			Vector3 vector = base.transform.position;
			vector += new Vector3(this.BoundingBox.center.x, this.BoundingBox.center.y, 0f);
			return new Bounds(vector, size);
		}
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x0005AD30 File Offset: 0x00058F30
	public bool PlayerInsideSameScene()
	{
		RuntimeSceneMetaData currentScene = Scenes.Manager.CurrentScene;
		return currentScene != null && currentScene.SceneMoonGuid == this.SceneRootGUID;
	}

	// Token: 0x04001166 RID: 4454
	public EntityController Controller;

	// Token: 0x04001167 RID: 4455
	public EntityDamageReciever DamageReciever;

	// Token: 0x04001168 RID: 4456
	public EntityDamageDealer DamageDealer;

	// Token: 0x04001169 RID: 4457
	public EntityTargetting Targetting;

	// Token: 0x0400116A RID: 4458
	protected MoonGuid SceneRootGUID;

	// Token: 0x0400116B RID: 4459
	public Rect BoundingBox = new Rect
	{
		width = 4f,
		height = 4f,
		center = Vector2.zero
	};

	// Token: 0x0400116C RID: 4460
	public bool FrustrumOptimized;

	// Token: 0x0400116D RID: 4461
	private bool m_registeredToSceneRootDisabled;

	// Token: 0x0400116E RID: 4462
	private bool m_insideFrustum = true;
}
