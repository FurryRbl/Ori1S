using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides key/value pair configuration information from a configuration section.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DD RID: 477
	public class DictionarySectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <returns>A configuration object.</returns>
		/// <param name="parent">Parent object.</param>
		/// <param name="context">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010AE RID: 4270 RVA: 0x0002D38C File Offset: 0x0002B58C
		public virtual object Create(object parent, object context, XmlNode section)
		{
			return ConfigHelper.GetDictionary(parent as IDictionary, section, this.KeyAttributeName, this.ValueAttributeName);
		}

		/// <summary>Gets the XML attribute name to use as the key in a key/value pair.</summary>
		/// <returns>A string value containing the name of the key attribute.</returns>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x0002D3B4 File Offset: 0x0002B5B4
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		/// <summary>Gets the XML attribute name to use as the value in a key/value pair.</summary>
		/// <returns>A string value containing the name of the value attribute.</returns>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x0002D3BC File Offset: 0x0002B5BC
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}
	}
}
