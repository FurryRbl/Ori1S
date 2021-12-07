using System;
using UnityEngine;

// Token: 0x02000980 RID: 2432
public class LimitFramesPerSecond : MonoBehaviour
{
	// Token: 0x06003544 RID: 13636 RVA: 0x000DF35F File Offset: 0x000DD55F
	private void LateUpdate()
	{
		while (this.m_time > Time.realtimeSinceStartup)
		{
		}
		this.m_time = Time.realtimeSinceStartup + 1f / this.FramesPerSecond;
	}

	// Token: 0x04002FDE RID: 12254
	public float FramesPerSecond = 60f;

	// Token: 0x04002FDF RID: 12255
	private float m_time;
}
