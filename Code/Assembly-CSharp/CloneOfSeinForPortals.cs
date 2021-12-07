using System;
using Game;
using UnityEngine;

// Token: 0x02000467 RID: 1127
public class CloneOfSeinForPortals : MonoBehaviour
{
	// Token: 0x06001EEA RID: 7914 RVA: 0x000881AF File Offset: 0x000863AF
	public void Awake()
	{
		Characters.Sein.CloneOfSeinForPortals = this;
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x000881BC File Offset: 0x000863BC
	private void FixedUpdate()
	{
		this.CloneRenderer.sharedMaterial = this.PlayerRenderer.sharedMaterial;
	}

	// Token: 0x04001AE0 RID: 6880
	public Renderer PlayerRenderer;

	// Token: 0x04001AE1 RID: 6881
	public Renderer CloneRenderer;
}
