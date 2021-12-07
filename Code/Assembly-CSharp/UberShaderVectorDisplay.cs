using System;
using UnityEngine;

// Token: 0x020007D1 RID: 2001
public class UberShaderVectorDisplay : PropertyAttribute
{
	// Token: 0x06002DF0 RID: 11760 RVA: 0x000C3BDB File Offset: 0x000C1DDB
	public UberShaderVectorDisplay(string xyName, string zwName)
	{
		this.X = xyName;
		this.Z = zwName;
		this.ShowAsVector2 = true;
	}

	// Token: 0x06002DF1 RID: 11761 RVA: 0x000C3BF8 File Offset: 0x000C1DF8
	public UberShaderVectorDisplay(string x, string y, string z, string w)
	{
		this.X = x;
		this.Y = y;
		this.Z = z;
		this.W = w;
		this.ShowAsVector2 = false;
	}

	// Token: 0x04002976 RID: 10614
	public bool ShowAsVector2;

	// Token: 0x04002977 RID: 10615
	public string X;

	// Token: 0x04002978 RID: 10616
	public string Y;

	// Token: 0x04002979 RID: 10617
	public string Z;

	// Token: 0x0400297A RID: 10618
	public string W;
}
