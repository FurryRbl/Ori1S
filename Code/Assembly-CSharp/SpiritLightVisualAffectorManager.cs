using System;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x020004C0 RID: 1216
[ExecuteInEditMode]
public class SpiritLightVisualAffectorManager : MonoBehaviour
{
	// Token: 0x060020F4 RID: 8436 RVA: 0x00090430 File Offset: 0x0008E630
	public void Update()
	{
		int num = 0;
		SpiritLightRadialVisualAffector spiritLightRadialVisualAffector = null;
		for (SpiritLightPriority spiritLightPriority = SpiritLightPriority.High; spiritLightPriority <= SpiritLightPriority.Low; spiritLightPriority++)
		{
			int num2 = 0;
			while (num2 < SpiritLightRadialVisualAffector.All.Count && (float)num < 20f)
			{
				SpiritLightRadialVisualAffector spiritLightRadialVisualAffector2 = SpiritLightRadialVisualAffector.All[num2];
				Vector4 vector = spiritLightRadialVisualAffector2.Position;
				if (spiritLightRadialVisualAffector2.IsVisibleInCamera(UI.Cameras.Current))
				{
					if (spiritLightRadialVisualAffector2.LightType == SpiritLightType.LightVessel)
					{
						if (!(spiritLightRadialVisualAffector != null) || spiritLightRadialVisualAffector != spiritLightRadialVisualAffector2)
						{
						}
						spiritLightRadialVisualAffector = spiritLightRadialVisualAffector2;
					}
					else if (spiritLightRadialVisualAffector2.LightPriority == spiritLightPriority)
					{
						string propertyName = SpiritLightVisualAffectorManager.s_lightSettingPropertyNames[num];
						Vector4 vec = new Vector4(vector.x, vector.y, spiritLightRadialVisualAffector2.LightRadiusInThisFrame, spiritLightRadialVisualAffector2.LightIntensityInThisFrame);
						Shader.SetGlobalVector(propertyName, vec);
						num++;
					}
				}
				num2++;
			}
		}
		int num3 = num;
		while ((float)num3 < 20f)
		{
			string propertyName2 = SpiritLightVisualAffectorManager.s_lightSettingPropertyNames[num3];
			Vector4 vec2 = new Vector4(1E+18f, 1E+18f, 0f, 1f);
			Shader.SetGlobalVector(propertyName2, vec2);
			num3++;
		}
		string propertyName3 = SpiritLightVisualAffectorManager.s_lightVesselLightPropertyNames[0];
		Vector4 vec3 = new Vector4(1E+18f, 1E+18f, 0f, 1f);
		if (spiritLightRadialVisualAffector != null)
		{
			Vector4 vector2 = spiritLightRadialVisualAffector.Position;
			vec3 = new Vector4(vector2.x, vector2.y, spiritLightRadialVisualAffector.LightRadiusInThisFrame, spiritLightRadialVisualAffector.LightIntensityInThisFrame);
		}
		Shader.SetGlobalVector(propertyName3, vec3);
		num = 0;
		int num4 = 0;
		while (num4 < SpiritLightCapsuleVisualAffector.All.Count && (float)num < 1f)
		{
			SpiritLightCapsuleVisualAffector spiritLightCapsuleVisualAffector = SpiritLightCapsuleVisualAffector.All[num4];
			if (spiritLightCapsuleVisualAffector.IsVisibleInCamera(UI.Cameras.Current))
			{
				string propertyName4 = SpiritLightVisualAffectorManager.s_capsuleLightPropertyNames[num];
				Vector3 startPointPosition = spiritLightCapsuleVisualAffector.StartPointPosition;
				Vector3 endPointPosition = spiritLightCapsuleVisualAffector.EndPointPosition;
				Vector4 vec4 = new Vector4(startPointPosition.x, startPointPosition.y, endPointPosition.x, endPointPosition.y);
				Shader.SetGlobalVector(propertyName4, vec4);
				string propertyName5 = SpiritLightVisualAffectorManager.s_capsuleLightPropertyNames[num + 1];
				Vector4 vec5 = new Vector4(spiritLightCapsuleVisualAffector.LightCapsuleRadiusInThisFrame, spiritLightCapsuleVisualAffector.LightIntensityInThisFrame, 0f, 0f);
				Shader.SetGlobalVector(propertyName5, vec5);
				num++;
			}
			num4++;
		}
		int num5 = num;
		while ((float)num5 < 1f)
		{
			string propertyName6 = SpiritLightVisualAffectorManager.s_capsuleLightPropertyNames[2 * num];
			Vector4 vec6 = new Vector4(1E+18f, 1E+18f, 1E+18f, 2E+18f);
			Shader.SetGlobalVector(propertyName6, vec6);
			string propertyName7 = SpiritLightVisualAffectorManager.s_capsuleLightPropertyNames[2 * num + 1];
			Vector4 vec7 = new Vector4(0f, 1f, 0f, 0f);
			Shader.SetGlobalVector(propertyName7, vec7);
			num5++;
		}
		float num6 = (!Sein.World.Events.DarknessLifted) ? 0f : 1f;
		if (!Application.isPlaying)
		{
			num6 = 0f;
		}
		Shader.SetGlobalFloat("globalLightAffectorOverride", (this.GlobalLightAffectorOverride <= 0f) ? num6 : this.GlobalLightAffectorOverride);
	}

	// Token: 0x04001BE8 RID: 7144
	private const float MAX_NUMBER_OF_CONCURRENT_RADIAL_LIGHTS = 20f;

	// Token: 0x04001BE9 RID: 7145
	private const float MAX_NUMBER_OF_CONCURRENT_CAPSULE_LIGHTS = 1f;

	// Token: 0x04001BEA RID: 7146
	private const string SPIRIT_LIGHT_GLOBAL_OVERRIDE_SHADER_PROPERTY = "globalLightAffectorOverride";

	// Token: 0x04001BEB RID: 7147
	private static string[] s_lightSettingPropertyNames = new string[]
	{
		"spiritLight1Settings",
		"spiritLight2Settings",
		"spiritLight3Settings",
		"spiritLight4Settings",
		"spiritLight5Settings",
		"spiritLight6Settings",
		"spiritLight7Settings",
		"spiritLight8Settings",
		"spiritLight9Settings",
		"spiritLight10Settings",
		"spiritLight11Settings",
		"spiritLight12Settings",
		"spiritLight13Settings",
		"spiritLight14Settings",
		"spiritLight15Settings",
		"spiritLight16Settings",
		"spiritLight17Settings",
		"spiritLight18Settings",
		"spiritLight19Settings",
		"spiritLight20Settings",
		"spiritLight21Settings",
		"spiritLight22Settings",
		"spiritLight23Settings",
		"spiritLight24Settings"
	};

	// Token: 0x04001BEC RID: 7148
	private static string[] s_capsuleLightPropertyNames = new string[]
	{
		"spiritCapsuleLight1Points",
		"spiritCapsuleLight1Settings"
	};

	// Token: 0x04001BED RID: 7149
	private static string[] s_lightVesselLightPropertyNames = new string[]
	{
		"spiritVesselLight1Settings"
	};

	// Token: 0x04001BEE RID: 7150
	public static bool DeactivateLightMechanics = false;

	// Token: 0x04001BEF RID: 7151
	public float GlobalLightAffectorOverride;
}
