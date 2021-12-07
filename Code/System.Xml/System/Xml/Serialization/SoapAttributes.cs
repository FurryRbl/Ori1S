using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of attribute objects that control how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes and deserializes SOAP methods.</summary>
	// Token: 0x0200026D RID: 621
	public class SoapAttributes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapAttributes" /> class.</summary>
		// Token: 0x06001920 RID: 6432 RVA: 0x00084968 File Offset: 0x00082B68
		public SoapAttributes()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapAttributes" /> class using the specified custom type.</summary>
		/// <param name="provider">Any object that implements the <see cref="T:System.Reflection.ICustomAttributeProvider" /> interface, such as the <see cref="T:System.Type" /> class. </param>
		// Token: 0x06001921 RID: 6433 RVA: 0x0008497C File Offset: 0x00082B7C
		public SoapAttributes(ICustomAttributeProvider provider)
		{
			object[] customAttributes = provider.GetCustomAttributes(false);
			foreach (object obj in customAttributes)
			{
				if (obj is SoapAttributeAttribute)
				{
					this.soapAttribute = (SoapAttributeAttribute)obj;
				}
				else if (obj is DefaultValueAttribute)
				{
					this.soapDefaultValue = ((DefaultValueAttribute)obj).Value;
				}
				else if (obj is SoapElementAttribute)
				{
					this.soapElement = (SoapElementAttribute)obj;
				}
				else if (obj is SoapEnumAttribute)
				{
					this.soapEnum = (SoapEnumAttribute)obj;
				}
				else if (obj is SoapIgnoreAttribute)
				{
					this.soapIgnore = true;
				}
				else if (obj is SoapTypeAttribute)
				{
					this.soapType = (SoapTypeAttribute)obj;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.SoapAttributeAttribute" /> to override.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapAttributeAttribute" /> that overrides the behavior of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> when the member is serialized.</returns>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x00084A60 File Offset: 0x00082C60
		// (set) Token: 0x06001923 RID: 6435 RVA: 0x00084A68 File Offset: 0x00082C68
		public SoapAttributeAttribute SoapAttribute
		{
			get
			{
				return this.soapAttribute;
			}
			set
			{
				this.soapAttribute = value;
			}
		}

		/// <summary>Gets or sets the default value of an XML element or attribute.</summary>
		/// <returns>An object that represents the default value of an XML element or attribute.</returns>
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x00084A74 File Offset: 0x00082C74
		// (set) Token: 0x06001925 RID: 6437 RVA: 0x00084A7C File Offset: 0x00082C7C
		public object SoapDefaultValue
		{
			get
			{
				return this.soapDefaultValue;
			}
			set
			{
				this.soapDefaultValue = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> to override.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> to override.</returns>
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x00084A88 File Offset: 0x00082C88
		// (set) Token: 0x06001927 RID: 6439 RVA: 0x00084A90 File Offset: 0x00082C90
		public SoapElementAttribute SoapElement
		{
			get
			{
				return this.soapElement;
			}
			set
			{
				this.soapElement = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a SOAP enumeration.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapEnumAttribute" />.</returns>
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x00084A9C File Offset: 0x00082C9C
		// (set) Token: 0x06001929 RID: 6441 RVA: 0x00084AA4 File Offset: 0x00082CA4
		public SoapEnumAttribute SoapEnum
		{
			get
			{
				return this.soapEnum;
			}
			set
			{
				this.soapEnum = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or property as encoded SOAP XML.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must not serialize the field or property; otherwise, false.</returns>
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x00084AB0 File Offset: 0x00082CB0
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x00084AB8 File Offset: 0x00082CB8
		public bool SoapIgnore
		{
			get
			{
				return this.soapIgnore;
			}
			set
			{
				this.soapIgnore = value;
			}
		}

		/// <summary>Gets or sets an object that instructs the <see cref="T:System.Xml.Serialization.XmlSerializer" /> how to serialize an object type into encoded SOAP XML.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapTypeAttribute" /> that either overrides a <see cref="T:System.Xml.Serialization.SoapTypeAttribute" /> applied to a class declaration, or is applied to a class declaration.</returns>
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x00084AC4 File Offset: 0x00082CC4
		// (set) Token: 0x0600192D RID: 6445 RVA: 0x00084ACC File Offset: 0x00082CCC
		public SoapTypeAttribute SoapType
		{
			get
			{
				return this.soapType;
			}
			set
			{
				this.soapType = value;
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00084AD8 File Offset: 0x00082CD8
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("SA ");
			if (this.soapIgnore)
			{
				sb.Append('i');
			}
			if (this.soapAttribute != null)
			{
				this.soapAttribute.AddKeyHash(sb);
			}
			if (this.soapElement != null)
			{
				this.soapElement.AddKeyHash(sb);
			}
			if (this.soapEnum != null)
			{
				this.soapEnum.AddKeyHash(sb);
			}
			if (this.soapType != null)
			{
				this.soapType.AddKeyHash(sb);
			}
			if (this.soapDefaultValue == null)
			{
				sb.Append("n");
			}
			else if (!(this.soapDefaultValue is DBNull))
			{
				string str = XmlCustomFormatter.ToXmlString(TypeTranslator.GetTypeData(this.soapDefaultValue.GetType()), this.soapDefaultValue);
				sb.Append("v" + str);
			}
			sb.Append("|");
		}

		// Token: 0x04000A7A RID: 2682
		private SoapAttributeAttribute soapAttribute;

		// Token: 0x04000A7B RID: 2683
		private object soapDefaultValue = DBNull.Value;

		// Token: 0x04000A7C RID: 2684
		private SoapElementAttribute soapElement;

		// Token: 0x04000A7D RID: 2685
		private SoapEnumAttribute soapEnum;

		// Token: 0x04000A7E RID: 2686
		private bool soapIgnore;

		// Token: 0x04000A7F RID: 2687
		private SoapTypeAttribute soapType;
	}
}
