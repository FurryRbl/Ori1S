using System;

namespace System.ComponentModel
{
	/// <summary>Provides the base class for a custom component editor.</summary>
	// Token: 0x020000DF RID: 223
	public abstract class ComponentEditor
	{
		/// <summary>Edits the component and returns a value indicating whether the component was modified.</summary>
		/// <returns>true if the component was modified; otherwise, false.</returns>
		/// <param name="component">The component to be edited. </param>
		// Token: 0x06000976 RID: 2422 RVA: 0x0001B760 File Offset: 0x00019960
		public bool EditComponent(object component)
		{
			return this.EditComponent(null, component);
		}

		/// <summary>Edits the component and returns a value indicating whether the component was modified based upon a given context.</summary>
		/// <returns>true if the component was modified; otherwise, false.</returns>
		/// <param name="context">An optional context object that can be used to obtain further information about the edit. </param>
		/// <param name="component">The component to be edited. </param>
		// Token: 0x06000977 RID: 2423
		public abstract bool EditComponent(ITypeDescriptorContext context, object component);
	}
}
