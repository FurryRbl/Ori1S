using System;
using System.Collections.Specialized;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides name/value-pair configuration information from a configuration section.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E9 RID: 489
	public class NameValueSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
		/// <returns>A configuration object.</returns>
		/// <param name="parent">Parent object.</param>
		/// <param name="context">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010DD RID: 4317 RVA: 0x0002D6DC File Offset: 0x0002B8DC
		public object Create(object parent, object context, XmlNode section)
		{
			return ConfigHelper.GetNameValueCollection(parent as System.Collections.Specialized.NameValueCollection, section, this.KeyAttributeName, this.ValueAttributeName);
		}

		/// <summary>Gets the XML attribute name to use as the key in a key/value pair.</summary>
		/// <returns>A <see cref="T:System.String" /> value containing the name of the key attribute.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0002D704 File Offset: 0x0002B904
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		/// <summary>Gets the XML attribute name to use as the value in a key/value pair.</summary>
		/// <returns>A <see cref="T:System.String" /> value containing the name of the value attribute.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0002D70C File Offset: 0x0002B90C
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}
	}
}
