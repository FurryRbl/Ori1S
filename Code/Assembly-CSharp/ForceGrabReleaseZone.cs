using System;
using UnityEngine;

// Token: 0x0200042C RID: 1068
public class ForceGrabReleaseZone : MonoBehaviour
{
	// Token: 0x06001DC2 RID: 7618 RVA: 0x000835F4 File Offset: 0x000817F4
	public static bool InsideZone(Vector3 position)
	{
		for (int i = 0; i < ForceGrabReleaseZone.m_all.Count; i++)
		{
			ForceGrabReleaseZone forceGrabReleaseZone = ForceGrabReleaseZone.m_all[i];
			if (forceGrabReleaseZone.m_bounds.Contains(position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x0008363C File Offset: 0x0008183C
	public void Awake()
	{
		this.m_bounds = Utility.RectFromBounds(Utility.BoundsFromTransform(base.transform));
		ForceGrabReleaseZone.m_all.Add(this);
	}

	// Token: 0x06001DC4 RID: 7620 RVA: 0x0008365F File Offset: 0x0008185F
	public void OnDestroy()
	{
		ForceGrabReleaseZone.m_all.Remove(this);
	}

	// Token: 0x040019A0 RID: 6560
	private static AllContainer<ForceGrabReleaseZone> m_all = new AllContainer<ForceGrabReleaseZone>();

	// Token: 0x040019A1 RID: 6561
	private Rect m_bounds;
}
