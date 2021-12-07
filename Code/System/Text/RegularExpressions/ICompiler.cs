using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000473 RID: 1139
	internal interface ICompiler
	{
		// Token: 0x0600288E RID: 10382
		void Reset();

		// Token: 0x0600288F RID: 10383
		IMachineFactory GetMachineFactory();

		// Token: 0x06002890 RID: 10384
		void EmitFalse();

		// Token: 0x06002891 RID: 10385
		void EmitTrue();

		// Token: 0x06002892 RID: 10386
		void EmitCharacter(char c, bool negate, bool ignore, bool reverse);

		// Token: 0x06002893 RID: 10387
		void EmitCategory(Category cat, bool negate, bool reverse);

		// Token: 0x06002894 RID: 10388
		void EmitNotCategory(Category cat, bool negate, bool reverse);

		// Token: 0x06002895 RID: 10389
		void EmitRange(char lo, char hi, bool negate, bool ignore, bool reverse);

		// Token: 0x06002896 RID: 10390
		void EmitSet(char lo, BitArray set, bool negate, bool ignore, bool reverse);

		// Token: 0x06002897 RID: 10391
		void EmitString(string str, bool ignore, bool reverse);

		// Token: 0x06002898 RID: 10392
		void EmitPosition(Position pos);

		// Token: 0x06002899 RID: 10393
		void EmitOpen(int gid);

		// Token: 0x0600289A RID: 10394
		void EmitClose(int gid);

		// Token: 0x0600289B RID: 10395
		void EmitBalanceStart(int gid, int balance, bool capture, LinkRef tail);

		// Token: 0x0600289C RID: 10396
		void EmitBalance();

		// Token: 0x0600289D RID: 10397
		void EmitReference(int gid, bool ignore, bool reverse);

		// Token: 0x0600289E RID: 10398
		void EmitIfDefined(int gid, LinkRef tail);

		// Token: 0x0600289F RID: 10399
		void EmitSub(LinkRef tail);

		// Token: 0x060028A0 RID: 10400
		void EmitTest(LinkRef yes, LinkRef tail);

		// Token: 0x060028A1 RID: 10401
		void EmitBranch(LinkRef next);

		// Token: 0x060028A2 RID: 10402
		void EmitJump(LinkRef target);

		// Token: 0x060028A3 RID: 10403
		void EmitRepeat(int min, int max, bool lazy, LinkRef until);

		// Token: 0x060028A4 RID: 10404
		void EmitUntil(LinkRef repeat);

		// Token: 0x060028A5 RID: 10405
		void EmitIn(LinkRef tail);

		// Token: 0x060028A6 RID: 10406
		void EmitInfo(int count, int min, int max);

		// Token: 0x060028A7 RID: 10407
		void EmitFastRepeat(int min, int max, bool lazy, LinkRef tail);

		// Token: 0x060028A8 RID: 10408
		void EmitAnchor(bool reverse, int offset, LinkRef tail);

		// Token: 0x060028A9 RID: 10409
		void EmitBranchEnd();

		// Token: 0x060028AA RID: 10410
		void EmitAlternationEnd();

		// Token: 0x060028AB RID: 10411
		LinkRef NewLink();

		// Token: 0x060028AC RID: 10412
		void ResolveLink(LinkRef link);
	}
}
