using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides event notifications when root designers are added and removed, when a selected component changes, and when the current root designer changes.</summary>
	// Token: 0x02000110 RID: 272
	public interface IDesignerEventService
	{
		/// <summary>Occurs when the current root designer changes.</summary>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000ABA RID: 2746
		// (remove) Token: 0x06000ABB RID: 2747
		event ActiveDesignerEventHandler ActiveDesignerChanged;

		/// <summary>Occurs when a root designer is created.</summary>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000ABC RID: 2748
		// (remove) Token: 0x06000ABD RID: 2749
		event DesignerEventHandler DesignerCreated;

		/// <summary>Occurs when a root designer for a document is disposed.</summary>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000ABE RID: 2750
		// (remove) Token: 0x06000ABF RID: 2751
		event DesignerEventHandler DesignerDisposed;

		/// <summary>Occurs when the current design-view selection changes.</summary>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000AC0 RID: 2752
		// (remove) Token: 0x06000AC1 RID: 2753
		event EventHandler SelectionChanged;

		/// <summary>Gets the root designer for the currently active document.</summary>
		/// <returns>The currently active document, or null if there is no active document.</returns>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000AC2 RID: 2754
		IDesignerHost ActiveDesigner { get; }

		/// <summary>Gets a collection of root designers for design documents that are currently active in the development environment.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerCollection" /> containing the root designers that have been created and not yet disposed.</returns>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000AC3 RID: 2755
		DesignerCollection Designers { get; }
	}
}
