using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000728 RID: 1832
public class Portal : MonoBehaviour
{
	// Token: 0x170006E1 RID: 1761
	// (get) Token: 0x06002B20 RID: 11040 RVA: 0x000B8A5D File Offset: 0x000B6C5D
	public SceneRoot SceneRoot
	{
		get
		{
			return SceneRoot.FindFromTransform(base.transform);
		}
	}

	// Token: 0x170006E2 RID: 1762
	// (get) Token: 0x06002B21 RID: 11041 RVA: 0x000B8A6A File Offset: 0x000B6C6A
	public Vector3 CenterPosition
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x06002B22 RID: 11042 RVA: 0x000B8A77 File Offset: 0x000B6C77
	public void Awake()
	{
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
	}

	// Token: 0x06002B23 RID: 11043 RVA: 0x000B8A90 File Offset: 0x000B6C90
	public void OnDestroy()
	{
		if (this.m_lastLoopSound)
		{
			this.m_lastLoopSound.FadeOut(1f, true);
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_lastLoopSound.gameObject);
			this.m_lastLoopSound = null;
		}
		if (this.m_anticipationEnterEffect)
		{
			InstantiateUtility.Destroy(this.m_anticipationEnterEffect);
		}
		if (this.m_anticipationExitEffect)
		{
			InstantiateUtility.Destroy(this.m_anticipationExitEffect);
		}
	}

	// Token: 0x170006E3 RID: 1763
	// (get) Token: 0x06002B24 RID: 11044 RVA: 0x000B8B10 File Offset: 0x000B6D10
	public bool OriInsideAnticipationZone
	{
		get
		{
			bool flag = Vector3.Distance(Characters.Sein.Position, base.transform.position) < this.AnticipationRadius;
			return flag && Characters.Sein.Controller.RayTest(base.gameObject);
		}
	}

	// Token: 0x06002B25 RID: 11045 RVA: 0x000B8B60 File Offset: 0x000B6D60
	public void UpdateAnticipationEffects()
	{
		if (!this.AnticipationEnterEffect || !this.AnticipationExitEffect)
		{
			return;
		}
		if (this.OriInsideAnticipationZone)
		{
			if (!this.m_wasOriInsideAnticipationZone)
			{
				if (this.AnticipationSound)
				{
					this.AnticipationSound.Play();
				}
				if (this.EnterAnticipationSound)
				{
					this.EnterAnticipationSound.Play();
				}
				this.m_wasOriInsideAnticipationZone = true;
				if (this.m_anticipationEnterEffect)
				{
					this.m_anticipationEnterEffectAnimator.AnimatorDriver.ContinueForward();
				}
				else
				{
					this.m_anticipationEnterEffect = (GameObject)InstantiateUtility.Instantiate(this.AnticipationEnterEffect, this.CenterPosition, base.transform.rotation * Quaternion.Euler(0f, 0f, (float)((!this.IsLeftPortal) ? 0 : 180)));
					this.m_anticipationEnterEffectAnimator = this.m_anticipationEnterEffect.GetComponent<BaseAnimator>();
				}
				if (this.m_anticipationExitEffect)
				{
					this.m_anticipationExitEffectAnimator.AnimatorDriver.ContinueForward();
				}
				else if (this.Other)
				{
					this.m_anticipationExitEffect = (GameObject)InstantiateUtility.Instantiate(this.AnticipationExitEffect, this.Other.CenterPosition, base.transform.rotation * Quaternion.Euler(0f, 0f, (float)((!this.IsLeftPortal) ? 0 : 180)));
					this.m_anticipationExitEffectAnimator = this.m_anticipationExitEffect.GetComponent<BaseAnimator>();
				}
			}
		}
		else if (this.m_wasOriInsideAnticipationZone)
		{
			if (this.AnticipationSound)
			{
				this.AnticipationSound.StopAndFadeOut(0.3f);
			}
			if (this.ExitAnticipationSound)
			{
				this.ExitAnticipationSound.Play();
			}
			this.m_wasOriInsideAnticipationZone = false;
			if (this.m_anticipationEnterEffect)
			{
				this.m_anticipationEnterEffectAnimator.AnimatorDriver.ContinueBackwards();
			}
			if (this.m_anticipationExitEffect)
			{
				this.m_anticipationExitEffectAnimator.AnimatorDriver.ContinueBackwards();
			}
		}
	}

	// Token: 0x06002B26 RID: 11046 RVA: 0x000B8D98 File Offset: 0x000B6F98
	public void FixedUpdate()
	{
		if (InstantiateUtility.IsDestroyed(this.m_lastLoopSound) && this.LoopSoundProvider)
		{
			this.m_lastLoopSound = Sound.PlayLooping(this.LoopSoundProvider.GetSound(null), base.transform.position, delegate()
			{
				this.m_lastLoopSound = null;
			});
		}
		for (int i = 0; i < PortalVistor.All.Count; i++)
		{
			IPortalVisitor portalVisitor = PortalVistor.All[i];
			Vector3 position = portalVisitor.Position;
			if (this.m_portalVisitors.Contains(portalVisitor))
			{
				if (!Utility.LineInBox(this.m_bounds, position, -portalVisitor.Speed * Time.deltaTime))
				{
					this.m_portalVisitors.Remove(portalVisitor);
				}
				else if (this.IsInPortal(position))
				{
					this.PerformPortalTeleportation(portalVisitor);
				}
			}
			else if (Utility.LineInBox(this.m_bounds, portalVisitor.Position, -portalVisitor.Speed * Time.deltaTime))
			{
				portalVisitor.OnPortalOverlapEnter();
				this.m_portalVisitors.Add(portalVisitor);
				if (this.IsInPortal(position))
				{
					this.PerformPortalTeleportation(portalVisitor);
				}
			}
		}
		this.UpdateAnticipationEffects();
	}

	// Token: 0x06002B27 RID: 11047 RVA: 0x000B8ED8 File Offset: 0x000B70D8
	private void PerformPortalTeleportation(IPortalVisitor portalVisitor)
	{
		if (this.Condition && !this.Condition.Validate(null))
		{
			return;
		}
		Portal portal = this.Other;
		if (this.OtherPortalName != string.Empty)
		{
			foreach (SceneManagerScene sceneManagerScene in Scenes.Manager.ActiveScenes)
			{
				foreach (Portal portal2 in sceneManagerScene.SceneRoot.SceneRootData.Portals)
				{
					if (portal2 && portal2.name == this.OtherPortalName && portal2 != this)
					{
						portal = portal2;
						break;
					}
				}
			}
		}
		if (portal != null)
		{
			bool flag = Characters.Sein && ((Component)portalVisitor).gameObject == Characters.Sein.GameObject;
			if (flag && this.EnterEffect)
			{
				Vector3 position;
				if (this.CenterEffects)
				{
					position = this.CenterPosition;
				}
				else
				{
					position = portalVisitor.Position;
					position = this.ClampPosition(position, this);
				}
				InstantiateUtility.Instantiate(this.EnterEffect, position, base.transform.rotation * Quaternion.Euler(0f, 0f, (float)((!this.IsLeftPortal) ? 0 : 180)));
			}
			portalVisitor.Position = this.CalculateEndPosition(portal, portalVisitor.Position);
			portalVisitor.Speed = this.CalculateEndSpeed(portal, portalVisitor.Speed);
			portalVisitor.OnGoThroughPortal();
			if (this.TeleportSound)
			{
				Sound.Play(this.TeleportSound.GetSound(null), portalVisitor.Position, null);
				Sound.Play(this.TeleportSound.GetSound(null), base.transform.position, null);
			}
			if (flag)
			{
				if (Characters.Ori)
				{
					Characters.Ori.MoveOriBackToPlayer();
				}
				base.StartCoroutine(this.DisableOriForABit(portal));
			}
		}
	}

	// Token: 0x06002B28 RID: 11048 RVA: 0x000B9150 File Offset: 0x000B7350
	private Vector3 ClampPosition(Vector3 position, Portal portal)
	{
		Vector3 position2 = portal.transform.position;
		Vector3 a = portal.transform.lossyScale;
		a -= new Vector3(10f, 10f, 0f);
		a.x = Mathf.Max(Mathf.Abs(a.x), 0f);
		a.y = Mathf.Max(Mathf.Abs(a.y), 0f);
		position.x = Mathf.Clamp(position.x, position2.x - a.x, position2.x + a.x);
		position.y = Mathf.Clamp(position.y, position2.y - a.y, position2.y + a.y);
		return position;
	}

	// Token: 0x06002B29 RID: 11049 RVA: 0x000B9230 File Offset: 0x000B7430
	public IEnumerator DisableOriForABit(Portal other)
	{
		Characters.Sein.Active = false;
		yield return new WaitForSeconds(this.DelayToPortal);
		Characters.Sein.Active = true;
		if (this.ExitEffect)
		{
			Vector3 position;
			if (this.CenterEffects)
			{
				position = this.CenterPosition;
			}
			else
			{
				position = Characters.Sein.Position;
				position = this.ClampPosition(position, other);
			}
			InstantiateUtility.Instantiate(this.ExitEffect, position, base.transform.rotation * Quaternion.Euler(0f, 0f, (float)((!this.IsLeftPortal) ? 0 : 180)));
		}
		yield break;
	}

	// Token: 0x06002B2A RID: 11050 RVA: 0x000B925C File Offset: 0x000B745C
	public Vector3 CalculateEndPosition(Portal other, Vector3 position)
	{
		Vector3 position2 = base.transform.InverseTransformPoint(position);
		Vector3 b = other.transform.TransformDirection((!other.IsLeftPortal) ? Vector3.left : Vector3.right) * 0.05f;
		return other.transform.TransformPoint(position2) + b;
	}

	// Token: 0x06002B2B RID: 11051 RVA: 0x000B92B8 File Offset: 0x000B74B8
	public Vector3 CalculateEndSpeed(Portal other, Vector3 speed)
	{
		Vector3 direction = base.transform.InverseTransformDirection(speed);
		return other.transform.TransformDirection(direction);
	}

	// Token: 0x06002B2C RID: 11052 RVA: 0x000B92E0 File Offset: 0x000B74E0
	public bool IsInPortal(Vector3 position)
	{
		Vector3 vector = base.transform.InverseTransformPoint(position);
		return (!this.IsLeftPortal) ? (vector.x > 0f) : (vector.x < 0f);
	}

	// Token: 0x040026BE RID: 9918
	public bool IsLeftPortal;

	// Token: 0x040026BF RID: 9919
	public string OtherPortalName;

	// Token: 0x040026C0 RID: 9920
	public Portal Other;

	// Token: 0x040026C1 RID: 9921
	public float DelayToPortal;

	// Token: 0x040026C2 RID: 9922
	public Condition Condition;

	// Token: 0x040026C3 RID: 9923
	private readonly List<IPortalVisitor> m_portalVisitors = new List<IPortalVisitor>(16);

	// Token: 0x040026C4 RID: 9924
	public SoundProvider TeleportSound;

	// Token: 0x040026C5 RID: 9925
	public SoundProvider LoopSoundProvider;

	// Token: 0x040026C6 RID: 9926
	private SoundPlayer m_lastLoopSound;

	// Token: 0x040026C7 RID: 9927
	public SoundSource AnticipationSound;

	// Token: 0x040026C8 RID: 9928
	public SoundSource EnterAnticipationSound;

	// Token: 0x040026C9 RID: 9929
	public SoundSource ExitAnticipationSound;

	// Token: 0x040026CA RID: 9930
	public GameObject EnterEffect;

	// Token: 0x040026CB RID: 9931
	public GameObject ExitEffect;

	// Token: 0x040026CC RID: 9932
	public GameObject AnticipationEnterEffect;

	// Token: 0x040026CD RID: 9933
	public GameObject AnticipationExitEffect;

	// Token: 0x040026CE RID: 9934
	private GameObject m_anticipationEnterEffect;

	// Token: 0x040026CF RID: 9935
	private GameObject m_anticipationExitEffect;

	// Token: 0x040026D0 RID: 9936
	private BaseAnimator m_anticipationEnterEffectAnimator;

	// Token: 0x040026D1 RID: 9937
	private BaseAnimator m_anticipationExitEffectAnimator;

	// Token: 0x040026D2 RID: 9938
	public bool HasAnticipationEffects = true;

	// Token: 0x040026D3 RID: 9939
	public bool CenterEffects = true;

	// Token: 0x040026D4 RID: 9940
	private Rect m_bounds;

	// Token: 0x040026D5 RID: 9941
	public float AnticipationRadius = 8f;

	// Token: 0x040026D6 RID: 9942
	private bool m_wasOriInsideAnticipationZone;
}
