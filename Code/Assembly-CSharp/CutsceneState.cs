using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CE RID: 206
public abstract class CutsceneState : MonoBehaviour
{
	// Token: 0x060008B7 RID: 2231
	public abstract void OnEnter();

	// Token: 0x060008B8 RID: 2232
	public abstract void OnExit();

	// Token: 0x060008B9 RID: 2233 RVA: 0x00025744 File Offset: 0x00023944
	public virtual void OnUpdate()
	{
		foreach (CutsceneTransition cutsceneTransition in this.Transitions)
		{
			if (cutsceneTransition.ShouldTransition())
			{
				this.Parent.ChangeState(cutsceneTransition.NewState);
			}
		}
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x000257B4 File Offset: 0x000239B4
	public void RegisterTransition(CutsceneTransition transition)
	{
		this.Transitions.Add(transition);
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x000257C3 File Offset: 0x000239C3
	public void UnregisterTransition(CutsceneTransition transition)
	{
		this.Transitions.Remove(transition);
	}

	// Token: 0x04000708 RID: 1800
	[HideInInspector]
	public CutsceneController Parent;

	// Token: 0x04000709 RID: 1801
	public HashSet<CutsceneTransition> Transitions = new HashSet<CutsceneTransition>();
}
