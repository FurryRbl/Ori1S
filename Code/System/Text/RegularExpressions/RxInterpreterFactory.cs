using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000494 RID: 1172
	internal class RxInterpreterFactory : IMachineFactory
	{
		// Token: 0x06002A46 RID: 10822 RVA: 0x00091A48 File Offset: 0x0008FC48
		public RxInterpreterFactory(byte[] program, EvalDelegate eval_del)
		{
			this.program = program;
			this.eval_del = eval_del;
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x00091A60 File Offset: 0x0008FC60
		public IMachine NewInstance()
		{
			return new RxInterpreter(this.program, this.eval_del);
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x00091A74 File Offset: 0x0008FC74
		public int GroupCount
		{
			get
			{
				return (int)this.program[1] | (int)this.program[2] << 8;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x00091A8C File Offset: 0x0008FC8C
		// (set) Token: 0x06002A4A RID: 10826 RVA: 0x00091A94 File Offset: 0x0008FC94
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

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x00091AA0 File Offset: 0x0008FCA0
		// (set) Token: 0x06002A4C RID: 10828 RVA: 0x00091AA8 File Offset: 0x0008FCA8
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

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x00091AB4 File Offset: 0x0008FCB4
		// (set) Token: 0x06002A4E RID: 10830 RVA: 0x00091ABC File Offset: 0x0008FCBC
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

		// Token: 0x04001A53 RID: 6739
		private IDictionary mapping;

		// Token: 0x04001A54 RID: 6740
		private byte[] program;

		// Token: 0x04001A55 RID: 6741
		private EvalDelegate eval_del;

		// Token: 0x04001A56 RID: 6742
		private string[] namesMapping;

		// Token: 0x04001A57 RID: 6743
		private int gap;
	}
}
