using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides the basic framework for building a custom designer.</summary>
	// Token: 0x0200010F RID: 271
	[ComVisible(true)]
	public interface IDesigner : IDisposable
	{
		/// <summary>Gets the base component that this designer is designing.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IComponent" /> indicating the base component that this designer is designing.</returns>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AB6 RID: 2742
		IComponent Component { get; }

		/// <summary>Gets a collection of the design-time verbs supported by the designer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> that contains the verbs supported by the designer, or null if the component has no verbs.</returns>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000AB7 RID: 2743
		DesignerVerbCollection Verbs { get; }

		/// <summary>Performs the default action for this designer.</summary>
		// Token: 0x06000AB8 RID: 2744
		void DoDefaultAction();

		/// <summary>Initializes the designer with the specified component.</summary>
		/// <param name="component">The component to associate with this designer. </param>
		// Token: 0x06000AB9 RID: 2745
		void Initialize(IComponent component);
	}
}
