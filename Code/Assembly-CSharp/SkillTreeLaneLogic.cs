using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class SkillTreeLaneLogic : SaveSerialize
{
	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x0600108E RID: 4238 RVA: 0x0004B8A7 File Offset: 0x00049AA7
	public float Index
	{
		get
		{
			return this.m_index;
		}
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x0004B8B0 File Offset: 0x00049AB0
	public void OnEnable()
	{
		this.UpdateItems(true);
		foreach (SkillItem skillItem in this.Skills)
		{
			skillItem.LargeIconColor = this.LargeIconColor;
		}
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0004B918 File Offset: 0x00049B18
	public void FixedUpdate()
	{
		this.UpdateItems(false);
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x0004B924 File Offset: 0x00049B24
	public void UpdateItems(bool instant)
	{
		int i;
		for (i = 0; i < this.Skills.Count; i++)
		{
			SkillItem skillItem = this.Skills[i];
			if (!skillItem.HasSkillItem)
			{
				break;
			}
		}
		this.m_index = ((!instant) ? Mathf.MoveTowards(this.m_index, (float)i, Time.deltaTime * 3f) : ((float)i));
		this.SkillEarntAnimator.Initialize();
		this.SkillEarntAnimator.SampleValue(this.m_index, true);
		if (!this.m_laneAchievedAwarded && this.HasAllSkills)
		{
			SkillTreeLaneLogic.OnSkillTreeDoneEvent(this.Type);
			this.m_laneAchievedAwarded = true;
		}
	}

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x06001092 RID: 4242 RVA: 0x0004B9E0 File Offset: 0x00049BE0
	public bool HasAllSkills
	{
		get
		{
			bool result = true;
			for (int i = 0; i < this.Skills.Count; i++)
			{
				if (!this.Skills[i].HasSkillItem)
				{
					result = false;
					break;
				}
			}
			return result;
		}
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x0004BA29 File Offset: 0x00049C29
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_laneAchievedAwarded);
	}

	// Token: 0x04000DFE RID: 3582
	public BaseAnimator SkillEarntAnimator;

	// Token: 0x04000DFF RID: 3583
	public List<SkillItem> Skills = new List<SkillItem>();

	// Token: 0x04000E00 RID: 3584
	private float m_index;

	// Token: 0x04000E01 RID: 3585
	public Color LargeIconColor;

	// Token: 0x04000E02 RID: 3586
	public SkillTreeLaneLogic.SkillTreeType Type;

	// Token: 0x04000E03 RID: 3587
	private bool m_laneAchievedAwarded;

	// Token: 0x04000E04 RID: 3588
	public static Action<SkillTreeLaneLogic.SkillTreeType> OnSkillTreeDoneEvent = delegate(SkillTreeLaneLogic.SkillTreeType A_0)
	{
	};

	// Token: 0x020001BF RID: 447
	public enum SkillTreeType
	{
		// Token: 0x04000E07 RID: 3591
		Energy,
		// Token: 0x04000E08 RID: 3592
		Utility,
		// Token: 0x04000E09 RID: 3593
		Combat
	}
}
