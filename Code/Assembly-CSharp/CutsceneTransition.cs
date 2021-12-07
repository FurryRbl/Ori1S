using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
[RequireComponent(typeof(CutsceneState))]
public abstract class CutsceneTransition : MonoBehaviour
{
	// Token: 0x060008AC RID: 2220
	public abstract bool ShouldTransition();

	// Token: 0x060008AD RID: 2221 RVA: 0x0002558F File Offset: 0x0002378F
	public void Awake()
	{
		this.m_parentState = base.GetComponent<CutsceneState>();
		this.m_parentState.RegisterTransition(this);
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x000255A9 File Offset: 0x000237A9
	public void Start()
	{
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x000255AB File Offset: 0x000237AB
	public void OnDestroy()
	{
		this.m_parentState.RegisterTransition(this);
	}

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x060008B0 RID: 2224 RVA: 0x000255B9 File Offset: 0x000237B9
	public CutsceneState ParentState
	{
		get
		{
			return this.m_parentState;
		}
	}

	// Token: 0x040006F0 RID: 1776
	public CutsceneState NewState;

	// Token: 0x040006F1 RID: 1777
	private CutsceneState m_parentState;
}
