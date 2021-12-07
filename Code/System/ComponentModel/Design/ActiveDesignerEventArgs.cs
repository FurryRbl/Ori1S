using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="P:System.ComponentModel.Design.IDesignerEventService.ActiveDesigner" /> event.</summary>
	// Token: 0x020000F3 RID: 243
	public class ActiveDesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ActiveDesignerEventArgs" /> class.</summary>
		/// <param name="oldDesigner">The document that is losing activation. </param>
		/// <param name="newDesigner">The document that is gaining activation. </param>
		// Token: 0x06000A03 RID: 2563 RVA: 0x0001CB10 File Offset: 0x0001AD10
		public ActiveDesignerEventArgs(IDesignerHost oldDesigner, IDesignerHost newDesigner)
		{
			this.oldDesigner = oldDesigner;
			this.newDesigner = newDesigner;
		}

		/// <summary>Gets the document that is gaining activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document gaining activation.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0001CB28 File Offset: 0x0001AD28
		public IDesignerHost NewDesigner
		{
			get
			{
				return this.newDesigner;
			}
		}

		/// <summary>Gets the document that is losing activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document losing activation.</returns>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0001CB30 File Offset: 0x0001AD30
		public IDesignerHost OldDesigner
		{
			get
			{
				return this.oldDesigner;
			}
		}

		// Token: 0x040002A8 RID: 680
		private IDesignerHost oldDesigner;

		// Token: 0x040002A9 RID: 681
		private IDesignerHost newDesigner;
	}
}
