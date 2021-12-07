using System;

namespace System.Xml.Schema
{
	// Token: 0x0200024D RID: 589
	internal class ValidationHandler
	{
		// Token: 0x060017C5 RID: 6085 RVA: 0x000782E0 File Offset: 0x000764E0
		public static void RaiseValidationEvent(ValidationEventHandler handle, Exception innerException, string message, XmlSchemaObject xsobj, object sender, string sourceUri, XmlSeverityType severity)
		{
			XmlSchemaException ex = new XmlSchemaException(message, sender, sourceUri, xsobj, innerException);
			ValidationEventArgs validationEventArgs = new ValidationEventArgs(ex, message, severity);
			if (handle == null)
			{
				if (validationEventArgs.Severity == XmlSeverityType.Error)
				{
					throw validationEventArgs.Exception;
				}
			}
			else
			{
				handle(sender, validationEventArgs);
			}
		}
	}
}
