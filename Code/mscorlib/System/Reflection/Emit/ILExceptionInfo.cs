using System;

namespace System.Reflection.Emit
{
	// Token: 0x020002DD RID: 733
	internal struct ILExceptionInfo
	{
		// Token: 0x06002575 RID: 9589 RVA: 0x00083AC8 File Offset: 0x00081CC8
		internal int NumHandlers()
		{
			return this.handlers.Length;
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x00083AD4 File Offset: 0x00081CD4
		internal void AddCatch(Type extype, int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 0;
			this.handlers[num].start = offset;
			this.handlers[num].extype = extype;
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x00083B30 File Offset: 0x00081D30
		internal void AddFinally(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 2;
			this.handlers[num].start = offset;
			this.handlers[num].extype = null;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x00083B8C File Offset: 0x00081D8C
		internal void AddFault(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 4;
			this.handlers[num].start = offset;
			this.handlers[num].extype = null;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x00083BE8 File Offset: 0x00081DE8
		internal void AddFilter(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = -1;
			this.handlers[num].extype = null;
			this.handlers[num].filter_offset = offset;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x00083C44 File Offset: 0x00081E44
		internal void End(int offset)
		{
			if (this.handlers == null)
			{
				return;
			}
			int num = this.handlers.Length - 1;
			if (num >= 0)
			{
				this.handlers[num].len = offset - this.handlers[num].start;
			}
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00083C94 File Offset: 0x00081E94
		internal int LastClauseType()
		{
			if (this.handlers != null)
			{
				return this.handlers[this.handlers.Length - 1].type;
			}
			return 0;
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x00083CC0 File Offset: 0x00081EC0
		internal void PatchFilterClause(int start)
		{
			if (this.handlers != null && this.handlers.Length > 0)
			{
				this.handlers[this.handlers.Length - 1].start = start;
				this.handlers[this.handlers.Length - 1].type = 1;
			}
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x00083D1C File Offset: 0x00081F1C
		internal void Debug(int b)
		{
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00083D20 File Offset: 0x00081F20
		private void add_block(int offset)
		{
			if (this.handlers != null)
			{
				int num = this.handlers.Length;
				ILExceptionBlock[] destinationArray = new ILExceptionBlock[num + 1];
				Array.Copy(this.handlers, destinationArray, num);
				this.handlers = destinationArray;
				this.handlers[num].len = offset - this.handlers[num].start;
			}
			else
			{
				this.handlers = new ILExceptionBlock[1];
				this.len = offset - this.start;
			}
		}

		// Token: 0x04000E0F RID: 3599
		private ILExceptionBlock[] handlers;

		// Token: 0x04000E10 RID: 3600
		internal int start;

		// Token: 0x04000E11 RID: 3601
		private int len;

		// Token: 0x04000E12 RID: 3602
		internal Label end;
	}
}
