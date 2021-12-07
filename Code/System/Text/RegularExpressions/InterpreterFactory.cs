using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000474 RID: 1140
	internal class InterpreterFactory : IMachineFactory
	{
		// Token: 0x060028AD RID: 10413 RVA: 0x0008517C File Offset: 0x0008337C
		public InterpreterFactory(ushort[] pattern)
		{
			this.pattern = pattern;
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x0008518C File Offset: 0x0008338C
		public IMachine NewInstance()
		{
			return new Interpreter(this.pattern);
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x0008519C File Offset: 0x0008339C
		public int GroupCount
		{
			get
			{
				return (int)this.pattern[1];
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x000851A8 File Offset: 0x000833A8
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x000851B0 File Offset: 0x000833B0
		public int Gap
		{
			get
			{
				return this.gap;
			}
			set
			{
				this.gap = value;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000851BC File Offset: 0x000833BC
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x000851C4 File Offset: 0x000833C4
		public IDictionary Mapping
		{
			get
			{
				return this.mapping;
			}
			set
			{
				this.mapping = value;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x000851D0 File Offset: 0x000833D0
		// (set) Token: 0x060028B5 RID: 10421 RVA: 0x000851D8 File Offset: 0x000833D8
		public string[] NamesMapping
		{
			get
			{
				return this.namesMapping;
			}
			set
			{
				this.namesMapping = value;
			}
		}

		// Token: 0x040019BF RID: 6591
		private IDictionary mapping;

		// Token: 0x040019C0 RID: 6592
		private ushort[] pattern;

		// Token: 0x040019C1 RID: 6593
		private string[] namesMapping;

		// Token: 0x040019C2 RID: 6594
		private int gap;
	}
}
