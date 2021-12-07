using System;
using Game;
using UnityEngine;

// Token: 0x020002EA RID: 746
public class GetItemAction : ActionMethod
{
	// Token: 0x0600169C RID: 5788 RVA: 0x00062E60 File Offset: 0x00061060
	public override void Perform(IContext context)
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Item);
		gameObject.transform.SetParentMaintainingRotationAndScale(Characters.Sein.Controller.GetItemTransform);
		gameObject.transform.localPosition = Vector3.zero;
	}

	// Token: 0x04001377 RID: 4983
	public GameObject Item;
}
