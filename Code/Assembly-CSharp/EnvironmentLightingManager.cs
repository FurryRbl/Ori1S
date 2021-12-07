using System;
using UnityEngine;

// Token: 0x020007F2 RID: 2034
[ExecuteInEditMode]
public class EnvironmentLightingManager : MonoBehaviour
{
	// Token: 0x06002EB4 RID: 11956 RVA: 0x000C5FDC File Offset: 0x000C41DC
	private void OnEnable()
	{
		EnvironmentLightingManager.Instance = this;
		if (!Application.isPlaying)
		{
			foreach (EnvironmentLight environmentLight in UnityEngine.Object.FindObjectsOfType<EnvironmentLight>())
			{
				this.AddLight(environmentLight);
			}
		}
	}

	// Token: 0x06002EB5 RID: 11957 RVA: 0x000C6020 File Offset: 0x000C4220
	public EnvironmentLight GetCharacterLightAtPos(Rect bounds)
	{
		float num = 0f;
		EnvironmentLight environmentLight = null;
		for (int i = 0; i < this.AllLights.Count; i++)
		{
			EnvironmentLight environmentLight2 = this.AllLights[i];
			if (bounds.Overlaps(environmentLight2.Area) && (!(environmentLight != null) || environmentLight2.Area.width * environmentLight2.Area.height <= num))
			{
				num = environmentLight2.Area.width * environmentLight2.Area.height;
				environmentLight = environmentLight2;
			}
		}
		return environmentLight;
	}

	// Token: 0x06002EB6 RID: 11958 RVA: 0x000C60B9 File Offset: 0x000C42B9
	public void AddLight(EnvironmentLight environmentLight)
	{
		if (!this.AllLights.Contains(environmentLight))
		{
			this.AllLights.Add(environmentLight);
		}
	}

	// Token: 0x06002EB7 RID: 11959 RVA: 0x000C60D8 File Offset: 0x000C42D8
	public void RemoveLight(EnvironmentLight environmentLight)
	{
		if (this.AllLights.Contains(environmentLight))
		{
			this.AllLights.Remove(environmentLight);
		}
	}

	// Token: 0x040029E9 RID: 10729
	public static EnvironmentLightingManager Instance;

	// Token: 0x040029EA RID: 10730
	public AllContainer<EnvironmentLight> AllLights = new AllContainer<EnvironmentLight>();
}
