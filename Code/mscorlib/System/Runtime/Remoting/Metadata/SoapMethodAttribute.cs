using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for a method. This class cannot be inherited.</summary>
	// Token: 0x020004C1 RID: 1217
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	public sealed class SoapMethodAttribute : SoapAttribute
	{
		/// <summary>Gets or sets the XML element name to use for the method response to the target method.</summary>
		/// <returns>The XML element name to use for the method response to the target method.</returns>
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x000A1DCC File Offset: 0x0009FFCC
		// (set) Token: 0x0600313B RID: 12603 RVA: 0x000A1DD4 File Offset: 0x0009FFD4
		public string ResponseXmlElementName
		{
			get
			{
				return this._responseElement;
			}
			set
			{
				this._responseElement = value;
			}
		}

		/// <summary>Gets or sets the XML element namesapce used for method response to the target method.</summary>
		/// <returns>The XML element namesapce used for method response to the target method.</returns>
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x000A1DE0 File Offset: 0x0009FFE0
		// (set) Token: 0x0600313D RID: 12605 RVA: 0x000A1DE8 File Offset: 0x0009FFE8
		public string ResponseXmlNamespace
		{
			get
			{
				return this._responseNamespace;
			}
			set
			{
				this._responseNamespace = value;
			}
		}

		/// <summary>Gets or sets the XML element name used for the return value from the target method.</summary>
		/// <returns>The XML element name used for the return value from the target method.</returns>
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x0600313E RID: 12606 RVA: 0x000A1DF4 File Offset: 0x0009FFF4
		// (set) Token: 0x0600313F RID: 12607 RVA: 0x000A1DFC File Offset: 0x0009FFFC
		public string ReturnXmlElementName
		{
			get
			{
				return this._returnElement;
			}
			set
			{
				this._returnElement = value;
			}
		}

		/// <summary>Gets or sets the SOAPAction header field used with HTTP requests sent with this method. This property is currently not implemented.</summary>
		/// <returns>The SOAPAction header field used with HTTP requests sent with this method.</returns>
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x000A1E08 File Offset: 0x000A0008
		// (set) Token: 0x06003141 RID: 12609 RVA: 0x000A1E10 File Offset: 0x000A0010
		public string SoapAction
		{
			get
			{
				return this._soapAction;
			}
			set
			{
				this._soapAction = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>The current implementation always returns false.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">An attempt was made to set the current property. </exception>
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000A1E1C File Offset: 0x000A001C
		// (set) Token: 0x06003143 RID: 12611 RVA: 0x000A1E24 File Offset: 0x000A0024
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

		/// <summary>Gets or sets the XML namespace that is used during serialization of remote method calls of the target method.</summary>
		/// <returns>The XML namespace that is used during serialization of remote method calls of the target method.</returns>
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x000A1E30 File Offset: 0x000A0030
		// (set) Token: 0x06003145 RID: 12613 RVA: 0x000A1E38 File Offset: 0x000A0038
		public override string XmlNamespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000A1E44 File Offset: 0x000A0044
		internal override void SetReflectionObject(object reflectionObject)
		{
			MethodBase methodBase = (MethodBase)reflectionObject;
			if (this._responseElement == null)
			{
				this._responseElement = methodBase.Name + "Response";
			}
			if (this._responseNamespace == null)
			{
				this._responseNamespace = SoapServices.GetXmlNamespaceForMethodResponse(methodBase);
			}
			if (this._returnElement == null)
			{
				this._returnElement = "return";
			}
			if (this._soapAction == null)
			{
				this._soapAction = SoapServices.GetXmlNamespaceForMethodCall(methodBase) + "#" + methodBase.Name;
			}
			if (this._namespace == null)
			{
				this._namespace = SoapServices.GetXmlNamespaceForMethodCall(methodBase);
			}
		}

		// Token: 0x040014D4 RID: 5332
		private string _responseElement;

		// Token: 0x040014D5 RID: 5333
		private string _responseNamespace;

		// Token: 0x040014D6 RID: 5334
		private string _returnElement;

		// Token: 0x040014D7 RID: 5335
		private string _soapAction;

		// Token: 0x040014D8 RID: 5336
		private bool _useAttribute;

		// Token: 0x040014D9 RID: 5337
		private string _namespace;
	}
}
