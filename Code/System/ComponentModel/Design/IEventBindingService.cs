using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a service for registering event handlers for component events.</summary>
	// Token: 0x02000115 RID: 277
	[ComVisible(true)]
	public interface IEventBindingService
	{
		/// <summary>Creates a unique name for an event-handler method for the specified component and event.</summary>
		/// <returns>The recommended name for the event-handler method for this event.</returns>
		/// <param name="component">The component instance the event is connected to. </param>
		/// <param name="e">The event to create a name for. </param>
		// Token: 0x06000AEB RID: 2795
		string CreateUniqueMethodName(IComponent component, EventDescriptor e);

		/// <summary>Gets a collection of event-handler methods that have a method signature compatible with the specified event.</summary>
		/// <returns>A collection of strings.</returns>
		/// <param name="e">The event to get the compatible event-handler methods for. </param>
		// Token: 0x06000AEC RID: 2796
		ICollection GetCompatibleMethods(EventDescriptor e);

		/// <summary>Gets an <see cref="T:System.ComponentModel.EventDescriptor" /> for the event that the specified property descriptor represents, if it represents an event.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> for the event that the property represents, or null if the property does not represent an event.</returns>
		/// <param name="property">The property that represents an event. </param>
		// Token: 0x06000AED RID: 2797
		EventDescriptor GetEvent(PropertyDescriptor property);

		/// <summary>Converts a set of event descriptors to a set of property descriptors.</summary>
		/// <returns>An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that describe the event set.</returns>
		/// <param name="events">The events to convert to properties. </param>
		// Token: 0x06000AEE RID: 2798
		PropertyDescriptorCollection GetEventProperties(EventDescriptorCollection events);

		/// <summary>Converts a single event descriptor to a property descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the event.</returns>
		/// <param name="e">The event to convert. </param>
		// Token: 0x06000AEF RID: 2799
		PropertyDescriptor GetEventProperty(EventDescriptor e);

		/// <summary>Displays the user code for the designer.</summary>
		/// <returns>true if the code is displayed; otherwise, false.</returns>
		// Token: 0x06000AF0 RID: 2800
		bool ShowCode();

		/// <summary>Displays the user code for the designer at the specified line.</summary>
		/// <returns>true if the code is displayed; otherwise, false.</returns>
		/// <param name="lineNumber">The line number to place the caret on. </param>
		// Token: 0x06000AF1 RID: 2801
		bool ShowCode(int lineNumber);

		/// <summary>Displays the user code for the specified event.</summary>
		/// <returns>true if the code is displayed; otherwise, false.</returns>
		/// <param name="component">The component that the event is connected to. </param>
		/// <param name="e">The event to display. </param>
		// Token: 0x06000AF2 RID: 2802
		bool ShowCode(IComponent component, EventDescriptor e);
	}
}
