using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> and <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> events.</summary>
	// Token: 0x020000FF RID: 255
	[ComVisible(true)]
	public class DesignerTransactionCloseEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class. </summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		/// <param name="lastTransaction">true if this is the last transaction to close; otherwise, false.</param>
		// Token: 0x06000A56 RID: 2646 RVA: 0x0001D2C8 File Offset: 0x0001B4C8
		public DesignerTransactionCloseEventArgs(bool commit, bool lastTransaction)
		{
			this.commit = commit;
			this.last_transaction = lastTransaction;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class, using the specified value that indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		// Token: 0x06000A57 RID: 2647 RVA: 0x0001D2E0 File Offset: 0x0001B4E0
		[Obsolete("Use another constructor that indicates lastTransaction")]
		public DesignerTransactionCloseEventArgs(bool commit)
		{
			this.commit = commit;
		}

		/// <summary>Gets a value indicating whether this is the last transaction to close.</summary>
		/// <returns>true, if this is the last transaction to close; otherwise, false. </returns>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0001D2F0 File Offset: 0x0001B4F0
		public bool LastTransaction
		{
			get
			{
				return this.last_transaction;
			}
		}

		/// <summary>Indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <returns>true if the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction; otherwise, false.</returns>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0001D2F8 File Offset: 0x0001B4F8
		public bool TransactionCommitted
		{
			get
			{
				return this.commit;
			}
		}

		// Token: 0x040002C1 RID: 705
		private bool commit;

		// Token: 0x040002C2 RID: 706
		private bool last_transaction;
	}
}
