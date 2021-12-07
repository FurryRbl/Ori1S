using System;
using UnityEngine;

// Token: 0x0200086A RID: 2154
public class UWPIdentityUI : MonoBehaviour
{
	// Token: 0x060030A1 RID: 12449 RVA: 0x000CEBB2 File Offset: 0x000CCDB2
	public void OnEnable()
	{
		UWPIdentityUI.IsVisible = true;
	}

	// Token: 0x060030A2 RID: 12450 RVA: 0x000CEBBA File Offset: 0x000CCDBA
	public void OnDisable()
	{
		UWPIdentityUI.IsVisible = false;
	}

	// Token: 0x060030A3 RID: 12451 RVA: 0x000CEBC4 File Offset: 0x000CCDC4
	public void Update()
	{
		this.Group.SetActive(false);
	}

	// Token: 0x04002BE7 RID: 11239
	public static bool IsVisible;

	// Token: 0x04002BE8 RID: 11240
	[NotNull]
	public MessageBox Username;

	// Token: 0x04002BE9 RID: 11241
	[NotNull]
	public GameObject Group;

	// Token: 0x04002BEA RID: 11242
	private string m_username;
}
