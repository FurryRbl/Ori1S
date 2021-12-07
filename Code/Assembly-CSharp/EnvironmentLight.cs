using System;
using UnityEngine;

// Token: 0x020007F0 RID: 2032
[ExecuteInEditMode]
public class EnvironmentLight : MonoBehaviour
{
	// Token: 0x06002EA6 RID: 11942 RVA: 0x000C5B8C File Offset: 0x000C3D8C
	// Note: this type is marked as 'beforefieldinit'.
	static EnvironmentLight()
	{
		string[,,] array = new string[2, 2, 3];
		array[0, 0, 0] = EnvironmentLight.MainLightName + "0";
		array[0, 0, 1] = EnvironmentLight.MainLightName + "0_US_ST";
		array[0, 0, 2] = EnvironmentLight.MainLightName + "Color0";
		array[0, 1, 0] = EnvironmentLight.MainLightName + "1";
		array[0, 1, 1] = EnvironmentLight.MainLightName + "1_US_ST";
		array[0, 1, 2] = EnvironmentLight.MainLightName + "Color1";
		array[1, 0, 0] = EnvironmentLight.BounceLightName + "0";
		array[1, 0, 1] = EnvironmentLight.BounceLightName + "0_US_ST";
		array[1, 0, 2] = EnvironmentLight.BounceLightName + "Color0";
		array[1, 1, 0] = EnvironmentLight.BounceLightName + "1";
		array[1, 1, 1] = EnvironmentLight.BounceLightName + "1_US_ST";
		array[1, 1, 2] = EnvironmentLight.BounceLightName + "Color1";
		EnvironmentLight.LightNames = array;
	}

	// Token: 0x1700077F RID: 1919
	// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x000C5CDA File Offset: 0x000C3EDA
	public bool IsSceneLight
	{
		get
		{
			return base.GetComponent<SceneRoot>() != null;
		}
	}

	// Token: 0x06002EA8 RID: 11944 RVA: 0x000C5CE8 File Offset: 0x000C3EE8
	private void OnEnable()
	{
		if (EnvironmentLightingManager.Instance != null)
		{
			EnvironmentLightingManager.Instance.AddLight(this);
		}
	}

	// Token: 0x06002EA9 RID: 11945 RVA: 0x000C5D05 File Offset: 0x000C3F05
	private void OnDisable()
	{
		if (EnvironmentLightingManager.Instance != null)
		{
			EnvironmentLightingManager.Instance.RemoveLight(this);
		}
	}

	// Token: 0x06002EAA RID: 11946 RVA: 0x000C5D24 File Offset: 0x000C3F24
	private void Update()
	{
		if (this.IsSceneLight)
		{
			SceneRoot component = base.GetComponent<SceneRoot>();
			SceneMetaData metaData = component.MetaData;
			this.Area = metaData.SceneBounds;
		}
		else
		{
			this.Area.center = base.transform.position;
		}
		Vector3 position = base.transform.position;
		position.z = 0f;
		base.transform.position = position;
	}

	// Token: 0x06002EAB RID: 11947 RVA: 0x000C5D9C File Offset: 0x000C3F9C
	public Color GetColorForChannel(EnvironmentLight.Channel channel)
	{
		switch (channel)
		{
		case EnvironmentLight.Channel.Ori:
			return this.ChannelColorOri;
		case EnvironmentLight.Channel.Naru:
			return this.ChannelColorNaru;
		case EnvironmentLight.Channel.Effects:
			return this.ChannelColorEffects;
		case EnvironmentLight.Channel.Enemies:
			return this.ChannelColorEnemies;
		default:
			return Color.white;
		}
	}

	// Token: 0x06002EAC RID: 11948 RVA: 0x000C5DE8 File Offset: 0x000C3FE8
	public static void ClearBind(Material bindMaterial, int num)
	{
		bindMaterial.SetColor(EnvironmentLight.LightNames[0, num, 0], Color.clear);
		bindMaterial.SetColor(EnvironmentLight.LightNames[1, num, 0], Color.clear);
	}

	// Token: 0x06002EAD RID: 11949 RVA: 0x000C5E25 File Offset: 0x000C4025
	public void BindLightToMaterial(Material bindMaterial, float randomOffset, int num)
	{
		this.MainLight.BindToMaterial(bindMaterial, 0, randomOffset, num);
		this.BounceLight.BindToMaterial(bindMaterial, 1, randomOffset, num);
	}

	// Token: 0x040029D5 RID: 10709
	public EnvironmentLightTexture MainLight = new EnvironmentLightTexture();

	// Token: 0x040029D6 RID: 10710
	public EnvironmentLightTexture BounceLight = new EnvironmentLightTexture();

	// Token: 0x040029D7 RID: 10711
	[HideInInspector]
	public Rect Area = new Rect(0f, 0f, 10f, 10f);

	// Token: 0x040029D8 RID: 10712
	public Color ChannelColorOri = Color.white;

	// Token: 0x040029D9 RID: 10713
	public Color ChannelColorNaru = Color.white;

	// Token: 0x040029DA RID: 10714
	public Color ChannelColorEffects = Color.white;

	// Token: 0x040029DB RID: 10715
	public Color ChannelColorEnemies = Color.white;

	// Token: 0x040029DC RID: 10716
	public static readonly string MainLightName = "_LightMain";

	// Token: 0x040029DD RID: 10717
	public static readonly string BounceLightName = "_LightBounce";

	// Token: 0x040029DE RID: 10718
	public static string[,,] LightNames;

	// Token: 0x040029DF RID: 10719
	public int ChannelMask = -1;

	// Token: 0x020007F4 RID: 2036
	public enum Channel
	{
		// Token: 0x040029EF RID: 10735
		Ori,
		// Token: 0x040029F0 RID: 10736
		Naru,
		// Token: 0x040029F1 RID: 10737
		Effects,
		// Token: 0x040029F2 RID: 10738
		Enemies
	}
}
