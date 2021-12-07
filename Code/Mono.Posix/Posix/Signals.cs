using System;

namespace Mono.Posix
{
	// Token: 0x02000071 RID: 113
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.Signum")]
	public enum Signals
	{
		// Token: 0x040003D6 RID: 982
		SIGHUP,
		// Token: 0x040003D7 RID: 983
		SIGINT,
		// Token: 0x040003D8 RID: 984
		SIGQUIT,
		// Token: 0x040003D9 RID: 985
		SIGILL,
		// Token: 0x040003DA RID: 986
		SIGTRAP,
		// Token: 0x040003DB RID: 987
		SIGABRT,
		// Token: 0x040003DC RID: 988
		SIGBUS,
		// Token: 0x040003DD RID: 989
		SIGFPE,
		// Token: 0x040003DE RID: 990
		SIGKILL,
		// Token: 0x040003DF RID: 991
		SIGUSR1,
		// Token: 0x040003E0 RID: 992
		SIGSEGV,
		// Token: 0x040003E1 RID: 993
		SIGUSR2,
		// Token: 0x040003E2 RID: 994
		SIGPIPE,
		// Token: 0x040003E3 RID: 995
		SIGALRM,
		// Token: 0x040003E4 RID: 996
		SIGTERM,
		// Token: 0x040003E5 RID: 997
		SIGCHLD,
		// Token: 0x040003E6 RID: 998
		SIGCONT,
		// Token: 0x040003E7 RID: 999
		SIGSTOP,
		// Token: 0x040003E8 RID: 1000
		SIGTSTP,
		// Token: 0x040003E9 RID: 1001
		SIGTTIN,
		// Token: 0x040003EA RID: 1002
		SIGTTOU,
		// Token: 0x040003EB RID: 1003
		SIGURG,
		// Token: 0x040003EC RID: 1004
		SIGXCPU,
		// Token: 0x040003ED RID: 1005
		SIGXFSZ,
		// Token: 0x040003EE RID: 1006
		SIGVTALRM,
		// Token: 0x040003EF RID: 1007
		SIGPROF,
		// Token: 0x040003F0 RID: 1008
		SIGWINCH,
		// Token: 0x040003F1 RID: 1009
		SIGIO,
		// Token: 0x040003F2 RID: 1010
		SIGSYS
	}
}
