using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000478 RID: 1144
	internal abstract class LinkStack : LinkRef
	{
		// Token: 0x060028E7 RID: 10471 RVA: 0x00085828 File Offset: 0x00083A28
		public LinkStack()
		{
			this.stack = new Stack();
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0008583C File Offset: 0x00083A3C
		public void Push()
		{
			this.stack.Push(this.GetCurrent());
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x00085850 File Offset: 0x00083A50
		public bool Pop()
		{
			if (this.stack.Count > 0)
			{
				this.SetCurrent(this.stack.Pop());
				return true;
			}
			return false;
		}

		// Token: 0x060028EA RID: 10474
		protected abstract object GetCurrent();

		// Token: 0x060028EB RID: 10475
		protected abstract void SetCurrent(object l);

		// Token: 0x040019C7 RID: 6599
		private Stack stack;
	}
}
