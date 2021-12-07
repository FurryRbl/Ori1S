using System;

// Token: 0x02000827 RID: 2087
public class CustomShaderBlock : Attribute
{
	// Token: 0x06002FCC RID: 12236 RVA: 0x000CAC6F File Offset: 0x000C8E6F
	public CustomShaderBlock(string name)
	{
		this.Name = name;
	}

	// Token: 0x04002B01 RID: 11009
	public string Name;
}
