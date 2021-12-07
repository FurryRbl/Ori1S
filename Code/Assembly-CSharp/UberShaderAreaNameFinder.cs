using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200081C RID: 2076
public static class UberShaderAreaNameFinder
{
	// Token: 0x06002FAB RID: 12203 RVA: 0x000CA0D8 File Offset: 0x000C82D8
	public static string GetAreaImplString(UberAtlasArea area)
	{
		return UberShaderAreaNameFinder.s_areas[(int)area];
	}

	// Token: 0x06002FAC RID: 12204 RVA: 0x000CA0E4 File Offset: 0x000C82E4
	public static string GetAreaName(string name)
	{
		if (UberShaderAreaNameFinder.s_nameToArea == null)
		{
			UberShaderAreaNameFinder.s_nameToArea = new Dictionary<string, string>();
		}
		string text;
		if (UberShaderAreaNameFinder.s_nameToArea.TryGetValue(name, out text))
		{
			return text;
		}
		foreach (string text2 in UberShaderAreaNameFinder.s_areas)
		{
			if (name.StartsWith(text2))
			{
				text = text2;
				break;
			}
		}
		if (string.IsNullOrEmpty(text))
		{
			return string.Empty;
		}
		UberShaderAreaNameFinder.s_nameToArea.Add(name, text);
		return text;
	}

	// Token: 0x06002FAD RID: 12205 RVA: 0x000CA168 File Offset: 0x000C8368
	public static string GetAreaName(Transform root)
	{
		if (root == null)
		{
			return string.Empty;
		}
		return UberShaderAreaNameFinder.GetAreaName(root.name);
	}

	// Token: 0x04002AE8 RID: 10984
	private static Dictionary<string, string> s_nameToArea;

	// Token: 0x04002AE9 RID: 10985
	private static string[] s_areas = new string[]
	{
		"titleScreen",
		"swallowsNest",
		"spiritTree",
		"sunkenGlades",
		"westGlades",
		"upperGlades",
		"thornfeltSwamp",
		"moonGrotto",
		"ginsoTree",
		"ginsoEntrance",
		"mistyWoods",
		"forlornRuins",
		"valleyOfTheWind",
		"horuFields",
		"mountHoru",
		"catAndMouse",
		"outro",
		"kuroAttack",
		"kuroNest",
		"kuroMoment",
		"theSacrifice",
		"worldMap",
		"prefab",
		"sorrowPass",
		"shared",
		"mangroveFalls",
		"northMangroveFalls",
		"southMangroveFalls"
	};
}
