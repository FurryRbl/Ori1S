using System;
using UnityEngine;

// Token: 0x02000840 RID: 2112
public static class UberShaderRenderQueue
{
	// Token: 0x06003024 RID: 12324 RVA: 0x000CBA74 File Offset: 0x000C9C74
	public static double GetUberComponentZ(UberShaderComponent component)
	{
		UberShaderBlock block = component.Block;
		double num;
		if (block is UberShaderBlockGrabPass)
		{
			num = -1.0;
		}
		else if (block.IsAlphaMasked && !block.DoAlphaMaskedOrder)
		{
			num = -1.0;
		}
		else
		{
			num = (double)component.transform.position.z;
			num += (double)component.Block.OffsetPositionZ;
		}
		if (component.Block.IsRotated && (!(component.Block is UberShaderBlockTextured) || !component.TexturedBlock.CenteredQueue))
		{
			num += (double)(component.transform.lossyScale.y * 0.5f);
		}
		return num + component.Block.RandomOffset;
	}

	// Token: 0x06003025 RID: 12325 RVA: 0x000CBB48 File Offset: 0x000C9D48
	private static string RenderlayerFromZ(double z)
	{
		for (int i = 0; i < UberShaderRenderQueue.s_zRanges.Length; i++)
		{
			if (z < UberShaderRenderQueue.s_zRanges[i])
			{
				if (i == 0)
				{
					i = 1;
				}
				return UberShaderRenderQueue.s_zLayers[i - 1];
			}
		}
		return "farForeground";
	}

	// Token: 0x06003026 RID: 12326 RVA: 0x000CBB98 File Offset: 0x000C9D98
	private static void SetQeueusFromZ(double z, Renderer renderer, bool inZRanges)
	{
		double num = 0.0;
		double num2 = 0.0;
		if (inZRanges)
		{
			if (z < UberShaderRenderQueue.s_zRanges[0])
			{
				num = double.NegativeInfinity;
				num2 = UberShaderRenderQueue.s_zRanges[0];
			}
			else if (z > UberShaderRenderQueue.s_zRanges[UberShaderRenderQueue.s_zRanges.Length - 1])
			{
				num2 = double.PositiveInfinity;
				num = UberShaderRenderQueue.s_zRanges[UberShaderRenderQueue.s_zRanges.Length - 1];
			}
			else
			{
				for (int i = 0; i < UberShaderRenderQueue.s_zRanges.Length; i++)
				{
					if (z <= UberShaderRenderQueue.s_zRanges[i])
					{
						num = UberShaderRenderQueue.s_zRanges[i - 1];
						num2 = UberShaderRenderQueue.s_zRanges[i];
						break;
					}
				}
			}
		}
		else
		{
			num = UberShaderRenderQueue.s_zRanges[0];
			num2 = UberShaderRenderQueue.s_zRanges[UberShaderRenderQueue.s_zRanges.Length - 1];
		}
		double num3 = Math.Abs(num2 - num);
		double num4 = 192000000.0 / num3;
		double num5 = 1.0 - (z - num) / num3;
		int num6 = (int)Math.Round(num5 * num4);
		Material sharedMaterial = renderer.sharedMaterial;
		int num7 = 3000 + num6 % 12000;
		if (sharedMaterial.renderQueue != num7)
		{
			sharedMaterial.renderQueue = num7;
		}
		renderer.sortingOrder = num6 / 12000;
	}

	// Token: 0x06003027 RID: 12327 RVA: 0x000CBCE8 File Offset: 0x000C9EE8
	public static void SetRenderQueueExplicit(GameObject go, float z)
	{
		Renderer component = go.GetComponent<Renderer>();
		component.sortingLayerName = UberShaderRenderQueue.RenderlayerFromZ((double)z);
		UberShaderRenderQueue.SetQeueusFromZ((double)z, component, true);
		if (component.sharedMaterial.HasProperty("_DepthFlipScreen"))
		{
			Vector4 vector = component.sharedMaterial.GetVector("_DepthFlipScreen");
			component.sharedMaterial.SetVector("_DepthFlipScreen", new Vector4(z, vector.y, vector.z, vector.w));
		}
	}

	// Token: 0x04002B57 RID: 11095
	public const float GrabPassZ = -1f;

	// Token: 0x04002B58 RID: 11096
	private static double[] s_zRanges = new double[]
	{
		-100.0,
		-5.0,
		-1.0,
		1.0,
		5.0,
		400.0
	};

	// Token: 0x04002B59 RID: 11097
	private static string[] s_zLayers = new string[]
	{
		"farForeground",
		"foreground",
		"center",
		"background",
		"farBackground"
	};
}
