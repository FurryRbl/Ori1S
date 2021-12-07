using System;
using UnityEngine;

// Token: 0x0200094E RID: 2382
public class TimeCounterDisplay : MonoBehaviour
{
	// Token: 0x06003476 RID: 13430 RVA: 0x000DC564 File Offset: 0x000DA764
	public void Update()
	{
		this.m_delay -= Time.deltaTime;
		if (this.m_delay < 0f)
		{
			this.m_delay = 1f;
			base.GetComponent<GUIText>().text = GameController.Instance.Timer.DisplayTimeAsString;
		}
	}

	// Token: 0x04002F56 RID: 12118
	private float m_delay;
}
