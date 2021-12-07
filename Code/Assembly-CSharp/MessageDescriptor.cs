using System;

// Token: 0x02000111 RID: 273
public struct MessageDescriptor
{
	// Token: 0x06000ABC RID: 2748 RVA: 0x0002EC40 File Offset: 0x0002CE40
	public MessageDescriptor(string message, EmotionType emotion, SoundProvider sound)
	{
		this.Message = message;
		this.Emotion = emotion;
		this.Sound = sound;
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x0002EC57 File Offset: 0x0002CE57
	public MessageDescriptor(string message)
	{
		this.Message = message;
		this.Emotion = EmotionType.Neutral;
		this.Sound = null;
	}

	// Token: 0x040008CA RID: 2250
	public string Message;

	// Token: 0x040008CB RID: 2251
	public EmotionType Emotion;

	// Token: 0x040008CC RID: 2252
	public SoundProvider Sound;
}
