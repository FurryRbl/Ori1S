using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000919 RID: 2329
public class SequencePlatform : SaveSerialize, ISuspendable
{
	// Token: 0x0600339D RID: 13213 RVA: 0x000D9693 File Offset: 0x000D7893
	public void OnEnable()
	{
		if (!this.m_activated)
		{
			this.m_activated = true;
			this.m_hasBeenTouched = false;
		}
	}

	// Token: 0x17000824 RID: 2084
	// (get) Token: 0x0600339F RID: 13215 RVA: 0x000D96C8 File Offset: 0x000D78C8
	// (set) Token: 0x0600339E RID: 13214 RVA: 0x000D96AE File Offset: 0x000D78AE
	public bool Activated
	{
		get
		{
			return this.m_activated;
		}
		set
		{
			this.m_activated = value;
			base.gameObject.SetActive(this.m_activated);
		}
	}

	// Token: 0x060033A0 RID: 13216 RVA: 0x000D96D0 File Offset: 0x000D78D0
	private new void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x060033A1 RID: 13217 RVA: 0x000D96DE File Offset: 0x000D78DE
	private new void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060033A2 RID: 13218 RVA: 0x000D96EC File Offset: 0x000D78EC
	private void Start()
	{
		if (this.VisibleAtStart)
		{
			this.Activated = true;
		}
		else
		{
			this.Activated = false;
		}
		this.m_originalScale = base.transform.localScale;
	}

	// Token: 0x060033A3 RID: 13219 RVA: 0x000D9728 File Offset: 0x000D7928
	private void FixedUpdate()
	{
		if (base.gameObject.activeSelf != this.m_activated)
		{
			base.gameObject.SetActive(this.m_activated);
		}
	}

	// Token: 0x060033A4 RID: 13220 RVA: 0x000D975C File Offset: 0x000D795C
	public void OnSequenceAppear()
	{
		this.Activated = true;
		this.m_hasBeenTouched = false;
		if (!this.NextPlatformOnTouch)
		{
			base.StartCoroutine(this.PerformNextPlatform());
		}
		if (!this.VanishOnTouch)
		{
			base.StartCoroutine(this.PerformVanish());
		}
	}

	// Token: 0x060033A5 RID: 13221 RVA: 0x000D97A8 File Offset: 0x000D79A8
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player") && !this.m_hasBeenTouched)
		{
			this.m_hasBeenTouched = true;
			if (this.NextPlatformOnTouch && this.NextPlatform)
			{
				base.StartCoroutine(this.PerformNextPlatform());
				HashSet<SequencePlatform> platformsToIgnore = new HashSet<SequencePlatform>
				{
					this,
					this.NextPlatform
				};
				this.NextPlatform.HideThePlatforms(platformsToIgnore);
			}
			if (this.VanishOnTouch)
			{
				base.StartCoroutine(this.PerformVanish());
			}
		}
	}

	// Token: 0x060033A6 RID: 13222 RVA: 0x000D9848 File Offset: 0x000D7A48
	private void HideThePlatforms(HashSet<SequencePlatform> platformsToIgnore)
	{
		if (!platformsToIgnore.Contains(this))
		{
			if (this.Vanish)
			{
				this.Activated = false;
			}
			platformsToIgnore.Add(this);
			if (this.NextPlatform)
			{
				this.NextPlatform.HideThePlatforms(platformsToIgnore);
			}
		}
	}

	// Token: 0x060033A7 RID: 13223 RVA: 0x000D9898 File Offset: 0x000D7A98
	private IEnumerator PerformNextPlatform()
	{
		for (float t = 0f; t < this.DelayToNextPlatformAppear; t += ((!this.IsSuspended) ? Time.deltaTime : 0f))
		{
			yield return new WaitForFixedUpdate();
		}
		if (this.NextPlatform)
		{
			this.NextPlatform.OnSequenceAppear();
		}
		yield break;
	}

	// Token: 0x060033A8 RID: 13224 RVA: 0x000D98B4 File Offset: 0x000D7AB4
	private IEnumerator PerformVanish()
	{
		this.m_vanishAmount = 1f;
		while (this.m_vanishAmount > 0f)
		{
			if (!this.IsSuspended)
			{
				this.m_vanishAmount -= Time.deltaTime / this.DelayToVanish;
				if (this.m_vanishAmount < 0f)
				{
					this.m_vanishAmount = 0f;
				}
			}
			yield return new WaitForFixedUpdate();
		}
		if (this.Vanish)
		{
			this.Activated = false;
		}
		yield break;
	}

	// Token: 0x060033A9 RID: 13225 RVA: 0x000D98D0 File Offset: 0x000D7AD0
	public override void Serialize(Archive ar)
	{
		base.StopAllCoroutines();
		bool activated = this.Activated;
		ar.Serialize(ref this.m_activated);
		ar.Serialize(ref this.m_hasBeenTouched);
		if (activated != this.m_activated)
		{
			this.Activated = this.m_activated;
		}
	}

	// Token: 0x17000825 RID: 2085
	// (get) Token: 0x060033AA RID: 13226 RVA: 0x000D991A File Offset: 0x000D7B1A
	// (set) Token: 0x060033AB RID: 13227 RVA: 0x000D9922 File Offset: 0x000D7B22
	public bool IsSuspended { get; set; }

	// Token: 0x04002E97 RID: 11927
	public SequencePlatform NextPlatform;

	// Token: 0x04002E98 RID: 11928
	public bool NextPlatformOnTouch = true;

	// Token: 0x04002E99 RID: 11929
	public bool VanishOnTouch = true;

	// Token: 0x04002E9A RID: 11930
	public float DelayToNextPlatformAppear;

	// Token: 0x04002E9B RID: 11931
	public float DelayToVanish = 3f;

	// Token: 0x04002E9C RID: 11932
	public bool Vanish = true;

	// Token: 0x04002E9D RID: 11933
	public bool VisibleAtStart;

	// Token: 0x04002E9E RID: 11934
	public Vector3 m_originalScale;

	// Token: 0x04002E9F RID: 11935
	private bool m_activated;

	// Token: 0x04002EA0 RID: 11936
	private bool m_hasBeenTouched;

	// Token: 0x04002EA1 RID: 11937
	private float m_vanishAmount;
}
