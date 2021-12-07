using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x02000038 RID: 56
	public sealed class ElementInit
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x000112DC File Offset: 0x0000F4DC
		internal ElementInit(MethodInfo add_method, ReadOnlyCollection<Expression> arguments)
		{
			this.add_method = add_method;
			this.arguments = arguments;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000112F4 File Offset: 0x0000F4F4
		public MethodInfo AddMethod
		{
			get
			{
				return this.add_method;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x000112FC File Offset: 0x0000F4FC
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00011304 File Offset: 0x0000F504
		public override string ToString()
		{
			return ExpressionPrinter.ToString(this);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001130C File Offset: 0x0000F50C
		private void EmitPopIfNeeded(EmitContext ec)
		{
			if (this.add_method.ReturnType == typeof(void))
			{
				return;
			}
			ec.ig.Emit(OpCodes.Pop);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001133C File Offset: 0x0000F53C
		internal void Emit(EmitContext ec, LocalBuilder local)
		{
			ec.EmitCall(local, this.arguments, this.add_method);
			this.EmitPopIfNeeded(ec);
		}

		// Token: 0x040000BD RID: 189
		private MethodInfo add_method;

		// Token: 0x040000BE RID: 190
		private ReadOnlyCollection<Expression> arguments;
	}
}
