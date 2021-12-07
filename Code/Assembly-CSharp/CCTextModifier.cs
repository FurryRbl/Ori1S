using System;
using UnityEngine;

// Token: 0x02000231 RID: 561
public abstract class CCTextModifier : MonoBehaviour
{
	// Token: 0x060012D1 RID: 4817
	public abstract void Modify(CCText text);

	// Token: 0x060012D2 RID: 4818 RVA: 0x00056844 File Offset: 0x00054A44
	public void UpdateAllCCText()
	{
		foreach (CCText cctext in UnityEngine.Object.FindObjectsOfType(typeof(CCText)))
		{
			if (cctext.Modifier == this)
			{
				cctext.UpdateText();
			}
		}
	}
}
