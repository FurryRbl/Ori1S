using System;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class ActionSequenceSerializer : SaveSerialize
{
	// Token: 0x060019B7 RID: 6583 RVA: 0x0006E214 File Offset: 0x0006C414
	public void OnValidate()
	{
		this.m_actionSequence = base.GetComponent<ActionSequence>();
	}

	// Token: 0x060019B8 RID: 6584 RVA: 0x0006E224 File Offset: 0x0006C424
	public override void Serialize(Archive ar)
	{
		this.m_actionSequence.Index = ar.Serialize(this.m_actionSequence.Index);
		this.m_actionSequence.IsRunning = ar.Serialize(this.m_actionSequence.IsRunning);
	}

	// Token: 0x04001610 RID: 5648
	[HideInInspector]
	[SerializeField]
	private ActionSequence m_actionSequence;
}
