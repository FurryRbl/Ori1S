using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A6 RID: 1190
	internal class Reference : Expression
	{
		// Token: 0x06002ABA RID: 10938 RVA: 0x00092FAC File Offset: 0x000911AC
		public Reference(bool ignore)
		{
			this.ignore = ignore;
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x00092FBC File Offset: 0x000911BC
		// (set) Token: 0x06002ABC RID: 10940 RVA: 0x00092FC4 File Offset: 0x000911C4
		public CapturingGroup CapturingGroup
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x00092FD0 File Offset: 0x000911D0
		// (set) Token: 0x06002ABE RID: 10942 RVA: 0x00092FD8 File Offset: 0x000911D8
		public bool IgnoreCase
		{
			get
			{
				return this.ignore;
			}
			set
			{
				this.ignore = value;
			}
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x00092FE4 File Offset: 0x000911E4
		public override void Compile(ICompiler cmp, bool reverse)
		{
			cmp.EmitReference(this.group.Index, this.ignore, reverse);
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x00093000 File Offset: 0x00091200
		public override void GetWidth(out int min, out int max)
		{
			min = 0;
			max = int.MaxValue;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x0009300C File Offset: 0x0009120C
		public override bool IsComplex()
		{
			return true;
		}

		// Token: 0x04001B0C RID: 6924
		private CapturingGroup group;

		// Token: 0x04001B0D RID: 6925
		private bool ignore;
	}
}
