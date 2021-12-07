using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml
{
	/// <summary>Resolves, adds, and removes namespaces to a collection and provides scope management for these namespaces. </summary>
	// Token: 0x02000100 RID: 256
	public class XmlNamespaceManager : IEnumerable, IXmlNamespaceResolver
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlNamespaceManager" /> class with the specified <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="nameTable">The <see cref="T:System.Xml.XmlNameTable" /> to use. </param>
		/// <exception cref="T:System.NullReferenceException">null is passed to the constructor </exception>
		// Token: 0x06000A25 RID: 2597 RVA: 0x00035BD0 File Offset: 0x00033DD0
		public XmlNamespaceManager(XmlNameTable nameTable)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("nameTable");
			}
			this.nameTable = nameTable;
			nameTable.Add("xmlns");
			nameTable.Add("xml");
			nameTable.Add(string.Empty);
			nameTable.Add("http://www.w3.org/2000/xmlns/");
			nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.InitData();
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00035C4C File Offset: 0x00033E4C
		private void InitData()
		{
			this.decls = new XmlNamespaceManager.NsDecl[10];
			this.scopes = new XmlNamespaceManager.NsScope[40];
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00035C68 File Offset: 0x00033E68
		private void GrowDecls()
		{
			XmlNamespaceManager.NsDecl[] sourceArray = this.decls;
			this.decls = new XmlNamespaceManager.NsDecl[this.declPos * 2 + 1];
			if (this.declPos > 0)
			{
				Array.Copy(sourceArray, 0, this.decls, 0, this.declPos);
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00035CB4 File Offset: 0x00033EB4
		private void GrowScopes()
		{
			XmlNamespaceManager.NsScope[] sourceArray = this.scopes;
			this.scopes = new XmlNamespaceManager.NsScope[this.scopePos * 2 + 1];
			if (this.scopePos > 0)
			{
				Array.Copy(sourceArray, 0, this.scopes, 0, this.scopePos);
			}
		}

		/// <summary>Gets the namespace URI for the default namespace.</summary>
		/// <returns>Returns the namespace URI for the default namespace, or String.Empty if there is no default namespace.</returns>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00035D00 File Offset: 0x00033F00
		public virtual string DefaultNamespace
		{
			get
			{
				return (this.defaultNamespace != null) ? this.defaultNamespace : string.Empty;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this object.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNameTable" /> used by this object.</returns>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00035D20 File Offset: 0x00033F20
		public virtual XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		/// <summary>Adds the given namespace to the collection.</summary>
		/// <param name="prefix">The prefix to associate with the namespace being added. Use String.Empty to add a default namespace.Note   If the <see cref="T:System.Xml.XmlNamespaceManager" /> will be used for resolving namespaces in an XML Path Language (XPath) expression, a prefix must be specified. If an XPath expression does not include a prefix, it is assumed that the namespace Uniform Resource Identifier (URI) is the empty namespace. For more information about XPath expressions and the <see cref="T:System.Xml.XmlNamespaceManager" />, refer to the <see cref="M:System.Xml.XmlNode.SelectNodes(System.String)" /> and <see cref="M:System.Xml.XPath.XPathExpression.SetContext(System.Xml.XmlNamespaceManager)" /> methods.</param>
		/// <param name="uri">The namespace to add. </param>
		/// <exception cref="T:System.ArgumentException">The value for <paramref name="prefix" /> is "xml" or "xmlns". </exception>
		/// <exception cref="T:System.ArgumentNullException">The value for <paramref name="prefix" /> or <paramref name="uri" /> is null. </exception>
		// Token: 0x06000A2B RID: 2603 RVA: 0x00035D28 File Offset: 0x00033F28
		public virtual void AddNamespace(string prefix, string uri)
		{
			this.AddNamespace(prefix, uri, false);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00035D34 File Offset: 0x00033F34
		private void AddNamespace(string prefix, string uri, bool atomizedNames)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix", "Value cannot be null.");
			}
			if (uri == null)
			{
				throw new ArgumentNullException("uri", "Value cannot be null.");
			}
			if (!atomizedNames)
			{
				prefix = this.nameTable.Add(prefix);
				uri = this.nameTable.Add(uri);
			}
			if (prefix == "xml" && uri == "http://www.w3.org/XML/1998/namespace")
			{
				return;
			}
			XmlNamespaceManager.IsValidDeclaration(prefix, uri, true);
			if (prefix.Length == 0)
			{
				this.defaultNamespace = uri;
			}
			for (int i = this.declPos; i > this.declPos - this.count; i--)
			{
				if (object.ReferenceEquals(this.decls[i].Prefix, prefix))
				{
					this.decls[i].Uri = uri;
					return;
				}
			}
			this.declPos++;
			this.count++;
			if (this.declPos == this.decls.Length)
			{
				this.GrowDecls();
			}
			this.decls[this.declPos].Prefix = prefix;
			this.decls[this.declPos].Uri = uri;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00035E80 File Offset: 0x00034080
		private static string IsValidDeclaration(string prefix, string uri, bool throwException)
		{
			string text = null;
			if (prefix == "xml" && uri != "http://www.w3.org/XML/1998/namespace")
			{
				text = string.Format("Prefix \"xml\" can only be bound to the fixed namespace URI \"{0}\". \"{1}\" is invalid.", "http://www.w3.org/XML/1998/namespace", uri);
			}
			else if (text == null && prefix == "xmlns")
			{
				text = "Declaring prefix named \"xmlns\" is not allowed to any namespace.";
			}
			else if (text == null && uri == "http://www.w3.org/2000/xmlns/")
			{
				text = string.Format("Namespace URI \"{0}\" cannot be declared with any namespace.", "http://www.w3.org/2000/xmlns/");
			}
			if (text != null && throwException)
			{
				throw new ArgumentException(text);
			}
			return text;
		}

		/// <summary>Returns an enumerator to use to iterate through the namespaces in the <see cref="T:System.Xml.XmlNamespaceManager" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> containing the prefixes stored by the <see cref="T:System.Xml.XmlNamespaceManager" />.</returns>
		// Token: 0x06000A2E RID: 2606 RVA: 0x00035F20 File Offset: 0x00034120
		public virtual IEnumerator GetEnumerator()
		{
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i <= this.declPos; i++)
			{
				if (this.decls[i].Prefix != string.Empty && this.decls[i].Uri != null)
				{
					hashtable[this.decls[i].Prefix] = this.decls[i].Uri;
				}
			}
			hashtable[string.Empty] = this.DefaultNamespace;
			hashtable["xml"] = "http://www.w3.org/XML/1998/namespace";
			hashtable["xmlns"] = "http://www.w3.org/2000/xmlns/";
			return hashtable.Keys.GetEnumerator();
		}

		/// <summary>Gets a collection of namespace names keyed by prefix which can be used to enumerate the namespaces currently in scope.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> object containing a collection of namespace and prefix pairs currently in scope.</returns>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value that specifies the type of namespace nodes to return.</param>
		// Token: 0x06000A2F RID: 2607 RVA: 0x00035FE4 File Offset: 0x000341E4
		public virtual IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			IDictionary namespacesInScopeImpl = this.GetNamespacesInScopeImpl(scope);
			IDictionary<string, string> dictionary = new Dictionary<string, string>(namespacesInScopeImpl.Count);
			foreach (object obj in namespacesInScopeImpl)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				dictionary[(string)dictionaryEntry.Key] = (string)dictionaryEntry.Value;
			}
			return dictionary;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00036080 File Offset: 0x00034280
		internal virtual IDictionary GetNamespacesInScopeImpl(XmlNamespaceScope scope)
		{
			Hashtable hashtable = new Hashtable();
			if (scope == XmlNamespaceScope.Local)
			{
				for (int i = 0; i < this.count; i++)
				{
					if (this.decls[this.declPos - i].Prefix == string.Empty && this.decls[this.declPos - i].Uri == string.Empty)
					{
						if (hashtable.Contains(string.Empty))
						{
							hashtable.Remove(string.Empty);
						}
					}
					else if (this.decls[this.declPos - i].Uri != null)
					{
						hashtable.Add(this.decls[this.declPos - i].Prefix, this.decls[this.declPos - i].Uri);
					}
				}
				return hashtable;
			}
			for (int j = 0; j <= this.declPos; j++)
			{
				if (this.decls[j].Prefix == string.Empty && this.decls[j].Uri == string.Empty)
				{
					if (hashtable.Contains(string.Empty))
					{
						hashtable.Remove(string.Empty);
					}
				}
				else if (this.decls[j].Uri != null)
				{
					hashtable[this.decls[j].Prefix] = this.decls[j].Uri;
				}
			}
			if (scope == XmlNamespaceScope.All)
			{
				hashtable.Add("xml", "http://www.w3.org/XML/1998/namespace");
			}
			return hashtable;
		}

		/// <summary>Gets a value indicating whether the supplied prefix has a namespace defined for the current pushed scope.</summary>
		/// <returns>true if there is a namespace defined; otherwise, false.</returns>
		/// <param name="prefix">The prefix of the namespace you want to find. </param>
		// Token: 0x06000A31 RID: 2609 RVA: 0x00036240 File Offset: 0x00034440
		public virtual bool HasNamespace(string prefix)
		{
			return this.HasNamespace(prefix, false);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0003624C File Offset: 0x0003444C
		private bool HasNamespace(string prefix, bool atomizedNames)
		{
			if (prefix == null || this.count == 0)
			{
				return false;
			}
			for (int i = this.declPos; i > this.declPos - this.count; i--)
			{
				if (this.decls[i].Prefix == prefix)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets the namespace URI for the specified prefix.</summary>
		/// <returns>Returns the namespace URI for <paramref name="prefix" /> or null if there is no mapped namespace. The returned string is atomized.For more information on atomized strings, see <see cref="T:System.Xml.XmlNameTable" />.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass String.Empty. </param>
		// Token: 0x06000A33 RID: 2611 RVA: 0x000362B0 File Offset: 0x000344B0
		public virtual string LookupNamespace(string prefix)
		{
			switch (prefix)
			{
			case "xmlns":
				return this.nameTable.Get("http://www.w3.org/2000/xmlns/");
			case "xml":
				return this.nameTable.Get("http://www.w3.org/XML/1998/namespace");

				return this.DefaultNamespace;
			case null:
				break;
			default:
				for (int i = this.declPos; i >= 0; i--)
				{
					if (this.CompareString(this.decls[i].Prefix, prefix, this.internalAtomizedNames) && this.decls[i].Uri != null)
					{
						return this.decls[i].Uri;
					}
				}
				return null;
				break;
			}
			return null;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000363B8 File Offset: 0x000345B8
		internal string LookupNamespace(string prefix, bool atomizedNames)
		{
			this.internalAtomizedNames = atomizedNames;
			string result = this.LookupNamespace(prefix);
			this.internalAtomizedNames = false;
			return result;
		}

		/// <summary>Finds the prefix declared for the given namespace URI.</summary>
		/// <returns>The matching prefix. If there is no mapped prefix, the method returns String.Empty. If a null value is supplied, then null is returned.</returns>
		/// <param name="uri">The namespace to resolve for the prefix. </param>
		// Token: 0x06000A35 RID: 2613 RVA: 0x000363DC File Offset: 0x000345DC
		public virtual string LookupPrefix(string uri)
		{
			return this.LookupPrefix(uri, false);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000363E8 File Offset: 0x000345E8
		private bool CompareString(string s1, string s2, bool atomizedNames)
		{
			if (atomizedNames)
			{
				return object.ReferenceEquals(s1, s2);
			}
			return s1 == s2;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00036400 File Offset: 0x00034600
		internal string LookupPrefix(string uri, bool atomizedName)
		{
			return this.LookupPrefixCore(uri, atomizedName, false);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0003640C File Offset: 0x0003460C
		internal string LookupPrefixExclusive(string uri, bool atomizedName)
		{
			return this.LookupPrefixCore(uri, atomizedName, true);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00036418 File Offset: 0x00034618
		private string LookupPrefixCore(string uri, bool atomizedName, bool excludeOverriden)
		{
			if (uri == null)
			{
				return null;
			}
			if (this.CompareString(uri, this.DefaultNamespace, atomizedName))
			{
				return string.Empty;
			}
			if (this.CompareString(uri, "http://www.w3.org/XML/1998/namespace", atomizedName))
			{
				return "xml";
			}
			if (this.CompareString(uri, "http://www.w3.org/2000/xmlns/", atomizedName))
			{
				return "xmlns";
			}
			for (int i = this.declPos; i >= 0; i--)
			{
				if (this.CompareString(this.decls[i].Uri, uri, atomizedName) && this.decls[i].Prefix.Length > 0 && (!excludeOverriden || !this.IsOverriden(i)))
				{
					return this.decls[i].Prefix;
				}
			}
			return null;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000364EC File Offset: 0x000346EC
		private bool IsOverriden(int idx)
		{
			if (idx == this.declPos)
			{
				return false;
			}
			string prefix = this.decls[idx + 1].Prefix;
			for (int i = idx + 1; i <= this.declPos; i++)
			{
				if (this.decls[idx].Prefix == prefix)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Pops a namespace scope off the stack.</summary>
		/// <returns>true if there are namespace scopes left on the stack; false if there are no more namespaces to pop.</returns>
		// Token: 0x06000A3B RID: 2619 RVA: 0x00036550 File Offset: 0x00034750
		public virtual bool PopScope()
		{
			if (this.scopePos == -1)
			{
				return false;
			}
			this.declPos -= this.count;
			this.defaultNamespace = this.scopes[this.scopePos].DefaultNamespace;
			this.count = this.scopes[this.scopePos].DeclCount;
			this.scopePos--;
			return true;
		}

		/// <summary>Pushes a namespace scope onto the stack.</summary>
		// Token: 0x06000A3C RID: 2620 RVA: 0x000365C8 File Offset: 0x000347C8
		public virtual void PushScope()
		{
			this.scopePos++;
			if (this.scopePos == this.scopes.Length)
			{
				this.GrowScopes();
			}
			this.scopes[this.scopePos].DefaultNamespace = this.defaultNamespace;
			this.scopes[this.scopePos].DeclCount = this.count;
			this.count = 0;
		}

		/// <summary>Removes the given namespace for the given prefix.</summary>
		/// <param name="prefix">The prefix for the namespace </param>
		/// <param name="uri">The namespace to remove for the given prefix. The namespace removed is from the current namespace scope. Namespaces outside the current scope are ignored. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="prefix" /> or <paramref name="uri" /> is null. </exception>
		// Token: 0x06000A3D RID: 2621 RVA: 0x0003663C File Offset: 0x0003483C
		public virtual void RemoveNamespace(string prefix, string uri)
		{
			this.RemoveNamespace(prefix, uri, false);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00036648 File Offset: 0x00034848
		private void RemoveNamespace(string prefix, string uri, bool atomizedNames)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (this.count == 0)
			{
				return;
			}
			for (int i = this.declPos; i > this.declPos - this.count; i--)
			{
				if (this.CompareString(this.decls[i].Prefix, prefix, atomizedNames) && this.CompareString(this.decls[i].Uri, uri, atomizedNames))
				{
					this.decls[i].Uri = null;
				}
			}
		}

		// Token: 0x04000515 RID: 1301
		internal const string XmlnsXml = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04000516 RID: 1302
		internal const string XmlnsXmlns = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000517 RID: 1303
		internal const string PrefixXml = "xml";

		// Token: 0x04000518 RID: 1304
		internal const string PrefixXmlns = "xmlns";

		// Token: 0x04000519 RID: 1305
		private XmlNamespaceManager.NsDecl[] decls;

		// Token: 0x0400051A RID: 1306
		private int declPos = -1;

		// Token: 0x0400051B RID: 1307
		private XmlNamespaceManager.NsScope[] scopes;

		// Token: 0x0400051C RID: 1308
		private int scopePos = -1;

		// Token: 0x0400051D RID: 1309
		private string defaultNamespace;

		// Token: 0x0400051E RID: 1310
		private int count;

		// Token: 0x0400051F RID: 1311
		private XmlNameTable nameTable;

		// Token: 0x04000520 RID: 1312
		internal bool internalAtomizedNames;

		// Token: 0x02000101 RID: 257
		private struct NsDecl
		{
			// Token: 0x04000522 RID: 1314
			public string Prefix;

			// Token: 0x04000523 RID: 1315
			public string Uri;
		}

		// Token: 0x02000102 RID: 258
		private struct NsScope
		{
			// Token: 0x04000524 RID: 1316
			public int DeclCount;

			// Token: 0x04000525 RID: 1317
			public string DefaultNamespace;
		}
	}
}
