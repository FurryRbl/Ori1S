using System;
using Game;
using UnityEngine;

// Token: 0x0200056A RID: 1386
public class EntityHighlightColor : MonoBehaviour, IEntityHighlight, IInScene
{
	// Token: 0x17000600 RID: 1536
	// (get) Token: 0x060023F5 RID: 9205 RVA: 0x0009CF44 File Offset: 0x0009B144
	// (set) Token: 0x060023F6 RID: 9206 RVA: 0x0009CF4C File Offset: 0x0009B14C
	public bool IsInScene
	{
		get
		{
			return this.m_isInScene;
		}
		set
		{
			this.m_isInScene = value;
		}
	}

	// Token: 0x060023F7 RID: 9207 RVA: 0x0009CF58 File Offset: 0x0009B158
	public void Awake()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_propertyID = Shader.PropertyToID(this.Property);
		Material sharedMaterial = this.m_renderer.sharedMaterial;
		if (!sharedMaterial.HasProperty(this.m_propertyID))
		{
			base.enabled = false;
		}
		else
		{
			this.m_originalColor = sharedMaterial.GetColor(this.m_propertyID);
			this.m_targetColor = this.m_originalColor;
		}
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		if (!this.IsInScene)
		{
			Material material = this.m_renderer.material;
			this.m_madeMaterial = true;
		}
	}

	// Token: 0x060023F8 RID: 9208 RVA: 0x0009CFFD File Offset: 0x0009B1FD
	public void OnDestroy()
	{
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
		if (this.m_madeMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.m_renderer.sharedMaterial);
		}
	}

	// Token: 0x060023F9 RID: 9209 RVA: 0x0009D030 File Offset: 0x0009B230
	public void OnRestoreCheckpoint()
	{
		this.Reset();
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x0009D038 File Offset: 0x0009B238
	public void Reset()
	{
		if (base.enabled)
		{
			this.m_targetColor = this.m_originalColor;
		}
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x0009D051 File Offset: 0x0009B251
	public void SetToBashHighlight()
	{
		if (base.enabled)
		{
			this.m_targetColor = this.BashHighlight;
		}
	}

	// Token: 0x060023FC RID: 9212 RVA: 0x0009D06A File Offset: 0x0009B26A
	public void SetToSpiritFlame()
	{
		if (base.enabled)
		{
			this.m_targetColor = this.SpiritFlameHighlight;
		}
	}

	// Token: 0x060023FD RID: 9213 RVA: 0x0009D083 File Offset: 0x0009B283
	public void SetToChargeDash()
	{
		if (base.enabled)
		{
			this.m_targetColor = new Color(0f, 0.46f, 1f, 0.5f);
		}
	}

	// Token: 0x060023FE RID: 9214 RVA: 0x0009D0B0 File Offset: 0x0009B2B0
	public void Update()
	{
		Material sharedMaterial = this.m_renderer.sharedMaterial;
		Color color = sharedMaterial.GetColor(this.m_propertyID);
		sharedMaterial.SetColor(this.m_propertyID, Color.Lerp(this.m_targetColor, color, 0.5f));
	}

	// Token: 0x04001E19 RID: 7705
	public Color BashHighlight = new Color(1f, 1f, 0f, 0.5f);

	// Token: 0x04001E1A RID: 7706
	public Color SpiritFlameHighlight = new Color(1f, 0f, 0f, 0.5f);

	// Token: 0x04001E1B RID: 7707
	private Color m_originalColor;

	// Token: 0x04001E1C RID: 7708
	public string Property = "_OutlineColor";

	// Token: 0x04001E1D RID: 7709
	private int m_propertyID;

	// Token: 0x04001E1E RID: 7710
	private Renderer m_renderer;

	// Token: 0x04001E1F RID: 7711
	private Color m_targetColor;

	// Token: 0x04001E20 RID: 7712
	private bool m_madeMaterial;

	// Token: 0x04001E21 RID: 7713
	[SerializeField]
	[HideInInspector]
	private bool m_isInScene;
}
