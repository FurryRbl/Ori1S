using System;
using Game;
using UnityEngine;

// Token: 0x020008DF RID: 2271
public class DoorSlot : MonoBehaviour
{
	// Token: 0x060032A8 RID: 12968 RVA: 0x000D5C40 File Offset: 0x000D3E40
	public void Awake()
	{
		this.m_animators = base.GetComponents<LegacyAnimator>();
		Events.Scheduler.OnGameSerializeLoad.Add(new Action(this.OnGameSerializeLoad));
	}

	// Token: 0x060032A9 RID: 12969 RVA: 0x000D5C69 File Offset: 0x000D3E69
	public void OnDestroy()
	{
		Events.Scheduler.OnGameSerializeLoad.Remove(new Action(this.OnGameSerializeLoad));
	}

	// Token: 0x060032AA RID: 12970 RVA: 0x000D5C88 File Offset: 0x000D3E88
	public void OnGameSerializeLoad()
	{
		if (this.Door.CurrentState != DoorWithSlots.State.Opened)
		{
			if (this.Door.NumberOfOrbsUsed > this.Index)
			{
				this.Activated = true;
				foreach (LegacyAnimator legacyAnimator in this.m_animators)
				{
					legacyAnimator.StopAndSampleAtEnd();
				}
			}
			else
			{
				this.Activated = false;
				foreach (LegacyAnimator legacyAnimator2 in this.m_animators)
				{
					legacyAnimator2.StopAndSampleAtStart();
				}
			}
		}
		else
		{
			this.Activated = true;
			foreach (LegacyAnimator legacyAnimator3 in this.m_animators)
			{
				legacyAnimator3.StopAndSampleAtStart();
			}
		}
	}

	// Token: 0x060032AB RID: 12971 RVA: 0x000D5D5C File Offset: 0x000D3F5C
	public void FixedUpdate()
	{
		if (!this.Activated && this.Door.NumberOfOrbsUsed > this.Index)
		{
			this.Activated = true;
			foreach (LegacyAnimator legacyAnimator in this.m_animators)
			{
				legacyAnimator.Restart();
			}
		}
		if (this.Activated && this.Door.NumberOfOrbsUsed <= this.Index)
		{
			this.Activated = false;
			foreach (LegacyAnimator legacyAnimator2 in this.m_animators)
			{
				legacyAnimator2.RestartReverse();
			}
		}
	}

	// Token: 0x04002D8D RID: 11661
	public int Index;

	// Token: 0x04002D8E RID: 11662
	public DoorWithSlots Door;

	// Token: 0x04002D8F RID: 11663
	public bool Activated;

	// Token: 0x04002D90 RID: 11664
	private LegacyAnimator[] m_animators;
}
