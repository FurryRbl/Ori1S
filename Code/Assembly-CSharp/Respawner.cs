using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200055A RID: 1370
public class Respawner : SaveSerialize, ISuspendable, IDynamicGraphicHierarchy
{
	// Token: 0x060023B6 RID: 9142 RVA: 0x0009C438 File Offset: 0x0009A638
	public static void UpdateRespawners()
	{
		try
		{
			for (int i = 0; i < Respawner.All.Count; i++)
			{
				Respawner.All[i].UpdateRespawner();
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060023B7 RID: 9143 RVA: 0x0009C48C File Offset: 0x0009A68C
	[UberBuildMethod]
	private void ProvideComponents()
	{
		this.m_respawnReciever = this.Target.FindComponentInChildren<IRespawnReciever>();
	}

	// Token: 0x170005FE RID: 1534
	// (get) Token: 0x060023B8 RID: 9144 RVA: 0x0009C49F File Offset: 0x0009A69F
	// (set) Token: 0x060023B9 RID: 9145 RVA: 0x0009C4A7 File Offset: 0x0009A6A7
	public bool IsSuspended { get; set; }

	// Token: 0x060023BA RID: 9146 RVA: 0x0009C4B0 File Offset: 0x0009A6B0
	public override void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
		Events.Scheduler.OnSceneStartLateBeforeSerialize.Add(new Action<SceneRoot>(this.OnSceneStartLateBeforeSerialize));
		Respawner.All.Add(this);
		this.m_sceneRoot = SceneRoot.FindFromTransform(base.transform);
	}

	// Token: 0x060023BB RID: 9147 RVA: 0x0009C500 File Offset: 0x0009A700
	public new void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
		Events.Scheduler.OnSceneStartLateBeforeSerialize.Remove(new Action<SceneRoot>(this.OnSceneStartLateBeforeSerialize));
		Respawner.All.RemoveUnordered(this);
	}

	// Token: 0x060023BC RID: 9148 RVA: 0x0009C540 File Offset: 0x0009A740
	public void OnSceneStartLateBeforeSerialize(SceneRoot sceneRoot)
	{
		if (sceneRoot == this.m_sceneRoot)
		{
			this.Prepare();
		}
	}

	// Token: 0x060023BD RID: 9149 RVA: 0x0009C55C File Offset: 0x0009A75C
	public void Start()
	{
		if (this.Target == null)
		{
			base.enabled = false;
			return;
		}
		this.ProvideComponents();
		if (this.m_respawnReciever != null)
		{
			this.m_respawnReciever.RegisterRespawnDelegate(new Action(this.OnTargetDestroyed));
		}
	}

	// Token: 0x060023BE RID: 9150 RVA: 0x0009C5AC File Offset: 0x0009A7AC
	public void Prepare()
	{
		if (!this.RespawnOnScrollLock && !this.RespawnOnTimeout && this.RespawnOnTimeout)
		{
			return;
		}
		List<SaveSerialize> list = new List<SaveSerialize>(this.Target.GetComponentsInChildren<SaveSerialize>());
		list.Remove(this);
		this.m_saveList.Convert(list.ToArray(), this.m_sceneRoot.SaveSceneManager);
		this.m_saveList.Save();
	}

	// Token: 0x060023BF RID: 9151 RVA: 0x0009C61B File Offset: 0x0009A81B
	public void OnTargetDestroyed(Damage damage)
	{
		this.OnTargetDestroyed();
	}

	// Token: 0x060023C0 RID: 9152 RVA: 0x0009C623 File Offset: 0x0009A823
	public void OnTargetDestroyed()
	{
		if (this.RespawnOnTimeout)
		{
			this.m_respawnTime = GameController.Instance.GameTime + this.RespawnTime;
			this.m_needsToRespawn = true;
		}
	}

	// Token: 0x060023C1 RID: 9153 RVA: 0x0009C650 File Offset: 0x0009A850
	public void UpdateRespawner()
	{
		if (GameController.FreezeFixedUpdate)
		{
			return;
		}
		if (this.IsSuspended)
		{
			return;
		}
		if (this.RespawnOnTimeout && this.RespawnTime != 0f && this.m_needsToRespawn && this.m_respawnTime < GameController.Instance.GameTime)
		{
			if (this.IsOnScreen)
			{
				if (this.RespawnOnScreen)
				{
					float num = Vector3.Distance(Characters.Sein.PlatformBehaviour.PlatformMovement.Position, base.transform.position);
					if (num > this.MinDistanceFromPlayer)
					{
						this.PerformTimedRespawn();
					}
				}
				else
				{
					this.m_respawnTime = GameController.Instance.GameTime + 0.2f;
				}
			}
			else
			{
				this.PerformRespawn();
			}
		}
	}

	// Token: 0x170005FF RID: 1535
	// (get) Token: 0x060023C2 RID: 9154 RVA: 0x0009C722 File Offset: 0x0009A922
	public bool IsOnScreen
	{
		get
		{
			return UI.Cameras.Current.IsOnScreenPadded(base.transform.position, 8f);
		}
	}

	// Token: 0x060023C3 RID: 9155 RVA: 0x0009C740 File Offset: 0x0009A940
	public void PerformTimedRespawn()
	{
		this.PerformRespawn();
		if (this.ScaleAnimator)
		{
			this.ScaleAnimator.Restart();
			this.ScaleAnimator.Sample(0f);
		}
		for (int i = 0; i < this.TimedRespawnAnimators.Length; i++)
		{
			LegacyAnimator legacyAnimator = this.TimedRespawnAnimators[i];
			legacyAnimator.Restart();
			legacyAnimator.Sample(0f);
		}
		if (this.TimedRespawnSoundSource)
		{
			this.TimedRespawnSoundSource.Play();
		}
		this.m_respawnReciever.OnTimedRespawn();
	}

	// Token: 0x060023C4 RID: 9156 RVA: 0x0009C7D7 File Offset: 0x0009A9D7
	public void PerformRespawn()
	{
		this.m_saveList.Load();
		this.m_needsToRespawn = false;
	}

	// Token: 0x060023C5 RID: 9157 RVA: 0x0009C7EC File Offset: 0x0009A9EC
	public override void Serialize(Archive ar)
	{
		ar.Serialize(0);
		ar.Serialize(0);
		ar.Serialize(ref this.m_needsToRespawn);
		ar.Serialize(ref this.m_respawnTime);
	}

	// Token: 0x04001DE9 RID: 7657
	public static List<Respawner> All = new List<Respawner>();

	// Token: 0x04001DEA RID: 7658
	public GameObject Target;

	// Token: 0x04001DEB RID: 7659
	public bool RespawnOnTimeout = true;

	// Token: 0x04001DEC RID: 7660
	public bool RespawnOnScreen;

	// Token: 0x04001DED RID: 7661
	public bool RespawnOnScrollLock;

	// Token: 0x04001DEE RID: 7662
	public bool RespawnOnRestoreCheckpoint;

	// Token: 0x04001DEF RID: 7663
	public float RespawnTime = 3f;

	// Token: 0x04001DF0 RID: 7664
	public float MinDistanceFromPlayer;

	// Token: 0x04001DF1 RID: 7665
	public LegacyScaleAnimator ScaleAnimator;

	// Token: 0x04001DF2 RID: 7666
	public LegacyAnimator[] TimedRespawnAnimators;

	// Token: 0x04001DF3 RID: 7667
	public SoundSource TimedRespawnSoundSource;

	// Token: 0x04001DF4 RID: 7668
	private float m_respawnTime;

	// Token: 0x04001DF5 RID: 7669
	private SceneRoot m_sceneRoot;

	// Token: 0x04001DF6 RID: 7670
	private bool m_needsToRespawn;

	// Token: 0x04001DF7 RID: 7671
	private readonly SaveObjectList m_saveList = new SaveObjectList();

	// Token: 0x04001DF8 RID: 7672
	[SerializeField]
	[HideInInspector]
	private IRespawnReciever m_respawnReciever;
}
