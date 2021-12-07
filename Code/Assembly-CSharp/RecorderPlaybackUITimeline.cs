using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000174 RID: 372
public class RecorderPlaybackUITimeline : MonoBehaviour
{
	// Token: 0x170002BD RID: 701
	// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x000437CA File Offset: 0x000419CA
	public int TotalFrames
	{
		get
		{
			return this.Recorder.RecorderData.Frames.Count;
		}
	}

	// Token: 0x170002BE RID: 702
	// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x000437E1 File Offset: 0x000419E1
	public int CurrentFrame
	{
		get
		{
			return this.Recorder.CurrentFrameIndex;
		}
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x000437F0 File Offset: 0x000419F0
	public int KeyframeBack()
	{
		for (int i = this.Keyframes.Count - 1; i >= 0; i--)
		{
			if (this.Keyframes[i] < this.Recorder.CurrentFrameIndex)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x0004383C File Offset: 0x00041A3C
	public int KeyframeForward()
	{
		for (int i = 0; i < this.Keyframes.Count; i++)
		{
			if (this.Keyframes[i] > this.Recorder.CurrentFrameIndex)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x00043884 File Offset: 0x00041A84
	public int GetKeyframe(int keyframe)
	{
		return this.Keyframes[keyframe];
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x00043894 File Offset: 0x00041A94
	public void Start()
	{
		this.m_background = this.TimelineSkin.FindStyle("Background");
		this.m_bar = this.TimelineSkin.FindStyle("Bar");
		this.m_handle = this.TimelineSkin.FindStyle("Handle");
		this.m_keyframe = this.TimelineSkin.FindStyle("Keyframe");
		this.Keyframes.Clear();
		int num = 0;
		for (int i = 0; i < this.Recorder.RecorderData.Frames.Count; i++)
		{
			RecorderFrame recorderFrame = this.Recorder.RecorderData.Frames[i];
			CheckpointData frameDataOfType = recorderFrame.GetFrameDataOfType<CheckpointData>();
			if (frameDataOfType != null)
			{
				this.Keyframes.Add(num);
			}
			num++;
		}
		RecorderPlaybackUITimeline.Instance = this;
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x00043966 File Offset: 0x00041B66
	public void OnDestory()
	{
		RecorderPlaybackUITimeline.Instance = null;
	}

	// Token: 0x04000BC5 RID: 3013
	public static RecorderPlaybackUITimeline Instance;

	// Token: 0x04000BC6 RID: 3014
	public Recorder Recorder;

	// Token: 0x04000BC7 RID: 3015
	public GUISkin TimelineSkin;

	// Token: 0x04000BC8 RID: 3016
	private GUIStyle m_background;

	// Token: 0x04000BC9 RID: 3017
	private GUIStyle m_bar;

	// Token: 0x04000BCA RID: 3018
	private GUIStyle m_handle;

	// Token: 0x04000BCB RID: 3019
	private GUIStyle m_keyframe;

	// Token: 0x04000BCC RID: 3020
	public List<int> Keyframes;
}
