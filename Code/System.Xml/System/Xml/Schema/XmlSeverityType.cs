using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the severity of the validation event.</summary>
	// Token: 0x0200024C RID: 588
	public enum XmlSeverityType
	{
		/// <summary>Indicates a validation error occurred when validating the instance document. This applies to document type definitions (DTDs) and XML Schema definition language (XSD) schemas. The World Wide Web Consortium (W3C) validity constraints are considered errors. If no validation event handler has been created, errors throw an exception.</summary>
		// Token: 0x040009B0 RID: 2480
		Error,
		/// <summary>Indicates that a validation event occurred that is not an error. A warning is typically issued when there is no DTD, or XML Schema to validate a particular element or attribute against. Unlike errors, warnings do not throw an exception if there is no validation event handler.</summary>
		// Token: 0x040009B1 RID: 2481
		Warning
	}
}
