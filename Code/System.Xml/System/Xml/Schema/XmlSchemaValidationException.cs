using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	/// <summary>Represents the exception thrown when XML Schema Definition Language (XSD) schema validation errors and warnings are encountered in an XML document being validated. </summary>
	// Token: 0x02000249 RID: 585
	[Serializable]
	public class XmlSchemaValidationException : XmlSchemaException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class.</summary>
		// Token: 0x060017AC RID: 6060 RVA: 0x0007761C File Offset: 0x0007581C
		public XmlSchemaValidationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the exception message specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		// Token: 0x060017AD RID: 6061 RVA: 0x00077624 File Offset: 0x00075824
		public XmlSchemaValidationException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects specified.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x060017AE RID: 6062 RVA: 0x00077630 File Offset: 0x00075830
		protected XmlSchemaValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the exception message specified, and the original <see cref="T:System.Exception" /> object, line number, and line position of the XML that cause this exception specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		/// <param name="innerException">The original <see cref="T:System.Exception" /> object that caused this exception.</param>
		/// <param name="lineNumber">The line number of the XML that caused this exception.</param>
		/// <param name="linePosition">The line position of the XML that caused this exception.</param>
		// Token: 0x060017AF RID: 6063 RVA: 0x0007763C File Offset: 0x0007583C
		public XmlSchemaValidationException(string message, Exception innerException, int lineNumber, int linePosition) : base(message, lineNumber, linePosition, null, null, innerException)
		{
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0007764C File Offset: 0x0007584C
		internal XmlSchemaValidationException(string message, int lineNumber, int linePosition, XmlSchemaObject sourceObject, string sourceUri, Exception innerException) : base(message, lineNumber, linePosition, sourceObject, sourceUri, innerException)
		{
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00077660 File Offset: 0x00075860
		internal XmlSchemaValidationException(string message, object sender, string sourceUri, XmlSchemaObject sourceObject, Exception innerException) : base(message, sender, sourceUri, sourceObject, innerException)
		{
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00077670 File Offset: 0x00075870
		internal XmlSchemaValidationException(string message, XmlSchemaObject sourceObject, Exception innerException) : base(message, sourceObject, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the exception message and original <see cref="T:System.Exception" /> object that caused this exception specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		/// <param name="innerException">The original <see cref="T:System.Exception" /> object that caused this exception.</param>
		// Token: 0x060017B3 RID: 6067 RVA: 0x0007767C File Offset: 0x0007587C
		public XmlSchemaValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Constructs a new <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> object with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> information that contains all the properties of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" />.</summary>
		/// <param name="info">
		///   <see cref="T:System.Runtime.Serialization.SerializationInfo" />
		/// </param>
		/// <param name="context">
		///   <see cref="T:System.Runtime.Serialization.StreamingContext" />
		/// </param>
		// Token: 0x060017B4 RID: 6068 RVA: 0x00077688 File Offset: 0x00075888
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Sets the XML node that causes the error.</summary>
		/// <param name="sourceObject">The source object.</param>
		// Token: 0x060017B5 RID: 6069 RVA: 0x00077694 File Offset: 0x00075894
		protected internal void SetSourceObject(object o)
		{
			this.source_object = o;
		}

		/// <summary>Gets the XML node that caused this <see cref="T:System.Xml.Schema.XmlSchemaValidationException" />.</summary>
		/// <returns>The XML node that caused this <see cref="T:System.Xml.Schema.XmlSchemaValidationException" />.</returns>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000776A0 File Offset: 0x000758A0
		public object SourceObject
		{
			get
			{
				return this.source_object;
			}
		}

		// Token: 0x040009A7 RID: 2471
		private object source_object;
	}
}
