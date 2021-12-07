using System;
using UnityEngine;

// Token: 0x0200056B RID: 1387
public class EntityHighlightLogic : MonoBehaviour, IEntityHighlight
{
	// Token: 0x06002400 RID: 9216 RVA: 0x0009D0FB File Offset: 0x0009B2FB
	public void Reset()
	{
	}

	// Token: 0x06002401 RID: 9217 RVA: 0x0009D100 File Offset: 0x0009B300
	public void SetToBashHighlight()
	{
		if (this.OnBashHighlightEffect)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.OnBashHighlightEffect, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = base.transform;
		}
	}

	// Token: 0x06002402 RID: 9218 RVA: 0x0009D150 File Offset: 0x0009B350
	public void SetToSpiritFlame()
	{
		if (this.OnSpiritFlameHighlightEffect)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.OnSpiritFlameHighlightEffect, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = base.transform;
		}
	}

	// Token: 0x06002403 RID: 9219 RVA: 0x0009D19F File Offset: 0x0009B39F
	public void SetToChargeDash()
	{
	}

	// Token: 0x04001E22 RID: 7714
	public GameObject OnBashHighlightEffect;

	// Token: 0x04001E23 RID: 7715
	public GameObject OnSpiritFlameHighlightEffect;
}
