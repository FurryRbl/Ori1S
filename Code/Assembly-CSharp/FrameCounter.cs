using System;
using UnityEngine;

// Token: 0x02000497 RID: 1175
[RequireComponent(typeof(GUIText))]
public class FrameCounter : MonoBehaviour
{
	// Token: 0x06001FD3 RID: 8147 RVA: 0x0008BCC6 File Offset: 0x00089EC6
	private void Start()
	{
		base.GetComponent<GUIText>().text = "0";
	}

	// Token: 0x06001FD4 RID: 8148 RVA: 0x0008BCD8 File Offset: 0x00089ED8
	private void FixedUpdate()
	{
		FrameCounter.Count++;
	}

	// Token: 0x06001FD5 RID: 8149 RVA: 0x0008BCE6 File Offset: 0x00089EE6
	private void Update()
	{
		base.GetComponent<GUIText>().text = FrameCounter.Count.ToString();
	}

	// Token: 0x04001B70 RID: 7024
	public static int Count;
}
