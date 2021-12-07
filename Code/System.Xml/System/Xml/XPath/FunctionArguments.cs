using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x0200018F RID: 399
	internal class FunctionArguments
	{
		// Token: 0x060010CC RID: 4300 RVA: 0x0004DD08 File Offset: 0x0004BF08
		public FunctionArguments(Expression arg, FunctionArguments tail)
		{
			this._arg = arg;
			this._tail = tail;
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0004DD20 File Offset: 0x0004BF20
		public Expression Arg
		{
			get
			{
				return this._arg;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0004DD28 File Offset: 0x0004BF28
		public FunctionArguments Tail
		{
			get
			{
				return this._tail;
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0004DD30 File Offset: 0x0004BF30
		public void ToArrayList(ArrayList a)
		{
			FunctionArguments functionArguments = this;
			do
			{
				a.Add(functionArguments._arg);
				functionArguments = functionArguments._tail;
			}
			while (functionArguments != null);
		}

		// Token: 0x04000708 RID: 1800
		protected Expression _arg;

		// Token: 0x04000709 RID: 1801
		protected FunctionArguments _tail;
	}
}
