using System;

namespace System.IO
{
	/// <summary>Contains information on the change that occurred.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002AA RID: 682
	public struct WaitForChangedResult
	{
		/// <summary>Gets or sets the type of change that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x0004146C File Offset: 0x0003F66C
		// (set) Token: 0x060017BE RID: 6078 RVA: 0x00041474 File Offset: 0x0003F674
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this.changeType;
			}
			set
			{
				this.changeType = value;
			}
		}

		/// <summary>Gets or sets the name of the file or directory that changed.</summary>
		/// <returns>The name of the file or directory that changed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00041480 File Offset: 0x0003F680
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x00041488 File Offset: 0x0003F688
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the original name of the file or directory that was renamed.</summary>
		/// <returns>The original name of the file or directory that was renamed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00041494 File Offset: 0x0003F694
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x0004149C File Offset: 0x0003F69C
		public string OldName
		{
			get
			{
				return this.oldName;
			}
			set
			{
				this.oldName = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the wait operation timed out.</summary>
		/// <returns>true if the <see cref="M:System.IO.FileSystemWatcher.WaitForChanged(System.IO.WatcherChangeTypes)" /> method timed out; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x000414A8 File Offset: 0x0003F6A8
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x000414B0 File Offset: 0x0003F6B0
		public bool TimedOut
		{
			get
			{
				return this.timedOut;
			}
			set
			{
				this.timedOut = value;
			}
		}

		// Token: 0x04000F1E RID: 3870
		private WatcherChangeTypes changeType;

		// Token: 0x04000F1F RID: 3871
		private string name;

		// Token: 0x04000F20 RID: 3872
		private string oldName;

		// Token: 0x04000F21 RID: 3873
		private bool timedOut;
	}
}
