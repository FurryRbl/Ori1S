using System;

namespace System.Xml
{
	/// <summary>Specifies the type of node.</summary>
	// Token: 0x02000110 RID: 272
	public enum XmlNodeType
	{
		/// <summary>This is returned by the <see cref="T:System.Xml.XmlReader" /> if a Read method has not been called.</summary>
		// Token: 0x04000559 RID: 1369
		None,
		/// <summary>An element (for example, &lt;item&gt; ).</summary>
		// Token: 0x0400055A RID: 1370
		Element,
		/// <summary>An attribute (for example, id='123' ).</summary>
		// Token: 0x0400055B RID: 1371
		Attribute,
		/// <summary>The text content of a node.</summary>
		// Token: 0x0400055C RID: 1372
		Text,
		/// <summary>A CDATA section (for example, &lt;![CDATA[my escaped text]]&gt; ).</summary>
		// Token: 0x0400055D RID: 1373
		CDATA,
		/// <summary>A reference to an entity (for example, &amp;num; ).</summary>
		// Token: 0x0400055E RID: 1374
		EntityReference,
		/// <summary>An entity declaration (for example, &lt;!ENTITY...&gt; ).</summary>
		// Token: 0x0400055F RID: 1375
		Entity,
		/// <summary>A processing instruction (for example, &lt;?pi test?&gt; ).</summary>
		// Token: 0x04000560 RID: 1376
		ProcessingInstruction,
		/// <summary>A comment (for example, &lt;!-- my comment --&gt; ).</summary>
		// Token: 0x04000561 RID: 1377
		Comment,
		/// <summary>A document object that, as the root of the document tree, provides access to the entire XML document.</summary>
		// Token: 0x04000562 RID: 1378
		Document,
		/// <summary>The document type declaration, indicated by the following tag (for example, &lt;!DOCTYPE...&gt; ).</summary>
		// Token: 0x04000563 RID: 1379
		DocumentType,
		/// <summary>A document fragment.</summary>
		// Token: 0x04000564 RID: 1380
		DocumentFragment,
		/// <summary>A notation in the document type declaration (for example, &lt;!NOTATION...&gt; ).</summary>
		// Token: 0x04000565 RID: 1381
		Notation,
		/// <summary>White space between markup.</summary>
		// Token: 0x04000566 RID: 1382
		Whitespace,
		/// <summary>White space between markup in a mixed content model or white space within the xml:space="preserve" scope.</summary>
		// Token: 0x04000567 RID: 1383
		SignificantWhitespace,
		/// <summary>An end element tag (for example, &lt;/item&gt; ).</summary>
		// Token: 0x04000568 RID: 1384
		EndElement,
		/// <summary>Returned when XmlReader gets to the end of the entity replacement as a result of a call to <see cref="M:System.Xml.XmlReader.ResolveEntity" />.</summary>
		// Token: 0x04000569 RID: 1385
		EndEntity,
		/// <summary>The XML declaration (for example, &lt;?xml version='1.0'?&gt; ).</summary>
		// Token: 0x0400056A RID: 1386
		XmlDeclaration
	}
}
