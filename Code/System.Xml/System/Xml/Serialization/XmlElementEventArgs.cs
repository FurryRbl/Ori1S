using System;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event.</summary>
	// Token: 0x0200028E RID: 654
	public class XmlElementEventArgs : EventArgs
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x0008AB6C File Offset: 0x00088D6C
		internal XmlElementEventArgs(XmlElement attr, int lineNum, int linePos, object source)
		{
			this.attr = attr;
			this.lineNumber = lineNum;
			this.linePosition = linePos;
			this.obj = source;
		}

		/// <summary>Gets the object that represents the unknown XML element.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" />.</returns>
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0008AB94 File Offset: 0x00088D94
		public XmlElement Element
		{
			get
			{
				return this.attr;
			}
		}

		/// <summary>Gets the line number where the unknown element was encountered if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />.</summary>
		/// <returns>The line number where the unknown element was encountered if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />; otherwise, -1.</returns>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x0008AB9C File Offset: 0x00088D9C
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the place in the line where the unknown element occurs if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />.</summary>
		/// <returns>The number in the line where the unknown element occurs if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />; otherwise, -1.</returns>
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x0008ABA4 File Offset: 0x00088DA4
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets the object the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is deserializing.</summary>
		/// <returns>The object that is being deserialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x0008ABAC File Offset: 0x00088DAC
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.obj;
			}
		}

		/// <summary>Gets a comma-delimited list of XML element names expected to be in an XML document instance.</summary>
		/// <returns>A comma-delimited list of XML element names. Each name is in the following format: <paramref name="namespace" />:<paramref name="name" />.</returns>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x0008ABB4 File Offset: 0x00088DB4
		// (set) Token: 0x06001A9F RID: 6815 RVA: 0x0008ABBC File Offset: 0x00088DBC
		public string ExpectedElements
		{
			get
			{
				return this.expectedElements;
			}
			internal set
			{
				this.expectedElements = value;
			}
		}

		// Token: 0x04000AE4 RID: 2788
		private XmlElement attr;

		// Token: 0x04000AE5 RID: 2789
		private int lineNumber;

		// Token: 0x04000AE6 RID: 2790
		private int linePosition;

		// Token: 0x04000AE7 RID: 2791
		private object obj;

		// Token: 0x04000AE8 RID: 2792
		private string expectedElements;
	}
}
