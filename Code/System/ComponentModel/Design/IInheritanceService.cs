using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides methods for identifying the components of a component.</summary>
	// Token: 0x02000119 RID: 281
	public interface IInheritanceService
	{
		/// <summary>Searches the specified component for fields that implement the <see cref="T:System.ComponentModel.IComponent" /> interface and adds each to the specified container, storing the inheritance level of each which can be retrieved using the <see cref="M:System.ComponentModel.Design.IInheritanceService.GetInheritanceAttribute(System.ComponentModel.IComponent)" /> method.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to search. Searching begins with this component. </param>
		/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to add components to. </param>
		// Token: 0x06000AFD RID: 2813
		void AddInheritedComponents(IComponent component, IContainer container);

		/// <summary>Gets the inheritance attribute for the specified component.</summary>
		/// <returns>An instance of <see cref="T:System.ComponentModel.InheritanceAttribute" /> that describes the level of inheritance of the specified component.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> for which to retrieve the inheritance attribute. </param>
		// Token: 0x06000AFE RID: 2814
		InheritanceAttribute GetInheritanceAttribute(IComponent component);
	}
}
