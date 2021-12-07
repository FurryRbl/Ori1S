﻿using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Enables enumeration of components at design time.</summary>
	// Token: 0x0200010D RID: 269
	public interface IComponentDiscoveryService
	{
		/// <summary>Gets the list of available component types.</summary>
		/// <returns>The list of available component types.</returns>
		/// <param name="designerHost">The designer host providing design-time services. Can be null.</param>
		/// <param name="baseType">The base type specifying the components to retrieve. Can be null.</param>
		// Token: 0x06000AB3 RID: 2739
		ICollection GetComponentTypes(IDesignerHost designerHost, Type baseType);
	}
}
