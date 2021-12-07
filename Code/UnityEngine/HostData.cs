using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200007A RID: 122
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class HostData
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0000AA20 File Offset: 0x00008C20
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x0000AA30 File Offset: 0x00008C30
		public bool useNat
		{
			get
			{
				return this.m_Nat != 0;
			}
			set
			{
				this.m_Nat = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0000AA48 File Offset: 0x00008C48
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x0000AA50 File Offset: 0x00008C50
		public string gameType
		{
			get
			{
				return this.m_GameType;
			}
			set
			{
				this.m_GameType = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0000AA5C File Offset: 0x00008C5C
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x0000AA64 File Offset: 0x00008C64
		public string gameName
		{
			get
			{
				return this.m_GameName;
			}
			set
			{
				this.m_GameName = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0000AA70 File Offset: 0x00008C70
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x0000AA78 File Offset: 0x00008C78
		public int connectedPlayers
		{
			get
			{
				return this.m_ConnectedPlayers;
			}
			set
			{
				this.m_ConnectedPlayers = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0000AA84 File Offset: 0x00008C84
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public int playerLimit
		{
			get
			{
				return this.m_PlayerLimit;
			}
			set
			{
				this.m_PlayerLimit = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0000AA98 File Offset: 0x00008C98
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		public string[] ip
		{
			get
			{
				return this.m_IP;
			}
			set
			{
				this.m_IP = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0000AAAC File Offset: 0x00008CAC
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0000AAB4 File Offset: 0x00008CB4
		public int port
		{
			get
			{
				return this.m_Port;
			}
			set
			{
				this.m_Port = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		public bool passwordProtected
		{
			get
			{
				return this.m_PasswordProtected != 0;
			}
			set
			{
				this.m_PasswordProtected = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		public string comment
		{
			get
			{
				return this.m_Comment;
			}
			set
			{
				this.m_Comment = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0000AAFC File Offset: 0x00008CFC
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x0000AB04 File Offset: 0x00008D04
		public string guid
		{
			get
			{
				return this.m_GUID;
			}
			set
			{
				this.m_GUID = value;
			}
		}

		// Token: 0x04000151 RID: 337
		private int m_Nat;

		// Token: 0x04000152 RID: 338
		private string m_GameType;

		// Token: 0x04000153 RID: 339
		private string m_GameName;

		// Token: 0x04000154 RID: 340
		private int m_ConnectedPlayers;

		// Token: 0x04000155 RID: 341
		private int m_PlayerLimit;

		// Token: 0x04000156 RID: 342
		private string[] m_IP;

		// Token: 0x04000157 RID: 343
		private int m_Port;

		// Token: 0x04000158 RID: 344
		private int m_PasswordProtected;

		// Token: 0x04000159 RID: 345
		private string m_Comment;

		// Token: 0x0400015A RID: 346
		private string m_GUID;
	}
}
