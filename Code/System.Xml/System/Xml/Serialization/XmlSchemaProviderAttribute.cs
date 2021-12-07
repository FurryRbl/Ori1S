using System;

namespace System.Xml.Serialization
{
	/// <summary>When applied to a type, stores the name of a static method of the type that returns an XML schema and a <see cref="T:System.Xml.XmlQualifiedName" /> (or <see cref="T:System.Xml.Schema.XmlSchemaType" /> for anonymous types) that controls the serialization of the type.</summary>
	// Token: 0x020002A2 RID: 674
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class XmlSchemaProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> class, taking the name of the static method that supplies the type's XML schema.</summary>
		/// <param name="methodName">The name of the static method that must be implemented.</param>
		// Token: 0x06001BAB RID: 7083 RVA: 0x00093594 File Offset: 0x00091794
		public XmlSchemaProviderAttribute(string methodName)
		{
			this._methodName = methodName;
		}

		/// <summary>Gets the name of the static method that supplies the type's XML schema and the name of its XML Schema data type.</summary>
		/// <returns>The name of the method that is invoked by the XML infrastructure to return an XML schema.</returns>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x000935A4 File Offset: 0x000917A4
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		/// <summary>Gets or sets a value that determines whether the target class is a wildcard, or that the schema for the class has contains only an xs:any element.</summary>
		/// <returns>true, if the class is a wildcard, or if the schema contains only the xs:any element; otherwise, false.</returns>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x000935AC File Offset: 0x000917AC
		// (set) Token: 0x06001BAE RID: 7086 RVA: 0x000935B4 File Offset: 0x000917B4
		public bool IsAny
		{
			get
			{
				return this._isAny;
			}
			set
			{
				this._isAny = value;
			}
		}

		// Token: 0x04000B3A RID: 2874
		private string _methodName;

		// Token: 0x04000B3B RID: 2875
		private bool _isAny;
	}
}
