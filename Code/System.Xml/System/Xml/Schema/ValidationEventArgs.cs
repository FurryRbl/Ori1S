using System;

namespace System.Xml.Schema
{
	/// <summary>Returns detailed information related to the ValidationEventHandler.</summary>
	// Token: 0x020001F3 RID: 499
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x0600138C RID: 5004 RVA: 0x000534F4 File Offset: 0x000516F4
		private ValidationEventArgs()
		{
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000534FC File Offset: 0x000516FC
		internal ValidationEventArgs(XmlSchemaException ex, string message, XmlSeverityType severity)
		{
			this.exception = ex;
			this.message = message;
			this.severity = severity;
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaException" /> associated with the validation event.</summary>
		/// <returns>The XmlSchemaException associated with the validation event.</returns>
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0005351C File Offset: 0x0005171C
		public XmlSchemaException Exception
		{
			get
			{
				return this.exception;
			}
		}

		/// <summary>Gets the text description corresponding to the validation event.</summary>
		/// <returns>The text description.</returns>
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x00053524 File Offset: 0x00051724
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		/// <summary>Gets the severity of the validation event.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSeverityType" /> value representing the severity of the validation event.</returns>
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0005352C File Offset: 0x0005172C
		public XmlSeverityType Severity
		{
			get
			{
				return this.severity;
			}
		}

		// Token: 0x0400077E RID: 1918
		private XmlSchemaException exception;

		// Token: 0x0400077F RID: 1919
		private string message;

		// Token: 0x04000780 RID: 1920
		private XmlSeverityType severity;
	}
}
