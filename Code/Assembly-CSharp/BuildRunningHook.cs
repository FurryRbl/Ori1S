using System;
using System.IO;
using UnityEngine;

// Token: 0x02000747 RID: 1863
public class BuildRunningHook : MonoBehaviour
{
	// Token: 0x06002BBC RID: 11196 RVA: 0x000BB5D0 File Offset: 0x000B97D0
	private void Awake()
	{
		BuildRunningHook.Instance = this;
		this.m_buildRunningHookFilePath = Path.Combine(OutputFolder.BuildOutputPath, this.m_buildRunningHookFileName);
	}

	// Token: 0x06002BBD RID: 11197 RVA: 0x000BB5F0 File Offset: 0x000B97F0
	private void Update()
	{
		if (this.ShouldCheck || Time.time - this.m_timestamp > this.m_interval)
		{
			if (File.Exists(this.m_buildRunningHookFilePath))
			{
				File.Delete(this.m_buildRunningHookFilePath);
			}
			this.m_timestamp = Time.time;
			this.ShouldCheck = false;
		}
	}

	// Token: 0x04002764 RID: 10084
	public static BuildRunningHook Instance;

	// Token: 0x04002765 RID: 10085
	public bool ShouldCheck;

	// Token: 0x04002766 RID: 10086
	private float m_timestamp;

	// Token: 0x04002767 RID: 10087
	private float m_interval = 10f;

	// Token: 0x04002768 RID: 10088
	private string m_buildRunningHookFileName = "buildRunningHook.txt";

	// Token: 0x04002769 RID: 10089
	private string m_buildRunningHookFilePath = string.Empty;
}
