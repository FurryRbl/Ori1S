using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Xml.XPath;
using Mono.Xml.Xsl;

namespace System.Xml.Xsl
{
	/// <summary>Transforms XML data using an XSLT style sheet.</summary>
	// Token: 0x020001AE RID: 430
	[MonoTODO]
	public sealed class XslCompiledTransform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XslCompiledTransform" /> class. </summary>
		// Token: 0x06001189 RID: 4489 RVA: 0x00050A34 File Offset: 0x0004EC34
		public XslCompiledTransform() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XslCompiledTransform" /> class with the specified debug setting. </summary>
		/// <param name="enableDebug">true to generate debug information; otherwise false. Setting this to true enables you to debug the style sheet with the Microsoft Visual Studio Debugger.</param>
		// Token: 0x0600118A RID: 4490 RVA: 0x00050A40 File Offset: 0x0004EC40
		public XslCompiledTransform(bool enableDebug)
		{
			this.enable_debug = enableDebug;
			if (this.enable_debug)
			{
				this.debugger = new NoOperationDebugger();
			}
			this.output_settings.ConformanceLevel = ConformanceLevel.Fragment;
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlWriterSettings" /> object that contains the output information derived from the xsl:output element of the style sheet.</summary>
		/// <returns>A read-only <see cref="T:System.Xml.XmlWriterSettings" /> object that contains the output information derived from the xsl:output element of the style sheet. This value can be null.</returns>
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00050A88 File Offset: 0x0004EC88
		[MonoTODO]
		public XmlWriterSettings OutputSettings
		{
			get
			{
				return this.output_settings;
			}
		}

		/// <summary>Gets the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> that contains the temporary files generated on disk after a successful call to the <see cref="Overload:System.Xml.Xsl.XslCompiledTransform.Load" /> method. </summary>
		/// <returns>The <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> that contains the temporary files generated on disk. This value is null if the <see cref="Overload:System.Xml.Xsl.XslCompiledTransform.Load" /> method has not been successfully called, or if debugging has not been enabled.</returns>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00050A90 File Offset: 0x0004EC90
		[MonoTODO]
		public TempFileCollection TemporaryFiles
		{
			get
			{
				return null;
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to a file.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="resultsFile">The URI of the output file.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="resultsFile" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The input document cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> or <paramref name="resultsFile" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> or <paramref name="resultsFile" /> value cannot be resolved.-or-An error occurred while processing the request</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="inputUri" /> or <paramref name="resultsFile" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x0600118D RID: 4493 RVA: 0x00050A94 File Offset: 0x0004EC94
		public void Transform(string inputfile, string outputfile)
		{
			using (Stream stream = File.Create(outputfile))
			{
				this.Transform(new XPathDocument(inputfile, XmlSpace.Preserve), null, stream);
			}
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x0600118E RID: 4494 RVA: 0x00050AE8 File Offset: 0x0004ECE8
		public void Transform(string inputfile, XmlWriter output)
		{
			this.Transform(inputfile, null, output);
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to stream. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The stream to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x0600118F RID: 4495 RVA: 0x00050AF4 File Offset: 0x0004ECF4
		public void Transform(string inputfile, XsltArgumentList args, Stream output)
		{
			this.Transform(new XPathDocument(inputfile, XmlSpace.Preserve), args, output);
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.IO.TextWriter" /> to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x06001190 RID: 4496 RVA: 0x00050B08 File Offset: 0x0004ED08
		public void Transform(string inputfile, XsltArgumentList args, TextWriter output)
		{
			this.Transform(new XPathDocument(inputfile, XmlSpace.Preserve), args, output);
		}

		/// <summary>Executes the transform using the input document specified by the URI and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="inputUri">The URI of the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="inputtUri" /> value includes a filename or directory cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="inputUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="inputUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the input document.</exception>
		// Token: 0x06001191 RID: 4497 RVA: 0x00050B1C File Offset: 0x0004ED1C
		public void Transform(string inputfile, XsltArgumentList args, XmlWriter output)
		{
			this.Transform(new XPathDocument(inputfile, XmlSpace.Preserve), args, output);
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">The <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06001192 RID: 4498 RVA: 0x00050B30 File Offset: 0x0004ED30
		public void Transform(XmlReader reader, XmlWriter output)
		{
			this.Transform(reader, null, output);
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to a stream. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The stream to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06001193 RID: 4499 RVA: 0x00050B3C File Offset: 0x0004ED3C
		public void Transform(XmlReader reader, XsltArgumentList args, Stream output)
		{
			this.Transform(new XPathDocument(reader, XmlSpace.Preserve), args, output);
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to a <see cref="T:System.IO.TextWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.IO.TextWriter" /> to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06001194 RID: 4500 RVA: 0x00050B50 File Offset: 0x0004ED50
		public void Transform(XmlReader reader, XsltArgumentList args, TextWriter output)
		{
			this.Transform(new XPathDocument(reader, XmlSpace.Preserve), args, output);
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06001195 RID: 4501 RVA: 0x00050B64 File Offset: 0x0004ED64
		public void Transform(XmlReader reader, XsltArgumentList args, XmlWriter output)
		{
			this.Transform(reader, args, output, null);
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to an <see cref="T:System.IO.TextWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.IO.TextWriter" /> to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06001196 RID: 4502 RVA: 0x00050B70 File Offset: 0x0004ED70
		public void Transform(IXPathNavigable input, XsltArgumentList args, TextWriter output)
		{
			this.Transform(input.CreateNavigator(), args, output);
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to a stream. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional runtime arguments.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The stream to which you want to output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06001197 RID: 4503 RVA: 0x00050B80 File Offset: 0x0004ED80
		public void Transform(IXPathNavigable input, XsltArgumentList args, Stream output)
		{
			this.Transform(input.CreateNavigator(), args, output);
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06001198 RID: 4504 RVA: 0x00050B90 File Offset: 0x0004ED90
		public void Transform(IXPathNavigable input, XmlWriter output)
		{
			this.Transform(input, null, output);
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x06001199 RID: 4505 RVA: 0x00050B9C File Offset: 0x0004ED9C
		public void Transform(IXPathNavigable input, XsltArgumentList args, XmlWriter output)
		{
			this.Transform(input.CreateNavigator(), args, output, null);
		}

		/// <summary>Executes the transform using the input document specified by the <see cref="T:System.Xml.XmlReader" /> object and outputs the results to an <see cref="T:System.Xml.XmlWriter" />. The <see cref="T:System.Xml.Xsl.XsltArgumentList" /> provides additional run-time arguments and the XmlResolver resolves the XSLT document() function.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XmlReader" /> containing the input document.</param>
		/// <param name="arguments">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform. This value can be null.</param>
		/// <param name="results">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output.If the style sheet contains an xsl:output element, you should create the <see cref="T:System.Xml.XmlWriter" /> using the <see cref="T:System.Xml.XmlWriterSettings" /> object returned from the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings" /> property. This ensures that the <see cref="T:System.Xml.XmlWriter" /> has the correct output settings.</param>
		/// <param name="documentResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> or <paramref name="results" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">There was an error executing the XSLT transform.</exception>
		// Token: 0x0600119A RID: 4506 RVA: 0x00050BB0 File Offset: 0x0004EDB0
		public void Transform(XmlReader input, XsltArgumentList args, XmlWriter output, XmlResolver resolver)
		{
			this.Transform(new XPathDocument(input, XmlSpace.Preserve).CreateNavigator(), args, output, resolver);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00050BC8 File Offset: 0x0004EDC8
		private void Transform(XPathNavigator input, XsltArgumentList args, XmlWriter output, XmlResolver resolver)
		{
			if (this.s == null)
			{
				throw new XsltException("No stylesheet was loaded.", null);
			}
			Outputter outputtter = new GenericOutputter(output, this.s.Outputs, null);
			new XslTransformProcessor(this.s, this.debugger).Process(input, outputtter, args, resolver);
			output.Flush();
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00050C20 File Offset: 0x0004EE20
		private void Transform(XPathNavigator input, XsltArgumentList args, Stream output)
		{
			XslOutput xslOutput = (XslOutput)this.s.Outputs[string.Empty];
			this.Transform(input, args, new StreamWriter(output, xslOutput.Encoding));
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00050C5C File Offset: 0x0004EE5C
		private void Transform(XPathNavigator input, XsltArgumentList args, TextWriter output)
		{
			if (this.s == null)
			{
				throw new XsltException("No stylesheet was loaded.", null);
			}
			Outputter outputter = new GenericOutputter(output, this.s.Outputs, output.Encoding);
			new XslTransformProcessor(this.s, this.debugger).Process(input, outputter, args, null);
			outputter.Done();
			output.Flush();
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00050CC0 File Offset: 0x0004EEC0
		private XmlReader GetXmlReader(string url)
		{
			XmlResolver xmlResolver = new XmlUrlResolver();
			Uri uri = xmlResolver.ResolveUri(null, url);
			Stream input = xmlResolver.GetEntity(uri, null, typeof(Stream)) as Stream;
			return new XmlValidatingReader(new XmlTextReader(uri.ToString(), input)
			{
				XmlResolver = xmlResolver
			})
			{
				XmlResolver = xmlResolver,
				ValidationType = ValidationType.None
			};
		}

		/// <summary>Loads and compiles the style sheet located at the specified URI.</summary>
		/// <param name="stylesheetUri">The URI of the style sheet.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheetUri" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The style sheet cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="stylesheetUri" /> value includes a filename or directory that cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="stylesheetUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="stylesheetUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the style sheet.</exception>
		// Token: 0x0600119F RID: 4511 RVA: 0x00050D24 File Offset: 0x0004EF24
		public void Load(string url)
		{
			using (XmlReader xmlReader = this.GetXmlReader(url))
			{
				this.Load(xmlReader);
			}
		}

		/// <summary>Compiles the style sheet contained in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> containing the style sheet.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x060011A0 RID: 4512 RVA: 0x00050D70 File Offset: 0x0004EF70
		public void Load(XmlReader stylesheet)
		{
			this.Load(stylesheet, null, null);
		}

		/// <summary>Compiles the style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the style sheet.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x060011A1 RID: 4513 RVA: 0x00050D7C File Offset: 0x0004EF7C
		public void Load(IXPathNavigable stylesheet)
		{
			this.Load(stylesheet.CreateNavigator(), null, null);
		}

		/// <summary>Compiles the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />. The <see cref="T:System.Xml.XmlResolver" /> resolves any XSLT import or include elements and the XSLT settings determine the permissions for the style sheet.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the Microsoft .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the style sheet.</param>
		/// <param name="settings">The <see cref="T:System.Xml.Xsl.XsltSettings" /> to apply to the style sheet. If this is null, the <see cref="P:System.Xml.Xsl.XsltSettings.Default" /> setting is applied.</param>
		/// <param name="stylesheetResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve any style sheets referenced in XSLT import and include elements. If this is null, external resources are not resolved.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x060011A2 RID: 4514 RVA: 0x00050D8C File Offset: 0x0004EF8C
		public void Load(IXPathNavigable stylesheet, XsltSettings settings, XmlResolver resolver)
		{
			this.Load(stylesheet.CreateNavigator(), settings, resolver);
		}

		/// <summary>Compiles the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />. The <see cref="T:System.Xml.XmlResolver" /> resolves any XSLT import or include elements and the XSLT settings determine the permissions for the style sheet.</summary>
		/// <param name="stylesheet">The <see cref="T:System.Xml.XmlReader" /> containing the style sheet.</param>
		/// <param name="settings">The <see cref="T:System.Xml.Xsl.XsltSettings" /> to apply to the style sheet. If this is null, the <see cref="P:System.Xml.Xsl.XsltSettings.Default" /> setting is applied.</param>
		/// <param name="stylesheetResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve any style sheets referenced in XSLT import and include elements. If this is null, external resources are not resolved.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheet" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		// Token: 0x060011A3 RID: 4515 RVA: 0x00050D9C File Offset: 0x0004EF9C
		public void Load(XmlReader stylesheet, XsltSettings settings, XmlResolver resolver)
		{
			this.Load(new XPathDocument(stylesheet, XmlSpace.Preserve).CreateNavigator(), settings, resolver);
		}

		/// <summary>Loads and compiles the XSLT style sheet specified by the URI. The <see cref="T:System.Xml.XmlResolver" /> resolves any XSLT import or include elements and the XSLT settings determine the permissions for the style sheet.</summary>
		/// <param name="stylesheetUri">The URI of the style sheet.</param>
		/// <param name="settings">The <see cref="T:System.Xml.Xsl.XsltSettings" /> to apply to the style sheet. If this is null, the <see cref="P:System.Xml.Xsl.XsltSettings.Default" /> setting is applied.</param>
		/// <param name="stylesheetResolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the style sheet URI and any style sheets referenced in XSLT import and include elements. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stylesheetUri" /> or <paramref name="stylesheetResolver" /> value is null.</exception>
		/// <exception cref="T:System.Xml.Xsl.XsltException">The style sheet contains an error.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The style sheet cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The <paramref name="stylesheetUri" /> value includes a filename or directory that cannot be found.</exception>
		/// <exception cref="T:System.Net.WebException">The <paramref name="stylesheetUri" /> value cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="stylesheetUri" /> is not a valid URI.</exception>
		/// <exception cref="T:System.Xml.XmlException">There was a parsing error loading the style sheet.</exception>
		// Token: 0x060011A4 RID: 4516 RVA: 0x00050DB4 File Offset: 0x0004EFB4
		public void Load(string stylesheet, XsltSettings settings, XmlResolver resolver)
		{
			this.Load(new XPathDocument(stylesheet, XmlSpace.Preserve).CreateNavigator(), settings, resolver);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00050DCC File Offset: 0x0004EFCC
		private void Load(XPathNavigator stylesheet, XsltSettings settings, XmlResolver resolver)
		{
			this.s = new Compiler(this.debugger).Compile(stylesheet, resolver, null);
		}

		// Token: 0x0400074D RID: 1869
		private bool enable_debug;

		// Token: 0x0400074E RID: 1870
		private object debugger;

		// Token: 0x0400074F RID: 1871
		private CompiledStylesheet s;

		// Token: 0x04000750 RID: 1872
		private XmlWriterSettings output_settings = new XmlWriterSettings();
	}
}
