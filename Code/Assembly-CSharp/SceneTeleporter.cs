using System;
using UnityEngine;

// Token: 0x0200071B RID: 1819
[ExecuteInEditMode]
public class SceneTeleporter : MonoBehaviour
{
	// Token: 0x06002B11 RID: 11025 RVA: 0x000B83A5 File Offset: 0x000B65A5
	public static void OnSceneSave()
	{
	}

	// Token: 0x06002B12 RID: 11026 RVA: 0x000B83A8 File Offset: 0x000B65A8
	public void Update()
	{
		if (Application.isPlaying)
		{
			return;
		}
		if (base.transform.position != this.m_previousPosition || this.m_previousName != base.name)
		{
			SceneTeleporter.OnSceneSave();
			this.m_previousPosition = base.transform.position;
			this.m_previousName = base.name;
		}
	}

	// Token: 0x04002661 RID: 9825
	public string Identifier;

	// Token: 0x04002662 RID: 9826
	private Vector3 m_previousPosition;

	// Token: 0x04002663 RID: 9827
	private string m_previousName;
}
