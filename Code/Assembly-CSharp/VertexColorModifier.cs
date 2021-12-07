using System;

// Token: 0x02000816 RID: 2070
[UberShaderCategory(UberShaderCategory.Utility)]
[CustomShaderModifier("Vertex color")]
[UberShaderOrder(UberShaderOrder.VertexColor)]
public class VertexColorModifier : UberShaderModifier
{
	// Token: 0x06002F9E RID: 12190 RVA: 0x000C9D82 File Offset: 0x000C7F82
	public override void SetProperties()
	{
	}

	// Token: 0x06002F9F RID: 12191 RVA: 0x000C9D84 File Offset: 0x000C7F84
	public override bool RequiresVertexColor()
	{
		return true;
	}
}
