using System;

namespace System.Xml
{
	/// <summary>Represents a CDATA section.</summary>
	// Token: 0x020000E9 RID: 233
	public class XmlCDataSection : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlCDataSection" /> class.</summary>
		/// <param name="data"></param>
		/// <param name="doc"></param>
		// Token: 0x06000845 RID: 2117 RVA: 0x0002E08C File Offset: 0x0002C28C
		protected internal XmlCDataSection(string data, XmlDocument doc) : base(data, doc)
		{
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For CDATA nodes, the local name is #cdata-section.</returns>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0002E098 File Offset: 0x0002C298
		public override string LocalName
		{
			get
			{
				return "#cdata-section";
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For CDATA nodes, the name is #cdata-section.</returns>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0002E0A0 File Offset: 0x0002C2A0
		public override string Name
		{
			get
			{
				return "#cdata-section";
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For CDATA nodes, the value is XmlNodeType.CDATA.</returns>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0002E0A8 File Offset: 0x0002C2A8
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.CDATA;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0002E0AC File Offset: 0x0002C2AC
		public override XmlNode ParentNode
		{
			get
			{
				return base.ParentNode;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. Because CDATA nodes do not have children, regardless of the parameter setting, the cloned node will always include the data content. </param>
		// Token: 0x0600084A RID: 2122 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
		public override XmlNode CloneNode(bool deep)
		{
			return new XmlCDataSection(this.Data, this.OwnerDocument);
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x0600084B RID: 2123 RVA: 0x0002E0D4 File Offset: 0x0002C2D4
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x0600084C RID: 2124 RVA: 0x0002E0D8 File Offset: 0x0002C2D8
		public override void WriteTo(XmlWriter w)
		{
			w.WriteCData(this.Data);
		}
	}
}
