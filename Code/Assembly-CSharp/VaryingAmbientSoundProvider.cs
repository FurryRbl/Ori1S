using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E9 RID: 489
public class VaryingAmbientSoundProvider : SoundProvider
{
	// Token: 0x060010DF RID: 4319 RVA: 0x0004D194 File Offset: 0x0004B394
	// Note: this type is marked as 'beforefieldinit'.
	static VaryingAmbientSoundProvider()
	{
		int[][] array = new int[10][];
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
		VaryingAmbientSoundProvider.Indicies = array;
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x0004D264 File Offset: 0x0004B464
	public override SoundDescriptor GetSound(IContext context)
	{
		this.m_index++;
		this.m_index %= this.AudioClips.Count;
		this.Descriptor.Reset();
		this.Descriptor.AudioClip = this.AudioClips[VaryingAmbientSoundProvider.Indicies[this.AudioClips.Count - 1][this.m_index]];
		this.Descriptor.Volume = this.ProviderVolume;
		this.Descriptor.MixerGroup = this.MixerGroup;
		return this.Descriptor;
	}

	// Token: 0x04000EA2 RID: 3746
	private static int[][] Indicies;

	// Token: 0x04000EA3 RID: 3747
	public List<AudioClip> AudioClips;

	// Token: 0x04000EA4 RID: 3748
	private int m_index;

	// Token: 0x04000EA5 RID: 3749
	public float ProviderVolume = 0.6f;

	// Token: 0x04000EA6 RID: 3750
	public MixerGroupType MixerGroup;
}
