using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	/// <summary>The exception that is thrown by the Load method when an error is found in the XSLT style sheet.</summary>
	// Token: 0x020001B3 RID: 435
	[Serializable]
	public class XsltCompileException : XsltException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltCompileException" /> class.</summary>
		// Token: 0x060011DA RID: 4570 RVA: 0x000516D8 File Offset: 0x0004F8D8
		public XsltCompileException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltCompileException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060011DB RID: 4571 RVA: 0x000516E0 File Offset: 0x0004F8E0
		public XsltCompileException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltCompileException" /> class specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or null if no inner exception is specified. </param>
		// Token: 0x060011DC RID: 4572 RVA: 0x000516EC File Offset: 0x0004F8EC
		public XsltCompileException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the XsltCompileException class using the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The SerializationInfo object containing all the properties of an XsltCompileException. </param>
		/// <param name="context">The StreamingContext object containing the context information. </param>
		// Token: 0x060011DD RID: 4573 RVA: 0x000516F8 File Offset: 0x0004F8F8
		protected XsltCompileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the XsltCompileException class.</summary>
		/// <param name="inner">The <see cref="T:System.Exception" /> that threw the XsltCompileException. </param>
		/// <param name="sourceUri">The location path of the style sheet. </param>
		/// <param name="lineNumber">The line number indicating where the error occurred in the style sheet. </param>
		/// <param name="linePosition">The line position indicating where the error occurred in the style sheet. </param>
		// Token: 0x060011DE RID: 4574 RVA: 0x00051704 File Offset: 0x0004F904
		public XsltCompileException(Exception inner, string sourceUri, int lineNumber, int linePosition) : base((lineNumber == 0) ? "{0}." : "{0} at {1}({2},{3}). See InnerException for details.", "XSLT compile error", inner, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00051738 File Offset: 0x0004F938
		internal XsltCompileException(string message, Exception innerException, XPathNavigator nav) : base(message, innerException, nav)
		{
		}

		/// <summary>Streams all the XsltCompileException properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The SerializationInfo object. </param>
		/// <param name="context">The StreamingContext object. </param>
		// Token: 0x060011E0 RID: 4576 RVA: 0x00051744 File Offset: 0x0004F944
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
