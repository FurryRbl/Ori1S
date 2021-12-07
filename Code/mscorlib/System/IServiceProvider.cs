﻿using System;

namespace System
{
	/// <summary>Defines a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000148 RID: 328
	public interface IServiceProvider
	{
		/// <summary>Gets the service object of the specified type.</summary>
		/// <returns>A service object of type <paramref name="serviceType" />.-or- null if there is no service object of type <paramref name="serviceType" />.</returns>
		/// <param name="serviceType">An object that specifies the type of service object to get. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011C6 RID: 4550
		object GetService(Type serviceType);
	}
}
