using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200030A RID: 778
[Category("System")]
public class PauseGameAction : ActionWithDuration
{
	// Token: 0x0600171F RID: 5919 RVA: 0x000641CE File Offset: 0x000623CE
	public override void Perform(IContext context)
	{
		this.PauseGame();
	}

	// Token: 0x06001720 RID: 5920 RVA: 0x000641D6 File Offset: 0x000623D6
	public override void Stop()
	{
		this.ResumeGame();
	}

	// Token: 0x17000414 RID: 1044
	// (get) Token: 0x06001721 RID: 5921 RVA: 0x000641DE File Offset: 0x000623DE
	public override bool IsPerforming
	{
		get
		{
			return this.m_isPaused;
		}
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x000641E6 File Offset: 0x000623E6
	public void OnDisable()
	{
		if (this.m_isPaused)
		{
			this.ResumeGame();
		}
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x000641F9 File Offset: 0x000623F9
	public override void Serialize(Archive ar)
	{
		base.Serialize(ar);
		if (ar.Reading && this.m_isPaused)
		{
			this.ResumeGame();
		}
	}

	// Token: 0x06001724 RID: 5924 RVA: 0x00064220 File Offset: 0x00062420
	public void ResumeGame()
	{
		this.m_isPaused = false;
		GameController.Instance.LockInput = false;
		SuspensionManager.ResumeExcluding(this.m_exclude);
		this.m_exclude.Clear();
		if (Characters.Sein && Characters.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedY < 0f)
		{
			Characters.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedY = 0f;
		}
		if (this.ShowLetterbox)
		{
			Letterbox.ShowLetterboxes = false;
		}
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x000642AC File Offset: 0x000624AC
	public void PauseGame()
	{
		this.m_isPaused = true;
		GameController.Instance.LockInput = true;
		this.m_pausedTime = 0f;
		this.m_exclude.Clear();
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.Exclude.Length; i++)
		{
			GameObject item = this.Exclude[i];
			list.Add(item);
		}
		list.Add(base.transform.parent.gameObject);
		if (!this.SuspendCamera)
		{
			list.Add(UI.Cameras.Current.GameObject);
		}
		if (!this.SuspendOri)
		{
			list.Add(Characters.Ori.gameObject);
		}
		if (!this.SuspendPlayer)
		{
			list.Add(Characters.Sein.Controller.gameObject);
		}
		list.Add(Letterbox.Instance.gameObject);
		SuspensionManager.GetSuspendables(this.m_exclude, list.ToArray());
		SuspensionManager.SuspendExcluding(this.m_exclude);
		if (this.ShowLetterbox)
		{
			Letterbox.ShowLetterboxes = true;
		}
	}

	// Token: 0x17000415 RID: 1045
	// (get) Token: 0x06001726 RID: 5926 RVA: 0x000643BA File Offset: 0x000625BA
	// (set) Token: 0x06001727 RID: 5927 RVA: 0x000643C2 File Offset: 0x000625C2
	public override float Duration
	{
		get
		{
			return this.PauseDuration;
		}
		set
		{
			this.PauseDuration = value;
		}
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x000643CC File Offset: 0x000625CC
	public void FixedUpdate()
	{
		if (this.m_isPaused)
		{
			if (!this.IsSuspended)
			{
				this.m_pausedTime += Time.deltaTime;
			}
			if (this.m_pausedTime > this.PauseDuration)
			{
				this.ResumeGame();
			}
		}
	}

	// Token: 0x040013DD RID: 5085
	public float PauseDuration;

	// Token: 0x040013DE RID: 5086
	private float m_pausedTime;

	// Token: 0x040013DF RID: 5087
	private bool m_isPaused;

	// Token: 0x040013E0 RID: 5088
	public GameObject[] Exclude;

	// Token: 0x040013E1 RID: 5089
	public bool SuspendCamera;

	// Token: 0x040013E2 RID: 5090
	public bool SuspendPlayer = true;

	// Token: 0x040013E3 RID: 5091
	public bool SuspendOri = true;

	// Token: 0x040013E4 RID: 5092
	private HashSet<ISuspendable> m_exclude = new HashSet<ISuspendable>();

	// Token: 0x040013E5 RID: 5093
	public bool ShowLetterbox;
}
