using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for target types. This class cannot be inherited.</summary>
	// Token: 0x020004C4 RID: 1220
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public sealed class SoapTypeAttribute : SoapAttribute
	{
		/// <summary>Gets or sets a <see cref="T:System.Runtime.Remoting.Metadata.SoapOption" /> configuration value.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.SoapOption" /> value.</returns>
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x000A1EF4 File Offset: 0x000A00F4
		// (set) Token: 0x0600314A RID: 12618 RVA: 0x000A1EFC File Offset: 0x000A00FC
		public SoapOption SoapOptions
		{
			get
			{
				return this._soapOption;
			}
			set
			{
				this._soapOption = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>The current implementation always returns false.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">An attempt was made to set the current property. </exception>
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x000A1F08 File Offset: 0x000A0108
		// (set) Token: 0x0600314C RID: 12620 RVA: 0x000A1F10 File Offset: 0x000A0110
		public override bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
			set
			{
				this._useAttribute = value;
			}
		}

		/// <summary>Gets or sets the XML element name.</summary>
		/// <returns>The XML element name.</returns>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600314D RID: 12621 RVA: 0x000A1F1C File Offset: 0x000A011C
		// (set) Token: 0x0600314E RID: 12622 RVA: 0x000A1F24 File Offset: 0x000A0124
		public string XmlElementName
		{
			get
			{
				return this._xmlElementName;
			}
			set
			{
				this._isElement = (value != null);
				this._xmlElementName = value;
			}
		}

		/// <summary>You should not use this property; it is not used by the .NET Framework remoting infrastructure.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.XmlFieldOrderOption" />.</returns>
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x000A1F3C File Offset: 0x000A013C
		// (set) Token: 0x06003150 RID: 12624 RVA: 0x000A1F44 File Offset: 0x000A0144
		public XmlFieldOrderOption XmlFieldOrder
		{
			get
			{
				return this._xmlFieldOrder;
			}
			set
			{
				this._xmlFieldOrder = value;
			}
		}

		/// <summary>Gets or sets the XML namespace that is used during serialization of the target object type.</summary>
		/// <returns>The XML namespace that is used during serialization of the target object type.</returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000A1F50 File Offset: 0x000A0150
		// (set) Token: 0x06003152 RID: 12626 RVA: 0x000A1F58 File Offset: 0x000A0158
		public override string XmlNamespace
		{
			get
			{
				return this._xmlNamespace;
			}
			set
			{
				this._isElement = (value != null);
				this._xmlNamespace = value;
			}
		}

		/// <summary>Gets or sets the XML type name for the target object type.</summary>
		/// <returns>The XML type name for the target object type.</returns>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x000A1F70 File Offset: 0x000A0170
		// (set) Token: 0x06003154 RID: 12628 RVA: 0x000A1F78 File Offset: 0x000A0178
		public string XmlTypeName
		{
			get
			{
				return this._xmlTypeName;
			}
			set
			{
				this._isType = (value != null);
				this._xmlTypeName = value;
			}
		}

		/// <summary>Gets or sets the XML type namespace for the current object type.</summary>
		/// <returns>The XML type namespace for the current object type.</returns>
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x000A1F90 File Offset: 0x000A0190
		// (set) Token: 0x06003156 RID: 12630 RVA: 0x000A1F98 File Offset: 0x000A0198
		public string XmlTypeNamespace
		{
			get
			{
				return this._xmlTypeNamespace;
			}
			set
			{
				this._isType = (value != null);
				this._xmlTypeNamespace = value;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06003157 RID: 12631 RVA: 0x000A1FB0 File Offset: 0x000A01B0
		internal bool IsInteropXmlElement
		{
			get
			{
				return this._isElement;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000A1FB8 File Offset: 0x000A01B8
		internal bool IsInteropXmlType
		{
			get
			{
				return this._isType;
			}
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000A1FC0 File Offset: 0x000A01C0
		internal override void SetReflectionObject(object reflectionObject)
		{
			Type type = (Type)reflectionObject;
			if (this._xmlElementName == null)
			{
				this._xmlElementName = type.Name;
			}
			if (this._xmlTypeName == null)
			{
				this._xmlTypeName = type.Name;
			}
			if (this._xmlTypeNamespace == null)
			{
				string assemblyName;
				if (type.Assembly == typeof(object).Assembly)
				{
					assemblyName = string.Empty;
				}
				else
				{
					assemblyName = type.Assembly.GetName().Name;
				}
				this._xmlTypeNamespace = SoapServices.CodeXmlNamespaceForClrTypeNamespace(type.Namespace, assemblyName);
			}
			if (this._xmlNamespace == null)
			{
				this._xmlNamespace = this._xmlTypeNamespace;
			}
		}

		// Token: 0x040014E1 RID: 5345
		private SoapOption _soapOption;

		// Token: 0x040014E2 RID: 5346
		private bool _useAttribute;

		// Token: 0x040014E3 RID: 5347
		private string _xmlElementName;

		// Token: 0x040014E4 RID: 5348
		private XmlFieldOrderOption _xmlFieldOrder;

		// Token: 0x040014E5 RID: 5349
		private string _xmlNamespace;

		// Token: 0x040014E6 RID: 5350
		private string _xmlTypeName;

		// Token: 0x040014E7 RID: 5351
		private string _xmlTypeNamespace;

		// Token: 0x040014E8 RID: 5352
		private bool _isType;

		// Token: 0x040014E9 RID: 5353
		private bool _isElement;
	}
}
