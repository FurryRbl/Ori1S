using System;
using Sein.World;
using UnityEngine;

// Token: 0x02000933 RID: 2355
public class WaterPurityLogic : MonoBehaviour, IDynamicGraphicHierarchy
{
	// Token: 0x06003417 RID: 13335 RVA: 0x000DB600 File Offset: 0x000D9800
	public void FixedUpdate()
	{
		bool waterPurified = Events.WaterPurified;
		if (this.CleanGroup.activeSelf != waterPurified)
		{
			this.CleanGroup.SetActive(waterPurified);
		}
		if (this.DiseasedGroup.activeSelf == waterPurified)
		{
			this.DiseasedGroup.SetActive(!waterPurified);
		}
	}

	// Token: 0x04002F17 RID: 12055
	[NotNull]
	public GameObject CleanGroup;

	// Token: 0x04002F18 RID: 12056
	[NotNull]
	public GameObject DiseasedGroup;
}
