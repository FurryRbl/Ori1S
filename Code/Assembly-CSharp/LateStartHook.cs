using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class LateStartHook : MonoBehaviour
{
	// Token: 0x06000AB2 RID: 2738 RVA: 0x0002EA18 File Offset: 0x0002CC18
	public static void AddLateStartMethod(Action method)
	{
		LateStartHook.Actions.Add(method);
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0002EA28 File Offset: 0x0002CC28
	private void Start()
	{
		for (int i = 0; i < LateStartHook.Actions.Count; i++)
		{
			Action action = LateStartHook.Actions[i];
			action();
		}
		LateStartHook.Actions.Clear();
		LateStartHook.m_poolsToDo.Clear();
		UnityEngine.Object.DestroyObject(base.gameObject);
	}

	// Token: 0x040008C2 RID: 2242
	public static List<Action> Actions = new List<Action>();

	// Token: 0x040008C3 RID: 2243
	private static List<GameObject> m_poolsToDo = new List<GameObject>();
}
