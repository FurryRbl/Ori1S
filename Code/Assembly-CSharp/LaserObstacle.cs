using System;
using Core;
using UnityEngine;

// Token: 0x02000908 RID: 2312
public class LaserObstacle : MonoBehaviour
{
	// Token: 0x06003354 RID: 13140 RVA: 0x000D83B8 File Offset: 0x000D65B8
	private void Start()
	{
		if (this.AnticipationCurve.keys[this.AnticipationCurve.length - 1].time != this.BeamTransparancyCurve.keys[this.BeamTransparancyCurve.length - 1].time)
		{
		}
		this.m_curveDuration = this.AnticipationCurve.keys[this.AnticipationCurve.length - 1].time;
		if (this.AnticipationCurve.postWrapMode == WrapMode.PingPong)
		{
			this.m_curveDuration *= 2f;
		}
		for (float num = 0f; num < this.m_curveDuration; num += 0.01f)
		{
			if (this.AnticipationCurve.Evaluate(num) != 0f)
			{
				this.m_anticipationSoundOffset = num;
				this.m_anticipationCountdown = this.m_anticipationSoundOffset + this.Offset;
				break;
			}
		}
		for (float num2 = 0f; num2 < this.m_curveDuration; num2 += 0.01f)
		{
			if (this.BeamTransparancyCurve.Evaluate(num2) != 0f)
			{
				this.m_beamStartOffset = num2 - this.m_anticipationSoundOffset;
				break;
			}
		}
		for (float num3 = this.m_anticipationSoundOffset + this.m_beamStartOffset; num3 < this.m_curveDuration; num3 += 0.01f)
		{
			if (this.BeamTransparancyCurve.Evaluate(num3) != 0f)
			{
				this.m_beamDuration = num3 - this.m_anticipationSoundOffset + this.m_beamStartOffset;
				break;
			}
		}
		Keyframe[] array = new Keyframe[this.BeamTransparancyCurve.keys.Length];
		int num4 = 0;
		foreach (Keyframe keyframe in this.BeamTransparancyCurve.keys)
		{
			array[num4] = new Keyframe(keyframe.time + this.Offset, keyframe.value, keyframe.inTangent, keyframe.outTangent);
			num4++;
		}
		this.BeamTransparancyCurve.keys = array;
		Keyframe[] array2 = new Keyframe[this.AnticipationCurve.keys.Length];
		int num5 = 0;
		foreach (Keyframe keyframe2 in this.AnticipationCurve.keys)
		{
			array2[num5] = new Keyframe(keyframe2.time + this.Offset, keyframe2.value, keyframe2.inTangent, keyframe2.outTangent);
			num5++;
		}
		this.AnticipationCurve.keys = array2;
		foreach (LegacyAnimator legacyAnimator in this.Beam.GetComponentsInChildren<LegacyAnimator>())
		{
			legacyAnimator.SetAnimationCurve(this.BeamTransparancyCurve);
		}
		foreach (LegacyAnimator legacyAnimator2 in this.Impact.GetComponentsInChildren<LegacyAnimator>())
		{
			legacyAnimator2.SetAnimationCurve(this.BeamTransparancyCurve);
		}
		foreach (LegacyAnimator legacyAnimator3 in this.Anticipation.GetComponentsInChildren<LegacyAnimator>())
		{
			legacyAnimator3.SetAnimationCurve(this.AnticipationCurve);
		}
	}

	// Token: 0x06003355 RID: 13141 RVA: 0x000D8724 File Offset: 0x000D6924
	private void FixedUpdate()
	{
		this.m_time += Time.fixedDeltaTime;
		this.m_anticipationCountdown -= Time.fixedDeltaTime;
		if (this.m_anticipationCountdown <= 0f)
		{
			Sound.Play(this.AnticipationSoundProvider.GetSound(null), base.transform.position, null);
			this.m_anticipationCountdown = 1000f;
			this.m_beamStartCountdown = this.m_beamStartOffset;
		}
		else if (this.m_beamStartCountdown <= this.m_beamStartOffset)
		{
			this.m_beamStartCountdown -= Time.fixedDeltaTime;
			if (this.m_beamStartCountdown <= 0f)
			{
				Sound.Play(this.StartSoundProvider.GetSound(null), base.transform.position, null);
				this.m_beamStartCountdown = 1000f;
				this.m_beamOnCountdown = this.m_beamDuration;
			}
		}
		else if (this.m_beamOnCountdown <= this.m_beamDuration)
		{
			this.m_beamOnCountdown -= Time.fixedDeltaTime;
			if (this.m_lastLoop == null)
			{
				this.m_lastLoop = Sound.Play(this.LoopSoundProvider.GetSound(null), base.transform.position, delegate()
				{
					this.m_lastLoop = null;
				});
			}
			if (this.m_lastImpactLoop == null)
			{
				this.m_lastImpactLoop = Sound.Play(this.ImpactSoundProvider.GetSound(null), this.Impact.transform.position, delegate()
				{
					this.m_lastImpactLoop = null;
				});
			}
			if (this.m_beamOnCountdown <= 0f)
			{
				Sound.Play(this.EndSoundProvider.GetSound(null), base.transform.position, null);
				this.m_lastLoop.FadeOut(0.2f, true);
				this.m_lastImpactLoop.FadeOut(0.2f, true);
				this.m_beamOnCountdown = 1000f;
			}
		}
		if (this.m_time >= this.m_curveDuration + this.Offset)
		{
			this.m_time = this.Offset;
			this.m_anticipationCountdown = this.m_anticipationSoundOffset;
		}
	}

	// Token: 0x04002E4C RID: 11852
	public AnimationCurve BeamTransparancyCurve;

	// Token: 0x04002E4D RID: 11853
	public AnimationCurve AnticipationCurve;

	// Token: 0x04002E4E RID: 11854
	public GameObject Beam;

	// Token: 0x04002E4F RID: 11855
	public GameObject Impact;

	// Token: 0x04002E50 RID: 11856
	public GameObject Anticipation;

	// Token: 0x04002E51 RID: 11857
	public float Offset;

	// Token: 0x04002E52 RID: 11858
	public Varying2DSoundProvider AnticipationSoundProvider;

	// Token: 0x04002E53 RID: 11859
	public Varying2DSoundProvider StartSoundProvider;

	// Token: 0x04002E54 RID: 11860
	public Varying2DSoundProvider EndSoundProvider;

	// Token: 0x04002E55 RID: 11861
	public Varying2DSoundProvider LoopSoundProvider;

	// Token: 0x04002E56 RID: 11862
	public Varying2DSoundProvider ImpactSoundProvider;

	// Token: 0x04002E57 RID: 11863
	private SoundPlayer m_lastLoop;

	// Token: 0x04002E58 RID: 11864
	private SoundPlayer m_lastImpactLoop;

	// Token: 0x04002E59 RID: 11865
	private float m_anticipationSoundOffset;

	// Token: 0x04002E5A RID: 11866
	private float m_curveDuration;

	// Token: 0x04002E5B RID: 11867
	private float m_time;

	// Token: 0x04002E5C RID: 11868
	private float m_anticipationCountdown;

	// Token: 0x04002E5D RID: 11869
	private float m_beamStartCountdown;

	// Token: 0x04002E5E RID: 11870
	private float m_beamStartOffset;

	// Token: 0x04002E5F RID: 11871
	private float m_beamDuration;

	// Token: 0x04002E60 RID: 11872
	private float m_beamOnCountdown;
}
