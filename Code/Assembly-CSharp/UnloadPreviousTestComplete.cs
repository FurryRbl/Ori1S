using System;
using UnityEngine;

// Token: 0x02000756 RID: 1878
public class UnloadPreviousTestComplete : MonoBehaviour
{
	// Token: 0x06002BED RID: 11245 RVA: 0x000BC5E0 File Offset: 0x000BA7E0
	private void Start()
	{
		Application.LoadLevel(UnloadPreviousTestComplete.LevelToLoad);
	}

	// Token: 0x040027AB RID: 10155
	public static string LevelToLoad = string.Empty;
}
