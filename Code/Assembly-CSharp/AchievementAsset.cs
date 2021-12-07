using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class AchievementAsset : ScriptableObject
{
	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06000485 RID: 1157 RVA: 0x000129ED File Offset: 0x00010BED
	public string Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000486 RID: 1158 RVA: 0x000129F5 File Offset: 0x00010BF5
	public string WSAIdentifier
	{
		get
		{
			return this.m_WSAIdentifier;
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06000487 RID: 1159 RVA: 0x000129FD File Offset: 0x00010BFD
	public string XboxOneIdentifier
	{
		get
		{
			return this.m_xboxOneIdentifier;
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06000488 RID: 1160 RVA: 0x00012A05 File Offset: 0x00010C05
	public string SteamIdentifier
	{
		get
		{
			return this.m_steamIdentifier;
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06000489 RID: 1161 RVA: 0x00012A0D File Offset: 0x00010C0D
	public int Xbox360Identifier
	{
		get
		{
			return this.m_xbox360Identifier;
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x0600048A RID: 1162 RVA: 0x00012A15 File Offset: 0x00010C15
	public Texture Icon
	{
		get
		{
			return this.m_icon;
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x0600048B RID: 1163 RVA: 0x00012A1D File Offset: 0x00010C1D
	public bool IsSecret
	{
		get
		{
			return this.m_isSecret;
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x0600048C RID: 1164 RVA: 0x00012A25 File Offset: 0x00010C25
	// (set) Token: 0x0600048D RID: 1165 RVA: 0x00012A2D File Offset: 0x00010C2D
	public bool IsEarnt
	{
		get
		{
			return this.m_isEarnt;
		}
		set
		{
			this.m_isEarnt = value;
		}
	}

	// Token: 0x040003AD RID: 941
	[SerializeField]
	private string m_name;

	// Token: 0x040003AE RID: 942
	[SerializeField]
	private Texture m_icon;

	// Token: 0x040003AF RID: 943
	[SerializeField]
	private string m_WSAIdentifier;

	// Token: 0x040003B0 RID: 944
	[SerializeField]
	private string m_xboxOneIdentifier;

	// Token: 0x040003B1 RID: 945
	[SerializeField]
	private string m_steamIdentifier;

	// Token: 0x040003B2 RID: 946
	[SerializeField]
	private int m_xbox360Identifier;

	// Token: 0x040003B3 RID: 947
	[SerializeField]
	private bool m_isSecret;

	// Token: 0x040003B4 RID: 948
	[SerializeField]
	private bool m_isEarnt;
}
