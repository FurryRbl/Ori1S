using System;
using System.Collections;
using System.Xml.Xsl;
using Mono.Xml.XPath;

namespace System.Xml.XPath
{
	/// <summary>Provides a typed class that represents a compiled XPath expression.</summary>
	// Token: 0x02000138 RID: 312
	public abstract class XPathExpression
	{
		// Token: 0x06000EF2 RID: 3826 RVA: 0x00049144 File Offset: 0x00047344
		internal XPathExpression()
		{
		}

		/// <summary>When overridden in a derived class, gets a string representation of the <see cref="T:System.Xml.XPath.XPathExpression" />.</summary>
		/// <returns>A string representation of the <see cref="T:System.Xml.XPath.XPathExpression" />.</returns>
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000EF3 RID: 3827
		public abstract string Expression { get; }

		/// <summary>When overridden in a derived class, gets the result type of the XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathResultType" /> value representing the result type of the XPath expression.</returns>
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000EF4 RID: 3828
		public abstract XPathResultType ReturnType { get; }

		/// <summary>When overridden in a derived class, sorts the nodes selected by the XPath expression according to the specified <see cref="T:System.Collections.IComparer" /> object.</summary>
		/// <param name="expr">An object representing the sort key. This can be the string value of the node or an <see cref="T:System.Xml.XPath.XPathExpression" /> object with a compiled XPath expression.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.IComparer" /> object that provides the specific data type comparisons for comparing two objects for equivalence. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The <see cref="T:System.Xml.XPath.XPathExpression" /> or sort key includes a prefix and either an <see cref="T:System.Xml.XmlNamespaceManager" /> is not provided, or the prefix cannot be found in the supplied <see cref="T:System.Xml.XmlNamespaceManager" />.</exception>
		// Token: 0x06000EF5 RID: 3829
		public abstract void AddSort(object expr, IComparer comparer);

		/// <summary>When overridden in a derived class, sorts the nodes selected by the XPath expression according to the supplied parameters.</summary>
		/// <param name="expr">An object representing the sort key. This can be the string value of the node or an <see cref="T:System.Xml.XPath.XPathExpression" /> object with a compiled XPath expression. </param>
		/// <param name="order">An <see cref="T:System.Xml.XPath.XmlSortOrder" /> value indicating the sort order. </param>
		/// <param name="caseOrder">An <see cref="T:System.Xml.XPath.XmlCaseOrder" /> value indicating how to sort uppercase and lowercase letters.</param>
		/// <param name="lang">The language to use for comparison. Uses the <see cref="T:System.Globalization.CultureInfo" /> class that can be passed to the <see cref="Overload:System.String.Compare" /> method for the language types, for example, "us-en" for U.S. English. If an empty string is specified, the system environment is used to determine the <see cref="T:System.Globalization.CultureInfo" />. </param>
		/// <param name="dataType">An <see cref="T:System.Xml.XPath.XmlDataType" /> value indicating the sort order for the data type. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The <see cref="T:System.Xml.XPath.XPathExpression" /> or sort key includes a prefix and either an <see cref="T:System.Xml.XmlNamespaceManager" /> is not provided, or the prefix cannot be found in the supplied <see cref="T:System.Xml.XmlNamespaceManager" />. </exception>
		// Token: 0x06000EF6 RID: 3830
		public abstract void AddSort(object expr, XmlSortOrder order, XmlCaseOrder caseOrder, string lang, XmlDataType dataType);

		/// <summary>When overridden in a derived class, returns a clone of this <see cref="T:System.Xml.XPath.XPathExpression" />.</summary>
		/// <returns>A new <see cref="T:System.Xml.XPath.XPathExpression" /> object.</returns>
		// Token: 0x06000EF7 RID: 3831
		public abstract XPathExpression Clone();

		/// <summary>When overridden in a derived class, specifies the <see cref="T:System.Xml.XmlNamespaceManager" /> object to use for namespace resolution.</summary>
		/// <param name="nsManager">An <see cref="T:System.Xml.XmlNamespaceManager" /> object to use for namespace resolution. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The <see cref="T:System.Xml.XmlNamespaceManager" /> object parameter is not derived from the <see cref="T:System.Xml.XmlNamespaceManager" /> class. </exception>
		// Token: 0x06000EF8 RID: 3832
		public abstract void SetContext(XmlNamespaceManager nsManager);

		/// <summary>Compiles the XPath expression specified and returns an <see cref="T:System.Xml.XPath.XPathExpression" /> object representing the XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathExpression" /> object.</returns>
		/// <param name="xpath">An XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression parameter is not a valid XPath expression.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000EF9 RID: 3833 RVA: 0x0004914C File Offset: 0x0004734C
		public static XPathExpression Compile(string xpath)
		{
			return XPathExpression.Compile(xpath, null, null);
		}

		/// <summary>Compiles the specified XPath expression, with the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified for namespace resolution, and returns an <see cref="T:System.Xml.XPath.XPathExpression" /> object that represents the XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathExpression" /> object.</returns>
		/// <param name="xpath">An XPath expression.</param>
		/// <param name="nsResolver">An object that implements the <see cref="T:System.Xml.IXmlNamespaceResolver" /> interface for namespace resolution.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression parameter is not a valid XPath expression.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000EFA RID: 3834 RVA: 0x00049158 File Offset: 0x00047358
		public static XPathExpression Compile(string xpath, IXmlNamespaceResolver nsmgr)
		{
			return XPathExpression.Compile(xpath, nsmgr, null);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00049164 File Offset: 0x00047364
		internal static XPathExpression Compile(string xpath, IXmlNamespaceResolver nsmgr, IStaticXsltContext ctx)
		{
			XPathParser xpathParser = new XPathParser(ctx);
			CompiledExpression compiledExpression = new CompiledExpression(xpath, xpathParser.Compile(xpath));
			compiledExpression.SetContext(nsmgr);
			return compiledExpression;
		}

		/// <summary>When overridden in a derived class, specifies the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object to use for namespace resolution.</summary>
		/// <param name="nsResolver">An object that implements the <see cref="T:System.Xml.IXmlNamespaceResolver" /> interface to use for namespace resolution.</param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object parameter is not derived from <see cref="T:System.Xml.IXmlNamespaceResolver" />. </exception>
		// Token: 0x06000EFC RID: 3836
		public abstract void SetContext(IXmlNamespaceResolver nsResolver);
	}
}
