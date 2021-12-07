﻿using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Handles the access to certain configuration sections.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DF RID: 479
	public interface IConfigurationSectionHandler
	{
		/// <summary>Creates a configuration section handler.</summary>
		/// <returns>The created section handler object.</returns>
		/// <param name="parent">Parent object.</param>
		/// <param name="configContext">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B4 RID: 4276
		object Create(object parent, object configContext, XmlNode section);
	}
}
