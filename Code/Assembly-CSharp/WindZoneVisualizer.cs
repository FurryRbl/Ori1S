using System;
using UnityEngine;

// Token: 0x02000937 RID: 2359
public class WindZoneVisualizer : MonoBehaviour, ISuspendable
{
	// Token: 0x06003430 RID: 13360 RVA: 0x000DB8F4 File Offset: 0x000D9AF4
	private void Awake()
	{
		SuspensionManager.Register(this);
		this.m_windArea = base.GetComponent<WindArea>();
	}

	// Token: 0x06003431 RID: 13361 RVA: 0x000DB908 File Offset: 0x000D9B08
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06003432 RID: 13362 RVA: 0x000DB910 File Offset: 0x000D9B10
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		Material material = this.m_windArea.GetComponent<Renderer>().material;
		material.mainTextureScale = new Vector2(base.transform.lossyScale.x / 4f, base.transform.lossyScale.y / 4f);
		material.mainTextureOffset += new Vector2(0f, -0.25f) * Time.deltaTime * this.m_windArea.Speed;
	}

	// Token: 0x1700083B RID: 2107
	// (get) Token: 0x06003433 RID: 13363 RVA: 0x000DB9B1 File Offset: 0x000D9BB1
	// (set) Token: 0x06003434 RID: 13364 RVA: 0x000DB9B9 File Offset: 0x000D9BB9
	public bool IsSuspended { get; set; }

	// Token: 0x04002F27 RID: 12071
	private WindArea m_windArea;
}
