using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	/// <summary>Returns information about errors encountered by the <see cref="T:System.Xml.Schema.XmlSchemaInference" /> class while inferring a schema from an XML document.</summary>
	// Token: 0x02000221 RID: 545
	[Serializable]
	public class XmlSchemaInferenceException : XmlSchemaException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class.</summary>
		// Token: 0x060015C7 RID: 5575 RVA: 0x000642C8 File Offset: 0x000624C8
		public XmlSchemaInferenceException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class with the error message specified.</summary>
		/// <param name="message">A description of the error.</param>
		// Token: 0x060015C8 RID: 5576 RVA: 0x000642D0 File Offset: 0x000624D0
		public XmlSchemaInferenceException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects specified that contain all the properties of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x060015C9 RID: 5577 RVA: 0x000642DC File Offset: 0x000624DC
		protected XmlSchemaInferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class with the error message specified and the original <see cref="T:System.Exception" /> that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> specified.</summary>
		/// <param name="message">A description of the error.</param>
		/// <param name="innerException">An <see cref="T:System.Exception" /> object containing the original exception that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" />.</param>
		// Token: 0x060015CA RID: 5578 RVA: 0x000642E8 File Offset: 0x000624E8
		public XmlSchemaInferenceException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class with the error message specified, the original <see cref="T:System.Exception" /> that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> specified, and the line number and line position of the error in the XML document specified.</summary>
		/// <param name="message">A description of the error.</param>
		/// <param name="innerException">An <see cref="T:System.Exception" /> object containing the original exception that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" />.</param>
		/// <param name="lineNumber">The line number in the XML document that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" />.</param>
		/// <param name="linePosition">The line position in the XML document that caused the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" />.</param>
		// Token: 0x060015CB RID: 5579 RVA: 0x000642F4 File Offset: 0x000624F4
		public XmlSchemaInferenceException(string message, Exception innerException, int line, int column) : base(message, innerException, line, column)
		{
		}

		/// <summary>Streams all the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> object properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object specified for the <see cref="T:System.Runtime.Serialization.StreamingContext" /> object specified.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x060015CC RID: 5580 RVA: 0x00064304 File Offset: 0x00062504
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
