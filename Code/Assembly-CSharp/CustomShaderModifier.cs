using System;

// Token: 0x020007CC RID: 1996
public class CustomShaderModifier : Attribute
{
	// Token: 0x06002DD7 RID: 11735 RVA: 0x000C3752 File Offset: 0x000C1952
	public CustomShaderModifier(string name)
	{
		this.Name = name;
	}

	// Token: 0x04002963 RID: 10595
	public string Name;
}
