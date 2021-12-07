using System;
using UnityEngine;

// Token: 0x0200092D RID: 2349
public class SpiritLanternPlaceholder : MonoBehaviour
{
	// Token: 0x060033FC RID: 13308 RVA: 0x000DA7D4 File Offset: 0x000D89D4
	public void Start()
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Prefab, base.transform.position, Quaternion.identity);
		gameObject.transform.parent = base.transform.parent;
		gameObject.GetComponentInChildren<AttachToRope>().RopeToAttachTo = this.RopeToAttachTo;
		if (this.RopeToAttachTo == null)
		{
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
		InstantiateUtility.Destroy(base.gameObject);
	}

	// Token: 0x04002EE9 RID: 12009
	public GameObject Prefab;

	// Token: 0x04002EEA RID: 12010
	public Rope RopeToAttachTo;
}
