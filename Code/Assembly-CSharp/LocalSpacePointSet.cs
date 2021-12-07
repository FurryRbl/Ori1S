using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000654 RID: 1620
public class LocalSpacePointSet : MonoBehaviour
{
	// Token: 0x17000647 RID: 1607
	// (get) Token: 0x06002791 RID: 10129 RVA: 0x000AC3F6 File Offset: 0x000AA5F6
	// (set) Token: 0x06002792 RID: 10130 RVA: 0x000AC404 File Offset: 0x000AA604
	public List<Vector3> WorldSpaceWorldSpaceInteractionPoints
	{
		get
		{
			this.UpdateWorldSpacePointCache();
			return this.m_cachedWorldSpaceInteractionPoints;
		}
		set
		{
			List<Vector3> list = new List<Vector3>(value);
			for (int i = 0; i < list.Count; i++)
			{
				list[i] = this.ParentTransform.InverseTransformPoint(list[i]);
			}
			this.LocalSpaceInteractionPoints = list.ToArray();
			this.MarkDirty();
		}
	}

	// Token: 0x06002793 RID: 10131 RVA: 0x000AC45A File Offset: 0x000AA65A
	public void MarkDirty()
	{
		this.m_cachedWorldSpaceInteractionPoints = null;
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x000AC464 File Offset: 0x000AA664
	private void UpdateWorldSpacePointCache()
	{
		if (this.LocalSpaceInteractionPoints == null)
		{
			return;
		}
		bool flag = this.m_cachedWorldSpaceInteractionPoints == null || this.m_cachedWorldSpaceInteractionPoints.Count != this.LocalSpaceInteractionPoints.Length;
		if (flag)
		{
			this.m_cachedWorldSpaceInteractionPoints = new List<Vector3>(this.LocalSpaceInteractionPoints.Length);
			for (int i = 0; i < this.LocalSpaceInteractionPoints.Length; i++)
			{
				this.m_cachedWorldSpaceInteractionPoints.Add(default(Vector3));
			}
		}
		if (flag || this.IsParentDynamic)
		{
			for (int j = 0; j < this.LocalSpaceInteractionPoints.Length; j++)
			{
				this.m_cachedWorldSpaceInteractionPoints[j] = this.ParentTransform.TransformPoint(this.LocalSpaceInteractionPoints[j]);
			}
		}
	}

	// Token: 0x04002234 RID: 8756
	public Transform ParentTransform;

	// Token: 0x04002235 RID: 8757
	public bool IsParentDynamic;

	// Token: 0x04002236 RID: 8758
	public Color PointColor = Color.white;

	// Token: 0x04002237 RID: 8759
	public Vector3[] LocalSpaceInteractionPoints;

	// Token: 0x04002238 RID: 8760
	private List<Vector3> m_cachedWorldSpaceInteractionPoints;
}
