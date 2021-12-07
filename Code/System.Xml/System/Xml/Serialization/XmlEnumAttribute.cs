using System;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Controls how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</summary>
	// Token: 0x0200028F RID: 655
	[AttributeUsage(AttributeTargets.Field)]
	public class XmlEnumAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlEnumAttribute" /> class.</summary>
		// Token: 0x06001AA0 RID: 6816 RVA: 0x0008ABC8 File Offset: 0x00088DC8
		public XmlEnumAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlEnumAttribute" /> class, and specifies the XML value that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates or recognizes (when it serializes or deserializes the enumeration, respectively).</summary>
		/// <param name="name">The overriding name of the enumeration member. </param>
		// Token: 0x06001AA1 RID: 6817 RVA: 0x0008ABD0 File Offset: 0x00088DD0
		public XmlEnumAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets or sets the value generated in an XML-document instance when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration, or the value recognized when it deserializes the enumeration member.</summary>
		/// <returns>The value generated in an XML-document instance when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes the enumeration, or the value recognized when it is deserializes the enumeration member.</returns>
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0008ABE0 File Offset: 0x00088DE0
		// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x0008ABE8 File Offset: 0x00088DE8
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0008ABF4 File Offset: 0x00088DF4
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("XENA ");
			KeyHelper.AddField(sb, 1, this.name);
			sb.Append('|');
		}

		// Token: 0x04000AE9 RID: 2793
		private string name;
	}
}
