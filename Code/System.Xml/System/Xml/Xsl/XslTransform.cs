using System;
using System.IO;
using System.Security.Policy;
using System.Xml.XPath;
using Mono.Xml.Xsl;

namespace System.Xml.Xsl
{
	/// <summary>Transforms XML data using an Extensible Stylesheet Language for Transformations (XSLT) style sheet.</summary>
	// Token: 0x020001B1 RID: 433
	public sealed class XslTransform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XslTransform" /> class.</summary>
		// Token: 0x060011AD RID: 4525 RVA: 0x00050EB8 File Offset: 0x0004F0B8
		public XslTransform() : this(XslTransform.GetDefaultDebugger())
		{
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00050EC8 File Offset: 0x0004F0C8
		internal XslTransform(object debugger)
		{
			this.debugger = debugger;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00050EE4 File Offset: 0x0004F0E4
		static XslTransform()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_XSLT_STACK_FRAME");
			string text = environmentVariable;
			switch (text)
			{
			case "stdout":
				XslTransform.TemplateStackFrameOutput = Console.Out;
				break;
			case "stderr":
				XslTransform.TemplateStackFrameOutput = Console.Error;
				break;
			case "error":
				XslTransform.TemplateStackFrameError = true;
				break;
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00050F94 File Offset: 0x0004F194
		private static object GetDefaultDebugger()
		{
			string text = null;
			try
			{
				text = Environment.GetEnvironmentVariable("MONO_XSLT_DEBUGGER");
			}
			catch (Exception)
			{
			}
			if (text == null)
			{
				return null;
			}
			if (text == "simple")
			{
				return new SimpleXsltDebugger();
			}
			return Activator.CreateInstance(Type.GetType(text));
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used to resolve external resources when the <see cref="Overload:System.Xml.Xsl.XslTransform.Transform" /> method is called.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlResolver" /> to use during transformation. If set to null, the XSLT document() function is not resolved.</returns>
		// Token: 0x1700051C RID: 1308
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x00051000 File Offset: 0x0004F200
		[MonoTODO]
		public XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		// Token: 0x060011B2 RID: 4530 RVA: 0x0005100C File Offset: 0x0004F20C
		public XmlReader Transform(IXPathNavigable input, XsltArgumentList args)
		{
			return this.Transform(input.CreateNavigator(), args, this.xmlResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.IXPathNavigable,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlResolver)" /> method completes. </param>
		// Token: 0x060011B3 RID: 4531 RVA: 0x00051024 File Offset: 0x0004F224
		public XmlReader Transform(IXPathNavigable input, XsltArgumentList args, XmlResolver resolver)
		{
			return this.Transform(input.CreateNavigator(), args, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011B4 RID: 4532 RVA: 0x00051034 File Offset: 0x0004F234
		public XmlReader Transform(XPathNavigator input, XsltArgumentList args)
		{
			return this.Transform(input, args, this.xmlResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> containing the results of the transformation.</returns>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011B5 RID: 4533 RVA: 0x00051044 File Offset: 0x0004F244
		public XmlReader Transform(XPathNavigator input, XsltArgumentList args, XmlResolver resolver)
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Transform(input, args, new XmlTextWriter(memoryStream, null), resolver);
			memoryStream.Position = 0L;
			return new XmlTextReader(memoryStream, XmlNodeType.Element, null);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011B6 RID: 4534 RVA: 0x00051078 File Offset: 0x0004F278
		public void Transform(IXPathNavigable input, XsltArgumentList args, TextWriter output)
		{
			this.Transform(input.CreateNavigator(), args, output, this.xmlResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.IXPathNavigable,System.Xml.Xsl.XsltArgumentList,System.IO.TextWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011B7 RID: 4535 RVA: 0x00051090 File Offset: 0x0004F290
		public void Transform(IXPathNavigable input, XsltArgumentList args, TextWriter output, XmlResolver resolver)
		{
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation.Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011B8 RID: 4536 RVA: 0x000510A4 File Offset: 0x0004F2A4
		public void Transform(IXPathNavigable input, XsltArgumentList args, Stream output)
		{
			this.Transform(input.CreateNavigator(), args, output, this.xmlResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Transform" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011B9 RID: 4537 RVA: 0x000510BC File Offset: 0x0004F2BC
		public void Transform(IXPathNavigable input, XsltArgumentList args, Stream output, XmlResolver resolver)
		{
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011BA RID: 4538 RVA: 0x000510D0 File Offset: 0x0004F2D0
		public void Transform(IXPathNavigable input, XsltArgumentList args, XmlWriter output)
		{
			this.Transform(input.CreateNavigator(), args, output, this.xmlResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.IXPathNavigable" /> using the specified <paramref name="args" /> and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.IXPathNavigable,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011BB RID: 4539 RVA: 0x000510E8 File Offset: 0x0004F2E8
		public void Transform(IXPathNavigable input, XsltArgumentList args, XmlWriter output, XmlResolver resolver)
		{
			this.Transform(input.CreateNavigator(), args, output, resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified args and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011BC RID: 4540 RVA: 0x000510FC File Offset: 0x0004F2FC
		public void Transform(XPathNavigator input, XsltArgumentList args, XmlWriter output)
		{
			this.Transform(input, args, output, this.xmlResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified args and outputs the result to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.Xml.XmlWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.Xml.XmlWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011BD RID: 4541 RVA: 0x00051110 File Offset: 0x0004F310
		public void Transform(XPathNavigator input, XsltArgumentList args, XmlWriter output, XmlResolver resolver)
		{
			if (this.s == null)
			{
				throw new XsltException("No stylesheet was loaded.", null);
			}
			Outputter outputtter = new GenericOutputter(output, this.s.Outputs, null);
			new XslTransformProcessor(this.s, this.debugger).Process(input, outputtter, args, resolver);
			output.Flush();
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011BE RID: 4542 RVA: 0x00051168 File Offset: 0x0004F368
		public void Transform(XPathNavigator input, XsltArgumentList args, Stream output)
		{
			this.Transform(input, args, output, this.xmlResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The stream to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.IO.Stream,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011BF RID: 4543 RVA: 0x0005117C File Offset: 0x0004F37C
		public void Transform(XPathNavigator input, XsltArgumentList args, Stream output, XmlResolver resolver)
		{
			XslOutput xslOutput = (XslOutput)this.s.Outputs[string.Empty];
			this.Transform(input, args, new StreamWriter(output, xslOutput.Encoding), resolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011C0 RID: 4544 RVA: 0x000511BC File Offset: 0x0004F3BC
		public void Transform(XPathNavigator input, XsltArgumentList args, TextWriter output)
		{
			this.Transform(input, args, output, this.xmlResolver);
		}

		/// <summary>Transforms the XML data in the <see cref="T:System.Xml.XPath.XPathNavigator" /> using the specified <paramref name="args" /> and outputs the result to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="input">An <see cref="T:System.Xml.XPath.XPathNavigator" /> containing the data to be transformed. </param>
		/// <param name="args">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transformation. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to which you want to output. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Transform(System.Xml.XPath.XPathNavigator,System.Xml.Xsl.XsltArgumentList,System.IO.TextWriter,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.InvalidOperationException">There was an error processing the XSLT transformation. Note: This is a change in behavior from earlier versions. An <see cref="T:System.Xml.Xsl.XsltException" /> is thrown if you are using Microsoft .NET Framework version 1.1 or earlier.</exception>
		// Token: 0x060011C1 RID: 4545 RVA: 0x000511D0 File Offset: 0x0004F3D0
		public void Transform(XPathNavigator input, XsltArgumentList args, TextWriter output, XmlResolver resolver)
		{
			if (this.s == null)
			{
				throw new XsltException("No stylesheet was loaded.", null);
			}
			Outputter outputter = new GenericOutputter(output, this.s.Outputs, output.Encoding);
			new XslTransformProcessor(this.s, this.debugger).Process(input, outputter, args, resolver);
			outputter.Done();
			output.Flush();
		}

		/// <summary>Transforms the XML data in the input file and outputs the result to an output file.</summary>
		/// <param name="inputfile">The URL of the source document to be transformed. </param>
		/// <param name="outputfile">The URL of the output file. </param>
		// Token: 0x060011C2 RID: 4546 RVA: 0x00051234 File Offset: 0x0004F434
		public void Transform(string inputfile, string outputfile)
		{
			this.Transform(inputfile, outputfile, this.xmlResolver);
		}

		/// <summary>Transforms the XML data in the input file and outputs the result to an output file.</summary>
		/// <param name="inputfile">The URL of the source document to be transformed. </param>
		/// <param name="outputfile">The URL of the output file. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve the XSLT document() function. If this is null, the document() function is not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Transform" /> method completes. </param>
		// Token: 0x060011C3 RID: 4547 RVA: 0x00051244 File Offset: 0x0004F444
		public void Transform(string inputfile, string outputfile, XmlResolver resolver)
		{
			using (Stream stream = new FileStream(outputfile, FileMode.Create, FileAccess.ReadWrite))
			{
				this.Transform(new XPathDocument(inputfile).CreateNavigator(), null, stream, resolver);
			}
		}

		/// <summary>Loads the XSLT style sheet specified by a URL.</summary>
		/// <param name="url">The URL that specifies the XSLT style sheet to load. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded script, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x060011C4 RID: 4548 RVA: 0x0005129C File Offset: 0x0004F49C
		public void Load(string url)
		{
			this.Load(url, null);
		}

		/// <summary>Loads the XSLT style sheet specified by a URL.</summary>
		/// <param name="url">The URL that specifies the XSLT style sheet to load. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> to use to load the style sheet and any style sheet(s) referenced in xsl:import and xsl:include elements.If this is null, a default <see cref="T:System.Xml.XmlUrlResolver" /> with no user credentials is used to open the style sheet. The default <see cref="T:System.Xml.XmlUrlResolver" /> is not used to resolve any external resources in the style sheet, so xsl:import and xsl:include elements are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Load(System.String,System.Xml.XmlResolver)" /> method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded script, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x060011C5 RID: 4549 RVA: 0x000512A8 File Offset: 0x0004F4A8
		public void Load(string url, XmlResolver resolver)
		{
			XmlResolver xmlResolver = resolver;
			if (xmlResolver == null)
			{
				xmlResolver = new XmlUrlResolver();
			}
			Uri uri = xmlResolver.ResolveUri(null, url);
			using (Stream stream = xmlResolver.GetEntity(uri, null, typeof(Stream)) as Stream)
			{
				this.Load(new XPathDocument(new XmlValidatingReader(new XmlTextReader(uri.ToString(), stream)
				{
					XmlResolver = xmlResolver
				})
				{
					XmlResolver = xmlResolver,
					ValidationType = ValidationType.None
				}, XmlSpace.Preserve).CreateNavigator(), resolver, null);
			}
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> object that contains the XSLT style sheet. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x060011C6 RID: 4550 RVA: 0x00051354 File Offset: 0x0004F554
		public void Load(XmlReader stylesheet)
		{
			this.Load(stylesheet, null, null);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> object that contains the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="M:System.Xml.Xsl.XslTransform.Load(System.Xml.XmlReader,System.Xml.XmlResolver)" />  method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x060011C7 RID: 4551 RVA: 0x00051360 File Offset: 0x0004F560
		public void Load(XmlReader stylesheet, XmlResolver resolver)
		{
			this.Load(stylesheet, resolver, null);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the XSLT style sheet. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x060011C8 RID: 4552 RVA: 0x0005136C File Offset: 0x0004F56C
		public void Load(XPathNavigator stylesheet)
		{
			this.Load(stylesheet, null, null);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object that contains the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x060011C9 RID: 4553 RVA: 0x00051378 File Offset: 0x0004F578
		public void Load(XPathNavigator stylesheet, XmlResolver resolver)
		{
			this.Load(stylesheet, resolver, null);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the XSLT style sheet. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x060011CA RID: 4554 RVA: 0x00051384 File Offset: 0x0004F584
		public void Load(IXPathNavigable stylesheet)
		{
			this.Load(stylesheet.CreateNavigator(), null);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The style sheet contains embedded scripts, and the caller does not have UnmanagedCode permission. </exception>
		// Token: 0x060011CB RID: 4555 RVA: 0x00051394 File Offset: 0x0004F594
		public void Load(IXPathNavigable stylesheet, XmlResolver resolver)
		{
			this.Load(stylesheet.CreateNavigator(), resolver);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.IXPathNavigable" />. This method allows you to limit the permissions of the style sheet by specifying evidence.</summary>
		/// <param name="stylesheet">An object implementing the <see cref="T:System.Xml.XPath.IXPathNavigable" /> interface. In the .NET Framework, this can be either an <see cref="T:System.Xml.XmlNode" /> (typically an <see cref="T:System.Xml.XmlDocument" />), or an <see cref="T:System.Xml.XPath.XPathDocument" /> containing the XSLT style sheet. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> set on the assembly generated for the script block in the XSLT style sheet.If this is null, script blocks are not processed, the XSLT document() function is not supported, and privileged extension objects are disallowed.The caller must have ControlEvidence permission in order to supply evidence for the script assembly. Semi-trusted callers can set this parameter to null. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The loaded resource is not a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The referenced style sheet requires functionality that is not allowed by the evidence provided.The caller tries to supply evidence and does not have ControlEvidence permission. </exception>
		// Token: 0x060011CC RID: 4556 RVA: 0x000513A4 File Offset: 0x0004F5A4
		public void Load(IXPathNavigable stylesheet, XmlResolver resolver, Evidence evidence)
		{
			this.Load(stylesheet.CreateNavigator(), resolver, evidence);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XPath.XPathNavigator" />. This method allows you to limit the permissions of the style sheet by specifying evidence.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XPath.XPathNavigator" /> object containing the style sheet to load. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> set on the assembly generated for the script block in the XSLT style sheet.If this is null, script blocks are not processed, the XSLT document() function is not supported, and privileged extension objects are disallowed.The caller must have ControlEvidence permission in order to supply evidence for the script assembly. Semi-trusted callers can set this parameter to null. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The referenced style sheet requires functionality that is not allowed by the evidence provided.The caller tries to supply evidence and does not have ControlEvidence permission. </exception>
		// Token: 0x060011CD RID: 4557 RVA: 0x000513B4 File Offset: 0x0004F5B4
		public void Load(XPathNavigator stylesheet, XmlResolver resolver, Evidence evidence)
		{
			this.s = new Compiler(this.debugger).Compile(stylesheet, resolver, evidence);
		}

		/// <summary>Loads the XSLT style sheet contained in the <see cref="T:System.Xml.XmlReader" />. This method allows you to limit the permissions of the style sheet by specifying evidence.</summary>
		/// <param name="stylesheet">An <see cref="T:System.Xml.XmlReader" /> object containing the style sheet to load. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to load any style sheets referenced in xsl:import and xsl:include elements. If this is null, external resources are not resolved.The <see cref="T:System.Xml.XmlResolver" /> is not cached after the <see cref="Overload:System.Xml.Xsl.XslTransform.Load" /> method completes. </param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> set on the assembly generated for the script block in the XSLT style sheet.If this is null, script blocks are not processed, the XSLT document() function is not supported, and privileged extension objects are disallowed.The caller must have ControlEvidence permission in order to supply evidence for the script assembly. Semi-trusted callers can set this parameter to null. </param>
		/// <exception cref="T:System.Xml.Xsl.XsltCompileException">The current node does not conform to a valid style sheet. </exception>
		/// <exception cref="T:System.Security.SecurityException">The referenced style sheet requires functionality that is not allowed by the evidence provided.The caller tries to supply evidence and does not have ControlEvidence permission. </exception>
		// Token: 0x060011CE RID: 4558 RVA: 0x000513D0 File Offset: 0x0004F5D0
		public void Load(XmlReader stylesheet, XmlResolver resolver, Evidence evidence)
		{
			this.Load(new XPathDocument(stylesheet, XmlSpace.Preserve).CreateNavigator(), resolver, evidence);
		}

		// Token: 0x04000751 RID: 1873
		internal static readonly bool TemplateStackFrameError;

		// Token: 0x04000752 RID: 1874
		internal static readonly TextWriter TemplateStackFrameOutput;

		// Token: 0x04000753 RID: 1875
		private object debugger;

		// Token: 0x04000754 RID: 1876
		private CompiledStylesheet s;

		// Token: 0x04000755 RID: 1877
		private XmlResolver xmlResolver = new XmlUrlResolver();
	}
}
