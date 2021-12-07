using System;
using System.Collections.Generic;

// Token: 0x020001D6 RID: 470
public class EnvrionmentBasedSoundProvider : SoundProvider
{
	// Token: 0x060010CB RID: 4299 RVA: 0x0004CC3F File Offset: 0x0004AE3F
	public override SoundDescriptor GetSound(IContext context)
	{
		return this.Sounds[0].SoundProvider.GetSound(context);
	}

	// Token: 0x04000E56 RID: 3670
	public List<EnvironmentSoundPair> Sounds;
}
