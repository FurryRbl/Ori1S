using System;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event.</summary>
	// Token: 0x02000299 RID: 665
	public class XmlNodeEventArgs : EventArgs
	{
		// Token: 0x06001AD0 RID: 6864 RVA: 0x0008B018 File Offset: 0x00089218
		internal XmlNodeEventArgs(int linenumber, int lineposition, string localname, string name, string nsuri, XmlNodeType nodetype, object source, string text)
		{
			this.linenumber = linenumber;
			this.lineposition = lineposition;
			this.localname = localname;
			this.name = name;
			this.nsuri = nsuri;
			this.nodetype = nodetype;
			this.source = source;
			this.text = text;
		}

		/// <summary>Gets the line number of the unknown XML node.</summary>
		/// <returns>The line number of the unknown XML node.</returns>
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x0008B068 File Offset: 0x00089268
		public int LineNumber
		{
			get
			{
				return this.linenumber;
			}
		}

		/// <summary>Gets the position in the line of the unknown XML node.</summary>
		/// <returns>The position number of the unknown XML node.</returns>
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x0008B070 File Offset: 0x00089270
		public int LinePosition
		{
			get
			{
				return this.lineposition;
			}
		}

		/// <summary>Gets the XML local name of the XML node being deserialized.</summary>
		/// <returns>The XML local name of the node being deserialized.</returns>
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0008B078 File Offset: 0x00089278
		public string LocalName
		{
			get
			{
				return this.localname;
			}
		}

		/// <summary>Gets the name of the XML node being deserialized.</summary>
		/// <returns>The name of the node being deserialized.</returns>
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x0008B080 File Offset: 0x00089280
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the namespace URI that is associated with the XML node being deserialized.</summary>
		/// <returns>The namespace URI that is associated with the XML node being deserialized.</returns>
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x0008B088 File Offset: 0x00089288
		public string NamespaceURI
		{
			get
			{
				return this.nsuri;
			}
		}

		/// <summary>Gets the type of the XML node being deserialized.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNodeType" /> that represents the XML node being deserialized.</returns>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x0008B090 File Offset: 0x00089290
		public XmlNodeType NodeType
		{
			get
			{
				return this.nodetype;
			}
		}

		/// <summary>Gets the object being deserialized.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x0008B098 File Offset: 0x00089298
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.source;
			}
		}

		/// <summary>Gets the text of the XML node being deserialized.</summary>
		/// <returns>The text of the XML node being deserialized.</returns>
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x0008B0A0 File Offset: 0x000892A0
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x04000B01 RID: 2817
		private int linenumber;

		// Token: 0x04000B02 RID: 2818
		private int lineposition;

		// Token: 0x04000B03 RID: 2819
		private string localname;

		// Token: 0x04000B04 RID: 2820
		private string name;

		// Token: 0x04000B05 RID: 2821
		private string nsuri;

		// Token: 0x04000B06 RID: 2822
		private XmlNodeType nodetype;

		// Token: 0x04000B07 RID: 2823
		private object source;

		// Token: 0x04000B08 RID: 2824
		private string text;
	}
}
