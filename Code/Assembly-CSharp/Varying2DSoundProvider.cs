using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001DF RID: 479
public class Varying2DSoundProvider : SoundProvider
{
	// Token: 0x060010DB RID: 4315 RVA: 0x0004CF68 File Offset: 0x0004B168
	// Note: this type is marked as 'beforefieldinit'.
	static Varying2DSoundProvider()
	{
		int[][] array = new int[12][];
		array[0] = new int[1];
		int num = 1;
		int[] array2 = new int[2];
		array2[0] = 1;
		array[num] = array2;
		array[2] = new int[]
		{
			2,
			0,
			1
		};
		array[3] = new int[]
		{
			3,
			1,
			0,
			2
		};
		array[4] = new int[]
		{
			4,
			1,
			3,
			0,
			2
		};
		array[5] = new int[]
		{
			1,
			0,
			4,
			2,
			5,
			3
		};
		array[6] = new int[]
		{
			5,
			1,
			6,
			0,
			2,
			4,
			3
		};
		array[7] = new int[]
		{
			0,
			3,
			4,
			2,
			1,
			6,
			7,
			5
		};
		array[8] = new int[]
		{
			5,
			8,
			6,
			1,
			3,
			0,
			7,
			2,
			4
		};
		array[9] = new int[]
		{
			9,
			7,
			0,
			5,
			1,
			3,
			8,
			4,
			2,
			6
		};
		array[10] = new int[]
		{
			9,
			7,
			0,
			5,
			1,
			3,
			10,
			8,
			4,
			2,
			6
		};
		array[11] = new int[]
		{
			9,
			7,
			10,
			0,
			5,
			11,
			1,
			3,
			8,
			4,
			2,
			6
		};
		Varying2DSoundProvider.Indicies = array;
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x0004D064 File Offset: 0x0004B264
	public override SoundDescriptor GetSound(IContext context)
	{
		if (this.AudioClips.Count == 0)
		{
			return null;
		}
		this.m_index++;
		this.m_index %= this.AudioClips.Count;
		this.Descriptor.Reset();
		this.Descriptor.AudioClip = this.AudioClips[Varying2DSoundProvider.Indicies[this.AudioClips.Count - 1][this.m_index]];
		this.Descriptor.Volume = this.ProviderVolume;
		this.Descriptor.SetSoundSize(this.SoundSize);
		this.Descriptor.ShouldBePanned = this.ShouldBePanned;
		this.Descriptor.Pitch = UnityEngine.Random.Range(this.MinPitch, this.MaxPitch);
		this.Descriptor.SetLowPassFilter(this.LowPassFilterSettings);
		this.Descriptor.SyncToTime = this.SyncToTime;
		this.Descriptor.SoundProvider = this;
		this.Descriptor.MixerGroup = this.MixerGroup;
		return this.Descriptor;
	}

	// Token: 0x04000E84 RID: 3716
	private static int[][] Indicies;

	// Token: 0x04000E85 RID: 3717
	public List<AudioClip> AudioClips;

	// Token: 0x04000E86 RID: 3718
	private int m_index;

	// Token: 0x04000E87 RID: 3719
	public float ProviderVolume = 0.6f;

	// Token: 0x04000E88 RID: 3720
	public float MinPitch = 1f;

	// Token: 0x04000E89 RID: 3721
	public float MaxPitch = 1f;

	// Token: 0x04000E8A RID: 3722
	public SoundSize SoundSize;

	// Token: 0x04000E8B RID: 3723
	public bool ShouldBePanned = true;

	// Token: 0x04000E8C RID: 3724
	public LowPassFilterSettings LowPassFilterSettings;

	// Token: 0x04000E8D RID: 3725
	public bool SyncToTime;

	// Token: 0x04000E8E RID: 3726
	public MixerGroupType MixerGroup;
}
