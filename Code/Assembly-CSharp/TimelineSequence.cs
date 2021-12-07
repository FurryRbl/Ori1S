using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000665 RID: 1637
public class TimelineSequence : BaseAnimator
{
	// Token: 0x060027E5 RID: 10213 RVA: 0x000AD5A4 File Offset: 0x000AB7A4
	public TimelineSequence.SequenceEntry FindEntry<T>()
	{
		foreach (TimelineSequence.SequenceEntry sequenceEntry in this.Entries)
		{
			if (sequenceEntry.Animator && sequenceEntry.Animator.GetType() == typeof(T))
			{
				return sequenceEntry;
			}
		}
		return null;
	}

	// Token: 0x060027E6 RID: 10214 RVA: 0x000AD62C File Offset: 0x000AB82C
	public override void CacheOriginals()
	{
		foreach (TimelineSequence.SequenceEntry sequenceEntry in this.Entries)
		{
			if (sequenceEntry.Animator)
			{
				sequenceEntry.Animator.Initialize();
			}
		}
	}

	// Token: 0x060027E7 RID: 10215 RVA: 0x000AD69C File Offset: 0x000AB89C
	[ContextMenu("Force to play")]
	public void ForceToPlay()
	{
		base.Initialize();
		base.AnimatorDriver.Restart();
	}

	// Token: 0x060027E8 RID: 10216 RVA: 0x000AD6B0 File Offset: 0x000AB8B0
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		for (int i = 0; i < this.Entries.Count; i++)
		{
			TimelineSequence.SequenceEntry sequenceEntry = this.Entries[i];
			if (sequenceEntry.Animator)
			{
				float num = value / sequenceEntry.Speed - sequenceEntry.StartTime / sequenceEntry.Speed;
				if (forceSample)
				{
					sequenceEntry.Animator.SampleValue(num, true);
				}
				else
				{
					float num2 = sequenceEntry.Animator.Duration / sequenceEntry.Speed + 0.02f;
					if ((num >= 0f && num < num2) || sequenceEntry.Animator.IsLooping)
					{
						sequenceEntry.Animator.SampleValue(num, false);
					}
				}
			}
		}
	}

	// Token: 0x1700065A RID: 1626
	// (get) Token: 0x060027E9 RID: 10217 RVA: 0x000AD778 File Offset: 0x000AB978
	public override float Duration
	{
		get
		{
			float num = 0f;
			for (int i = 0; i < this.Entries.Count; i++)
			{
				TimelineSequence.SequenceEntry sequenceEntry = this.Entries[i];
				if (sequenceEntry.Animator)
				{
					num = Mathf.Max(num, sequenceEntry.Animator.Duration / sequenceEntry.Speed + sequenceEntry.StartTime);
				}
			}
			return base.AnimationCurveTimeToTime(num);
		}
	}

	// Token: 0x060027EA RID: 10218 RVA: 0x000AD7EC File Offset: 0x000AB9EC
	public override void RestoreToOriginalState()
	{
		foreach (TimelineSequence.SequenceEntry sequenceEntry in this.Entries)
		{
			if (sequenceEntry.Animator)
			{
				sequenceEntry.Animator.RestoreToOriginalState();
			}
		}
	}

	// Token: 0x060027EB RID: 10219 RVA: 0x000AD85C File Offset: 0x000ABA5C
	[ContextMenu("Sort by start time")]
	public void SortByTime()
	{
		this.Entries.RemoveAll((TimelineSequence.SequenceEntry a) => a.Animator == null);
		this.Entries.Sort((TimelineSequence.SequenceEntry a, TimelineSequence.SequenceEntry b) => a.StartTime.CompareTo(b.StartTime));
	}

	// Token: 0x060027EC RID: 10220 RVA: 0x000AD8BC File Offset: 0x000ABABC
	[ContextMenu("Sort by name")]
	public void SortByName()
	{
		this.Entries.RemoveAll((TimelineSequence.SequenceEntry a) => a.Animator == null);
		this.Entries.Sort((TimelineSequence.SequenceEntry a, TimelineSequence.SequenceEntry b) => (a.Animator.name + a.Animator.GetType().Name).CompareTo(b.Animator.name + b.Animator.GetType().Name));
	}

	// Token: 0x060027ED RID: 10221 RVA: 0x000AD91A File Offset: 0x000ABB1A
	[ContextMenu("Refresh entries")]
	public void RefreshEntries()
	{
	}

	// Token: 0x1700065B RID: 1627
	// (get) Token: 0x060027EE RID: 10222 RVA: 0x000AD91C File Offset: 0x000ABB1C
	public override bool IsLooping
	{
		get
		{
			return this.Loop;
		}
	}

	// Token: 0x0400227A RID: 8826
	public TimelineSequence.SortMode Mode = TimelineSequence.SortMode.Time;

	// Token: 0x0400227B RID: 8827
	public bool ExcludeFromOtherTimelines;

	// Token: 0x0400227C RID: 8828
	public bool Loop;

	// Token: 0x0400227D RID: 8829
	public List<TimelineSequence.SequenceEntry> Entries = new List<TimelineSequence.SequenceEntry>();

	// Token: 0x02000666 RID: 1638
	[Serializable]
	public class SequenceEntry
	{
		// Token: 0x04002282 RID: 8834
		public BaseAnimator Animator;

		// Token: 0x04002283 RID: 8835
		public float StartTime;

		// Token: 0x04002284 RID: 8836
		public float Speed = 1f;

		// Token: 0x04002285 RID: 8837
		public bool External;
	}

	// Token: 0x02000773 RID: 1907
	public enum SortMode
	{
		// Token: 0x04002830 RID: 10288
		None,
		// Token: 0x04002831 RID: 10289
		Name,
		// Token: 0x04002832 RID: 10290
		Time
	}
}
