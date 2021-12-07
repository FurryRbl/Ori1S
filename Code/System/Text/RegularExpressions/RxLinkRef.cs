using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000492 RID: 1170
	internal class RxLinkRef : LinkRef
	{
		// Token: 0x06002A17 RID: 10775 RVA: 0x0009032C File Offset: 0x0008E52C
		public RxLinkRef()
		{
			this.offsets = new int[8];
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x00090340 File Offset: 0x0008E540
		public void PushInstructionBase(int offset)
		{
			if ((this.current & 1) != 0)
			{
				throw new Exception();
			}
			if (this.current == this.offsets.Length)
			{
				int[] destinationArray = new int[this.offsets.Length * 2];
				Array.Copy(this.offsets, destinationArray, this.offsets.Length);
				this.offsets = destinationArray;
			}
			this.offsets[this.current++] = offset;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000903B8 File Offset: 0x0008E5B8
		public void PushOffsetPosition(int offset)
		{
			if ((this.current & 1) == 0)
			{
				throw new Exception();
			}
			this.offsets[this.current++] = offset;
		}

		// Token: 0x04001A4F RID: 6735
		public int[] offsets;

		// Token: 0x04001A50 RID: 6736
		public int current;
	}
}
