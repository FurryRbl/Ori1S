using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Represents a collection of <see cref="T:System.Net.Mail.AlternateView" /> objects.</summary>
	// Token: 0x02000333 RID: 819
	public sealed class AlternateViewCollection : Collection<AlternateView>, IDisposable
	{
		// Token: 0x06001D0E RID: 7438 RVA: 0x00056484 File Offset: 0x00054684
		internal AlternateViewCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AlternateViewCollection" />.</summary>
		// Token: 0x06001D0F RID: 7439 RVA: 0x0005648C File Offset: 0x0005468C
		public void Dispose()
		{
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x00056490 File Offset: 0x00054690
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00056498 File Offset: 0x00054698
		protected override void InsertItem(int index, AlternateView item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000564A4 File Offset: 0x000546A4
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000564B0 File Offset: 0x000546B0
		protected override void SetItem(int index, AlternateView item)
		{
			base.SetItem(index, item);
		}
	}
}
