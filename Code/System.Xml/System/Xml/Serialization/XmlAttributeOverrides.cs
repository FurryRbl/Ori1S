using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Allows you to override property, field, and class attributes when you use the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to serialize or deserialize an object.</summary>
	// Token: 0x02000285 RID: 645
	public class XmlAttributeOverrides
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> class. </summary>
		// Token: 0x06001A16 RID: 6678 RVA: 0x0008866C File Offset: 0x0008686C
		public XmlAttributeOverrides()
		{
			this.overrides = new Hashtable();
		}

		/// <summary>Gets the object associated with the specified, base-class, type.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributes" /> that represents the collection of overriding attributes.</returns>
		/// <param name="type">The base class <see cref="T:System.Type" /> that is associated with the collection of attributes you want to retrieve. </param>
		// Token: 0x1700078C RID: 1932
		public XmlAttributes this[Type type]
		{
			get
			{
				return this[type, string.Empty];
			}
		}

		/// <summary>Gets the object associated with the specified (base-class) type. The member parameter specifies the base-class member that is overridden.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributes" /> that represents the collection of overriding attributes.</returns>
		/// <param name="type">The base class <see cref="T:System.Type" /> that is associated with the collection of attributes you want. </param>
		/// <param name="member">The name of the overridden member that specifies the <see cref="T:System.Xml.Serialization.XmlAttributes" /> to return. </param>
		// Token: 0x1700078D RID: 1933
		public XmlAttributes this[Type type, string member]
		{
			get
			{
				return (XmlAttributes)this.overrides[this.GetKey(type, member)];
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlAttributes" /> object to the collection of <see cref="T:System.Xml.Serialization.XmlAttributes" /> objects. The <paramref name="type" /> parameter specifies an object to be overridden by the <see cref="T:System.Xml.Serialization.XmlAttributes" /> object.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object that is overridden. </param>
		/// <param name="attributes">An <see cref="T:System.Xml.Serialization.XmlAttributes" /> object that represents the overriding attributes. </param>
		// Token: 0x06001A19 RID: 6681 RVA: 0x000886AC File Offset: 0x000868AC
		public void Add(Type type, XmlAttributes attributes)
		{
			this.Add(type, string.Empty, attributes);
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlAttributes" /> object to the collection of <see cref="T:System.Xml.Serialization.XmlAttributes" /> objects. The <paramref name="type" /> parameter specifies an object to be overridden. The <paramref name="member" /> parameter specifies the name of a member that is overridden.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to override. </param>
		/// <param name="member">The name of the member to override. </param>
		/// <param name="attributes">An <see cref="T:System.Xml.Serialization.XmlAttributes" /> object that represents the overriding attributes. </param>
		// Token: 0x06001A1A RID: 6682 RVA: 0x000886BC File Offset: 0x000868BC
		public void Add(Type type, string member, XmlAttributes attributes)
		{
			if (this.overrides[this.GetKey(type, member)] != null)
			{
				throw new Exception("The attributes for the given type and Member already exist in the collection");
			}
			this.overrides.Add(this.GetKey(type, member), attributes);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00088700 File Offset: 0x00086900
		private TypeMember GetKey(Type type, string member)
		{
			return new TypeMember(type, member);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0008870C File Offset: 0x0008690C
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("XAO ");
			foreach (object obj in this.overrides)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				XmlAttributes xmlAttributes = (XmlAttributes)dictionaryEntry.Value;
				IFormattable formattable = dictionaryEntry.Key as IFormattable;
				sb.Append((formattable == null) ? dictionaryEntry.Key.ToString() : formattable.ToString(null, CultureInfo.InvariantCulture)).Append(' ');
				xmlAttributes.AddKeyHash(sb);
			}
			sb.Append("|");
		}

		// Token: 0x04000AC4 RID: 2756
		private Hashtable overrides;
	}
}
