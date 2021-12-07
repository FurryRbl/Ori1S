using System;
using UnityEngine;

// Token: 0x020001EB RID: 491
public class SoundMessages : MonoBehaviour
{
	// Token: 0x060010E5 RID: 4325 RVA: 0x0004D3E8 File Offset: 0x0004B5E8
	public static void ShowMessage(string name)
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject.transform.position = OnScreenPositions.TopCenter;
		gameObject.AddComponent<SoundMessage>().text = name;
		gameObject.AddComponent<LimitedLifetime>().SetRemainingLifetime(0.5f);
		gameObject.gameObject.layer = LayerMask.NameToLayer("ui");
	}
}
