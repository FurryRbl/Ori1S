using System;

namespace System.Xml.Schema
{
	/// <summary>Defines the post-schema-validation infoset of a validated XML node.</summary>
	// Token: 0x020001EE RID: 494
	public interface IXmlSchemaInfo
	{
		/// <summary>Gets a value indicating if this validated XML node was set as the result of a default being applied during XML Schema Definition Language (XSD) schema validation.</summary>
		/// <returns>true if this validated XML node was set as the result of a default being applied during schema validation; otherwise, false.</returns>
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600136C RID: 4972
		bool IsDefault { get; }

		/// <summary>Gets a value indicating if the value for this validated XML node is nil.</summary>
		/// <returns>true if the value for this validated XML node is nil; otherwise, false.</returns>
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600136D RID: 4973
		bool IsNil { get; }

		/// <summary>Gets the dynamic schema type for this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" />.</returns>
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600136E RID: 4974
		XmlSchemaSimpleType MemberType { get; }

		/// <summary>Gets the compiled <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAttribute" />.</returns>
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600136F RID: 4975
		XmlSchemaAttribute SchemaAttribute { get; }

		/// <summary>Gets the compiled <see cref="T:System.Xml.Schema.XmlSchemaElement" /> that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaElement" />.</returns>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001370 RID: 4976
		XmlSchemaElement SchemaElement { get; }

		/// <summary>Gets the static XML Schema Definition Language (XSD) schema type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" />.</returns>
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001371 RID: 4977
		XmlSchemaType SchemaType { get; }

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value.</returns>
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001372 RID: 4978
		XmlSchemaValidity Validity { get; }
	}
}
