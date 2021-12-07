using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores attachments to be sent as part of an e-mail message.</summary>
	// Token: 0x02000337 RID: 823
	public sealed class AttachmentCollection : Collection<Attachment>, IDisposable
	{
		// Token: 0x06001D33 RID: 7475 RVA: 0x00058B48 File Offset: 0x00056D48
		internal AttachmentCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AttachmentCollection" />. </summary>
		// Token: 0x06001D34 RID: 7476 RVA: 0x00058B50 File Offset: 0x00056D50
		public void Dispose()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].Dispose();
			}
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x00058B80 File Offset: 0x00056D80
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x00058B88 File Offset: 0x00056D88
		protected override void InsertItem(int index, Attachment item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x00058B94 File Offset: 0x00056D94
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x00058BA0 File Offset: 0x00056DA0
		protected override void SetItem(int index, Attachment item)
		{
			base.SetItem(index, item);
		}
	}
}
