using System;

// Token: 0x02000346 RID: 838
[Category("Sound")]
public class SoundAction : ActionMethod
{
	// Token: 0x060017FA RID: 6138 RVA: 0x00066DE0 File Offset: 0x00064FE0
	public override void Perform(IContext context)
	{
		switch (this.Command)
		{
		case SoundAction.CommandType.Play:
			this.Sound.Play();
			break;
		case SoundAction.CommandType.Pause:
			this.Sound.Pause();
			break;
		case SoundAction.CommandType.Stop:
			this.Sound.Stop();
			break;
		case SoundAction.CommandType.StopAndFadeOut:
			this.Sound.StopAndFadeOut(this.FadeDuration);
			break;
		}
	}

	// Token: 0x17000434 RID: 1076
	// (get) Token: 0x060017FB RID: 6139 RVA: 0x00066E58 File Offset: 0x00065058
	private string TargetName
	{
		get
		{
			return (!(this.Sound != null)) ? "unkown" : this.Sound.name;
		}
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x00066E8C File Offset: 0x0006508C
	public override string GetNiceName()
	{
		switch (this.Command)
		{
		case SoundAction.CommandType.Play:
			return "Play " + this.TargetName + " sound";
		case SoundAction.CommandType.Pause:
			return "Pause " + this.TargetName + " sound";
		case SoundAction.CommandType.Stop:
			return "Stop " + this.TargetName + " sound";
		case SoundAction.CommandType.StopAndFadeOut:
			return "Stop and fade out " + this.TargetName + " sound";
		default:
			return base.GetNiceName();
		}
	}

	// Token: 0x040014AF RID: 5295
	public SoundAction.CommandType Command;

	// Token: 0x040014B0 RID: 5296
	[NotNull]
	public SoundSource Sound;

	// Token: 0x040014B1 RID: 5297
	public float FadeDuration;

	// Token: 0x02000347 RID: 839
	public enum CommandType
	{
		// Token: 0x040014B3 RID: 5299
		Play,
		// Token: 0x040014B4 RID: 5300
		Pause,
		// Token: 0x040014B5 RID: 5301
		Stop,
		// Token: 0x040014B6 RID: 5302
		StopAndFadeOut
	}
}
