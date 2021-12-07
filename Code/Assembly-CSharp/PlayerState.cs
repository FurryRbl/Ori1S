using System;

// Token: 0x020008B0 RID: 2224
public class PlayerState
{
	// Token: 0x06003197 RID: 12695 RVA: 0x000D3416 File Offset: 0x000D1616
	public PlayerState(int userIndex)
	{
	}

	// Token: 0x06003198 RID: 12696 RVA: 0x000D341E File Offset: 0x000D161E
	public bool IsSignedIn(bool onlineOnly)
	{
		return (this.IsUserSignedInLocal && !onlineOnly) || this.IsUserSignedInOnline;
	}

	// Token: 0x04002CD8 RID: 11480
	public string Name;

	// Token: 0x04002CD9 RID: 11481
	public bool IsUserSignedInLocal;

	// Token: 0x04002CDA RID: 11482
	public bool IsUserSignedInOnline;
}
