using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200097D RID: 2429
public class LeakLocator : MonoBehaviour
{
	// Token: 0x0600353D RID: 13629 RVA: 0x000DF235 File Offset: 0x000DD435
	private void Awake()
	{
		this.m_time = Time.time + this.CheckInterval;
		this.UpdateList();
	}

	// Token: 0x0600353E RID: 13630 RVA: 0x000DF250 File Offset: 0x000DD450
	private void UpdateList()
	{
		this.m_initialListOfObjects.Clear();
		UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(UnityEngine.Object));
		foreach (UnityEngine.Object item in array)
		{
			this.m_initialListOfObjects.Add(item);
		}
	}

	// Token: 0x0600353F RID: 13631 RVA: 0x000DF2A0 File Offset: 0x000DD4A0
	private void FixedUpdate()
	{
		if (this.m_time <= Time.time)
		{
			this.m_time = Time.time + this.CheckInterval;
		}
	}

	// Token: 0x04002FD6 RID: 12246
	public float CheckInterval = 5f;

	// Token: 0x04002FD7 RID: 12247
	private readonly HashSet<UnityEngine.Object> m_initialListOfObjects = new HashSet<UnityEngine.Object>();

	// Token: 0x04002FD8 RID: 12248
	private float m_time;
}
