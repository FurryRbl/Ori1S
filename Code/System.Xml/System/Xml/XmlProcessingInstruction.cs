using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a processing instruction, which XML defines to keep processor-specific information in the text of the document.</summary>
	// Token: 0x02000114 RID: 276
	public class XmlProcessingInstruction : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlProcessingInstruction" /> class.</summary>
		/// <param name="target">The target of the processing instruction; see the <see cref="P:System.Xml.XmlProcessingInstruction.Target" /> property.</param>
		/// <param name="data">The content of the instruction; see the <see cref="P:System.Xml.XmlProcessingInstruction.Data" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06000B27 RID: 2855 RVA: 0x00039F7C File Offset: 0x0003817C
		protected internal XmlProcessingInstruction(string target, string data, XmlDocument doc) : base(doc)
		{
			XmlConvert.VerifyName(target);
			if (data == null)
			{
				data = string.Empty;
			}
			this.target = target;
			this.data = data;
		}

		/// <summary>Gets or sets the content of the processing instruction, excluding the target.</summary>
		/// <returns>The content of the processing instruction, excluding the target.</returns>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00039FA8 File Offset: 0x000381A8
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x00039FB0 File Offset: 0x000381B0
		public string Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children.</returns>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00039FBC File Offset: 0x000381BC
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x00039FC4 File Offset: 0x000381C4
		public override string InnerText
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.data = value;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For processing instruction nodes, this property returns the target of the processing instruction.</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00039FD0 File Offset: 0x000381D0
		public override string LocalName
		{
			get
			{
				return this.target;
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For processing instruction nodes, this property returns the target of the processing instruction.</returns>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00039FD8 File Offset: 0x000381D8
		public override string Name
		{
			get
			{
				return this.target;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlProcessingInstruction nodes, this value is XmlNodeType.ProcessingInstruction.</returns>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00039FE0 File Offset: 0x000381E0
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.ProcessingInstruction;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00039FE4 File Offset: 0x000381E4
		internal override XPathNodeType XPathNodeType
		{
			get
			{
				return XPathNodeType.ProcessingInstruction;
			}
		}

		/// <summary>Gets the target of the processing instruction.</summary>
		/// <returns>The target of the processing instruction.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00039FE8 File Offset: 0x000381E8
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The entire content of the processing instruction, excluding the target.</returns>
		/// <exception cref="T:System.ArgumentException">Node is read-only. </exception>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00039FF0 File Offset: 0x000381F0
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x00039FF8 File Offset: 0x000381F8
		public override string Value
		{
			get
			{
				return this.data;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new ArgumentException("This node is read-only.");
				}
				this.data = value;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The duplicate node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		// Token: 0x06000B33 RID: 2867 RVA: 0x0003A018 File Offset: 0x00038218
		public override XmlNode CloneNode(bool deep)
		{
			return new XmlProcessingInstruction(this.target, this.data, this.OwnerDocument);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because ProcessingInstruction nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000B34 RID: 2868 RVA: 0x0003A040 File Offset: 0x00038240
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000B35 RID: 2869 RVA: 0x0003A044 File Offset: 0x00038244
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.target, this.data);
		}

		// Token: 0x04000580 RID: 1408
		private string target;

		// Token: 0x04000581 RID: 1409
		private string data;
	}
}
