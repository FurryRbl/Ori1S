using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x020006B9 RID: 1721
public class SoundCompositionPlayer : MonoBehaviour
{
	// Token: 0x0600295C RID: 10588 RVA: 0x000B26F6 File Offset: 0x000B08F6
	public void Awake()
	{
		this.SetSoundComposition(this.SoundComposition);
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x0600295D RID: 10589 RVA: 0x000B2720 File Offset: 0x000B0920
	public void OnGameReset()
	{
		for (int i = 0; i < this.Loops.Count; i++)
		{
			SoundPlayer soundPlayer = this.Loops[i];
			if (soundPlayer)
			{
				InstantiateUtility.Destroy(soundPlayer.gameObject);
			}
		}
		for (int j = 0; j < this.Layers.Count; j++)
		{
			SoundPlayer soundPlayer2 = this.Layers[j];
			if (soundPlayer2)
			{
				InstantiateUtility.Destroy(soundPlayer2.gameObject);
			}
		}
		this.Loops.Clear();
		this.Layers.Clear();
	}

	// Token: 0x0600295E RID: 10590 RVA: 0x000B27C4 File Offset: 0x000B09C4
	public void OnDestroy()
	{
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		for (int i = 0; i < this.Loops.Count; i++)
		{
			SoundPlayer soundPlayer = this.Loops[i];
			if (soundPlayer)
			{
				InstantiateUtility.Destroy(soundPlayer.gameObject);
			}
		}
		for (int j = 0; j < this.Layers.Count; j++)
		{
			SoundPlayer soundPlayer2 = this.Layers[j];
			if (soundPlayer2)
			{
				InstantiateUtility.Destroy(soundPlayer2.gameObject);
			}
		}
		this.Loops.Clear();
		this.Layers.Clear();
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x000B2880 File Offset: 0x000B0A80
	public void SetSoundComposition(global::SoundComposition soundComposition)
	{
		this.SoundComposition = soundComposition;
		this.Loops.Clear();
		this.Layers.Clear();
		if (!this.SoundComposition)
		{
			return;
		}
		for (int i = 0; i < this.SoundComposition.Loops.Count; i++)
		{
			global::SoundComposition.SoundLoop soundLoop = this.SoundComposition.Loops[i];
			if (soundLoop != null)
			{
				this.Loops.Add(null);
			}
		}
		for (int j = 0; j < this.SoundComposition.Layers.Count; j++)
		{
			global::SoundComposition.SoundLayer soundLayer = this.SoundComposition.Layers[j];
			if (soundLayer != null)
			{
				this.Layers.Add(null);
			}
		}
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x000B2948 File Offset: 0x000B0B48
	public void Play()
	{
		for (int i = 0; i < this.SoundComposition.Loops.Count; i++)
		{
			global::SoundComposition.SoundLoop soundLoop = this.SoundComposition.Loops[i];
			SoundDescriptor soundDescriptor = new SoundDescriptor(soundLoop.Sound, 1f);
			soundDescriptor.SoundSize.Radius = 0f;
			soundDescriptor.SoundSize.FalloffMargin = 0f;
			soundDescriptor.ShouldBePanned = false;
			soundDescriptor.MixerGroup = MixerGroupType.MusicLoops;
			int index = i;
			SoundPlayer soundPlayer = Sound.PlayLooping(soundDescriptor, Vector3.zero, delegate()
			{
				this.Loops[index] = null;
			});
			soundPlayer.SoundType = SoundType.Music;
			this.Loops[i] = soundPlayer;
		}
		this.m_time = 0f;
		this.m_isPlaying = true;
		for (int j = 0; j < this.Loops.Count; j++)
		{
			SoundPlayer soundPlayer2 = this.Loops[j];
			soundPlayer2.Play();
		}
	}

	// Token: 0x06002961 RID: 10593 RVA: 0x000B2A58 File Offset: 0x000B0C58
	public void FixedUpdate()
	{
		if (UI.Menu.ResumeScreenVisible || DebugMenuB.Active)
		{
			return;
		}
		if (!this.m_isPlaying)
		{
			return;
		}
		this.m_time += Time.deltaTime;
		if (this.m_time > this.SoundComposition.LoopDuration)
		{
			this.m_time -= this.SoundComposition.LoopDuration;
			this.m_loop++;
			if (this.m_loop == this.SoundComposition.LoopCount)
			{
				this.m_loop = 0;
			}
			for (int i = 0; i < this.SoundComposition.Layers.Count; i++)
			{
				global::SoundComposition.SoundLayer soundLayer = this.SoundComposition.Layers[i];
				if (soundLayer.LoopsToPlay[this.m_loop])
				{
					SoundDescriptor soundDescriptor = new SoundDescriptor(soundLayer.Sound, 1f);
					soundDescriptor.SoundSize.Radius = 0f;
					soundDescriptor.SoundSize.FalloffMargin = 0f;
					soundDescriptor.ShouldBePanned = false;
					soundDescriptor.MixerGroup = MixerGroupType.MusicLoops;
					int index = i;
					if (this.Layers[index])
					{
						UberPoolManager.Instance.RemoveOnDestroyed(this.Layers[index].gameObject);
					}
					SoundPlayer soundPlayer = Sound.Play(soundDescriptor, Vector3.zero, delegate()
					{
						this.Layers[index] = null;
					});
					soundPlayer.SoundType = SoundType.Music;
					this.Layers[i] = soundPlayer;
				}
			}
		}
		for (int j = 0; j < this.Loops.Count; j++)
		{
			SoundPlayer soundPlayer2 = this.Loops[j];
			float num;
			if (!Core.SoundComposition.SoundVolumes.Volumes.TryGetValue(this.SoundComposition.Loops[j].Sound, out num))
			{
				num = 1f;
			}
			soundPlayer2.Volume = this.SoundComposition.Loops[j].VolumeOverTime.Evaluate(this.m_time) * this.SoundComposition.Loops[j].Volume * num;
		}
		for (int k = 0; k < this.Layers.Count; k++)
		{
			SoundPlayer soundPlayer3 = this.Layers[k];
			float num2;
			if (!Core.SoundComposition.SoundVolumes.Volumes.TryGetValue(this.SoundComposition.Layers[k].Sound, out num2))
			{
				num2 = 1f;
			}
			if (soundPlayer3)
			{
				soundPlayer3.Volume = this.SoundComposition.Layers[k].VolumeOverTime.Evaluate(this.m_time) * this.SoundComposition.Layers[k].Volume * num2;
			}
		}
	}

	// Token: 0x040024EA RID: 9450
	public global::SoundComposition SoundComposition;

	// Token: 0x040024EB RID: 9451
	public List<SoundPlayer> Layers = new List<SoundPlayer>();

	// Token: 0x040024EC RID: 9452
	public List<SoundPlayer> Loops = new List<SoundPlayer>();

	// Token: 0x040024ED RID: 9453
	private bool m_isPlaying;

	// Token: 0x040024EE RID: 9454
	private float m_time;

	// Token: 0x040024EF RID: 9455
	private int m_loop;

	// Token: 0x040024F0 RID: 9456
	private float m_loopDuration;
}
