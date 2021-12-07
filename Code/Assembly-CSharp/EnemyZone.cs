using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200057E RID: 1406
public class EnemyZone : MonoBehaviour
{
	// Token: 0x06002451 RID: 9297 RVA: 0x0009E7E4 File Offset: 0x0009C9E4
	public static bool InSameZone(Vector2 origin, Vector2 position)
	{
		for (int i = 0; i < EnemyZone.All.Count; i++)
		{
			EnemyZone enemyZone = EnemyZone.All[i];
			if (enemyZone.IsInside(origin) && !enemyZone.IsInside(position))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002452 RID: 9298 RVA: 0x0009E840 File Offset: 0x0009CA40
	public bool IsInside(Vector3 position)
	{
		if (this.m_childBounds != null)
		{
			for (int i = 0; i < this.m_childBounds.Length; i++)
			{
				if (this.m_childBounds[i].Contains(position))
				{
					return true;
				}
			}
			return false;
		}
		return this.m_bounds.Contains(position);
	}

	// Token: 0x06002453 RID: 9299 RVA: 0x0009E898 File Offset: 0x0009CA98
	public void Awake()
	{
		EnemyZone.All.Add(this);
		if (base.transform.childCount > 0)
		{
			this.m_childBounds = new Rect[base.transform.childCount];
			for (int i = 0; i < this.m_childBounds.Length; i++)
			{
				Transform child = base.transform.GetChild(i);
				this.m_childBounds[i] = new Rect
				{
					width = Mathf.Abs(child.localScale.x),
					height = Mathf.Abs(child.localScale.y),
					center = child.position
				};
			}
		}
		else
		{
			this.m_bounds = new Rect
			{
				width = Mathf.Abs(base.transform.localScale.x),
				height = Mathf.Abs(base.transform.localScale.y),
				center = base.transform.position
			};
		}
	}

	// Token: 0x06002454 RID: 9300 RVA: 0x0009E9CA File Offset: 0x0009CBCA
	public void OnDestroy()
	{
		EnemyZone.All.Remove(this);
	}

	// Token: 0x04001E91 RID: 7825
	public static List<EnemyZone> All = new List<EnemyZone>();

	// Token: 0x04001E92 RID: 7826
	private Rect m_bounds;

	// Token: 0x04001E93 RID: 7827
	private Rect[] m_childBounds;
}
