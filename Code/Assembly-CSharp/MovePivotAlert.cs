using System;
using UnityEngine;

// Token: 0x0200048B RID: 1163
public class MovePivotAlert : MonoBehaviour
{
	// Token: 0x06001FAB RID: 8107 RVA: 0x0008B324 File Offset: 0x00089524
	private void OnDrawGizmos()
	{
		GUIStyle guistyle = new GUIStyle();
		guistyle.fontSize = 26;
		guistyle.normal.textColor = Color.red;
	}
}
