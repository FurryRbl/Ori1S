using System;
using UnityEngine;

// Token: 0x02000284 RID: 644
public class CutsceneScreenController : MonoBehaviour
{
	// Token: 0x06001532 RID: 5426 RVA: 0x0005E6EC File Offset: 0x0005C8EC
	public void Awake()
	{
		CutsceneScreenController.Instance = this;
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x0005E6F4 File Offset: 0x0005C8F4
	public void OnDestroy()
	{
		if (CutsceneScreenController.Instance == this)
		{
			CutsceneScreenController.Instance = null;
		}
	}

	// Token: 0x04001257 RID: 4695
	public static CutsceneScreenController Instance;

	// Token: 0x04001258 RID: 4696
	public MessageProvider LockedMessageProvider;

	// Token: 0x04001259 RID: 4697
	public ActionMethod OnLockedItemPressed;
}
