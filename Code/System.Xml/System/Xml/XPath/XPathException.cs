using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.XPath
{
	/// <summary>Provides the exception thrown when an error occurs while processing an XPath expression. </summary>
	// Token: 0x02000142 RID: 322
	[Serializable]
	public class XPathException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class.</summary>
		// Token: 0x06000F1A RID: 3866 RVA: 0x00049328 File Offset: 0x00047528
		public XPathException() : base(string.Empty)
		{
		}

		/// <summary>Uses the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects to initialize a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains all the properties of an <see cref="T:System.Xml.XPath.XPathException" />. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object. </param>
		// Token: 0x06000F1B RID: 3867 RVA: 0x00049338 File Offset: 0x00047538
		protected XPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class using the specified exception message and <see cref="T:System.Exception" /> object.</summary>
		/// <param name="message">The description of the error condition. </param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that threw the <see cref="T:System.Xml.XPath.XPathException" />, if any. This value can be null. </param>
		// Token: 0x06000F1C RID: 3868 RVA: 0x00049344 File Offset: 0x00047544
		public XPathException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class with the specified exception message.</summary>
		/// <param name="message">The description of the error condition.</param>
		// Token: 0x06000F1D RID: 3869 RVA: 0x00049350 File Offset: 0x00047550
		public XPathException(string message) : base(message, null)
		{
		}

		/// <summary>Gets the description of the error condition for this exception.</summary>
		/// <returns>The string description of the error condition for this exception.</returns>
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0004935C File Offset: 0x0004755C
		public override string Message
		{
			get
			{
				return base.Message;
			}
		}

		/// <summary>Streams all the <see cref="T:System.Xml.XPath.XPathException" /> properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the specified <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06000F1F RID: 3871 RVA: 0x00049364 File Offset: 0x00047564
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
