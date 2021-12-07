using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004E RID: 78
	[Map("struct timezone")]
	public struct Timezone : IEquatable<Timezone>
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000B104 File Offset: 0x00009304
		public override int GetHashCode()
		{
			return this.tz_minuteswest.GetHashCode();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000B114 File Offset: 0x00009314
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == base.GetType() && ((Timezone)obj).tz_minuteswest == this.tz_minuteswest;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B15C File Offset: 0x0000935C
		public bool Equals(Timezone value)
		{
			return value.tz_minuteswest == this.tz_minuteswest;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B170 File Offset: 0x00009370
		public static bool operator ==(Timezone lhs, Timezone rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B17C File Offset: 0x0000937C
		public static bool operator !=(Timezone lhs, Timezone rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0400035C RID: 860
		public int tz_minuteswest;

		// Token: 0x0400035D RID: 861
		private int tz_dsttime;
	}
}
