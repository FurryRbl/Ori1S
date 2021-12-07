using System;
using UnityEngine;

// Token: 0x02000493 RID: 1171
[Serializable]
public class OverridableColorProperty
{
	// Token: 0x06001FBD RID: 8125 RVA: 0x0008B585 File Offset: 0x00089785
	public OverridableColorProperty()
	{
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x0008B590 File Offset: 0x00089790
	public OverridableColorProperty(OverridableColorProperty colorProperty)
	{
		this.Override = colorProperty.Override;
		this.Name = colorProperty.Name;
		this.Color = colorProperty.Color;
	}

	// Token: 0x06001FBF RID: 8127 RVA: 0x0008B5C7 File Offset: 0x000897C7
	public void Apply(OverridableColorProperty colorProperty)
	{
		this.Color = colorProperty.Color;
	}

	// Token: 0x04001B59 RID: 7001
	public bool Override;

	// Token: 0x04001B5A RID: 7002
	public string Name;

	// Token: 0x04001B5B RID: 7003
	public Color Color;
}
