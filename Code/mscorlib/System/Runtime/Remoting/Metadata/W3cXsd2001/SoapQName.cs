using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD QName type.</summary>
	// Token: 0x020004E2 RID: 1250
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapQName : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class.</summary>
		// Token: 0x06003239 RID: 12857 RVA: 0x000A3374 File Offset: 0x000A1574
		public SoapQName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the local part of a qualified name.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains the local part of a qualified name. </param>
		// Token: 0x0600323A RID: 12858 RVA: 0x000A337C File Offset: 0x000A157C
		public SoapQName(string value)
		{
			this._name = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the namespace alias and the local part of a qualified name.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the namespace alias of a qualified name. </param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the local part of a qualified name. </param>
		// Token: 0x0600323B RID: 12859 RVA: 0x000A338C File Offset: 0x000A158C
		public SoapQName(string key, string name)
		{
			this._key = key;
			this._name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> class with the namespace alias, the local part of a qualified name, and the namespace that is referenced by the alias.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that contains the namespace alias of a qualified name. </param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the local part of a qualified name. </param>
		/// <param name="namespaceValue">A <see cref="T:System.String" /> that contains the namespace that is referenced by <paramref name="key" />. </param>
		// Token: 0x0600323C RID: 12860 RVA: 0x000A33A4 File Offset: 0x000A15A4
		public SoapQName(string key, string name, string namespaceValue)
		{
			this._key = key;
			this._name = name;
			this._namespace = namespaceValue;
		}

		/// <summary>Gets or sets the namespace alias of a qualified name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace alias of a qualified name.</returns>
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000A33C4 File Offset: 0x000A15C4
		// (set) Token: 0x0600323E RID: 12862 RVA: 0x000A33CC File Offset: 0x000A15CC
		public string Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		/// <summary>Gets or sets the name portion of a qualified name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name portion of a qualified name.</returns>
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000A33D8 File Offset: 0x000A15D8
		// (set) Token: 0x06003240 RID: 12864 RVA: 0x000A33E0 File Offset: 0x000A15E0
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets or sets the namespace that is referenced by <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace that is referenced by <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" />.</returns>
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000A33EC File Offset: 0x000A15EC
		// (set) Token: 0x06003242 RID: 12866 RVA: 0x000A33F4 File Offset: 0x000A15F4
		public string Namespace
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

		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000A3400 File Offset: 0x000A1600
		public static string XsdType
		{
			get
			{
				return "QName";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the XSD of the current SOAP type.</returns>
		// Token: 0x06003244 RID: 12868 RVA: 0x000A3408 File Offset: 0x000A1608
		public string GetXsdType()
		{
			return SoapQName.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		// Token: 0x06003245 RID: 12869 RVA: 0x000A3410 File Offset: 0x000A1610
		public static SoapQName Parse(string value)
		{
			SoapQName soapQName = new SoapQName();
			int num = value.IndexOf(':');
			if (num != -1)
			{
				soapQName.Key = value.Substring(0, num);
				soapQName.Name = value.Substring(num + 1);
			}
			else
			{
				soapQName.Name = value;
			}
			return soapQName;
		}

		/// <summary>Returns the qualified name as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> in the format " <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> : <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" /> ". If <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Key" /> is not specified, this method returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapQName.Name" />.</returns>
		// Token: 0x06003246 RID: 12870 RVA: 0x000A3460 File Offset: 0x000A1660
		public override string ToString()
		{
			if (this._key == null || this._key == string.Empty)
			{
				return this._name;
			}
			return this._key + ":" + this._name;
		}

		// Token: 0x04001511 RID: 5393
		private string _name;

		// Token: 0x04001512 RID: 5394
		private string _key;

		// Token: 0x04001513 RID: 5395
		private string _namespace;
	}
}
