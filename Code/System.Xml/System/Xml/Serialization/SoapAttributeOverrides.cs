using System;
using System.Collections;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Allows you to override attributes applied to properties, fields, and classes when you use an <see cref="T:System.Xml.Serialization.XmlSerializer" /> to serialize or deserialize an object as encoded SOAP.</summary>
	// Token: 0x0200026C RID: 620
	public class SoapAttributeOverrides
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapAttributeOverrides" /> class. </summary>
		// Token: 0x06001919 RID: 6425 RVA: 0x0008480C File Offset: 0x00082A0C
		public SoapAttributeOverrides()
		{
			this.overrides = new Hashtable();
		}

		/// <summary>Gets the object associated with the specified (base class) type.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that represents the collection of overriding attributes.</returns>
		/// <param name="type">The base class <see cref="T:System.Type" /> that is associated with the collection of attributes you want to retrieve. </param>
		// Token: 0x17000748 RID: 1864
		public SoapAttributes this[Type type]
		{
			get
			{
				return this[type, string.Empty];
			}
		}

		/// <summary>Gets the object associated with the specified (base class) type. The <paramref name="member" /> parameter specifies the base class member that is overridden.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that represents the collection of overriding attributes.</returns>
		/// <param name="type">The base class <see cref="T:System.Type" /> that is associated with the collection of attributes you want to override. </param>
		/// <param name="member">The name of the overridden member that specifies the <see cref="T:System.Xml.Serialization.SoapAttributes" /> to return. </param>
		// Token: 0x17000749 RID: 1865
		public SoapAttributes this[Type type, string member]
		{
			get
			{
				return (SoapAttributes)this.overrides[this.GetKey(type, member)];
			}
		}

		/// <summary>Adds a <see cref="T:System.Xml.Serialization.SoapAttributes" /> to a collection of <see cref="T:System.Xml.Serialization.SoapAttributes" /> objects. The <paramref name="type" /> parameter specifies an object to be overridden by the <see cref="T:System.Xml.Serialization.SoapAttributes" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object that is overridden. </param>
		/// <param name="attributes">A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that represents the overriding attributes. </param>
		// Token: 0x0600191C RID: 6428 RVA: 0x0008484C File Offset: 0x00082A4C
		public void Add(Type type, SoapAttributes attributes)
		{
			this.Add(type, string.Empty, attributes);
		}

		/// <summary>Adds a <see cref="T:System.Xml.Serialization.SoapAttributes" /> to the collection of <see cref="T:System.Xml.Serialization.SoapAttributes" /> objects contained by the <see cref="T:System.Xml.Serialization.SoapAttributeOverrides" />. The <paramref name="type" /> parameter specifies the object to be overridden by the <see cref="T:System.Xml.Serialization.SoapAttributes" />. The <paramref name="member" /> parameter specifies the name of a member that is overridden.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to override. </param>
		/// <param name="member">The name of the member to override. </param>
		/// <param name="attributes">A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that represents the overriding attributes. </param>
		// Token: 0x0600191D RID: 6429 RVA: 0x0008485C File Offset: 0x00082A5C
		public void Add(Type type, string member, SoapAttributes attributes)
		{
			if (this.overrides[this.GetKey(type, member)] != null)
			{
				throw new Exception("The attributes for the given type and Member already exist in the collection");
			}
			this.overrides.Add(this.GetKey(type, member), attributes);
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000848A0 File Offset: 0x00082AA0
		private TypeMember GetKey(Type type, string member)
		{
			return new TypeMember(type, member);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x000848AC File Offset: 0x00082AAC
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("SAO ");
			foreach (object obj in this.overrides)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				SoapAttributes soapAttributes = (SoapAttributes)this.overrides[dictionaryEntry.Key];
				sb.Append(dictionaryEntry.Key.ToString()).Append(' ');
				soapAttributes.AddKeyHash(sb);
			}
			sb.Append("|");
		}

		// Token: 0x04000A79 RID: 2681
		private Hashtable overrides;
	}
}
