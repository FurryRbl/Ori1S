using System;
using UnityEngine;

// Token: 0x0200093E RID: 2366
[RequireComponent(typeof(GUIText))]
public class MemoryCounter : MonoBehaviour
{
	// Token: 0x06003441 RID: 13377 RVA: 0x000DBAC8 File Offset: 0x000D9CC8
	private void FixedUpdate()
	{
		if (Time.realtimeSinceStartup - this.m_lastCheckTime < 1f)
		{
			return;
		}
		this.m_lastCheckTime = Time.realtimeSinceStartup;
		this.m_monoUsed = Profiler.GetMonoUsedSize() / 1024U / 1024U;
		this.m_monoHeap = Profiler.GetMonoHeapSize() / 1024U / 1024U;
		base.GetComponent<GUIText>().text = "Mono Used: " + this.m_monoUsed + "MB\n";
		GUIText component = base.GetComponent<GUIText>();
		string text = component.text;
		component.text = string.Concat(new object[]
		{
			text,
			"Mono Heap: ",
			this.m_monoHeap,
			"MB\n"
		});
		GUIText component2 = base.GetComponent<GUIText>();
		text = component2.text;
		component2.text = string.Concat(new object[]
		{
			text,
			Screen.width,
			"x",
			Screen.height
		});
		if (this.m_monoHeap > 430f)
		{
			base.GetComponent<GUIText>().fontSize = 20;
			base.GetComponent<GUIText>().fontStyle = FontStyle.Bold;
		}
		else
		{
			base.GetComponent<GUIText>().fontSize = 0;
			base.GetComponent<GUIText>().fontStyle = FontStyle.Normal;
		}
	}

	// Token: 0x04002F30 RID: 12080
	private float m_lastCheckTime;

	// Token: 0x04002F31 RID: 12081
	private float m_monoUsed;

	// Token: 0x04002F32 RID: 12082
	private float m_monoHeap;
}
