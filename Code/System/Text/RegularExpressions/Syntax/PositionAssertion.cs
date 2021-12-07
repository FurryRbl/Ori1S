using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A5 RID: 1189
	internal class PositionAssertion : Expression
	{
		// Token: 0x06002AB3 RID: 10931 RVA: 0x00092F18 File Offset: 0x00091118
		public PositionAssertion(Position pos)
		{
			this.pos = pos;
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x00092F28 File Offset: 0x00091128
		// (set) Token: 0x06002AB5 RID: 10933 RVA: 0x00092F30 File Offset: 0x00091130
		public Position Position
		{
			get
			{
				return this.pos;
			}
			set
			{
				this.pos = value;
			}
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x00092F3C File Offset: 0x0009113C
		public override void Compile(ICompiler cmp, bool reverse)
		{
			cmp.EmitPosition(this.pos);
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x00092F4C File Offset: 0x0009114C
		public override void GetWidth(out int min, out int max)
		{
			min = (max = 0);
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x00092F64 File Offset: 0x00091164
		public override bool IsComplex()
		{
			return false;
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x00092F68 File Offset: 0x00091168
		public override AnchorInfo GetAnchorInfo(bool revers)
		{
			switch (this.pos)
			{
			case Position.StartOfString:
			case Position.StartOfLine:
			case Position.StartOfScan:
				return new AnchorInfo(this, 0, 0, this.pos);
			default:
				return new AnchorInfo(this, 0);
			}
		}

		// Token: 0x04001B0B RID: 6923
		private Position pos;
	}
}
