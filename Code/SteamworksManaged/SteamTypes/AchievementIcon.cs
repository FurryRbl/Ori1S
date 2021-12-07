using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000AC RID: 172
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AchievementIcon : IEquatable<AchievementIcon>
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x000090B9 File Offset: 0x000072B9
		public AchievementIcon(int value)
		{
			this.handle = value;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x000090C2 File Offset: 0x000072C2
		public int AsInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000090CA File Offset: 0x000072CA
		public static bool operator ==(AchievementIcon x, AchievementIcon y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000090DC File Offset: 0x000072DC
		public static bool operator !=(AchievementIcon x, AchievementIcon y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000090F1 File Offset: 0x000072F1
		public bool Equals(AchievementIcon other)
		{
			return this == other;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000090FF File Offset: 0x000072FF
		public override bool Equals(object obj)
		{
			return obj is AchievementIcon && this == (AchievementIcon)obj;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000911C File Offset: 0x0000731C
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00009129 File Offset: 0x00007329
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x040002F2 RID: 754
		private int handle;
	}
}
