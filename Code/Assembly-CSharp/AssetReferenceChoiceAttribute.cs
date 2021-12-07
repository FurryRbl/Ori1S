using System;
using UnityEngine;

// Token: 0x020005AA RID: 1450
public class AssetReferenceChoiceAttribute : PropertyAttribute
{
	// Token: 0x0600250E RID: 9486 RVA: 0x000A1BED File Offset: 0x0009FDED
	public AssetReferenceChoiceAttribute(string displayName, params string[] choices)
	{
		this.DisplayName = displayName;
		this.PrefabChoices = choices;
	}

	// Token: 0x04001F8C RID: 8076
	public string DisplayName;

	// Token: 0x04001F8D RID: 8077
	public readonly string[] PrefabChoices;
}
