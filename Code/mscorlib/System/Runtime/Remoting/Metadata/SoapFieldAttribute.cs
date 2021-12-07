using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for a field. This class cannot be inherited.</summary>
	// Token: 0x020004C0 RID: 1216
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class SoapFieldAttribute : SoapAttribute
	{
		/// <summary>You should not use this property; it is not used by the .NET Framework remoting infrastructure.</summary>
		/// <returns>A <see cref="T:System.Int32" />.</returns>
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06003133 RID: 12595 RVA: 0x000A1D5C File Offset: 0x0009FF5C
		// (set) Token: 0x06003134 RID: 12596 RVA: 0x000A1D64 File Offset: 0x0009FF64
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		/// <summary>Gets or sets the XML element name of the field contained in the <see cref="T:System.Runtime.Remoting.Metadata.SoapFieldAttribute" /> attribute.</summary>
		/// <returns>The XML element name of the field contained in this attribute.</returns>
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x000A1D70 File Offset: 0x0009FF70
		// (set) Token: 0x06003136 RID: 12598 RVA: 0x000A1D78 File Offset: 0x0009FF78
		public string XmlElementName
		{
			get
			{
				return this._elementName;
			}
			set
			{
				this._isElement = (value != null);
				this._elementName = value;
			}
		}

		/// <summary>Returns a value indicating whether the current attribute contains interop XML element values.</summary>
		/// <returns>true if the current attribute contains interop XML element values; otherwise, false.</returns>
		// Token: 0x06003137 RID: 12599 RVA: 0x000A1D90 File Offset: 0x0009FF90
		public bool IsInteropXmlElement()
		{
			return this._isElement;
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000A1D98 File Offset: 0x0009FF98
		internal override void SetReflectionObject(object reflectionObject)
		{
			FieldInfo fieldInfo = (FieldInfo)reflectionObject;
			if (this._elementName == null)
			{
				this._elementName = fieldInfo.Name;
			}
		}

		// Token: 0x040014D1 RID: 5329
		private int _order;

		// Token: 0x040014D2 RID: 5330
		private string _elementName;

		// Token: 0x040014D3 RID: 5331
		private bool _isElement;
	}
}
