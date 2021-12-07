using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000662 RID: 1634
public class MasterTimelineSequence : BaseAnimator
{
	// Token: 0x060027CE RID: 10190 RVA: 0x000ACF20 File Offset: 0x000AB120
	public void OnSkipCutscene()
	{
		this.OnFinish.Perform(null);
		base.AnimatorDriver.Pause();
		if (this.TimelineSequence)
		{
			for (int i = 0; i < this.TimelineSequence.Entries.Count; i++)
			{
				TimelineSequence.SequenceEntry sequenceEntry = this.TimelineSequence.Entries[i];
				if (sequenceEntry.Animator is SoundAnimator)
				{
					((SoundAnimator)sequenceEntry.Animator).enabled = false;
				}
			}
		}
	}

	// Token: 0x060027CF RID: 10191 RVA: 0x000ACFA8 File Offset: 0x000AB1A8
	[ContextMenu("Generate start times")]
	public void GenerateStartTimes()
	{
		float num = 0f;
		foreach (MasterTimelineSequence.SceneSettings sceneSettings in this.Scenes)
		{
			sceneSettings.StartTime = num;
			num += sceneSettings.Duration;
		}
	}

	// Token: 0x060027D0 RID: 10192 RVA: 0x000AD014 File Offset: 0x000AB214
	public MasterTimelineSequence.SceneSettings FindSceneAtTime(float time)
	{
		for (int i = 0; i < this.Scenes.Count; i++)
		{
			MasterTimelineSequence.SceneSettings sceneSettings = this.Scenes[i];
			if (sceneSettings.StartTime <= time && sceneSettings.EndTime > time)
			{
				return sceneSettings;
			}
		}
		return null;
	}

	// Token: 0x060027D1 RID: 10193 RVA: 0x000AD068 File Offset: 0x000AB268
	public MasterTimelineSequence.SceneSettings FindSceneToPreloadAtTime(float time)
	{
		for (int i = 0; i < this.Scenes.Count; i++)
		{
			MasterTimelineSequence.SceneSettings sceneSettings = this.Scenes[i];
			if (!this.m_preloadedScenes.Contains(sceneSettings))
			{
				if (sceneSettings.StartTime - sceneSettings.PreloadTime < time && sceneSettings.StartTime > time)
				{
					return sceneSettings;
				}
			}
		}
		return null;
	}

	// Token: 0x17000656 RID: 1622
	// (get) Token: 0x060027D2 RID: 10194 RVA: 0x000AD0D6 File Offset: 0x000AB2D6
	public override bool IsLooping
	{
		get
		{
			return false;
		}
	}

	// Token: 0x060027D3 RID: 10195 RVA: 0x000AD0D9 File Offset: 0x000AB2D9
	public override void CacheOriginals()
	{
		FrameCounter.Count = 0;
	}

	// Token: 0x060027D4 RID: 10196 RVA: 0x000AD0E1 File Offset: 0x000AB2E1
	public void OnLoadedScene()
	{
		this.SampleValue(this.m_time, true);
	}

	// Token: 0x060027D5 RID: 10197 RVA: 0x000AD0F0 File Offset: 0x000AB2F0
	public new void Start()
	{
		base.Start();
		this.m_lastRealTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060027D6 RID: 10198 RVA: 0x000AD104 File Offset: 0x000AB304
	public void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (!this.IsSuspended && this.m_playing)
		{
			float num = realtimeSinceStartup - this.m_lastRealTime;
			this.m_realTime += num;
		}
		this.m_lastRealTime = realtimeSinceStartup;
	}

	// Token: 0x060027D7 RID: 10199 RVA: 0x000AD14C File Offset: 0x000AB34C
	public override void SampleValue(float value, bool forceSample)
	{
		if (value == 0f)
		{
			this.m_playing = false;
		}
		if (value > 0f && !this.m_playing)
		{
			this.m_realTime = base.AnimatorDriver.CurrentTime;
		}
		this.m_time = value;
		if (!this.m_hasRunBeforeFinishAction && this.BeforeFinishAction && value >= this.Duration - this.BeforeFinishActionTimeOffset)
		{
			this.m_hasRunBeforeFinishAction = true;
			this.BeforeFinishAction.Perform(null);
		}
		if (!this.m_hasFinished && value >= this.Duration)
		{
			this.m_hasFinished = true;
			this.OnFinish.Perform(null);
		}
		MasterTimelineSequence.SceneSettings sceneSettings = this.FindSceneAtTime(value);
		if (sceneSettings == null)
		{
			return;
		}
		MasterTimelineSequence.SceneSettings sceneSettings2 = this.FindSceneToPreloadAtTime(value);
		if (sceneSettings != this.m_currentScene)
		{
			if (!Core.Scenes.Manager.SceneIsEnabled(sceneSettings.SceneMetaData))
			{
				float num = this.m_realTime - base.AnimatorDriver.CurrentTime;
				num = Mathf.Clamp(num, 0f, 2f);
				base.AnimatorDriver.CurrentTime += num;
				this.m_time = this.m_realTime;
				value += num;
				if (sceneSettings.CrossfadeDuration > 0f)
				{
					this.m_crossFadingFromScene = this.m_currentScene;
					this.m_crossFadePadding = sceneSettings.CrossfadeDuration;
					this.m_crossFadeTimeOffset = num;
					UI.Cameras.System.CrossFadeManager.PerformCrossFade(sceneSettings.SceneMetaData, sceneSettings.CrossfadeDuration);
				}
				else
				{
					GoToSceneController.Instance.GoToSceneImmediately(sceneSettings.SceneMetaData, new Action(this.OnLoadedScene));
				}
			}
			this.m_currentScene = sceneSettings;
			forceSample = true;
		}
		if (sceneSettings2 != null)
		{
			this.m_preloadedScenes.Add(sceneSettings2);
			Core.Scenes.Manager.PreloadScene(sceneSettings2.SceneMetaData);
		}
		if (this.m_currentScene != null)
		{
			BaseAnimator animator = this.m_currentScene.Animator;
			if (animator)
			{
				float value2 = value - this.m_currentScene.StartTime;
				animator.Initialize();
				animator.SampleValue(value2, forceSample);
			}
		}
		if (this.m_crossFadingFromScene != null)
		{
			BaseAnimator animator2 = this.m_crossFadingFromScene.Animator;
			if (animator2)
			{
				float value3 = value - this.m_crossFadingFromScene.StartTime - this.m_crossFadeTimeOffset;
				animator2.Initialize();
				animator2.SampleValue(value3, forceSample);
			}
			if (value > this.m_crossFadingFromScene.StartTime + this.m_crossFadingFromScene.Duration + this.m_crossFadePadding)
			{
				this.m_crossFadingFromScene = null;
			}
		}
		this.TimelineSequence.SampleValue(value, forceSample);
		if (value > 0f && !this.m_playing)
		{
			this.m_playing = true;
			this.m_realTime = 0f;
			this.m_lastRealTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x17000657 RID: 1623
	// (get) Token: 0x060027D8 RID: 10200 RVA: 0x000AD414 File Offset: 0x000AB614
	public override float Duration
	{
		get
		{
			float num = 0f;
			for (int i = 0; i < this.Scenes.Count; i++)
			{
				if (this.Scenes[i].EndTime > num)
				{
					num = this.Scenes[i].EndTime;
				}
			}
			return this.TimeOffset + num;
		}
	}

	// Token: 0x060027D9 RID: 10201 RVA: 0x000AD474 File Offset: 0x000AB674
	public override void RestoreToOriginalState()
	{
	}

	// Token: 0x060027DA RID: 10202 RVA: 0x000AD476 File Offset: 0x000AB676
	public int FindEntryIndex(MasterTimelineSequence.SceneSettings entry)
	{
		return this.Scenes.IndexOf(entry);
	}

	// Token: 0x04002263 RID: 8803
	public List<MasterTimelineSequence.SceneSettings> Scenes = new List<MasterTimelineSequence.SceneSettings>();

	// Token: 0x04002264 RID: 8804
	public TimelineSequence TimelineSequence;

	// Token: 0x04002265 RID: 8805
	public ActionSequence OnFinish;

	// Token: 0x04002266 RID: 8806
	public float BeforeFinishActionTimeOffset;

	// Token: 0x04002267 RID: 8807
	public ActionSequence BeforeFinishAction;

	// Token: 0x04002268 RID: 8808
	private float m_time;

	// Token: 0x04002269 RID: 8809
	private MasterTimelineSequence.SceneSettings m_crossFadingFromScene;

	// Token: 0x0400226A RID: 8810
	private float m_crossFadePadding;

	// Token: 0x0400226B RID: 8811
	private bool m_playing;

	// Token: 0x0400226C RID: 8812
	private float m_realTime;

	// Token: 0x0400226D RID: 8813
	private float m_lastRealTime;

	// Token: 0x0400226E RID: 8814
	private float m_crossFadeTimeOffset;

	// Token: 0x0400226F RID: 8815
	private MasterTimelineSequence.SceneSettings m_currentScene;

	// Token: 0x04002270 RID: 8816
	private HashSet<MasterTimelineSequence.SceneSettings> m_preloadedScenes = new HashSet<MasterTimelineSequence.SceneSettings>();

	// Token: 0x04002271 RID: 8817
	private bool m_hasFinished;

	// Token: 0x04002272 RID: 8818
	private bool m_hasRunBeforeFinishAction;

	// Token: 0x02000663 RID: 1635
	[Serializable]
	public class SceneSettings
	{
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x000AD497 File Offset: 0x000AB697
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x000AD4A6 File Offset: 0x000AB6A6
		public float EndTime
		{
			get
			{
				return this.StartTime + this.Duration;
			}
			set
			{
				this.Duration = value - this.StartTime;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x000AD4B8 File Offset: 0x000AB6B8
		public BaseAnimator Animator
		{
			get
			{
				if (this.m_animator == null)
				{
					for (int i = 0; i < TimelineSequenceLabel.All.Count; i++)
					{
						TimelineSequenceLabel timelineSequenceLabel = TimelineSequenceLabel.All[i];
						SceneRoot sceneRoot = SceneRoot.FindFromTransform(timelineSequenceLabel.transform);
						if (sceneRoot && sceneRoot.MetaData == this.SceneMetaData)
						{
							this.m_animator = timelineSequenceLabel.GetComponent<TimelineSequence>();
						}
					}
				}
				return this.m_animator;
			}
		}

		// Token: 0x04002273 RID: 8819
		public SceneMetaData SceneMetaData;

		// Token: 0x04002274 RID: 8820
		public float StartTime;

		// Token: 0x04002275 RID: 8821
		public float Duration;

		// Token: 0x04002276 RID: 8822
		public float CrossfadeDuration;

		// Token: 0x04002277 RID: 8823
		private BaseAnimator m_animator;

		// Token: 0x04002278 RID: 8824
		public float PreloadTime = 1f;
	}
}
