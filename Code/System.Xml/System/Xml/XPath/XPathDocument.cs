using System;
using System.IO;
using Mono.Xml.XPath;

namespace System.Xml.XPath
{
	/// <summary>Provides a fast, read-only, in-memory representation of an XML document by using the XPath data model.</summary>
	// Token: 0x02000141 RID: 321
	public class XPathDocument : IXPathNavigable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data in the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> object that contains the XML data.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.IO.Stream" /> object passed as a parameter is null.</exception>
		// Token: 0x06000F12 RID: 3858 RVA: 0x0004921C File Offset: 0x0004741C
		public XPathDocument(Stream stream)
		{
			this.Initialize(new XmlValidatingReader(new XmlTextReader(stream))
			{
				ValidationType = ValidationType.None
			}, XmlSpace.None);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data in the specified file.</summary>
		/// <param name="uri">The path of the file that contains the XML data.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The file path parameter is null.</exception>
		// Token: 0x06000F13 RID: 3859 RVA: 0x0004924C File Offset: 0x0004744C
		public XPathDocument(string uri) : this(uri, XmlSpace.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data that is contained in the specified <see cref="T:System.IO.TextReader" /> object.</summary>
		/// <param name="textReader">The <see cref="T:System.IO.TextReader" /> object that contains the XML data.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.IO.TextReader" /> object passed as a parameter is null.</exception>
		// Token: 0x06000F14 RID: 3860 RVA: 0x00049258 File Offset: 0x00047458
		public XPathDocument(TextReader reader)
		{
			this.Initialize(new XmlValidatingReader(new XmlTextReader(reader))
			{
				ValidationType = ValidationType.None
			}, XmlSpace.None);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data that is contained in the specified <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that contains the XML data. </param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object passed as a parameter is null.</exception>
		// Token: 0x06000F15 RID: 3861 RVA: 0x00049288 File Offset: 0x00047488
		public XPathDocument(XmlReader reader) : this(reader, XmlSpace.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data in the file specified with the white space handling specified.</summary>
		/// <param name="uri">The path of the file that contains the XML data.</param>
		/// <param name="space">An <see cref="T:System.Xml.XmlSpace" /> object.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The file path parameter or <see cref="T:System.Xml.XmlSpace" /> object parameter is null.</exception>
		// Token: 0x06000F16 RID: 3862 RVA: 0x00049294 File Offset: 0x00047494
		public XPathDocument(string uri, XmlSpace space)
		{
			XmlValidatingReader xmlValidatingReader = null;
			try
			{
				xmlValidatingReader = new XmlValidatingReader(new XmlTextReader(uri));
				xmlValidatingReader.ValidationType = ValidationType.None;
				this.Initialize(xmlValidatingReader, space);
			}
			finally
			{
				if (xmlValidatingReader != null)
				{
					xmlValidatingReader.Close();
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data that is contained in the specified <see cref="T:System.Xml.XmlReader" /> object with the specified white space handling.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that contains the XML data.</param>
		/// <param name="space">An <see cref="T:System.Xml.XmlSpace" /> object.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter or <see cref="T:System.Xml.XmlSpace" /> object parameter is null.</exception>
		// Token: 0x06000F17 RID: 3863 RVA: 0x000492F4 File Offset: 0x000474F4
		public XPathDocument(XmlReader reader, XmlSpace space)
		{
			this.Initialize(reader, space);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00049304 File Offset: 0x00047504
		private void Initialize(XmlReader reader, XmlSpace space)
		{
			this.document = new DTMXPathDocumentBuilder2(reader, space).CreateDocument();
		}

		/// <summary>Initializes a read-only <see cref="T:System.Xml.XPath.XPathNavigator" /> object for navigating through nodes in this <see cref="T:System.Xml.XPath.XPathDocument" />.</summary>
		/// <returns>A read-only <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x06000F19 RID: 3865 RVA: 0x00049318 File Offset: 0x00047518
		public XPathNavigator CreateNavigator()
		{
			return this.document.CreateNavigator();
		}

		// Token: 0x040006AC RID: 1708
		private IXPathNavigable document;
	}
}
