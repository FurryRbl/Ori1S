using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x02000078 RID: 120
	public static class StencilMaterial
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x000146BC File Offset: 0x000128BC
		[Obsolete("Use Material.Add instead.", true)]
		public static Material Add(Material baseMat, int stencilID)
		{
			return null;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000146C0 File Offset: 0x000128C0
		public static Material Add(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask)
		{
			return StencilMaterial.Add(baseMat, stencilID, operation, compareFunction, colorWriteMask, 255, 255);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000146D8 File Offset: 0x000128D8
		public static Material Add(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask, int readMask, int writeMask)
		{
			if ((stencilID <= 0 && colorWriteMask == ColorWriteMask.All) || baseMat == null)
			{
				return baseMat;
			}
			if (!baseMat.HasProperty("_Stencil"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _Stencil property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilOp"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilOp property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilComp"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilComp property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilReadMask"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilReadMask property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilReadMask"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilWriteMask property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_ColorMask"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _ColorMask property", baseMat);
				return baseMat;
			}
			for (int i = 0; i < StencilMaterial.m_List.Count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				if (matEntry.baseMat == baseMat && matEntry.stencilId == stencilID && matEntry.operation == operation && matEntry.compareFunction == compareFunction && matEntry.readMask == readMask && matEntry.writeMask == writeMask && matEntry.colorMask == colorWriteMask)
				{
					matEntry.count++;
					return matEntry.customMat;
				}
			}
			StencilMaterial.MatEntry matEntry2 = new StencilMaterial.MatEntry();
			matEntry2.count = 1;
			matEntry2.baseMat = baseMat;
			matEntry2.customMat = new Material(baseMat);
			matEntry2.customMat.hideFlags = HideFlags.HideAndDontSave;
			matEntry2.stencilId = stencilID;
			matEntry2.operation = operation;
			matEntry2.compareFunction = compareFunction;
			matEntry2.readMask = readMask;
			matEntry2.writeMask = writeMask;
			matEntry2.colorMask = colorWriteMask;
			matEntry2.useAlphaClip = (operation != StencilOp.Keep && writeMask > 0);
			matEntry2.customMat.name = string.Format("Stencil Id:{0}, Op:{1}, Comp:{2}, WriteMask:{3}, ReadMask:{4}, ColorMask:{5} AlphaClip:{6} ({7})", new object[]
			{
				stencilID,
				operation,
				compareFunction,
				writeMask,
				readMask,
				colorWriteMask,
				matEntry2.useAlphaClip,
				baseMat.name
			});
			matEntry2.customMat.SetInt("_Stencil", stencilID);
			matEntry2.customMat.SetInt("_StencilOp", (int)operation);
			matEntry2.customMat.SetInt("_StencilComp", (int)compareFunction);
			matEntry2.customMat.SetInt("_StencilReadMask", readMask);
			matEntry2.customMat.SetInt("_StencilWriteMask", writeMask);
			matEntry2.customMat.SetInt("_ColorMask", (int)colorWriteMask);
			if (matEntry2.customMat.HasProperty("_UseAlphaClip"))
			{
				matEntry2.customMat.SetInt("_UseAlphaClip", (!matEntry2.useAlphaClip) ? 0 : 1);
			}
			if (matEntry2.useAlphaClip)
			{
				matEntry2.customMat.EnableKeyword("UNITY_UI_ALPHACLIP");
			}
			else
			{
				matEntry2.customMat.DisableKeyword("UNITY_UI_ALPHACLIP");
			}
			StencilMaterial.m_List.Add(matEntry2);
			return matEntry2.customMat;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00014A64 File Offset: 0x00012C64
		public static void Remove(Material customMat)
		{
			if (customMat == null)
			{
				return;
			}
			for (int i = 0; i < StencilMaterial.m_List.Count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				if (!(matEntry.customMat != customMat))
				{
					if (--matEntry.count == 0)
					{
						Misc.DestroyImmediate(matEntry.customMat);
						matEntry.baseMat = null;
						StencilMaterial.m_List.RemoveAt(i);
					}
					return;
				}
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00014AF0 File Offset: 0x00012CF0
		public static void ClearAll()
		{
			for (int i = 0; i < StencilMaterial.m_List.Count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				Misc.DestroyImmediate(matEntry.customMat);
				matEntry.baseMat = null;
			}
			StencilMaterial.m_List.Clear();
		}

		// Token: 0x0400021E RID: 542
		private static List<StencilMaterial.MatEntry> m_List = new List<StencilMaterial.MatEntry>();

		// Token: 0x02000079 RID: 121
		private class MatEntry
		{
			// Token: 0x0400021F RID: 543
			public Material baseMat;

			// Token: 0x04000220 RID: 544
			public Material customMat;

			// Token: 0x04000221 RID: 545
			public int count;

			// Token: 0x04000222 RID: 546
			public int stencilId;

			// Token: 0x04000223 RID: 547
			public StencilOp operation;

			// Token: 0x04000224 RID: 548
			public CompareFunction compareFunction = CompareFunction.Always;

			// Token: 0x04000225 RID: 549
			public int readMask;

			// Token: 0x04000226 RID: 550
			public int writeMask;

			// Token: 0x04000227 RID: 551
			public bool useAlphaClip;

			// Token: 0x04000228 RID: 552
			public ColorWriteMask colorMask;
		}
	}
}
