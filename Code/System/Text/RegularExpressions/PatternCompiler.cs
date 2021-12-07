using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000475 RID: 1141
	internal class PatternCompiler : ICompiler
	{
		// Token: 0x060028B6 RID: 10422 RVA: 0x000851E4 File Offset: 0x000833E4
		public PatternCompiler()
		{
			this.pgm = new ArrayList();
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000851F8 File Offset: 0x000833F8
		public static ushort EncodeOp(OpCode op, OpFlags flags)
		{
			return (ushort)(op | (OpCode)(flags & (OpFlags)65280));
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x00085204 File Offset: 0x00083404
		public static void DecodeOp(ushort word, out OpCode op, out OpFlags flags)
		{
			op = (OpCode)(word & 255);
			flags = (OpFlags)(word & 65280);
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x0008521C File Offset: 0x0008341C
		public void Reset()
		{
			this.pgm.Clear();
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x0008522C File Offset: 0x0008342C
		public IMachineFactory GetMachineFactory()
		{
			ushort[] array = new ushort[this.pgm.Count];
			this.pgm.CopyTo(array);
			return new InterpreterFactory(array);
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x0008525C File Offset: 0x0008345C
		public void EmitFalse()
		{
			this.Emit(OpCode.False);
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x00085268 File Offset: 0x00083468
		public void EmitTrue()
		{
			this.Emit(OpCode.True);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x00085274 File Offset: 0x00083474
		private void EmitCount(int count)
		{
			this.Emit((ushort)(count & 65535));
			this.Emit((ushort)((uint)count >> 16));
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x0008529C File Offset: 0x0008349C
		public void EmitCharacter(char c, bool negate, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Character, PatternCompiler.MakeFlags(negate, ignore, reverse, false));
			if (ignore)
			{
				c = char.ToLower(c);
			}
			this.Emit((ushort)c);
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000852D0 File Offset: 0x000834D0
		public void EmitCategory(Category cat, bool negate, bool reverse)
		{
			this.Emit(OpCode.Category, PatternCompiler.MakeFlags(negate, false, reverse, false));
			this.Emit((ushort)cat);
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000852EC File Offset: 0x000834EC
		public void EmitNotCategory(Category cat, bool negate, bool reverse)
		{
			this.Emit(OpCode.NotCategory, PatternCompiler.MakeFlags(negate, false, reverse, false));
			this.Emit((ushort)cat);
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x00085308 File Offset: 0x00083508
		public void EmitRange(char lo, char hi, bool negate, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Range, PatternCompiler.MakeFlags(negate, ignore, reverse, false));
			this.Emit((ushort)lo);
			this.Emit((ushort)hi);
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x00085338 File Offset: 0x00083538
		public void EmitSet(char lo, BitArray set, bool negate, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Set, PatternCompiler.MakeFlags(negate, ignore, reverse, false));
			this.Emit((ushort)lo);
			int num = set.Length + 15 >> 4;
			this.Emit((ushort)num);
			int num2 = 0;
			while (num-- != 0)
			{
				ushort num3 = 0;
				for (int i = 0; i < 16; i++)
				{
					if (num2 >= set.Length)
					{
						break;
					}
					if (set[num2++])
					{
						num3 |= (ushort)(1 << i);
					}
				}
				this.Emit(num3);
			}
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000853CC File Offset: 0x000835CC
		public void EmitString(string str, bool ignore, bool reverse)
		{
			this.Emit(OpCode.String, PatternCompiler.MakeFlags(false, ignore, reverse, false));
			int length = str.Length;
			this.Emit((ushort)length);
			if (ignore)
			{
				str = str.ToLower();
			}
			for (int i = 0; i < length; i++)
			{
				this.Emit((ushort)str[i]);
			}
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x00085428 File Offset: 0x00083628
		public void EmitPosition(Position pos)
		{
			this.Emit(OpCode.Position, OpFlags.None);
			this.Emit((ushort)pos);
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x0008543C File Offset: 0x0008363C
		public void EmitOpen(int gid)
		{
			this.Emit(OpCode.Open);
			this.Emit((ushort)gid);
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x00085450 File Offset: 0x00083650
		public void EmitClose(int gid)
		{
			this.Emit(OpCode.Close);
			this.Emit((ushort)gid);
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x00085464 File Offset: 0x00083664
		public void EmitBalanceStart(int gid, int balance, bool capture, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.BalanceStart);
			this.Emit((ushort)gid);
			this.Emit((ushort)balance);
			this.Emit((!capture) ? 0 : 1);
			this.EmitLink(tail);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000854B0 File Offset: 0x000836B0
		public void EmitBalance()
		{
			this.Emit(OpCode.Balance);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000854BC File Offset: 0x000836BC
		public void EmitReference(int gid, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Reference, PatternCompiler.MakeFlags(false, ignore, reverse, false));
			this.Emit((ushort)gid);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000854D8 File Offset: 0x000836D8
		public void EmitIfDefined(int gid, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.IfDefined);
			this.EmitLink(tail);
			this.Emit((ushort)gid);
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x00085504 File Offset: 0x00083704
		public void EmitSub(LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.Sub);
			this.EmitLink(tail);
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0008551C File Offset: 0x0008371C
		public void EmitTest(LinkRef yes, LinkRef tail)
		{
			this.BeginLink(yes);
			this.BeginLink(tail);
			this.Emit(OpCode.Test);
			this.EmitLink(yes);
			this.EmitLink(tail);
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x00085550 File Offset: 0x00083750
		public void EmitBranch(LinkRef next)
		{
			this.BeginLink(next);
			this.Emit(OpCode.Branch, OpFlags.None);
			this.EmitLink(next);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x0008556C File Offset: 0x0008376C
		public void EmitJump(LinkRef target)
		{
			this.BeginLink(target);
			this.Emit(OpCode.Jump, OpFlags.None);
			this.EmitLink(target);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x00085588 File Offset: 0x00083788
		public void EmitRepeat(int min, int max, bool lazy, LinkRef until)
		{
			this.BeginLink(until);
			this.Emit(OpCode.Repeat, PatternCompiler.MakeFlags(false, false, false, lazy));
			this.EmitLink(until);
			this.EmitCount(min);
			this.EmitCount(max);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000855C4 File Offset: 0x000837C4
		public void EmitUntil(LinkRef repeat)
		{
			this.ResolveLink(repeat);
			this.Emit(OpCode.Until);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000855D8 File Offset: 0x000837D8
		public void EmitFastRepeat(int min, int max, bool lazy, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.FastRepeat, PatternCompiler.MakeFlags(false, false, false, lazy));
			this.EmitLink(tail);
			this.EmitCount(min);
			this.EmitCount(max);
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x00085614 File Offset: 0x00083814
		public void EmitIn(LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.In);
			this.EmitLink(tail);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x0008562C File Offset: 0x0008382C
		public void EmitAnchor(bool reverse, int offset, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.Anchor, PatternCompiler.MakeFlags(false, false, reverse, false));
			this.EmitLink(tail);
			this.Emit((ushort)offset);
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x00085660 File Offset: 0x00083860
		public void EmitInfo(int count, int min, int max)
		{
			this.Emit(OpCode.Info);
			this.EmitCount(count);
			this.EmitCount(min);
			this.EmitCount(max);
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x0008568C File Offset: 0x0008388C
		public LinkRef NewLink()
		{
			return new PatternCompiler.PatternLinkStack();
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x00085694 File Offset: 0x00083894
		public void ResolveLink(LinkRef lref)
		{
			PatternCompiler.PatternLinkStack patternLinkStack = (PatternCompiler.PatternLinkStack)lref;
			while (patternLinkStack.Pop())
			{
				this.pgm[patternLinkStack.OffsetAddress] = (ushort)patternLinkStack.GetOffset(this.CurrentAddress);
			}
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000856DC File Offset: 0x000838DC
		public void EmitBranchEnd()
		{
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000856E0 File Offset: 0x000838E0
		public void EmitAlternationEnd()
		{
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000856E4 File Offset: 0x000838E4
		private static OpFlags MakeFlags(bool negate, bool ignore, bool reverse, bool lazy)
		{
			OpFlags opFlags = OpFlags.None;
			if (negate)
			{
				opFlags |= OpFlags.Negate;
			}
			if (ignore)
			{
				opFlags |= OpFlags.IgnoreCase;
			}
			if (reverse)
			{
				opFlags |= OpFlags.RightToLeft;
			}
			if (lazy)
			{
				opFlags |= OpFlags.Lazy;
			}
			return opFlags;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x00085730 File Offset: 0x00083930
		private void Emit(OpCode op)
		{
			this.Emit(op, OpFlags.None);
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0008573C File Offset: 0x0008393C
		private void Emit(OpCode op, OpFlags flags)
		{
			this.Emit(PatternCompiler.EncodeOp(op, flags));
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0008574C File Offset: 0x0008394C
		private void Emit(ushort word)
		{
			this.pgm.Add(word);
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x00085760 File Offset: 0x00083960
		private int CurrentAddress
		{
			get
			{
				return this.pgm.Count;
			}
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x00085770 File Offset: 0x00083970
		private void BeginLink(LinkRef lref)
		{
			PatternCompiler.PatternLinkStack patternLinkStack = (PatternCompiler.PatternLinkStack)lref;
			patternLinkStack.BaseAddress = this.CurrentAddress;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x00085790 File Offset: 0x00083990
		private void EmitLink(LinkRef lref)
		{
			PatternCompiler.PatternLinkStack patternLinkStack = (PatternCompiler.PatternLinkStack)lref;
			patternLinkStack.OffsetAddress = this.CurrentAddress;
			this.Emit(0);
			patternLinkStack.Push();
		}

		// Token: 0x040019C3 RID: 6595
		private ArrayList pgm;

		// Token: 0x02000476 RID: 1142
		private class PatternLinkStack : LinkStack
		{
			// Token: 0x17000B5B RID: 2907
			// (set) Token: 0x060028E1 RID: 10465 RVA: 0x000857C8 File Offset: 0x000839C8
			public int BaseAddress
			{
				set
				{
					this.link.base_addr = value;
				}
			}

			// Token: 0x17000B5C RID: 2908
			// (get) Token: 0x060028E2 RID: 10466 RVA: 0x000857D8 File Offset: 0x000839D8
			// (set) Token: 0x060028E3 RID: 10467 RVA: 0x000857E8 File Offset: 0x000839E8
			public int OffsetAddress
			{
				get
				{
					return this.link.offset_addr;
				}
				set
				{
					this.link.offset_addr = value;
				}
			}

			// Token: 0x060028E4 RID: 10468 RVA: 0x000857F8 File Offset: 0x000839F8
			public int GetOffset(int target_addr)
			{
				return target_addr - this.link.base_addr;
			}

			// Token: 0x060028E5 RID: 10469 RVA: 0x00085808 File Offset: 0x00083A08
			protected override object GetCurrent()
			{
				return this.link;
			}

			// Token: 0x060028E6 RID: 10470 RVA: 0x00085818 File Offset: 0x00083A18
			protected override void SetCurrent(object l)
			{
				this.link = (PatternCompiler.PatternLinkStack.Link)l;
			}

			// Token: 0x040019C4 RID: 6596
			private PatternCompiler.PatternLinkStack.Link link;

			// Token: 0x02000477 RID: 1143
			private struct Link
			{
				// Token: 0x040019C5 RID: 6597
				public int base_addr;

				// Token: 0x040019C6 RID: 6598
				public int offset_addr;
			}
		}
	}
}
