using System;

// Token: 0x020007CA RID: 1994
public class UberShaderOrderAttribute : Attribute
{
	// Token: 0x06002DD6 RID: 11734 RVA: 0x000C3743 File Offset: 0x000C1943
	public UberShaderOrderAttribute(UberShaderOrder queue)
	{
		this.Queue = queue;
	}

	// Token: 0x0400292A RID: 10538
	public UberShaderOrder Queue;
}
