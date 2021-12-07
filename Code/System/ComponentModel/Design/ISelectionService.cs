using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for a designer to select components.</summary>
	// Token: 0x0200011E RID: 286
	[ComVisible(true)]
	public interface ISelectionService
	{
		/// <summary>Occurs when the current selection changes.</summary>
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000B10 RID: 2832
		// (remove) Token: 0x06000B11 RID: 2833
		event EventHandler SelectionChanged;

		/// <summary>Occurs when the current selection is about to change.</summary>
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000B12 RID: 2834
		// (remove) Token: 0x06000B13 RID: 2835
		event EventHandler SelectionChanging;

		/// <summary>Gets a value indicating whether the specified component is currently selected.</summary>
		/// <returns>true if the component is part of the user's current selection; otherwise, false.</returns>
		/// <param name="component">The component to test. </param>
		// Token: 0x06000B14 RID: 2836
		bool GetComponentSelected(object component);

		/// <summary>Gets a collection of components that are currently selected.</summary>
		/// <returns>A collection that represents the current set of components that are selected.</returns>
		// Token: 0x06000B15 RID: 2837
		ICollection GetSelectedComponents();

		/// <summary>Selects the components from within the specified collection of components that match the specified selection type.</summary>
		/// <param name="components">The collection of components to select. </param>
		/// <param name="selectionType">A value from the <see cref="T:System.ComponentModel.Design.SelectionTypes" /> enumeration. The default is <see cref="F:System.ComponentModel.Design.SelectionTypes.Normal" />. </param>
		// Token: 0x06000B16 RID: 2838
		void SetSelectedComponents(ICollection components, SelectionTypes selectionType);

		/// <summary>Selects the specified collection of components.</summary>
		/// <param name="components">The collection of components to select. </param>
		// Token: 0x06000B17 RID: 2839
		void SetSelectedComponents(ICollection components);

		/// <summary>Gets the object that is currently the primary selected object.</summary>
		/// <returns>The object that is currently the primary selected object.</returns>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000B18 RID: 2840
		object PrimarySelection { get; }

		/// <summary>Gets the count of selected objects.</summary>
		/// <returns>The number of selected objects.</returns>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000B19 RID: 2841
		int SelectionCount { get; }
	}
}
