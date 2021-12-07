using System;
using Game;
using UnityEngine;

// Token: 0x0200046E RID: 1134
public class SoulFlame : MonoBehaviour
{
	// Token: 0x17000573 RID: 1395
	// (get) Token: 0x06001F3D RID: 7997 RVA: 0x0008A14D File Offset: 0x0008834D
	public bool IsInside
	{
		get
		{
			return this.m_isInside && base.gameObject.activeSelf;
		}
	}

	// Token: 0x17000574 RID: 1396
	// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0008A168 File Offset: 0x00088368
	// (set) Token: 0x06001F3F RID: 7999 RVA: 0x0008A175 File Offset: 0x00088375
	public Vector3 Position
	{
		get
		{
			return this.m_transform.position;
		}
		set
		{
			this.m_transform.position = value;
		}
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x0008A184 File Offset: 0x00088384
	public void Awake()
	{
		this.m_transform = base.transform;
		this.m_isInside = true;
		Events.Scheduler.OnGameSerializeLoad.Add(new Action(this.OnGameSerializeLoad));
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x0008A1BF File Offset: 0x000883BF
	public void OnDestroy()
	{
		Events.Scheduler.OnGameSerializeLoad.Remove(new Action(this.OnGameSerializeLoad));
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x0008A1DC File Offset: 0x000883DC
	public void OnGameSerializeLoad()
	{
		this.m_isInside = true;
	}

	// Token: 0x06001F43 RID: 8003 RVA: 0x0008A1E8 File Offset: 0x000883E8
	public void OnRekindle()
	{
		this.TriggerSequence.Perform(null);
		if (this.RekindleEffect != null)
		{
			InstantiateUtility.Instantiate(this.RekindleEffect, base.transform.position, base.transform.localRotation);
		}
	}

	// Token: 0x06001F44 RID: 8004 RVA: 0x0008A234 File Offset: 0x00088434
	public void FixedUpdate()
	{
		if (GameController.FreezeFixedUpdate)
		{
			return;
		}
		if (Characters.Sein && Characters.Sein.Active)
		{
			if (this.m_isInside)
			{
				if (Vector3.Distance(Characters.Sein.Position, this.m_transform.position) > this.ExitRadius)
				{
					this.m_isInside = false;
				}
			}
			else if (Vector3.Distance(Characters.Sein.Position, this.m_transform.position) < this.EnterRadius)
			{
				this.m_isInside = true;
			}
		}
	}

	// Token: 0x06001F45 RID: 8005 RVA: 0x0008A2D2 File Offset: 0x000884D2
	public void OnDisable()
	{
		this.m_isInside = false;
	}

	// Token: 0x06001F46 RID: 8006 RVA: 0x0008A2DB File Offset: 0x000884DB
	public void Disappear()
	{
		InstantiateUtility.Destroy(base.gameObject);
	}

	// Token: 0x06001F47 RID: 8007 RVA: 0x0008A2E8 File Offset: 0x000884E8
	public void Start()
	{
		this.AppearAnimator.AnimatorDriver.Restart();
	}

	// Token: 0x04001AFB RID: 6907
	public BaseAnimator AppearAnimator;

	// Token: 0x04001AFC RID: 6908
	public ActionSequence TriggerSequence;

	// Token: 0x04001AFD RID: 6909
	public GameObject RekindleEffect;

	// Token: 0x04001AFE RID: 6910
	public float EnterRadius = 1f;

	// Token: 0x04001AFF RID: 6911
	public float ExitRadius = 2f;

	// Token: 0x04001B00 RID: 6912
	private bool m_isInside;

	// Token: 0x04001B01 RID: 6913
	private Transform m_transform;
}
