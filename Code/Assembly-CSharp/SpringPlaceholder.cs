using System;
using UnityEngine;

// Token: 0x0200092E RID: 2350
public class SpringPlaceholder : MonoBehaviour
{
	// Token: 0x060033FE RID: 13310 RVA: 0x000DA864 File Offset: 0x000D8A64
	public void Awake()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Spring, base.transform.position, base.transform.localRotation);
		gameObject.transform.parent = base.transform.parent;
		SpringSeinAction componentInChildren = gameObject.GetComponentInChildren<SpringSeinAction>();
		componentInChildren.Height = this.Height;
		InstantiateUtility.Destroy(base.gameObject);
	}

	// Token: 0x04002EEB RID: 12011
	public GameObject Spring;

	// Token: 0x04002EEC RID: 12012
	public float Height = 12f;
}
