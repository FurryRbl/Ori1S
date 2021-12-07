using System;
using System.Xml.Serialization;
using Mono.Xml.Schema;

namespace System.Xml.Schema
{
	/// <summary>Class for the identity constraints: key, keyref, and unique elements.</summary>
	// Token: 0x0200021B RID: 539
	public class XmlSchemaIdentityConstraint : XmlSchemaAnnotated
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaIdentityConstraint" /> class.</summary>
		// Token: 0x06001585 RID: 5509 RVA: 0x00061C28 File Offset: 0x0005FE28
		public XmlSchemaIdentityConstraint()
		{
			this.fields = new XmlSchemaObjectCollection();
			this.qName = XmlQualifiedName.Empty;
		}

		/// <summary>Gets or sets the name of the identity constraint.</summary>
		/// <returns>The name of the identity constraint.</returns>
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00061C48 File Offset: 0x0005FE48
		// (set) Token: 0x06001587 RID: 5511 RVA: 0x00061C50 File Offset: 0x0005FE50
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the XPath expression selector element.</summary>
		/// <returns>The XPath expression selector element.</returns>
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x00061C5C File Offset: 0x0005FE5C
		// (set) Token: 0x06001589 RID: 5513 RVA: 0x00061C64 File Offset: 0x0005FE64
		[XmlElement("selector", typeof(XmlSchemaXPath))]
		public XmlSchemaXPath Selector
		{
			get
			{
				return this.selector;
			}
			set
			{
				this.selector = value;
			}
		}

		/// <summary>Gets the collection of fields that apply as children for the XML Path Language (XPath) expression selector.</summary>
		/// <returns>The collection of fields.</returns>
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x00061C70 File Offset: 0x0005FE70
		[XmlElement("field", typeof(XmlSchemaXPath))]
		public XmlSchemaObjectCollection Fields
		{
			get
			{
				return this.fields;
			}
		}

		/// <summary>Gets the qualified name of the identity constraint, which holds the post-compilation value of the QualifiedName property.</summary>
		/// <returns>The post-compilation value of the QualifiedName property.</returns>
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00061C78 File Offset: 0x0005FE78
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qName;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00061C80 File Offset: 0x0005FE80
		internal XsdIdentitySelector CompiledSelector
		{
			get
			{
				return this.compiledSelector;
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00061C88 File Offset: 0x0005FE88
		internal override void SetParent(XmlSchemaObject parent)
		{
			base.SetParent(parent);
			if (this.Selector != null)
			{
				this.Selector.SetParent(this);
			}
			foreach (XmlSchemaObject xmlSchemaObject in this.Fields)
			{
				xmlSchemaObject.SetParent(this);
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00061D10 File Offset: 0x0005FF10
		internal override int Compile(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.CompilationId == schema.CompilationId)
			{
				return 0;
			}
			if (this.Name == null)
			{
				base.error(h, "Required attribute name must be present");
			}
			else if (!XmlSchemaUtil.CheckNCName(this.name))
			{
				base.error(h, "attribute name must be NCName");
			}
			else
			{
				this.qName = new XmlQualifiedName(this.Name, base.AncestorSchema.TargetNamespace);
				if (schema.NamedIdentities.Contains(this.qName))
				{
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = schema.NamedIdentities[this.qName] as XmlSchemaIdentityConstraint;
					base.error(h, string.Format("There is already same named identity constraint in this namespace. Existing item is at {0}({1},{2})", xmlSchemaIdentityConstraint.SourceUri, xmlSchemaIdentityConstraint.LineNumber, xmlSchemaIdentityConstraint.LinePosition));
				}
				else
				{
					schema.NamedIdentities.Add(this.qName, this);
				}
			}
			if (this.Selector == null)
			{
				base.error(h, "selector must be present");
			}
			else
			{
				this.Selector.isSelector = true;
				this.errorCount += this.Selector.Compile(h, schema);
				if (this.selector.errorCount == 0)
				{
					this.compiledSelector = new XsdIdentitySelector(this.Selector);
				}
			}
			if (this.errorCount > 0)
			{
				return this.errorCount;
			}
			if (this.Fields.Count == 0)
			{
				base.error(h, "atleast one field value must be present");
			}
			else
			{
				for (int i = 0; i < this.Fields.Count; i++)
				{
					XmlSchemaXPath xmlSchemaXPath = this.Fields[i] as XmlSchemaXPath;
					if (xmlSchemaXPath != null)
					{
						this.errorCount += xmlSchemaXPath.Compile(h, schema);
						if (xmlSchemaXPath.errorCount == 0)
						{
							this.compiledSelector.AddField(new XsdIdentityField(xmlSchemaXPath, i));
						}
					}
					else
					{
						base.error(h, "Object of type " + this.Fields[i].GetType() + " is invalid in the Fields Collection");
					}
				}
			}
			XmlSchemaUtil.CompileID(base.Id, this, schema.IDCollection, h);
			this.CompilationId = schema.CompilationId;
			return this.errorCount;
		}

		// Token: 0x04000897 RID: 2199
		private XmlSchemaObjectCollection fields;

		// Token: 0x04000898 RID: 2200
		private string name;

		// Token: 0x04000899 RID: 2201
		private XmlQualifiedName qName;

		// Token: 0x0400089A RID: 2202
		private XmlSchemaXPath selector;

		// Token: 0x0400089B RID: 2203
		private XsdIdentitySelector compiledSelector;
	}
}
