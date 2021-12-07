using System;
using System.Collections.Specialized;

namespace System.Xml.Serialization
{
	/// <summary>Contains the XML namespaces and prefixes that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> uses to generate qualified names in an XML-document instance.</summary>
	// Token: 0x020002B7 RID: 695
	public class XmlSerializerNamespaces
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class.</summary>
		// Token: 0x06001D3F RID: 7487 RVA: 0x0009B380 File Offset: 0x00099580
		public XmlSerializerNamespaces()
		{
			this.namespaces = new ListDictionary();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class.</summary>
		/// <param name="namespaces">An array of <see cref="T:System.Xml.XmlQualifiedName" /> objects. </param>
		// Token: 0x06001D40 RID: 7488 RVA: 0x0009B394 File Offset: 0x00099594
		public XmlSerializerNamespaces(XmlQualifiedName[] namespaces) : this()
		{
			foreach (XmlQualifiedName xmlQualifiedName in namespaces)
			{
				this.namespaces[xmlQualifiedName.Name] = xmlQualifiedName;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class, using the specified instance of XmlSerializerNamespaces containing the collection of prefix and namespace pairs.</summary>
		/// <param name="namespaces">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" />containing the namespace and prefix pairs. </param>
		// Token: 0x06001D41 RID: 7489 RVA: 0x0009B3D4 File Offset: 0x000995D4
		public XmlSerializerNamespaces(XmlSerializerNamespaces namespaces) : this(namespaces.ToArray())
		{
		}

		/// <summary>Adds a prefix and namespace pair to an <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> object.</summary>
		/// <param name="prefix">The prefix associated with an XML namespace. </param>
		/// <param name="ns">An XML namespace. </param>
		// Token: 0x06001D42 RID: 7490 RVA: 0x0009B3E4 File Offset: 0x000995E4
		public void Add(string prefix, string ns)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(prefix, ns);
			this.namespaces[xmlQualifiedName.Name] = xmlQualifiedName;
		}

		/// <summary>Gets the array of prefix and namespace pairs in an <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> object.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlQualifiedName" /> objects that are used as qualified names in an XML document.</returns>
		// Token: 0x06001D43 RID: 7491 RVA: 0x0009B40C File Offset: 0x0009960C
		public XmlQualifiedName[] ToArray()
		{
			XmlQualifiedName[] array = new XmlQualifiedName[this.namespaces.Count];
			this.namespaces.Values.CopyTo(array, 0);
			return array;
		}

		/// <summary>Gets the number of prefix and namespace pairs in the collection.</summary>
		/// <returns>The number of prefix and namespace pairs in the collection.</returns>
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0009B440 File Offset: 0x00099640
		public int Count
		{
			get
			{
				return this.namespaces.Count;
			}
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0009B450 File Offset: 0x00099650
		internal string GetPrefix(string Ns)
		{
			foreach (object obj in this.namespaces.Keys)
			{
				string text = (string)obj;
				if (Ns == ((XmlQualifiedName)this.namespaces[text]).Namespace)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x0009B4E8 File Offset: 0x000996E8
		internal ListDictionary Namespaces
		{
			get
			{
				return this.namespaces;
			}
		}

		// Token: 0x04000B9D RID: 2973
		private ListDictionary namespaces;
	}
}
