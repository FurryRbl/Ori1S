﻿using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a callback mechanism that can create an instance of a service on demand.</summary>
	/// <returns>The service specified by <paramref name="serviceType" />, or null if the service could not be created. </returns>
	/// <param name="container">The service container that requested the creation of the service. </param>
	/// <param name="serviceType">The type of service to create. </param>
	// Token: 0x02000501 RID: 1281
	// (Invoke) Token: 0x06002CB0 RID: 11440
	[ComVisible(true)]
	public delegate object ServiceCreatorCallback(IServiceContainer container, Type serviceType);
}
