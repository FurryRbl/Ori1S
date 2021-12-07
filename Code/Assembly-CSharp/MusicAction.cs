using System;

// Token: 0x020006A4 RID: 1700
[Category("Music")]
public class MusicAction : ActionMethod
{
	// Token: 0x06002920 RID: 10528 RVA: 0x000B1A7C File Offset: 0x000AFC7C
	public override void Perform(IContext context)
	{
		MusicAction.MusicCommand command = this.Command;
		if (command != MusicAction.MusicCommand.Play)
		{
			if (command == MusicAction.MusicCommand.Stop)
			{
				this.Music.Stop();
			}
		}
		else
		{
			this.Music.Play();
		}
	}

	// Token: 0x06002921 RID: 10529 RVA: 0x000B1AC4 File Offset: 0x000AFCC4
	public override string GetNiceName()
	{
		MusicAction.MusicCommand command = this.Command;
		if (command == MusicAction.MusicCommand.Play)
		{
			return "Play " + ActionHelper.GetName(this.Music) + " music";
		}
		if (command != MusicAction.MusicCommand.Stop)
		{
			return null;
		}
		return "Stop " + ActionHelper.GetName(this.Music) + " music";
	}

	// Token: 0x040024A7 RID: 9383
	[NotNull]
	public MusicSource Music;

	// Token: 0x040024A8 RID: 9384
	public MusicAction.MusicCommand Command;

	// Token: 0x020006A5 RID: 1701
	public enum MusicCommand
	{
		// Token: 0x040024AA RID: 9386
		Play,
		// Token: 0x040024AB RID: 9387
		Stop
	}
}
