using System;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Indicates to the <see cref="T:System.Xml.Serialization.XmlSerializer" /> that the member must be treated as XML text when the class that contains it is serialized or deserialized.</summary>
	// Token: 0x020002B9 RID: 697
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class XmlTextAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlTextAttribute" /> class.</summary>
		// Token: 0x06001D51 RID: 7505 RVA: 0x0009B558 File Offset: 0x00099758
		public XmlTextAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlTextAttribute" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the member to be serialized. </param>
		// Token: 0x06001D52 RID: 7506 RVA: 0x0009B560 File Offset: 0x00099760
		public XmlTextAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets or sets the XML Schema definition language (XSD) data type of the text generated by the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
		/// <returns>An XML Schema (XSD) data type, as defined by the World Wide Web Consortium (www.w3.org) document "XML Schema Part 2: Datatypes".</returns>
		/// <exception cref="T:System.Exception">The XML Schema data type you have specified cannot be mapped to the .NET data type. </exception>
		/// <exception cref="T:System.InvalidOperationException">The XML Schema data type you have specified is invalid for the property and cannot be converted to the member type. </exception>
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0009B570 File Offset: 0x00099770
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x0009B58C File Offset: 0x0009978C
		public string DataType
		{
			get
			{
				if (this.dataType == null)
				{
					return string.Empty;
				}
				return this.dataType;
			}
			set
			{
				this.dataType = value;
			}
		}

		/// <summary>Gets or sets the type of the member.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the member.</returns>
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x0009B598 File Offset: 0x00099798
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x0009B5A0 File Offset: 0x000997A0
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0009B5AC File Offset: 0x000997AC
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("XTXA ");
			KeyHelper.AddField(sb, 1, this.type);
			KeyHelper.AddField(sb, 2, this.dataType);
			sb.Append('|');
		}

		// Token: 0x04000BA2 RID: 2978
		private string dataType;

		// Token: 0x04000BA3 RID: 2979
		private Type type;
	}
}
