using System;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event.</summary>
	// Token: 0x02000284 RID: 644
	public class XmlAttributeEventArgs : EventArgs
	{
		// Token: 0x06001A0F RID: 6671 RVA: 0x00088610 File Offset: 0x00086810
		internal XmlAttributeEventArgs(XmlAttribute attr, int lineNum, int linePos, object source)
		{
			this.attr = attr;
			this.lineNumber = lineNum;
			this.linePosition = linePos;
			this.obj = source;
		}

		/// <summary>Gets an object that represents the unknown XML attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlAttribute" /> that represents the unknown XML attribute.</returns>
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x00088638 File Offset: 0x00086838
		public XmlAttribute Attr
		{
			get
			{
				return this.attr;
			}
		}

		/// <summary>Gets the line number of the unknown XML attribute.</summary>
		/// <returns>The line number of the unknown XML attribute.</returns>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x00088640 File Offset: 0x00086840
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the position in the line of the unknown XML attribute.</summary>
		/// <returns>The position number of the unknown XML attribute.</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x00088648 File Offset: 0x00086848
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets the object being deserialized.</summary>
		/// <returns>The object being deserialized.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x00088650 File Offset: 0x00086850
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.obj;
			}
		}

		/// <summary>Gets a comma-delimited list of XML attribute names expected to be in an XML document instance.</summary>
		/// <returns>A comma-delimited list of XML attribute names. Each name is in the following format: <paramref name="namespace" />:<paramref name="name" />.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x00088658 File Offset: 0x00086858
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x00088660 File Offset: 0x00086860
		public string ExpectedAttributes
		{
			get
			{
				return this.expectedAttributes;
			}
			internal set
			{
				this.expectedAttributes = value;
			}
		}

		// Token: 0x04000ABF RID: 2751
		private XmlAttribute attr;

		// Token: 0x04000AC0 RID: 2752
		private int lineNumber;

		// Token: 0x04000AC1 RID: 2753
		private int linePosition;

		// Token: 0x04000AC2 RID: 2754
		private object obj;

		// Token: 0x04000AC3 RID: 2755
		private string expectedAttributes;
	}
}
