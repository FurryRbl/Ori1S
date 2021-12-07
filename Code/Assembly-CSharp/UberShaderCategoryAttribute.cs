using System;

// Token: 0x020007CD RID: 1997
public class UberShaderCategoryAttribute : Attribute
{
	// Token: 0x06002DD8 RID: 11736 RVA: 0x000C3761 File Offset: 0x000C1961
	public UberShaderCategoryAttribute(UberShaderCategory category)
	{
		this.Category = category;
	}

	// Token: 0x04002964 RID: 10596
	public UberShaderCategory Category;
}
