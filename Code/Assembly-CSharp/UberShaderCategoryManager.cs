using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200082D RID: 2093
public static class UberShaderCategoryManager
{
	// Token: 0x06002FD6 RID: 12246 RVA: 0x000CAE0C File Offset: 0x000C900C
	public static Color GetCategoryColor(UberShaderCategory category)
	{
		return UberShaderCategoryManager.s_categoryColor[category];
	}

	// Token: 0x04002B15 RID: 11029
	private static Dictionary<UberShaderCategory, Color> s_categoryColor = new Dictionary<UberShaderCategory, Color>
	{
		{
			UberShaderCategory.Lighting,
			new Color(0.5f, 1f, 0.5f)
		},
		{
			UberShaderCategory.Turbulence,
			new Color(1f, 0f, 0f)
		},
		{
			UberShaderCategory.Interaction,
			new Color(0.9f, 0.3f, 0.25f)
		},
		{
			UberShaderCategory.Utility,
			new Color(0.65f, 0.5f, 0.25f)
		},
		{
			UberShaderCategory.Water,
			new Color(0f, 0.2f, 1f)
		},
		{
			UberShaderCategory.Distortion,
			new Color(0.4f, 0.4f, 1f)
		},
		{
			UberShaderCategory.Masking,
			new Color(0.7f, 0.8f, 0.7f)
		},
		{
			UberShaderCategory.Effects,
			new Color(1f, 1f, 0.1f)
		},
		{
			UberShaderCategory.Animation,
			new Color(1f, 0.4f, 0.5f)
		},
		{
			UberShaderCategory.Text,
			new Color(0.9f, 0.9f, 0.9f)
		}
	};
}
