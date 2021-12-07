using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	/// <summary>Provides an interface to a given function defined in the Extensible Stylesheet Language for Transformations (XSLT) style sheet during runtime execution.</summary>
	// Token: 0x020001AC RID: 428
	public interface IXsltContextFunction
	{
		/// <summary>Gets the supplied XML Path Language (XPath) types for the function's argument list. This information can be used to discover the signature of the function which allows you to differentiate between overloaded functions.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XPath.XPathResultType" /> representing the types for the function's argument list.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001180 RID: 4480
		XPathResultType[] ArgTypes { get; }

		/// <summary>Gets the maximum number of arguments for the function. This enables the user to differentiate between overloaded functions.</summary>
		/// <returns>The maximum number of arguments for the function.</returns>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001181 RID: 4481
		int Maxargs { get; }

		/// <summary>Gets the minimum number of arguments for the function. This enables the user to differentiate between overloaded functions.</summary>
		/// <returns>The minimum number of arguments for the function.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001182 RID: 4482
		int Minargs { get; }

		/// <summary>Gets the <see cref="T:System.Xml.XPath.XPathResultType" /> representing the XPath type returned by the function.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathResultType" /> representing the XPath type returned by the function </returns>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001183 RID: 4483
		XPathResultType ReturnType { get; }

		/// <summary>Provides the method to invoke the function with the given arguments in the given context.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the return value of the function.</returns>
		/// <param name="xsltContext">The XSLT context for the function call. </param>
		/// <param name="args">The arguments of the function call. Each argument is an element in the array. </param>
		/// <param name="docContext">The context node for the function call. </param>
		// Token: 0x06001184 RID: 4484
		object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext);
	}
}
