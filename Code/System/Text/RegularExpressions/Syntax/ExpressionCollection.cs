using System;
using System.Collections;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000497 RID: 1175
	internal class ExpressionCollection : CollectionBase
	{
		// Token: 0x06002A5A RID: 10842 RVA: 0x00091FEC File Offset: 0x000901EC
		public void Add(Expression e)
		{
			base.List.Add(e);
		}

		// Token: 0x17000BA3 RID: 2979
		public Expression this[int i]
		{
			get
			{
				return (Expression)base.List[i];
			}
			set
			{
				base.List[i] = value;
			}
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x00092020 File Offset: 0x00090220
		protected override void OnValidate(object o)
		{
		}
	}
}
