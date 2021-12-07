using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface to modify the set of member descriptors for a component in design mode.</summary>
	// Token: 0x02000121 RID: 289
	public interface ITypeDescriptorFilterService
	{
		/// <summary>Filters the attributes that a component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <returns>true if the set of filtered attributes is to be cached; false if the filter service must query again.</returns>
		/// <param name="component">The component to filter the attributes of. </param>
		/// <param name="attributes">A dictionary of attributes that can be modified. </param>
		// Token: 0x06000B22 RID: 2850
		bool FilterAttributes(IComponent component, IDictionary attributes);

		/// <summary>Filters the events that a component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <returns>true if the set of filtered events is to be cached; false if the filter service must query again.</returns>
		/// <param name="component">The component to filter events for. </param>
		/// <param name="events">A dictionary of events that can be modified. </param>
		// Token: 0x06000B23 RID: 2851
		bool FilterEvents(IComponent component, IDictionary events);

		/// <summary>Filters the properties that a component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <returns>true if the set of filtered properties is to be cached; false if the filter service must query again.</returns>
		/// <param name="component">The component to filter properties for. </param>
		/// <param name="properties">A dictionary of properties that can be modified. </param>
		// Token: 0x06000B24 RID: 2852
		bool FilterProperties(IComponent component, IDictionary properties);
	}
}
