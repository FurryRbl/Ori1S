using System;

namespace System.Xml
{
	/// <summary>Represents an XML qualified name.</summary>
	// Token: 0x02000115 RID: 277
	[Serializable]
	public class XmlQualifiedName
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlQualifiedName" /> class.</summary>
		// Token: 0x06000B36 RID: 2870 RVA: 0x0003A058 File Offset: 0x00038258
		public XmlQualifiedName() : this(string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlQualifiedName" /> class with the specified name.</summary>
		/// <param name="name">The local name to use as the name of the <see cref="T:System.Xml.XmlQualifiedName" /> object. </param>
		// Token: 0x06000B37 RID: 2871 RVA: 0x0003A06C File Offset: 0x0003826C
		public XmlQualifiedName(string name) : this(name, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlQualifiedName" /> class with the specified name and namespace.</summary>
		/// <param name="name">The local name to use as the name of the <see cref="T:System.Xml.XmlQualifiedName" /> object. </param>
		/// <param name="ns">The namespace for the <see cref="T:System.Xml.XmlQualifiedName" /> object. </param>
		// Token: 0x06000B38 RID: 2872 RVA: 0x0003A07C File Offset: 0x0003827C
		public XmlQualifiedName(string name, string ns)
		{
			this.name = ((name != null) ? name : string.Empty);
			this.ns = ((ns != null) ? ns : string.Empty);
			this.hash = (this.name.GetHashCode() ^ this.ns.GetHashCode());
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlQualifiedName" /> is empty.</summary>
		/// <returns>true if name and namespace are empty strings; otherwise, false.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0003A0E8 File Offset: 0x000382E8
		public bool IsEmpty
		{
			get
			{
				return this.name.Length == 0 && this.ns.Length == 0;
			}
		}

		/// <summary>Gets a string representation of the qualified name of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>A string representation of the qualified name or String.Empty if a name is not defined for the object.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0003A10C File Offset: 0x0003830C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a string representation of the namespace of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>A string representation of the namespace or String.Empty if a namespace is not defined for the object.</returns>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0003A114 File Offset: 0x00038314
		public string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Xml.XmlQualifiedName" /> object is equal to the current <see cref="T:System.Xml.XmlQualifiedName" /> object. </summary>
		/// <returns>true if the two are the same instance object; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		// Token: 0x06000B3D RID: 2877 RVA: 0x0003A11C File Offset: 0x0003831C
		public override bool Equals(object other)
		{
			return this == other as XmlQualifiedName;
		}

		/// <summary>Returns the hash code for the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>A hash code for this object.</returns>
		// Token: 0x06000B3E RID: 2878 RVA: 0x0003A12C File Offset: 0x0003832C
		public override int GetHashCode()
		{
			return this.hash;
		}

		/// <summary>Returns the string value of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>The string value of the <see cref="T:System.Xml.XmlQualifiedName" /> in the format of namespace:localname. If the object does not have a namespace defined, this method returns just the local name.</returns>
		// Token: 0x06000B3F RID: 2879 RVA: 0x0003A134 File Offset: 0x00038334
		public override string ToString()
		{
			if (this.ns == string.Empty)
			{
				return this.name;
			}
			return this.ns + ":" + this.name;
		}

		/// <summary>Returns the string value of the <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <returns>The string value of the <see cref="T:System.Xml.XmlQualifiedName" /> in the format of namespace:localname. If the object does not have a namespace defined, this method returns just the local name.</returns>
		/// <param name="name">The name of the object. </param>
		/// <param name="ns">The namespace of the object. </param>
		// Token: 0x06000B40 RID: 2880 RVA: 0x0003A174 File Offset: 0x00038374
		public static string ToString(string name, string ns)
		{
			if (ns == string.Empty)
			{
				return name;
			}
			return ns + ":" + name;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0003A194 File Offset: 0x00038394
		internal static XmlQualifiedName Parse(string name, IXmlNamespaceResolver resolver)
		{
			return XmlQualifiedName.Parse(name, resolver, false);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0003A1A0 File Offset: 0x000383A0
		internal static XmlQualifiedName Parse(string name, IXmlNamespaceResolver resolver, bool considerDefaultNamespace)
		{
			int num = name.IndexOf(':');
			if (num < 0 && !considerDefaultNamespace)
			{
				return new XmlQualifiedName(name);
			}
			string prefix = (num >= 0) ? name.Substring(0, num) : string.Empty;
			string text = (num >= 0) ? name.Substring(num + 1) : name;
			string text2 = resolver.LookupNamespace(prefix);
			if (text2 == null)
			{
				throw new ArgumentException("Invalid qualified name.");
			}
			return new XmlQualifiedName(text, text2);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0003A21C File Offset: 0x0003841C
		internal static XmlQualifiedName Parse(string name, XmlReader reader)
		{
			int num = name.IndexOf(':');
			if (num < 0)
			{
				return new XmlQualifiedName(name);
			}
			string text = reader.LookupNamespace(name.Substring(0, num));
			if (text == null)
			{
				throw new ArgumentException("Invalid qualified name.");
			}
			return new XmlQualifiedName(name.Substring(num + 1), text);
		}

		/// <summary>Compares two <see cref="T:System.Xml.XmlQualifiedName" /> objects.</summary>
		/// <returns>true if the two objects have the same name and namespace values; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		/// <param name="b">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		// Token: 0x06000B44 RID: 2884 RVA: 0x0003A270 File Offset: 0x00038470
		public static bool operator ==(XmlQualifiedName a, XmlQualifiedName b)
		{
			return a == b || (a != null && b != null && (a.hash == b.hash && a.name == b.name) && a.ns == b.ns);
		}

		/// <summary>Compares two <see cref="T:System.Xml.XmlQualifiedName" /> objects.</summary>
		/// <returns>true if the name and namespace values for the two objects differ; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		/// <param name="b">An <see cref="T:System.Xml.XmlQualifiedName" /> to compare. </param>
		// Token: 0x06000B45 RID: 2885 RVA: 0x0003A2D0 File Offset: 0x000384D0
		public static bool operator !=(XmlQualifiedName a, XmlQualifiedName b)
		{
			return !(a == b);
		}

		/// <summary>Provides an empty <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		// Token: 0x04000582 RID: 1410
		public static readonly XmlQualifiedName Empty = new XmlQualifiedName();

		// Token: 0x04000583 RID: 1411
		private readonly string name;

		// Token: 0x04000584 RID: 1412
		private readonly string ns;

		// Token: 0x04000585 RID: 1413
		private readonly int hash;
	}
}
