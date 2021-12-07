﻿using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides a legacy section-handler definition for configuration sections that are not handled by the <see cref="N:System.Configuration" /> types.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E3 RID: 483
	public class IgnoreSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds the specified configuration object to the section-handler collection.</summary>
		/// <returns>The created configuration handler object.</returns>
		/// <param name="parent">The configuration settings in a corresponding parent configuration section. </param>
		/// <param name="configContext">The virtual path for which the configuration section handler computes configuration values. Normally this parameter is reserved and is null. </param>
		/// <param name="section">An <see cref="T:System.Xml.XmlNode" /> that contains the configuration information to be handled. Provides direct access to the XML contents of the configuration section. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010C1 RID: 4289 RVA: 0x0002D478 File Offset: 0x0002B678
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return null;
		}
	}
}
