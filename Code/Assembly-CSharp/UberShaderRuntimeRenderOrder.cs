using System;
using UnityEngine;

// Token: 0x02000841 RID: 2113
[ExecuteInEditMode]
public class UberShaderRuntimeRenderOrder : MonoBehaviour, IInScene
{
	// Token: 0x170007B8 RID: 1976
	// (get) Token: 0x06003029 RID: 12329 RVA: 0x000CBD6B File Offset: 0x000C9F6B
	// (set) Token: 0x0600302A RID: 12330 RVA: 0x000CBD73 File Offset: 0x000C9F73
	public bool IsInScene
	{
		get
		{
			return this.m_isInScene;
		}
		set
		{
			this.m_isInScene = value;
		}
	}

	// Token: 0x0600302B RID: 12331 RVA: 0x000CBD7C File Offset: 0x000C9F7C
	public void Awake()
	{
	}

	// Token: 0x0600302C RID: 12332 RVA: 0x000CBD80 File Offset: 0x000C9F80
	public void Update()
	{
		if (base.transform.position.z != this.m_lastZ || Application.isEditor)
		{
			this.SetRenderQueueOn(base.transform);
			this.m_lastZ = base.transform.position.z;
		}
	}

	// Token: 0x0600302D RID: 12333 RVA: 0x000CBDDC File Offset: 0x000C9FDC
	private void SetRenderQueueOn(Transform trans)
	{
		Renderer component = trans.GetComponent<Renderer>();
		if (component != null)
		{
			if (!this.IsInScene)
			{
				component.material = component.sharedMaterial;
			}
			UberShaderRenderQueue.SetRenderQueueExplicit(trans.gameObject, trans.position.z + this.OffsetPositionZ - 0.0001f);
		}
		for (int i = 0; i < trans.childCount; i++)
		{
			this.SetRenderQueueOn(trans.GetChild(i));
		}
	}

	// Token: 0x04002B5A RID: 11098
	public float OffsetPositionZ;

	// Token: 0x04002B5B RID: 11099
	[SerializeField]
	[HideInInspector]
	private bool m_isInScene;

	// Token: 0x04002B5C RID: 11100
	private float m_lastZ;
}
