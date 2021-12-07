using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a way to group a series of design-time actions to improve performance and enable most types of changes to be undone.</summary>
	// Token: 0x02000100 RID: 256
	public abstract class DesignerTransaction : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class with no description.</summary>
		// Token: 0x06000A5A RID: 2650 RVA: 0x0001D300 File Offset: 0x0001B500
		protected DesignerTransaction() : this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class using the specified transaction description.</summary>
		/// <param name="description">A description for this transaction. </param>
		// Token: 0x06000A5B RID: 2651 RVA: 0x0001D310 File Offset: 0x0001B510
		protected DesignerTransaction(string description)
		{
			this.description = description;
			this.committed = false;
			this.canceled = false;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. </summary>
		// Token: 0x06000A5C RID: 2652 RVA: 0x0001D330 File Offset: 0x0001B530
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000A5D RID: 2653 RVA: 0x0001D33C File Offset: 0x0001B53C
		protected virtual void Dispose(bool disposing)
		{
			this.Cancel();
			if (disposing)
			{
				GC.SuppressFinalize(true);
			}
		}

		/// <summary>Raises the Cancel event.</summary>
		// Token: 0x06000A5E RID: 2654
		protected abstract void OnCancel();

		/// <summary>Performs the actual work of committing a transaction.</summary>
		// Token: 0x06000A5F RID: 2655
		protected abstract void OnCommit();

		/// <summary>Cancels the transaction and attempts to roll back the changes made by the events of the transaction.</summary>
		// Token: 0x06000A60 RID: 2656 RVA: 0x0001D358 File Offset: 0x0001B558
		public void Cancel()
		{
			if (!this.Canceled && !this.Committed)
			{
				this.canceled = true;
				this.OnCancel();
			}
		}

		/// <summary>Commits this transaction.</summary>
		// Token: 0x06000A61 RID: 2657 RVA: 0x0001D380 File Offset: 0x0001B580
		public void Commit()
		{
			if (!this.Canceled && !this.Committed)
			{
				this.committed = true;
				this.OnCommit();
			}
		}

		/// <summary>Gets a value indicating whether the transaction was canceled.</summary>
		/// <returns>true if the transaction was canceled; otherwise, false.</returns>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		public bool Canceled
		{
			get
			{
				return this.canceled;
			}
		}

		/// <summary>Gets a value indicating whether the transaction was committed.</summary>
		/// <returns>true if the transaction was committed; otherwise, false.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		public bool Committed
		{
			get
			{
				return this.committed;
			}
		}

		/// <summary>Gets a description for the transaction.</summary>
		/// <returns>A description for the transaction.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0001D3B8 File Offset: 0x0001B5B8
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		/// <summary>Releases the resources associated with this object. This override commits this transaction if it was not already committed.</summary>
		// Token: 0x06000A65 RID: 2661 RVA: 0x0001D3C0 File Offset: 0x0001B5C0
		~DesignerTransaction()
		{
			this.Dispose(false);
		}

		// Token: 0x040002C3 RID: 707
		private string description;

		// Token: 0x040002C4 RID: 708
		private bool committed;

		// Token: 0x040002C5 RID: 709
		private bool canceled;
	}
}
