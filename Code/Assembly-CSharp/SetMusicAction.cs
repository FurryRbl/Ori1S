using System;

// Token: 0x02000269 RID: 617
public class SetMusicAction : ActionMethod
{
	// Token: 0x060014B7 RID: 5303 RVA: 0x0005D74E File Offset: 0x0005B94E
	public override void Perform(IContext context)
	{
		this.MusicZone.SetSoundProvider(this.Music);
	}

	// Token: 0x04001204 RID: 4612
	[NotNull]
	public MusicZone MusicZone;

	// Token: 0x04001205 RID: 4613
	public SoundProvider Music;
}
