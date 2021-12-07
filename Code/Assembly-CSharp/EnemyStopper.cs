using System;
using UnityEngine;

// Token: 0x0200053F RID: 1343
public class EnemyStopper : MonoBehaviour
{
	// Token: 0x06002346 RID: 9030 RVA: 0x0009A4E0 File Offset: 0x000986E0
	public static bool InsideEnemyStopper(Vector3 position, Vector3 direction, out bool doesContain)
	{
		for (int i = 0; i < EnemyStopper.m_all.Count; i++)
		{
			EnemyStopper enemyStopper = EnemyStopper.m_all[i];
			if (enemyStopper.m_bounds.Contains(position))
			{
				doesContain = true;
				if (Vector3.Dot((enemyStopper.transform.position - position).normalized, direction) > 0f)
				{
					return true;
				}
			}
		}
		doesContain = false;
		return false;
	}

	// Token: 0x06002347 RID: 9031 RVA: 0x0009A558 File Offset: 0x00098758
	public static bool InsideEnemyStopper(Vector3 position)
	{
		for (int i = 0; i < EnemyStopper.m_all.Count; i++)
		{
			EnemyStopper enemyStopper = EnemyStopper.m_all[i];
			if (enemyStopper.m_bounds.Contains(position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002348 RID: 9032 RVA: 0x0009A5A0 File Offset: 0x000987A0
	public void Awake()
	{
		Bounds bounds = Utility.BoundsFromTransform(base.transform);
		bounds.Expand(1f);
		this.m_bounds = Utility.RectFromBounds(bounds);
		EnemyStopper.m_all.Add(this);
	}

	// Token: 0x06002349 RID: 9033 RVA: 0x0009A5DC File Offset: 0x000987DC
	public void OnDestroy()
	{
		EnemyStopper.m_all.Remove(this);
	}

	// Token: 0x04001DAC RID: 7596
	private static AllContainer<EnemyStopper> m_all = new AllContainer<EnemyStopper>();

	// Token: 0x04001DAD RID: 7597
	private Rect m_bounds;
}
