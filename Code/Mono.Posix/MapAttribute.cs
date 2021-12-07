using System;

// Token: 0x02000027 RID: 39
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Delegate)]
internal class MapAttribute : Attribute
{
	// Token: 0x06000209 RID: 521 RVA: 0x00008864 File Offset: 0x00006A64
	public MapAttribute()
	{
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000886C File Offset: 0x00006A6C
	public MapAttribute(string nativeType)
	{
		this.nativeType = nativeType;
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x0600020B RID: 523 RVA: 0x0000887C File Offset: 0x00006A7C
	public string NativeType
	{
		get
		{
			return this.nativeType;
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x0600020C RID: 524 RVA: 0x00008884 File Offset: 0x00006A84
	// (set) Token: 0x0600020D RID: 525 RVA: 0x0000888C File Offset: 0x00006A8C
	public string SuppressFlags
	{
		get
		{
			return this.suppressFlags;
		}
		set
		{
			this.suppressFlags = value;
		}
	}

	// Token: 0x04000093 RID: 147
	private string nativeType;

	// Token: 0x04000094 RID: 148
	private string suppressFlags;
}
