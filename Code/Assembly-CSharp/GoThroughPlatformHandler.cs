using System;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x0200043D RID: 1085
public class GoThroughPlatformHandler : MonoBehaviour
{
	// Token: 0x06001E2E RID: 7726 RVA: 0x00084BEF File Offset: 0x00082DEF
	[UberBuildMethod]
	private void ProvideComponent()
	{
		this.m_tester = base.gameObject.FindComponentInChildren<IGoThroughPlatformTester>();
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x00084C04 File Offset: 0x00082E04
	public void Awake()
	{
		this.m_playerLayer = LayerMask.NameToLayer("player");
		this.m_platformLayer = LayerMask.NameToLayer("platform");
		base.gameObject.layer = this.m_playerLayer;
		this.ProvideComponent();
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x00084C5E File Offset: 0x00082E5E
	public void OnDestroy()
	{
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x00084C78 File Offset: 0x00082E78
	public bool FallThroughPlatform()
	{
		bool flag = false;
		if (LightSource.AnyExist && !Sein.World.Events.DarknessLifted)
		{
			for (int i = 0; i < GoThroughPlatformManager.GoThroughPlatforms.Count; i++)
			{
				GoThroughPlatform goThroughPlatform = GoThroughPlatformManager.GoThroughPlatforms[i];
				Ray goThroughPlatformTestingRayLeft = this.m_tester.GoThroughPlatformTestingRayLeft;
				Ray goThroughPlatformTestingRayRight = this.m_tester.GoThroughPlatformTestingRayRight;
				float maxDistance = 1f;
				for (int j = 0; j < goThroughPlatform.Length; j++)
				{
					Collider collider = goThroughPlatform.Colliders[j];
					RaycastHit raycastHit;
					RaycastHit raycastHit2;
					if (goThroughPlatform.LightPlatform)
					{
						bool insideLight = goThroughPlatform.LightPlatform.InsideLight;
						if (collider.Raycast(goThroughPlatformTestingRayLeft, out raycastHit, maxDistance) && !collider.Raycast(goThroughPlatformTestingRayLeft, out raycastHit2, 0.01f) && insideLight == LightSource.TestPosition(raycastHit.point, 0f))
						{
							flag = true;
							break;
						}
						if (collider.Raycast(goThroughPlatformTestingRayRight, out raycastHit, maxDistance) && !collider.Raycast(goThroughPlatformTestingRayRight, out raycastHit2, 0.01f) && insideLight == LightSource.TestPosition(raycastHit.point, 0f))
						{
							flag = true;
							break;
						}
					}
					else if ((collider.Raycast(goThroughPlatformTestingRayLeft, out raycastHit, maxDistance) && !collider.Raycast(goThroughPlatformTestingRayLeft, out raycastHit2, 0.01f)) || (collider.Raycast(goThroughPlatformTestingRayRight, out raycastHit, maxDistance) && !collider.Raycast(goThroughPlatformTestingRayRight, out raycastHit2, 0.01f)))
					{
						flag = true;
						break;
					}
				}
			}
		}
		else
		{
			for (int k = 0; k < GoThroughPlatformManager.GoThroughPlatforms.Count; k++)
			{
				GoThroughPlatform goThroughPlatform2 = GoThroughPlatformManager.GoThroughPlatforms[k];
				Ray goThroughPlatformTestingRayLeft2 = this.m_tester.GoThroughPlatformTestingRayLeft;
				Ray goThroughPlatformTestingRayRight2 = this.m_tester.GoThroughPlatformTestingRayRight;
				for (int l = 0; l < goThroughPlatform2.Length; l++)
				{
					float maxDistance2 = 1f;
					Collider collider2 = goThroughPlatform2.Colliders[l];
					RaycastHit raycastHit3;
					bool flag2 = (collider2.Raycast(goThroughPlatformTestingRayLeft2, out raycastHit3, maxDistance2) && !collider2.Raycast(goThroughPlatformTestingRayLeft2, out raycastHit3, 0.01f)) || (collider2.Raycast(goThroughPlatformTestingRayRight2, out raycastHit3, maxDistance2) && !collider2.Raycast(goThroughPlatformTestingRayRight2, out raycastHit3, 0.01f));
					if (flag2)
					{
						flag = true;
						break;
					}
				}
			}
		}
		if (flag)
		{
			this.m_disabledTimeRemaing = 0.2f;
			this.UpdateLayerCollision(true);
		}
		return flag;
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x00084F07 File Offset: 0x00083107
	public void FixedUpdate()
	{
		this.UpdateColliders();
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x00084F0F File Offset: 0x0008310F
	public void OnRestoreCheckpoint()
	{
		this.UpdateColliders();
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x00084F17 File Offset: 0x00083117
	public void OnEnable()
	{
		this.UpdateLayerCollision(false);
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x00084F20 File Offset: 0x00083120
	public void UpdateColliders()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.m_disabledTimeRemaing > 0f)
		{
			this.m_disabledTimeRemaing -= Time.deltaTime;
			return;
		}
		Ray goThroughPlatformTestingRayLeft = this.m_tester.GoThroughPlatformTestingRayLeft;
		Ray goThroughPlatformTestingRayRight = this.m_tester.GoThroughPlatformTestingRayRight;
		float goThroughPlatformTestingRayRadius = this.m_tester.GoThroughPlatformTestingRayRadius;
		int count = GoThroughPlatformManager.GoThroughPlatforms.Count;
		bool flag = false;
		if (LightSource.AnyExist)
		{
			for (int i = 0; i < count; i++)
			{
				GoThroughPlatform goThroughPlatform = GoThroughPlatformManager.GoThroughPlatforms[i];
				for (int j = 0; j < goThroughPlatform.Length; j++)
				{
					Collider collider = goThroughPlatform.Colliders[j];
					if (!(collider == null) && collider.enabled)
					{
						RaycastHit raycastHit;
						RaycastHit raycastHit2;
						if (goThroughPlatform.LightPlatform)
						{
							bool insideLight = goThroughPlatform.LightPlatform.InsideLight;
							if (collider.Raycast(goThroughPlatformTestingRayLeft, out raycastHit, goThroughPlatformTestingRayRadius) && !collider.Raycast(goThroughPlatformTestingRayLeft, out raycastHit2, 0.01f) && insideLight == LightSource.TestPosition(raycastHit.point, 0f))
							{
								flag = true;
								break;
							}
							if (collider.Raycast(goThroughPlatformTestingRayRight, out raycastHit, goThroughPlatformTestingRayRadius) && !collider.Raycast(goThroughPlatformTestingRayRight, out raycastHit2, 0.01f) && insideLight == LightSource.TestPosition(raycastHit.point, 0f))
							{
								flag = true;
								break;
							}
						}
						else if ((collider.Raycast(goThroughPlatformTestingRayLeft, out raycastHit, goThroughPlatformTestingRayRadius) && !collider.Raycast(goThroughPlatformTestingRayLeft, out raycastHit2, 0.01f)) || (collider.Raycast(goThroughPlatformTestingRayRight, out raycastHit, goThroughPlatformTestingRayRadius) && !collider.Raycast(goThroughPlatformTestingRayRight, out raycastHit2, 0.01f)))
						{
							flag = true;
							break;
						}
					}
				}
			}
		}
		else
		{
			for (int k = 0; k < count; k++)
			{
				GoThroughPlatform goThroughPlatform2 = GoThroughPlatformManager.GoThroughPlatforms[k];
				for (int l = 0; l < goThroughPlatform2.Length; l++)
				{
					Collider collider2 = goThroughPlatform2.Colliders[l];
					if (!(collider2 == null) && collider2.enabled)
					{
						RaycastHit raycastHit3;
						if ((collider2.Raycast(goThroughPlatformTestingRayLeft, out raycastHit3, goThroughPlatformTestingRayRadius) && !collider2.Raycast(goThroughPlatformTestingRayLeft, out raycastHit3, 0.01f)) || (collider2.Raycast(goThroughPlatformTestingRayRight, out raycastHit3, goThroughPlatformTestingRayRadius) && !collider2.Raycast(goThroughPlatformTestingRayRight, out raycastHit3, 0.01f)))
						{
							flag = true;
						}
					}
				}
			}
		}
		this.UpdateLayerCollision(!flag);
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x000851D2 File Offset: 0x000833D2
	public void UpdateLayerCollision(bool ignore)
	{
		if (this.m_playerLayer == 0)
		{
			return;
		}
		if (GoThroughPlatformHandler.m_ignore != ignore)
		{
			GoThroughPlatformHandler.m_ignore = ignore;
			Physics.IgnoreLayerCollision(this.m_playerLayer, this.m_platformLayer, ignore);
		}
	}

	// Token: 0x040019FE RID: 6654
	[HideInInspector]
	[SerializeField]
	private IGoThroughPlatformTester m_tester;

	// Token: 0x040019FF RID: 6655
	private int m_playerLayer;

	// Token: 0x04001A00 RID: 6656
	private int m_platformLayer;

	// Token: 0x04001A01 RID: 6657
	private float m_disabledTimeRemaing;

	// Token: 0x04001A02 RID: 6658
	private static bool m_ignore = true;
}
