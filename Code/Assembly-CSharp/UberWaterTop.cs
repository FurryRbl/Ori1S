using System;
using UnityEngine;

// Token: 0x0200085B RID: 2139
[ExecuteInEditMode]
public class UberWaterTop : UberWaterComponent
{
	// Token: 0x170007C1 RID: 1985
	// (get) Token: 0x06003078 RID: 12408 RVA: 0x000CDE44 File Offset: 0x000CC044
	private UberWaterReflection Reflection
	{
		get
		{
			if (this.m_reflection == null)
			{
				this.m_reflection = new UberWaterReflection(base.Control, base.GetComponent<Renderer>(), base.transform);
			}
			return this.m_reflection;
		}
	}

	// Token: 0x06003079 RID: 12409 RVA: 0x000CDE80 File Offset: 0x000CC080
	private void OnEnable()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_isLava = (this.m_renderer.sharedMaterial.shader.name == "SeinWater/Lava");
		this.m_renderer.sortingLayerName = "water";
	}

	// Token: 0x0600307A RID: 12410 RVA: 0x000CDECE File Offset: 0x000CC0CE
	private void OnBecameVisible()
	{
		if (base.Control != null)
		{
			base.Control.DoSim = true;
		}
	}

	// Token: 0x0600307B RID: 12411 RVA: 0x000CDEED File Offset: 0x000CC0ED
	private void OnBecameInvisible()
	{
		if (base.Control != null)
		{
			base.Control.DoSim = false;
		}
	}

	// Token: 0x0600307C RID: 12412 RVA: 0x000CDF0C File Offset: 0x000CC10C
	public override void GenerateMesh()
	{
		this.m_normalMesh = this.GetMesh(this.m_normalMesh, -100f, base.Control.transform.localScale.z);
		base.Filter.sharedMesh = this.m_normalMesh;
	}

	// Token: 0x0600307D RID: 12413 RVA: 0x000CDF5C File Offset: 0x000CC15C
	public void EnsureMesh()
	{
		if (base.Control == null)
		{
			return;
		}
		if (this.m_normalMesh == null)
		{
			this.m_normalMesh = this.GetMesh(this.m_normalMesh, 0f, base.Control.transform.localScale.z);
		}
		base.Filter.sharedMesh = this.m_normalMesh;
	}

	// Token: 0x0600307E RID: 12414 RVA: 0x000CDFCC File Offset: 0x000CC1CC
	private Mesh GetMesh(Mesh var, float minZ, float maxZ)
	{
		float num = 0.02f;
		float num2 = base.Control.ExtendBack / base.Control.transform.localScale.z;
		float num3 = base.Control.ExtendLeft / base.Control.transform.localScale.x;
		float num4 = base.Control.ExtendRight / base.Control.transform.localScale.x;
		float num5 = base.Control.ExtendFront / base.Control.transform.localScale.z;
		int num6 = Mathf.CeilToInt(num2 / 0.2f);
		int num7 = Mathf.CeilToInt(num3 / 0.2f);
		int num8 = Mathf.CeilToInt(num4 / 0.2f);
		int num9 = 6 + num6 + num7 + num8;
		float num10 = 1f / base.Control.transform.localScale.z;
		float num11 = -0.5f - num5;
		float num12 = 0f;
		if (base.Control.CrossSection)
		{
			num9 += base.ResX;
			num11 += num10;
			num12 += num10;
		}
		float f = (0.5f - num11) * base.Control.transform.localScale.z;
		int num13 = Mathf.CeilToInt(f);
		num9 += num13;
		base.StartMesh(num9);
		if (base.Control.CrossSection && minZ <= 0f)
		{
			base.AppendQuadStrip(new Vector2(-0.5f, -0.5f - num5), new Vector2(0.5f, -0.5f - num5 + num10), new Vector2(0f, 0f), new Vector2(1f, num10), base.ResX, 1);
		}
		float num14 = (0.5f - num11) / (float)num13;
		float num15 = (1f - num - num12) / (float)num13;
		for (int i = 0; i < num13; i++)
		{
			float num16 = (num11 + (float)(i - 2) * num14 + 0.5f) * base.Control.transform.localScale.z;
			float num17 = (num11 + (float)(i + 2) * num14 + 0.5f) * base.Control.transform.localScale.z;
			if (num17 >= minZ)
			{
				if (num16 <= maxZ)
				{
					base.AppendQuad(new Vector2(-0.5f, num11 + (float)i * num14), new Vector2(0.5f, num11 + (float)(i + 1) * num14), new Vector2(0f, num10 + num15 * (float)i), new Vector2(1f, num10 + num15 * (float)(i + 1)));
				}
			}
		}
		float num18 = num2 / (float)num6;
		for (int j = 1; j <= num6; j++)
		{
			float num19 = (0.5f + (float)(j - 2) * num18) * base.Control.transform.localScale.z;
			float num20 = (0.5f + (float)(j + 2) * num18) * base.Control.transform.localScale.z;
			if (num19 >= minZ)
			{
				if (num20 <= maxZ)
				{
					float value = UnityEngine.Random.value;
					base.AppendQuad(new Vector2(-0.5f, 0.5f + (float)(j - 1) * num18), new Vector2(0.5f, 0.5f + (float)j * num18), new Vector2(value, 0.8f), new Vector2(value + 1f, 1f - num));
				}
			}
		}
		num18 = num3 / (float)num7;
		for (int k = 1; k <= num7; k++)
		{
			float num21 = UnityEngine.Random.Range(0.2f, 0.8f);
			base.AppendQuad(new Vector2(-0.5f - (float)k * num18, -0.5f - num5), new Vector2(-0.5f - (float)(k - 1) * num18, 0.5f + num2), new Vector2(num, num21), new Vector2(0.2f, num21 + 1f + num2));
		}
		num18 = num4 / (float)num8;
		for (int l = 1; l <= num8; l++)
		{
			float num22 = UnityEngine.Random.Range(0.2f, 0.8f);
			base.AppendQuad(new Vector2(0.5f + (float)(l - 1) * num18, -0.5f - num5), new Vector2(0.5f + (float)l * num18, 0.5f + num2), new Vector2(0.8f, num22), new Vector2(1f - num, num22 + 1f + num2));
		}
		return base.CreateMesh(var, false);
	}

	// Token: 0x0600307F RID: 12415 RVA: 0x000CE4B0 File Offset: 0x000CC6B0
	private void SetZOffset()
	{
		if (base.transform.rotation.eulerAngles.x == 0f)
		{
			CustomizeMaterial component = base.GetComponent<CustomizeMaterial>();
			if (component != null)
			{
				component.OffsetPositionZ = (base.Control.transform.localScale.z + base.Control.ExtendBack) * 0.5f;
			}
		}
	}

	// Token: 0x170007C2 RID: 1986
	// (get) Token: 0x06003080 RID: 12416 RVA: 0x000CE525 File Offset: 0x000CC725
	public int FrontSize
	{
		get
		{
			if (this.m_frontSize == 0)
			{
				this.m_frontSize = Shader.PropertyToID("_FrontSize");
			}
			return this.m_frontSize;
		}
	}

	// Token: 0x06003081 RID: 12417 RVA: 0x000CE548 File Offset: 0x000CC748
	public void OnWillRenderObject()
	{
		if (base.Control == null || this.m_renderer == null || this.m_renderer.sharedMaterial == null || UberWaterReflection.ReflectionRender)
		{
			return;
		}
		base.Control.BindShaderVariablesToMaterial(this.m_renderer.sharedMaterial);
		this.m_renderer.sharedMaterial.SetFloat(this.FrontSize, base.Control.transform.localScale.z);
		if (!this.m_isLava)
		{
			this.Reflection.Control = base.Control;
			this.Reflection.ReflectPlane = this;
			this.Reflection.OnWillRenderObject();
		}
	}

	// Token: 0x06003082 RID: 12418 RVA: 0x000CE60F File Offset: 0x000CC80F
	private void OnDestroy()
	{
		if (this.m_reflection != null)
		{
			this.m_reflection.OnDestroy();
		}
	}

	// Token: 0x04002BCE RID: 11214
	[SerializeField]
	private UberWaterReflection m_reflection;

	// Token: 0x04002BCF RID: 11215
	[HideInInspector]
	[SerializeField]
	private Mesh m_normalMesh;

	// Token: 0x04002BD0 RID: 11216
	private Renderer m_renderer;

	// Token: 0x04002BD1 RID: 11217
	private bool m_isLava;

	// Token: 0x04002BD2 RID: 11218
	private int m_frontSize;
}
