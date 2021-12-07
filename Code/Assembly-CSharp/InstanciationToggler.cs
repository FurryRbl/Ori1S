using System;
using UnityEngine;

// Token: 0x020004CF RID: 1231
public class InstanciationToggler : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x170005A8 RID: 1448
	// (get) Token: 0x0600214E RID: 8526 RVA: 0x00091F56 File Offset: 0x00090156
	public string Name
	{
		get
		{
			return "Instanciation profile";
		}
	}

	// Token: 0x170005A9 RID: 1449
	// (get) Token: 0x0600214F RID: 8527 RVA: 0x00091F5D File Offset: 0x0009015D
	public string HelpText
	{
		get
		{
			return "Toggle differnt Instanciation Options";
		}
	}

	// Token: 0x170005AA RID: 1450
	// (get) Token: 0x06002150 RID: 8528 RVA: 0x00091F64 File Offset: 0x00090164
	public string[] ToggleOptions
	{
		get
		{
			return new string[]
			{
				"Instanciation Profile Off",
				"Instanciation Profile On"
			};
		}
	}

	// Token: 0x170005AB RID: 1451
	// (get) Token: 0x06002151 RID: 8529 RVA: 0x00091F7C File Offset: 0x0009017C
	// (set) Token: 0x06002152 RID: 8530 RVA: 0x00091F90 File Offset: 0x00090190
	public int CurrentToggleOptionId
	{
		get
		{
			return (!this.m_doProfile) ? 0 : 1;
		}
		set
		{
			this.m_doProfile = !this.m_doProfile;
			InstantiateUtility.ProfileInstantiate = this.m_doProfile;
		}
	}

	// Token: 0x04001C29 RID: 7209
	private bool m_doProfile;
}
