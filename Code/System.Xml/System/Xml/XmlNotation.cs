using System;

namespace System.Xml
{
	/// <summary>Represents a notation declaration, such as &lt;!NOTATION... &gt;.</summary>
	// Token: 0x020000EE RID: 238
	public class XmlNotation : XmlNode
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x0002F7B0 File Offset: 0x0002D9B0
		internal XmlNotation(string localName, string prefix, string publicId, string systemId, XmlDocument doc) : base(doc)
		{
			this.localName = doc.NameTable.Add(localName);
			this.prefix = doc.NameTable.Add(prefix);
			this.publicId = publicId;
			this.systemId = systemId;
		}

		/// <summary>Gets the markup representing the children of this node.</summary>
		/// <returns>For XmlNotation nodes, String.Empty is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">Attempting to set the property. </exception>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0002F7FC File Offset: 0x0002D9FC
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0002F804 File Offset: 0x0002DA04
		public override string InnerXml
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException("This operation is not allowed.");
			}
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because XmlNotation nodes are read-only, this property always returns true.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0002F810 File Offset: 0x0002DA10
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the name of the current node without the namespace prefix.</summary>
		/// <returns>For XmlNotation nodes, this property returns the name of the notation.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0002F814 File Offset: 0x0002DA14
		public override string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		/// <summary>Gets the name of the current node.</summary>
		/// <returns>The name of the notation.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0002F81C File Offset: 0x0002DA1C
		public override string Name
		{
			get
			{
				return (!(this.prefix != string.Empty)) ? this.localName : (this.prefix + ":" + this.localName);
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For XmlNotation nodes, the value is XmlNodeType.Notation.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0002F860 File Offset: 0x0002DA60
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Notation;
			}
		}

		/// <summary>Gets the markup representing this node and all its children.</summary>
		/// <returns>For XmlNotation nodes, String.Empty is returned.</returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0002F864 File Offset: 0x0002DA64
		public override string OuterXml
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets the value of the public identifier on the notation declaration.</summary>
		/// <returns>The public identifier on the notation. If there is no public identifier, null is returned.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0002F86C File Offset: 0x0002DA6C
		public string PublicId
		{
			get
			{
				if (this.publicId != null)
				{
					return this.publicId;
				}
				return null;
			}
		}

		/// <summary>Gets the value of the system identifier on the notation declaration.</summary>
		/// <returns>The system identifier on the notation. If there is no system identifier, null is returned.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0002F884 File Offset: 0x0002DA84
		public string SystemId
		{
			get
			{
				if (this.systemId != null)
				{
					return this.systemId;
				}
				return null;
			}
		}

		/// <summary>Creates a duplicate of this node. Notation nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlNotation" /> object throws an exception.</summary>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself.</param>
		/// <exception cref="T:System.InvalidOperationException">Notation nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlNotation" /> object throws an exception.</exception>
		// Token: 0x060008A0 RID: 2208 RVA: 0x0002F89C File Offset: 0x0002DA9C
		public override XmlNode CloneNode(bool deep)
		{
			throw new InvalidOperationException("This operation is not allowed.");
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. This method has no effect on XmlNotation nodes.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060008A1 RID: 2209 RVA: 0x0002F8A8 File Offset: 0x0002DAA8
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />. This method has no effect on XmlNotation nodes.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060008A2 RID: 2210 RVA: 0x0002F8AC File Offset: 0x0002DAAC
		public override void WriteTo(XmlWriter w)
		{
		}

		// Token: 0x040004BC RID: 1212
		private string localName;

		// Token: 0x040004BD RID: 1213
		private string publicId;

		// Token: 0x040004BE RID: 1214
		private string systemId;

		// Token: 0x040004BF RID: 1215
		private string prefix;
	}
}
