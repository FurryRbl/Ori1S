using System;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x0200004E RID: 78
	public sealed class ParameterExpression : Expression
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x000141D0 File Offset: 0x000123D0
		internal ParameterExpression(Type type, string name) : base(ExpressionType.Parameter, type)
		{
			this.name = name;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000141E4 File Offset: 0x000123E4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000141EC File Offset: 0x000123EC
		private void EmitLocalParameter(EmitContext ec, int position)
		{
			ec.ig.Emit(OpCodes.Ldarg, position);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00014200 File Offset: 0x00012400
		private void EmitHoistedLocal(EmitContext ec, int level, int position)
		{
			ec.EmitScope();
			for (int i = 0; i < level; i++)
			{
				ec.EmitParentScope();
			}
			ec.EmitLoadLocals();
			ec.ig.Emit(OpCodes.Ldc_I4, position);
			ec.ig.Emit(OpCodes.Ldelem, typeof(object));
			ec.EmitLoadStrongBoxValue(base.Type);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00014268 File Offset: 0x00012468
		internal override void Emit(EmitContext ec)
		{
			int position = -1;
			if (ec.IsLocalParameter(this, ref position))
			{
				this.EmitLocalParameter(ec, position);
				return;
			}
			int level = 0;
			if (ec.IsHoistedLocal(this, ref level, ref position))
			{
				this.EmitHoistedLocal(ec, level, position);
				return;
			}
			throw new InvalidOperationException("Parameter out of scope");
		}

		// Token: 0x04000118 RID: 280
		private string name;
	}
}
