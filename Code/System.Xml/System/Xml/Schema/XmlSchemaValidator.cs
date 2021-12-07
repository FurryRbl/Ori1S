using System;
using System.Collections;
using System.IO;
using System.Text;
using Mono.Xml.Schema;

namespace System.Xml.Schema
{
	/// <summary>Represents an XML Schema Definition Language (XSD) Schema validation engine. The <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> class cannot be inherited.</summary>
	// Token: 0x02000246 RID: 582
	public sealed class XmlSchemaValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> class.</summary>
		/// <param name="nameTable">An <see cref="T:System.Xml.XmlNameTable" /> object containing element and attribute names as atomized strings.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object containing the XML Schema Definition Language (XSD) schemas used for validation.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used for resolving namespaces encountered during validation.</param>
		/// <param name="validationFlags">An <see cref="T:System.Xml.Schema.XmlSchemaValidationFlags" /> value specifying schema validation options.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are null.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An error occurred while compiling schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> parameter.</exception>
		// Token: 0x06001763 RID: 5987 RVA: 0x00074C64 File Offset: 0x00072E64
		public XmlSchemaValidator(XmlNameTable nameTable, XmlSchemaSet schemas, IXmlNamespaceResolver nsResolver, XmlSchemaValidationFlags options)
		{
			this.nameTable = nameTable;
			this.schemas = schemas;
			this.nsResolver = nsResolver;
			this.options = options;
		}

		/// <summary>The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> that receives schema validation warnings and errors encountered during schema validation.</summary>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001765 RID: 5989 RVA: 0x00074D18 File Offset: 0x00072F18
		// (remove) Token: 0x06001766 RID: 5990 RVA: 0x00074D34 File Offset: 0x00072F34
		public event ValidationEventHandler ValidationEventHandler;

		/// <summary>Gets or sets the object sent as the sender object of a validation event.</summary>
		/// <returns>An <see cref="T:System.Object" />; the default is this <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object.</returns>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00074D50 File Offset: 0x00072F50
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x00074D58 File Offset: 0x00072F58
		public object ValidationEventSender
		{
			get
			{
				return this.nominalEventSender;
			}
			set
			{
				this.nominalEventSender = value;
			}
		}

		/// <summary>Gets or sets the line number information for the XML node being validated.</summary>
		/// <returns>An <see cref="T:System.Xml.IXmlLineInfo" /> object.</returns>
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x00074D64 File Offset: 0x00072F64
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x00074D6C File Offset: 0x00072F6C
		public IXmlLineInfo LineInfoProvider
		{
			get
			{
				return this.lineInfo;
			}
			set
			{
				this.lineInfo = value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> object used to resolve xs:import and xs:include elements as well as xsi:schemaLocation and xsi:noNamespaceSchemaLocation attributes.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlResolver" /> object; the default is an <see cref="T:System.Xml.XmlUrlResolver" /> object.</returns>
		// Token: 0x17000713 RID: 1811
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x00074D78 File Offset: 0x00072F78
		public XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		/// <summary>Gets or sets the source URI for the XML node being validated.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object representing the source URI for the XML node being validated; the default is null.</returns>
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x00074D84 File Offset: 0x00072F84
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x00074D8C File Offset: 0x00072F8C
		public Uri SourceUri
		{
			get
			{
				return this.sourceUri;
			}
			set
			{
				this.sourceUri = value;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x00074D98 File Offset: 0x00072F98
		private string BaseUri
		{
			get
			{
				return (!(this.sourceUri != null)) ? string.Empty : this.sourceUri.AbsoluteUri;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x00074DCC File Offset: 0x00072FCC
		private XsdValidationContext Context
		{
			get
			{
				return this.state.Context;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x00074DDC File Offset: 0x00072FDC
		private bool IgnoreWarnings
		{
			get
			{
				return (this.options & XmlSchemaValidationFlags.ReportValidationWarnings) == XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00074DEC File Offset: 0x00072FEC
		private bool IgnoreIdentity
		{
			get
			{
				return (this.options & XmlSchemaValidationFlags.ProcessIdentityConstraints) == XmlSchemaValidationFlags.None;
			}
		}

		/// <summary>Returns the expected attributes for the current element context.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> objects or an empty array if there are no expected attributes.</returns>
		// Token: 0x06001772 RID: 6002 RVA: 0x00074DFC File Offset: 0x00072FFC
		public XmlSchemaAttribute[] GetExpectedAttributes()
		{
			XmlSchemaComplexType xmlSchemaComplexType = this.Context.ActualType as XmlSchemaComplexType;
			if (xmlSchemaComplexType == null)
			{
				return XmlSchemaValidator.emptyAttributeArray;
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in xmlSchemaComplexType.AttributeUses)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!this.occuredAtts.Contains((XmlQualifiedName)dictionaryEntry.Key))
				{
					arrayList.Add(dictionaryEntry.Value);
				}
			}
			return (XmlSchemaAttribute[])arrayList.ToArray(typeof(XmlSchemaAttribute));
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00074ECC File Offset: 0x000730CC
		private void CollectAtomicParticles(XmlSchemaParticle p, ArrayList al)
		{
			if (p is XmlSchemaGroupBase)
			{
				foreach (XmlSchemaObject xmlSchemaObject in ((XmlSchemaGroupBase)p).Items)
				{
					XmlSchemaParticle p2 = (XmlSchemaParticle)xmlSchemaObject;
					this.CollectAtomicParticles(p2, al);
				}
			}
			else
			{
				al.Add(p);
			}
		}

		/// <summary>Returns the expected particles in the current element context.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Schema.XmlSchemaParticle" /> objects or an empty array if there are no expected particles.</returns>
		// Token: 0x06001774 RID: 6004 RVA: 0x00074F5C File Offset: 0x0007315C
		[MonoTODO]
		public XmlSchemaParticle[] GetExpectedParticles()
		{
			ArrayList arrayList = new ArrayList();
			this.Context.State.GetExpectedParticles(arrayList);
			ArrayList arrayList2 = new ArrayList();
			foreach (object obj in arrayList)
			{
				XmlSchemaParticle p = (XmlSchemaParticle)obj;
				this.CollectAtomicParticles(p, arrayList2);
			}
			return (XmlSchemaParticle[])arrayList2.ToArray(typeof(XmlSchemaParticle));
		}

		/// <summary>Validates identity constraints on the default attributes and populates the specified <see cref="T:System.Collections.ArrayList" /> with <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> objects for any attributes with default values that have not already been validated using the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> method in the element context.</summary>
		/// <param name="defaultAttributes">An <see cref="T:System.Collections.ArrayList" /> to populate with <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> objects for any attributes not yet validated in the element context.</param>
		// Token: 0x06001775 RID: 6005 RVA: 0x00074FFC File Offset: 0x000731FC
		public void GetUnspecifiedDefaultAttributes(ArrayList defaultAttributeList)
		{
			if (defaultAttributeList == null)
			{
				throw new ArgumentNullException("defaultAttributeList");
			}
			if (this.transition != XmlSchemaValidator.Transition.StartTag)
			{
				throw new InvalidOperationException("Method 'GetUnsoecifiedDefaultAttributes' works only when the validator state is inside a start tag.");
			}
			foreach (XmlSchemaAttribute xmlSchemaAttribute in this.GetExpectedAttributes())
			{
				if (xmlSchemaAttribute.ValidatedDefaultValue != null || xmlSchemaAttribute.ValidatedFixedValue != null)
				{
					defaultAttributeList.Add(xmlSchemaAttribute);
				}
			}
			defaultAttributeList.AddRange(this.defaultAttributes);
		}

		/// <summary>Adds an XML Schema Definition Language (XSD) schema to the set of schemas used for validation.</summary>
		/// <param name="schema">An <see cref="T:System.Xml.Schema.XmlSchema" /> object to add to the set of schemas used for validation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> parameter specified is null.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The target namespace of the <see cref="T:System.Xml.Schema.XmlSchema" /> parameter matches that of any element or attribute already encountered by the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The <see cref="T:System.Xml.Schema.XmlSchema" /> parameter is invalid.</exception>
		// Token: 0x06001776 RID: 6006 RVA: 0x0007507C File Offset: 0x0007327C
		public void AddSchema(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			this.schemas.Add(schema);
			this.schemas.Compile();
		}

		/// <summary>Initializes the state of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object.</summary>
		/// <exception cref="T:System.InvalidOperationException">Calling the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.Initialize" /> method is valid immediately after the construction of an <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object or after a call to <see cref="M:System.Xml.Schema.XmlSchemaValidator.EndValidation" /> only.</exception>
		// Token: 0x06001777 RID: 6007 RVA: 0x000750A8 File Offset: 0x000732A8
		public void Initialize()
		{
			this.transition = XmlSchemaValidator.Transition.Content;
			this.state = new XsdParticleStateManager();
			if (!this.schemas.IsCompiled)
			{
				this.schemas.Compile();
			}
		}

		/// <summary>Initializes the state of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object using the <see cref="T:System.Xml.Schema.XmlSchemaObject" /> specified for partial validation.</summary>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaAttribute" />, or <see cref="T:System.Xml.Schema.XmlSchemaType" /> object used to initialize the validation context of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object for partial validation.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.Initialize" /> method is valid immediately after the construction of an <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object or after a call to <see cref="M:System.Xml.Schema.XmlSchemaValidator.EndValidation" /> only.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> parameter is not an <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaAttribute" />, or <see cref="T:System.Xml.Schema.XmlSchemaType" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> parameter cannot be null.</exception>
		// Token: 0x06001778 RID: 6008 RVA: 0x000750D8 File Offset: 0x000732D8
		public void Initialize(XmlSchemaObject partialValidationType)
		{
			if (partialValidationType == null)
			{
				throw new ArgumentNullException("partialValidationType");
			}
			this.startType = partialValidationType;
			this.Initialize();
		}

		/// <summary>Ends validation and checks identity constraints for the entire XML document.</summary>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">An identity constraint error was found in the XML document.</exception>
		// Token: 0x06001779 RID: 6009 RVA: 0x000750F8 File Offset: 0x000732F8
		public void EndValidation()
		{
			this.CheckState(XmlSchemaValidator.Transition.Content);
			this.transition = XmlSchemaValidator.Transition.Finished;
			if (this.schemas.Count == 0)
			{
				return;
			}
			if (this.depth > 0)
			{
				throw new InvalidOperationException(string.Format("There are {0} open element(s). ValidateEndElement() must be called for each open element.", this.depth));
			}
			if (!this.IgnoreIdentity && this.idManager.HasMissingIDReferences())
			{
				this.HandleError("There are missing ID references: " + this.idManager.GetMissingIDString());
			}
		}

		/// <summary>Skips validation of the current element content and prepares the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object to validate content in the parent element's context.</summary>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set if the current element content is successfully skipped. This parameter can be null.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" /> method was not called in the correct sequence. For example, calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		// Token: 0x0600177A RID: 6010 RVA: 0x00075184 File Offset: 0x00073384
		[MonoTODO]
		public void SkipToEndElement(XmlSchemaInfo info)
		{
			this.CheckState(XmlSchemaValidator.Transition.Content);
			if (this.schemas.Count == 0)
			{
				return;
			}
			this.state.PopContext();
		}

		/// <summary>Validates the attribute name, namespace URI, and value in the current element context.</summary>
		/// <returns>The validated attribute's value.</returns>
		/// <param name="localName">The local name of the attribute to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute to validate.</param>
		/// <param name="attributeValue">The value of the attribute to validate.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the attribute. This parameter can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The attribute is not valid in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> method was not called in the correct sequence. For example, calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are null.</exception>
		// Token: 0x0600177B RID: 6011 RVA: 0x000751AC File Offset: 0x000733AC
		public object ValidateAttribute(string localName, string ns, string attributeValue, XmlSchemaInfo info)
		{
			if (attributeValue == null)
			{
				throw new ArgumentNullException("attributeValue");
			}
			return this.ValidateAttribute(localName, ns, () => attributeValue, info);
		}

		/// <summary>Validates the attribute name, namespace URI, and value in the current element context.</summary>
		/// <returns>The validated attribute's value.</returns>
		/// <param name="localName">The local name of the attribute to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute to validate.</param>
		/// <param name="attributeValue">An <see cref="T:System.Xml.Schema.XmlValueGetter" />delegate used to pass the attribute's value as a Common Language Runtime (CLR) type compatible with the XML Schema Definition Language (XSD) type of the attribute.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the attribute. This parameter and can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The attribute is not valid in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> method was not called in the correct sequence. For example, calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are null.</exception>
		// Token: 0x0600177C RID: 6012 RVA: 0x000751F4 File Offset: 0x000733F4
		public object ValidateAttribute(string localName, string ns, XmlValueGetter attributeValue, XmlSchemaInfo info)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (ns == null)
			{
				throw new ArgumentNullException("ns");
			}
			if (attributeValue == null)
			{
				throw new ArgumentNullException("attributeValue");
			}
			this.CheckState(XmlSchemaValidator.Transition.StartTag);
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(localName, ns);
			if (this.occuredAtts.Contains(xmlQualifiedName))
			{
				throw new InvalidOperationException(string.Format("Attribute '{0}' has already been validated in the same element.", xmlQualifiedName));
			}
			this.occuredAtts.Add(xmlQualifiedName);
			if (ns == "http://www.w3.org/2000/xmlns/")
			{
				return null;
			}
			if (this.schemas.Count == 0)
			{
				return null;
			}
			if (this.Context.Element != null && this.Context.XsiType == null)
			{
				if (this.Context.ActualType is XmlSchemaComplexType)
				{
					return this.AssessAttributeElementLocallyValidType(localName, ns, attributeValue, info);
				}
				this.HandleError("Current simple type cannot accept attributes other than schema instance namespace.");
			}
			return null;
		}

		/// <summary>Validates the element in the current context.</summary>
		/// <param name="localName">The local name of the element to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the element to validate.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the element's name. This parameter can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's name is not valid in the current context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x0600177D RID: 6013 RVA: 0x000752E4 File Offset: 0x000734E4
		public void ValidateElement(string localName, string ns, XmlSchemaInfo info)
		{
			this.ValidateElement(localName, ns, info, null, null, null, null);
		}

		/// <summary>Validates the element in the current context with the xsi:Type, xsi:Nil, xsi:SchemaLocation, and xsi:NoNamespaceSchemaLocation attribute values specified.</summary>
		/// <param name="localName">The local name of the element to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the element to validate.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the element's name. This parameter can be null.</param>
		/// <param name="xsiType">The xsi:Type attribute value of the element. This parameter can be null.</param>
		/// <param name="xsiNil">The xsi:Nil attribute value of the element. This parameter can be null.</param>
		/// <param name="xsiSchemaLocation">The xsi:SchemaLocation attribute value of the element. This parameter can be null.</param>
		/// <param name="xsiNoNamespaceSchemaLocation">The xsi:NoNamespaceSchemaLocation attribute value of the element. This parameter can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's name is not valid in the current context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x0600177E RID: 6014 RVA: 0x00075300 File Offset: 0x00073500
		public void ValidateElement(string localName, string ns, XmlSchemaInfo info, string xsiType, string xsiNil, string schemaLocation, string noNsSchemaLocation)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (ns == null)
			{
				throw new ArgumentNullException("ns");
			}
			this.CheckState(XmlSchemaValidator.Transition.Content);
			this.transition = XmlSchemaValidator.Transition.StartTag;
			if (schemaLocation != null)
			{
				this.HandleSchemaLocation(schemaLocation);
			}
			if (noNsSchemaLocation != null)
			{
				this.HandleNoNSSchemaLocation(noNsSchemaLocation);
			}
			this.elementQNameStack.Add(new XmlQualifiedName(localName, ns));
			if (this.schemas.Count == 0)
			{
				return;
			}
			if (!this.IgnoreIdentity)
			{
				this.idManager.OnStartElement();
			}
			this.defaultAttributes = XmlSchemaValidator.emptyAttributeArray;
			if (this.skipValidationDepth < 0 || this.depth <= this.skipValidationDepth)
			{
				if (this.shouldValidateCharacters)
				{
					this.ValidateEndSimpleContent(null);
				}
				this.AssessOpenStartElementSchemaValidity(localName, ns);
			}
			if (xsiNil != null)
			{
				this.HandleXsiNil(xsiNil, info);
			}
			if (xsiType != null)
			{
				this.HandleXsiType(xsiType);
			}
			if (this.xsiNilDepth < this.depth)
			{
				this.shouldValidateCharacters = true;
			}
			if (info != null)
			{
				info.IsNil = (this.xsiNilDepth >= 0);
				info.SchemaElement = this.Context.Element;
				info.SchemaType = this.Context.ActualSchemaType;
				info.SchemaAttribute = null;
				info.IsDefault = false;
				info.MemberType = null;
			}
		}

		/// <summary>Verifies if the text content of the element is valid according to its data type for elements with simple content, and verifies if the content of the current element is complete according to its data type for elements with complex content.</summary>
		/// <returns>The parsed, typed text value of the element if the element has simple content.</returns>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the element. This parameter can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's content is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method was not called in the correct sequence. For example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method is called after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		// Token: 0x0600177F RID: 6015 RVA: 0x00075460 File Offset: 0x00073660
		public object ValidateEndElement(XmlSchemaInfo info)
		{
			return this.ValidateEndElement(info, null);
		}

		/// <summary>Verifies if the text content of the element specified is valid according to its data type.</summary>
		/// <returns>The parsed, typed simple content of the element.</returns>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the text content of the element. This parameter can be null.</param>
		/// <param name="typedValue">The typed text content of the element.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's text content is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method was not called in the correct sequence (for example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method is called after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />), calls to the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method have been previously made, or the element has complex content.</exception>
		/// <exception cref="T:System.ArgumentNullException">The typed text content parameter cannot be null.</exception>
		// Token: 0x06001780 RID: 6016 RVA: 0x0007546C File Offset: 0x0007366C
		[MonoTODO]
		public object ValidateEndElement(XmlSchemaInfo info, object var)
		{
			if (this.transition == XmlSchemaValidator.Transition.StartTag)
			{
				this.ValidateEndOfAttributes(info);
			}
			this.CheckState(XmlSchemaValidator.Transition.Content);
			this.elementQNameStack.RemoveAt(this.elementQNameStack.Count - 1);
			if (this.schemas.Count == 0)
			{
				return null;
			}
			if (this.depth == 0)
			{
				throw new InvalidOperationException("There was no corresponding call to 'ValidateElement' method.");
			}
			this.depth--;
			object result = null;
			if (this.depth == this.skipValidationDepth)
			{
				this.skipValidationDepth = -1;
			}
			else if (this.skipValidationDepth < 0 || this.depth <= this.skipValidationDepth)
			{
				result = this.AssessEndElementSchemaValidity(info);
			}
			return result;
		}

		/// <summary>Verifies whether all the required attributes in the element context are present and prepares the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object to validate the child content of the element.</summary>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful verification that all the required attributes in the element context are present. This parameter can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">One or more of the required attributes in the current element context were not found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" /> method was not called in the correct sequence. For example, calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are null.</exception>
		// Token: 0x06001781 RID: 6017 RVA: 0x00075528 File Offset: 0x00073728
		public void ValidateEndOfAttributes(XmlSchemaInfo info)
		{
			try
			{
				this.CheckState(XmlSchemaValidator.Transition.StartTag);
				this.transition = XmlSchemaValidator.Transition.Content;
				if (this.schemas.Count != 0)
				{
					if (this.skipValidationDepth < 0 || this.depth <= this.skipValidationDepth)
					{
						this.AssessCloseStartElementSchemaValidity(info);
					}
					this.depth++;
				}
			}
			finally
			{
				this.occuredAtts.Clear();
			}
		}

		/// <summary>Validates whether the text string specified is allowed in the current element context, and accumulates the text for validation if the current element has simple content.</summary>
		/// <param name="elementValue">A text string to validate in the current element context.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The text string specified is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The text string parameter cannot be null.</exception>
		// Token: 0x06001782 RID: 6018 RVA: 0x000755B8 File Offset: 0x000737B8
		public void ValidateText(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.ValidateText(() => value);
		}

		/// <summary>Validates whether the text returned by the <see cref="T:System.Xml.Schema.XmlValueGetter" /> object specified is allowed in the current element context, and accumulates the text for validation if the current element has simple content.</summary>
		/// <param name="elementValue">An <see cref="T:System.Xml.Schema.XmlValueGetter" />delegate used to pass the text value as a Common Language Runtime (CLR) type compatible with the XML Schema Definition Language (XSD) type of the attribute.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The text string specified is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The text string parameter cannot be null.</exception>
		// Token: 0x06001783 RID: 6019 RVA: 0x000755FC File Offset: 0x000737FC
		public void ValidateText(XmlValueGetter getter)
		{
			if (getter == null)
			{
				throw new ArgumentNullException("getter");
			}
			this.CheckState(XmlSchemaValidator.Transition.Content);
			if (this.schemas.Count == 0)
			{
				return;
			}
			if (this.skipValidationDepth >= 0 && this.depth > this.skipValidationDepth)
			{
				return;
			}
			XmlSchemaComplexType xmlSchemaComplexType = this.Context.ActualType as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null)
			{
				XmlSchemaContentType contentType = xmlSchemaComplexType.ContentType;
				if (contentType != XmlSchemaContentType.Empty)
				{
					if (contentType == XmlSchemaContentType.ElementOnly)
					{
						string text = this.storedCharacters.ToString();
						if (text.Length > 0 && !XmlChar.IsWhitespace(text))
						{
							this.HandleError("Not allowed character content was found.");
						}
					}
				}
				else
				{
					this.HandleError("Not allowed character content was found.");
				}
			}
			this.ValidateCharacters(getter);
		}

		/// <summary>Validates whether the white space in the string specified is allowed in the current element context, and accumulates the white space for validation if the current element has simple content.</summary>
		/// <param name="elementValue">A white space string to validate in the current element context.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">White space is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method was not called in the correct sequence. For example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x06001784 RID: 6020 RVA: 0x000756CC File Offset: 0x000738CC
		public void ValidateWhitespace(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.ValidateWhitespace(() => value);
		}

		/// <summary>Validates whether the white space returned by the <see cref="T:System.Xml.Schema.XmlValueGetter" /> object specified is allowed in the current element context, and accumulates the white space for validation if the current element has simple content.</summary>
		/// <param name="elementValue">An <see cref="T:System.Xml.Schema.XmlValueGetter" />delegate used to pass the white space value as a Common Language Runtime (CLR) type compatible with the XML Schema Definition Language (XSD) type of the attribute.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">White space is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method was not called in the correct sequence. For example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x06001785 RID: 6021 RVA: 0x00075710 File Offset: 0x00073910
		public void ValidateWhitespace(XmlValueGetter getter)
		{
			this.ValidateText(getter);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0007571C File Offset: 0x0007391C
		private void HandleError(string message)
		{
			this.HandleError(message, null, false);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00075728 File Offset: 0x00073928
		private void HandleError(string message, Exception innerException)
		{
			this.HandleError(message, innerException, false);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00075734 File Offset: 0x00073934
		private void HandleError(string message, Exception innerException, bool isWarning)
		{
			if (isWarning && this.IgnoreWarnings)
			{
				return;
			}
			XmlSchemaValidationException exception = new XmlSchemaValidationException(message, this.nominalEventSender, this.BaseUri, null, innerException);
			this.HandleError(exception, isWarning);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00075770 File Offset: 0x00073970
		private void HandleError(XmlSchemaValidationException exception)
		{
			this.HandleError(exception, false);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0007577C File Offset: 0x0007397C
		private void HandleError(XmlSchemaValidationException exception, bool isWarning)
		{
			if (isWarning && this.IgnoreWarnings)
			{
				return;
			}
			if (this.ValidationEventHandler == null)
			{
				throw exception;
			}
			ValidationEventArgs e = new ValidationEventArgs(exception, exception.Message, (!isWarning) ? XmlSeverityType.Error : XmlSeverityType.Warning);
			this.ValidationEventHandler(this.nominalEventSender, e);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000757D4 File Offset: 0x000739D4
		private void CheckState(XmlSchemaValidator.Transition expected)
		{
			if (this.transition == expected)
			{
				return;
			}
			if (this.transition == XmlSchemaValidator.Transition.None)
			{
				throw new InvalidOperationException("Initialize() must be called before processing validation.");
			}
			throw new InvalidOperationException(string.Format("Unexpected attempt to validate state transition from {0} to {1}.", this.transition, expected));
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00075824 File Offset: 0x00073A24
		private XmlSchemaElement FindElement(string name, string ns)
		{
			return (XmlSchemaElement)this.schemas.GlobalElements[new XmlQualifiedName(name, ns)];
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00075844 File Offset: 0x00073A44
		private XmlSchemaType FindType(XmlQualifiedName qname)
		{
			return (XmlSchemaType)this.schemas.GlobalTypes[qname];
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0007585C File Offset: 0x00073A5C
		private void ValidateStartElementParticle(string localName, string ns)
		{
			if (this.Context.State == null)
			{
				return;
			}
			this.Context.XsiType = null;
			this.state.CurrentElement = null;
			this.Context.EvaluateStartElement(localName, ns);
			if (this.Context.IsInvalid)
			{
				this.HandleError("Invalid start element: " + ns + ":" + localName);
			}
			this.Context.PushCurrentElement(this.state.CurrentElement);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000758DC File Offset: 0x00073ADC
		private void AssessOpenStartElementSchemaValidity(string localName, string ns)
		{
			if (this.xsiNilDepth >= 0 && this.xsiNilDepth < this.depth)
			{
				this.HandleError("Element item appeared, while current element context is nil.");
			}
			this.ValidateStartElementParticle(localName, ns);
			if (this.Context.Element == null)
			{
				this.state.CurrentElement = this.FindElement(localName, ns);
				this.Context.PushCurrentElement(this.state.CurrentElement);
			}
			if (!this.IgnoreIdentity)
			{
				this.ValidateKeySelectors();
				this.ValidateKeyFields(false, this.xsiNilDepth == this.depth, this.Context.ActualType, null, null, null);
			}
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00075988 File Offset: 0x00073B88
		private void AssessCloseStartElementSchemaValidity(XmlSchemaInfo info)
		{
			if (this.Context.XsiType != null)
			{
				this.AssessCloseStartElementLocallyValidType(info);
			}
			else if (this.Context.Element != null)
			{
				this.AssessElementLocallyValidElement();
				if (this.Context.Element.ElementType != null)
				{
					this.AssessCloseStartElementLocallyValidType(info);
				}
			}
			if (this.Context.Element == null)
			{
				XmlSchemaContentProcessing processContents = this.state.ProcessContents;
				if (processContents != XmlSchemaContentProcessing.Skip)
				{
					if (processContents != XmlSchemaContentProcessing.Lax)
					{
						XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)this.elementQNameStack[this.elementQNameStack.Count - 1];
						if (this.Context.XsiType == null && (this.schemas.Contains(xmlQualifiedName.Namespace) || !this.schemas.MissedSubComponents(xmlQualifiedName.Namespace)))
						{
							this.HandleError("Element declaration for " + xmlQualifiedName + " is missing.");
						}
					}
				}
			}
			this.state.PushContext();
			XsdValidationState xsdValidationState = null;
			if (this.state.ProcessContents == XmlSchemaContentProcessing.Skip)
			{
				this.skipValidationDepth = this.depth;
			}
			else
			{
				XmlSchemaComplexType xmlSchemaComplexType = this.Context.ActualType as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null)
				{
					xsdValidationState = this.state.Create(xmlSchemaComplexType.ValidatableParticle);
				}
				else if (this.state.ProcessContents == XmlSchemaContentProcessing.Lax)
				{
					xsdValidationState = this.state.Create(XmlSchemaAny.AnyTypeContent);
				}
				else
				{
					xsdValidationState = this.state.Create(XmlSchemaParticle.Empty);
				}
			}
			this.Context.State = xsdValidationState;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00075B34 File Offset: 0x00073D34
		private void AssessElementLocallyValidElement()
		{
			XmlSchemaElement element = this.Context.Element;
			XmlQualifiedName arg = (XmlQualifiedName)this.elementQNameStack[this.elementQNameStack.Count - 1];
			if (element == null)
			{
				this.HandleError("Element declaration is required for " + arg);
			}
			if (element.ActualIsAbstract)
			{
				this.HandleError("Abstract element declaration was specified for " + arg);
			}
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00075BA0 File Offset: 0x00073DA0
		private void AssessCloseStartElementLocallyValidType(XmlSchemaInfo info)
		{
			object actualType = this.Context.ActualType;
			if (actualType == null)
			{
				this.HandleError("Schema type does not exist.");
				return;
			}
			XmlSchemaComplexType xmlSchemaComplexType = actualType as XmlSchemaComplexType;
			XmlSchemaSimpleType xmlSchemaSimpleType = actualType as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType == null)
			{
				if (xmlSchemaComplexType != null)
				{
					this.AssessCloseStartElementLocallyValidComplexType(xmlSchemaComplexType, info);
				}
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00075BF4 File Offset: 0x00073DF4
		private void AssessCloseStartElementLocallyValidComplexType(XmlSchemaComplexType cType, XmlSchemaInfo info)
		{
			if (cType.IsAbstract)
			{
				this.HandleError("Target complex type is abstract.");
				return;
			}
			foreach (XmlSchemaAttribute xmlSchemaAttribute in this.GetExpectedAttributes())
			{
				if (xmlSchemaAttribute.ValidatedUse == XmlSchemaUse.Required && xmlSchemaAttribute.ValidatedFixedValue == null)
				{
					this.HandleError("Required attribute " + xmlSchemaAttribute.QualifiedName + " was not found.");
				}
				else if (xmlSchemaAttribute.ValidatedDefaultValue != null || xmlSchemaAttribute.ValidatedFixedValue != null)
				{
					this.defaultAttributesCache.Add(xmlSchemaAttribute);
				}
			}
			if (this.defaultAttributesCache.Count == 0)
			{
				this.defaultAttributes = XmlSchemaValidator.emptyAttributeArray;
			}
			else
			{
				this.defaultAttributes = (XmlSchemaAttribute[])this.defaultAttributesCache.ToArray(typeof(XmlSchemaAttribute));
			}
			this.defaultAttributesCache.Clear();
			if (!this.IgnoreIdentity)
			{
				foreach (XmlSchemaAttribute xmlSchemaAttribute2 in this.defaultAttributes)
				{
					XmlSchemaDatatype dt = (xmlSchemaAttribute2.AttributeType as XmlSchemaDatatype) ?? xmlSchemaAttribute2.AttributeSchemaType.Datatype;
					object parsedValue = xmlSchemaAttribute2.ValidatedFixedValue ?? xmlSchemaAttribute2.ValidatedDefaultValue;
					string text = this.idManager.AssessEachAttributeIdentityConstraint(dt, parsedValue, ((XmlQualifiedName)this.elementQNameStack[this.elementQNameStack.Count - 1]).Name);
					if (text != null)
					{
						this.HandleError(text);
					}
				}
			}
			if (!this.IgnoreIdentity)
			{
				foreach (XmlSchemaAttribute xmlSchemaAttribute3 in this.defaultAttributes)
				{
					this.ValidateKeyFieldsAttribute(xmlSchemaAttribute3, xmlSchemaAttribute3.ValidatedFixedValue ?? xmlSchemaAttribute3.ValidatedDefaultValue);
				}
			}
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00075DD4 File Offset: 0x00073FD4
		private object AssessAttributeElementLocallyValidType(string localName, string ns, XmlValueGetter getter, XmlSchemaInfo info)
		{
			XmlSchemaComplexType cType = this.Context.ActualType as XmlSchemaComplexType;
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(localName, ns);
			XmlSchemaObject xmlSchemaObject = XmlSchemaUtil.FindAttributeDeclaration(ns, this.schemas, cType, xmlQualifiedName);
			if (xmlSchemaObject == null)
			{
				this.HandleError("Attribute declaration was not found for " + xmlQualifiedName);
			}
			XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaObject as XmlSchemaAttribute;
			if (xmlSchemaAttribute != null)
			{
				this.AssessAttributeLocallyValidUse(xmlSchemaAttribute);
				return this.AssessAttributeLocallyValid(xmlSchemaAttribute, info, getter);
			}
			return null;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00075E40 File Offset: 0x00074040
		private object AssessAttributeLocallyValid(XmlSchemaAttribute attr, XmlSchemaInfo info, XmlValueGetter getter)
		{
			if (attr.AttributeType == null)
			{
				this.HandleError("Attribute type is missing for " + attr.QualifiedName);
			}
			XmlSchemaDatatype xmlSchemaDatatype = attr.AttributeType as XmlSchemaDatatype;
			if (xmlSchemaDatatype == null)
			{
				xmlSchemaDatatype = ((XmlSchemaSimpleType)attr.AttributeType).Datatype;
			}
			object obj = null;
			if (xmlSchemaDatatype == XmlSchemaSimpleType.AnySimpleType)
			{
				if (attr.ValidatedFixedValue == null)
				{
					goto IL_115;
				}
			}
			try
			{
				this.CurrentAttributeType = xmlSchemaDatatype;
				obj = getter();
			}
			catch (Exception innerException)
			{
				this.HandleError(string.Format("Attribute value is invalid against its data type {0}", (xmlSchemaDatatype == null) ? XmlTokenizedType.CDATA : xmlSchemaDatatype.TokenizedType), innerException);
			}
			XmlSchemaSimpleType xmlSchemaSimpleType = attr.AttributeType as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType != null)
			{
				this.ValidateRestrictedSimpleTypeValue(xmlSchemaSimpleType, ref xmlSchemaDatatype, new XmlAtomicValue(obj, attr.AttributeSchemaType).Value);
			}
			if (attr.ValidatedFixedValue != null)
			{
				if (!XmlSchemaUtil.AreSchemaDatatypeEqual(attr.AttributeSchemaType, attr.ValidatedFixedTypedValue, attr.AttributeSchemaType, obj))
				{
					this.HandleError(string.Format("The value of the attribute {0} does not match with its fixed value '{1}' in the space of type {2}", attr.QualifiedName, attr.ValidatedFixedValue, xmlSchemaDatatype));
				}
				obj = attr.ValidatedFixedTypedValue;
			}
			IL_115:
			if (!this.IgnoreIdentity)
			{
				string text = this.idManager.AssessEachAttributeIdentityConstraint(xmlSchemaDatatype, obj, ((XmlQualifiedName)this.elementQNameStack[this.elementQNameStack.Count - 1]).Name);
				if (text != null)
				{
					this.HandleError(text);
				}
			}
			if (!this.IgnoreIdentity)
			{
				this.ValidateKeyFieldsAttribute(attr, obj);
			}
			return obj;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00075FE0 File Offset: 0x000741E0
		private void AssessAttributeLocallyValidUse(XmlSchemaAttribute attr)
		{
			if (attr.ValidatedUse == XmlSchemaUse.Prohibited)
			{
				this.HandleError("Attribute " + attr.QualifiedName + " is prohibited in this context.");
			}
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00076014 File Offset: 0x00074214
		private object AssessEndElementSchemaValidity(XmlSchemaInfo info)
		{
			object result = this.ValidateEndSimpleContent(info);
			this.ValidateEndElementParticle();
			if (!this.IgnoreIdentity)
			{
				this.ValidateEndElementKeyConstraints();
			}
			if (this.xsiNilDepth == this.depth)
			{
				this.xsiNilDepth = -1;
			}
			return result;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0007605C File Offset: 0x0007425C
		private void ValidateEndElementParticle()
		{
			if (this.Context.State != null && !this.Context.EvaluateEndElement())
			{
				this.HandleError("Invalid end element. There are still required content items.");
			}
			this.Context.PopCurrentElement();
			this.state.PopContext();
			this.Context.XsiType = null;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x000760B8 File Offset: 0x000742B8
		private void ValidateCharacters(XmlValueGetter getter)
		{
			if (this.xsiNilDepth >= 0 && this.xsiNilDepth < this.depth)
			{
				this.HandleError("Element item appeared, while current element context is nil.");
			}
			if (this.shouldValidateCharacters)
			{
				this.CurrentAttributeType = null;
				this.storedCharacters.Append(getter());
			}
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00076114 File Offset: 0x00074314
		private object ValidateEndSimpleContent(XmlSchemaInfo info)
		{
			object result = null;
			if (this.shouldValidateCharacters)
			{
				result = this.ValidateEndSimpleContentCore(info);
			}
			this.shouldValidateCharacters = false;
			this.storedCharacters.Length = 0;
			return result;
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0007614C File Offset: 0x0007434C
		private object ValidateEndSimpleContentCore(XmlSchemaInfo info)
		{
			if (this.Context.ActualType == null)
			{
				return null;
			}
			string text = this.storedCharacters.ToString();
			object result = null;
			if (text.Length == 0 && this.Context.Element != null && this.Context.Element.ValidatedDefaultValue != null)
			{
				text = this.Context.Element.ValidatedDefaultValue;
			}
			XmlSchemaDatatype xmlSchemaDatatype = this.Context.ActualType as XmlSchemaDatatype;
			XmlSchemaSimpleType xmlSchemaSimpleType = this.Context.ActualType as XmlSchemaSimpleType;
			if (xmlSchemaDatatype == null)
			{
				if (xmlSchemaSimpleType != null)
				{
					xmlSchemaDatatype = xmlSchemaSimpleType.Datatype;
				}
				else
				{
					XmlSchemaComplexType xmlSchemaComplexType = this.Context.ActualType as XmlSchemaComplexType;
					xmlSchemaDatatype = xmlSchemaComplexType.Datatype;
					XmlSchemaContentType contentType = xmlSchemaComplexType.ContentType;
					if (contentType != XmlSchemaContentType.Empty)
					{
						if (contentType == XmlSchemaContentType.ElementOnly)
						{
							if (text.Length > 0 && !XmlChar.IsWhitespace(text))
							{
								this.HandleError("Character content not allowed in an elementOnly model.");
							}
						}
					}
					else if (text.Length > 0)
					{
						this.HandleError("Character content not allowed in an empty model.");
					}
				}
			}
			if (xmlSchemaDatatype != null)
			{
				if (this.Context.Element != null && this.Context.Element.ValidatedFixedValue != null && text != this.Context.Element.ValidatedFixedValue)
				{
					this.HandleError("Fixed value constraint was not satisfied.");
				}
				result = this.AssessStringValid(xmlSchemaSimpleType, xmlSchemaDatatype, text);
			}
			if (!this.IgnoreIdentity)
			{
				this.ValidateSimpleContentIdentity(xmlSchemaDatatype, text);
			}
			this.shouldValidateCharacters = false;
			if (info != null)
			{
				info.IsNil = (this.xsiNilDepth >= 0);
				info.SchemaElement = null;
				info.SchemaType = (this.Context.ActualType as XmlSchemaType);
				if (info.SchemaType == null)
				{
					info.SchemaType = XmlSchemaType.GetBuiltInSimpleType(xmlSchemaDatatype.TypeCode);
				}
				info.SchemaAttribute = null;
				info.IsDefault = false;
				info.MemberType = null;
			}
			return result;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0007634C File Offset: 0x0007454C
		private object AssessStringValid(XmlSchemaSimpleType st, XmlSchemaDatatype dt, string value)
		{
			XmlSchemaDatatype xmlSchemaDatatype = dt;
			object result = null;
			if (st != null)
			{
				string text = xmlSchemaDatatype.Normalize(value);
				XmlSchemaDerivationMethod derivedBy = st.DerivedBy;
				if (derivedBy != XmlSchemaDerivationMethod.Restriction)
				{
					if (derivedBy != XmlSchemaDerivationMethod.List)
					{
						if (derivedBy == XmlSchemaDerivationMethod.Union)
						{
							XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = st.Content as XmlSchemaSimpleTypeUnion;
							string text2 = text;
							bool flag = false;
							object[] validatedTypes = xmlSchemaSimpleTypeUnion.ValidatedTypes;
							int i = 0;
							while (i < validatedTypes.Length)
							{
								object obj = validatedTypes[i];
								XmlSchemaDatatype xmlSchemaDatatype2 = obj as XmlSchemaDatatype;
								XmlSchemaSimpleType xmlSchemaSimpleType = obj as XmlSchemaSimpleType;
								if (xmlSchemaDatatype2 != null)
								{
									try
									{
										result = xmlSchemaDatatype2.ParseValue(text2, this.nameTable, this.nsResolver);
									}
									catch (Exception)
									{
										goto IL_1A2;
									}
									goto IL_19A;
								}
								try
								{
									result = this.AssessStringValid(xmlSchemaSimpleType, xmlSchemaSimpleType.Datatype, text2);
								}
								catch (XmlSchemaValidationException)
								{
									goto IL_1A2;
								}
								goto IL_19A;
								IL_1A2:
								i++;
								continue;
								IL_19A:
								flag = true;
								break;
							}
							if (!flag)
							{
								this.HandleError("Union type value contains one or more invalid values.");
							}
						}
					}
					else
					{
						XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = st.Content as XmlSchemaSimpleTypeList;
						string[] array = text.Split(XmlChar.WhitespaceChars);
						object[] array2 = new object[array.Length];
						XmlSchemaDatatype xmlSchemaDatatype2 = xmlSchemaSimpleTypeList.ValidatedListItemType as XmlSchemaDatatype;
						XmlSchemaSimpleType xmlSchemaSimpleType = xmlSchemaSimpleTypeList.ValidatedListItemType as XmlSchemaSimpleType;
						for (int j = 0; j < array.Length; j++)
						{
							string text3 = array[j];
							if (!(text3 == string.Empty))
							{
								if (xmlSchemaDatatype2 != null)
								{
									try
									{
										array2[j] = xmlSchemaDatatype2.ParseValue(text3, this.nameTable, this.nsResolver);
									}
									catch (Exception innerException)
									{
										this.HandleError("List type value contains one or more invalid values.", innerException);
										break;
									}
								}
								else
								{
									this.AssessStringValid(xmlSchemaSimpleType, xmlSchemaSimpleType.Datatype, text3);
								}
							}
						}
						result = array2;
					}
				}
				else
				{
					XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = st.Content as XmlSchemaSimpleTypeRestriction;
					if (xmlSchemaSimpleTypeRestriction != null)
					{
						XmlSchemaSimpleType xmlSchemaSimpleType2 = st.BaseXmlSchemaType as XmlSchemaSimpleType;
						if (xmlSchemaSimpleType2 != null)
						{
							result = this.AssessStringValid(xmlSchemaSimpleType2, dt, value);
						}
						if (!xmlSchemaSimpleTypeRestriction.ValidateValueWithFacets(value, this.nameTable, this.nsResolver))
						{
							this.HandleError("Specified value was invalid against the facets.");
							goto IL_237;
						}
					}
					xmlSchemaDatatype = st.Datatype;
				}
			}
			IL_237:
			if (xmlSchemaDatatype != null)
			{
				try
				{
					result = xmlSchemaDatatype.ParseValue(value, this.nameTable, this.nsResolver);
				}
				catch (Exception innerException2)
				{
					this.HandleError(string.Format("Invalidly typed data was specified.", new object[0]), innerException2);
				}
			}
			return result;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00076634 File Offset: 0x00074834
		private void ValidateRestrictedSimpleTypeValue(XmlSchemaSimpleType st, ref XmlSchemaDatatype dt, string normalized)
		{
			XmlSchemaDerivationMethod derivedBy = st.DerivedBy;
			if (derivedBy != XmlSchemaDerivationMethod.Restriction)
			{
				if (derivedBy != XmlSchemaDerivationMethod.List)
				{
					if (derivedBy == XmlSchemaDerivationMethod.Union)
					{
						XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = st.Content as XmlSchemaSimpleTypeUnion;
						bool flag = false;
						object[] validatedTypes = xmlSchemaSimpleTypeUnion.ValidatedTypes;
						int i = 0;
						while (i < validatedTypes.Length)
						{
							object obj = validatedTypes[i];
							XmlSchemaDatatype xmlSchemaDatatype = obj as XmlSchemaDatatype;
							XmlSchemaSimpleType xmlSchemaSimpleType = obj as XmlSchemaSimpleType;
							if (xmlSchemaDatatype != null)
							{
								try
								{
									xmlSchemaDatatype.ParseValue(normalized, this.nameTable, this.nsResolver);
								}
								catch (Exception)
								{
									goto IL_170;
								}
								goto IL_168;
							}
							try
							{
								this.AssessStringValid(xmlSchemaSimpleType, xmlSchemaSimpleType.Datatype, normalized);
							}
							catch (XmlSchemaValidationException)
							{
								goto IL_170;
							}
							goto IL_168;
							IL_170:
							i++;
							continue;
							IL_168:
							flag = true;
							break;
						}
						if (!flag)
						{
							this.HandleError("Union type value contains one or more invalid values.");
						}
					}
				}
				else
				{
					XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = st.Content as XmlSchemaSimpleTypeList;
					string[] array = normalized.Split(XmlChar.WhitespaceChars);
					XmlSchemaDatatype xmlSchemaDatatype = xmlSchemaSimpleTypeList.ValidatedListItemType as XmlSchemaDatatype;
					XmlSchemaSimpleType xmlSchemaSimpleType = xmlSchemaSimpleTypeList.ValidatedListItemType as XmlSchemaSimpleType;
					foreach (string text in array)
					{
						if (!(text == string.Empty))
						{
							if (xmlSchemaDatatype != null)
							{
								try
								{
									xmlSchemaDatatype.ParseValue(text, this.nameTable, this.nsResolver);
								}
								catch (Exception innerException)
								{
									this.HandleError("List type value contains one or more invalid values.", innerException);
									break;
								}
							}
							else
							{
								this.AssessStringValid(xmlSchemaSimpleType, xmlSchemaSimpleType.Datatype, text);
							}
						}
					}
				}
			}
			else
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = st.Content as XmlSchemaSimpleTypeRestriction;
				if (xmlSchemaSimpleTypeRestriction != null)
				{
					XmlSchemaSimpleType xmlSchemaSimpleType2 = st.BaseXmlSchemaType as XmlSchemaSimpleType;
					if (xmlSchemaSimpleType2 != null)
					{
						this.AssessStringValid(xmlSchemaSimpleType2, dt, normalized);
					}
					if (!xmlSchemaSimpleTypeRestriction.ValidateValueWithFacets(normalized, this.nameTable, this.nsResolver))
					{
						this.HandleError("Specified value was invalid against the facets.");
						return;
					}
				}
				dt = st.Datatype;
			}
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00076894 File Offset: 0x00074A94
		private XsdKeyTable CreateNewKeyTable(XmlSchemaIdentityConstraint ident)
		{
			XsdKeyTable xsdKeyTable = new XsdKeyTable(ident);
			xsdKeyTable.StartDepth = this.depth;
			this.keyTables.Add(xsdKeyTable);
			return xsdKeyTable;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000768C4 File Offset: 0x00074AC4
		private void ValidateKeySelectors()
		{
			if (this.tmpKeyrefPool != null)
			{
				this.tmpKeyrefPool.Clear();
			}
			if (this.Context.Element != null && this.Context.Element.Constraints.Count > 0)
			{
				for (int i = 0; i < this.Context.Element.Constraints.Count; i++)
				{
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)this.Context.Element.Constraints[i];
					XsdKeyTable value = this.CreateNewKeyTable(xmlSchemaIdentityConstraint);
					if (xmlSchemaIdentityConstraint is XmlSchemaKeyref)
					{
						if (this.tmpKeyrefPool == null)
						{
							this.tmpKeyrefPool = new ArrayList();
						}
						this.tmpKeyrefPool.Add(value);
					}
				}
			}
			for (int j = 0; j < this.keyTables.Count; j++)
			{
				XsdKeyTable xsdKeyTable = (XsdKeyTable)this.keyTables[j];
				if (xsdKeyTable.SelectorMatches(this.elementQNameStack, this.depth) != null)
				{
					XsdKeyEntry entry = new XsdKeyEntry(xsdKeyTable, this.depth, this.lineInfo);
					xsdKeyTable.Entries.Add(entry);
				}
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000769F4 File Offset: 0x00074BF4
		private void ValidateKeyFieldsAttribute(XmlSchemaAttribute attr, object value)
		{
			this.ValidateKeyFields(true, false, attr.AttributeType, attr.QualifiedName.Name, attr.QualifiedName.Namespace, value);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00076A28 File Offset: 0x00074C28
		private void ValidateKeyFields(bool isAttr, bool isNil, object schemaType, string attrName, string attrNs, object value)
		{
			for (int i = 0; i < this.keyTables.Count; i++)
			{
				XsdKeyTable xsdKeyTable = (XsdKeyTable)this.keyTables[i];
				for (int j = 0; j < xsdKeyTable.Entries.Count; j++)
				{
					this.CurrentAttributeType = null;
					try
					{
						xsdKeyTable.Entries[j].ProcessMatch(isAttr, this.elementQNameStack, this.nominalEventSender, this.nameTable, this.BaseUri, schemaType, this.nsResolver, this.lineInfo, (!isAttr) ? this.depth : (this.depth + 1), attrName, attrNs, value, isNil, this.currentKeyFieldConsumers);
					}
					catch (XmlSchemaValidationException exception)
					{
						this.HandleError(exception);
					}
				}
			}
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00076B14 File Offset: 0x00074D14
		private void ValidateEndElementKeyConstraints()
		{
			for (int i = 0; i < this.keyTables.Count; i++)
			{
				XsdKeyTable xsdKeyTable = this.keyTables[i] as XsdKeyTable;
				if (xsdKeyTable.StartDepth == this.depth)
				{
					this.ValidateEndKeyConstraint(xsdKeyTable);
				}
				else
				{
					for (int j = 0; j < xsdKeyTable.Entries.Count; j++)
					{
						XsdKeyEntry xsdKeyEntry = xsdKeyTable.Entries[j];
						if (xsdKeyEntry.StartDepth == this.depth)
						{
							if (xsdKeyEntry.KeyFound)
							{
								xsdKeyTable.FinishedEntries.Add(xsdKeyEntry);
							}
							else if (xsdKeyTable.SourceSchemaIdentity is XmlSchemaKey)
							{
								this.HandleError("Key sequence is missing.");
							}
							xsdKeyTable.Entries.RemoveAt(j);
							j--;
						}
						else
						{
							for (int k = 0; k < xsdKeyEntry.KeyFields.Count; k++)
							{
								XsdKeyEntryField xsdKeyEntryField = xsdKeyEntry.KeyFields[k];
								if (!xsdKeyEntryField.FieldFound && xsdKeyEntryField.FieldFoundDepth == this.depth)
								{
									xsdKeyEntryField.FieldFoundDepth = 0;
									xsdKeyEntryField.FieldFoundPath = null;
								}
							}
						}
					}
				}
			}
			for (int l = 0; l < this.keyTables.Count; l++)
			{
				XsdKeyTable xsdKeyTable2 = this.keyTables[l] as XsdKeyTable;
				if (xsdKeyTable2.StartDepth == this.depth)
				{
					this.keyTables.RemoveAt(l);
					l--;
				}
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00076CA8 File Offset: 0x00074EA8
		private void ValidateEndKeyConstraint(XsdKeyTable seq)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < seq.Entries.Count; i++)
			{
				XsdKeyEntry xsdKeyEntry = seq.Entries[i];
				if (!xsdKeyEntry.KeyFound)
				{
					if (seq.SourceSchemaIdentity is XmlSchemaKey)
					{
						arrayList.Add(string.Concat(new object[]
						{
							"line ",
							xsdKeyEntry.SelectorLineNumber,
							"position ",
							xsdKeyEntry.SelectorLinePosition
						}));
					}
				}
			}
			if (arrayList.Count > 0)
			{
				this.HandleError("Invalid identity constraints were found. Key was not found. " + string.Join(", ", arrayList.ToArray(typeof(string)) as string[]));
			}
			arrayList.Clear();
			XmlSchemaKeyref xmlSchemaKeyref = seq.SourceSchemaIdentity as XmlSchemaKeyref;
			if (xmlSchemaKeyref != null)
			{
				for (int j = this.keyTables.Count - 1; j >= 0; j--)
				{
					XsdKeyTable xsdKeyTable = this.keyTables[j] as XsdKeyTable;
					if (xsdKeyTable.SourceSchemaIdentity == xmlSchemaKeyref.Target)
					{
						seq.ReferencedKey = xsdKeyTable;
						for (int k = 0; k < seq.FinishedEntries.Count; k++)
						{
							XsdKeyEntry xsdKeyEntry2 = seq.FinishedEntries[k];
							for (int l = 0; l < xsdKeyTable.FinishedEntries.Count; l++)
							{
								XsdKeyEntry other = xsdKeyTable.FinishedEntries[l];
								if (xsdKeyEntry2.CompareIdentity(other))
								{
									xsdKeyEntry2.KeyRefFound = true;
									break;
								}
							}
						}
					}
				}
				if (seq.ReferencedKey == null)
				{
					this.HandleError("Target key was not found.");
				}
				for (int m = 0; m < seq.FinishedEntries.Count; m++)
				{
					XsdKeyEntry xsdKeyEntry3 = seq.FinishedEntries[m];
					if (!xsdKeyEntry3.KeyRefFound)
					{
						arrayList.Add(string.Concat(new object[]
						{
							" line ",
							xsdKeyEntry3.SelectorLineNumber,
							", position ",
							xsdKeyEntry3.SelectorLinePosition
						}));
					}
				}
				if (arrayList.Count > 0)
				{
					this.HandleError("Invalid identity constraints were found. Referenced key was not found: " + string.Join(" / ", arrayList.ToArray(typeof(string)) as string[]));
				}
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00076F2C File Offset: 0x0007512C
		private void ValidateSimpleContentIdentity(XmlSchemaDatatype dt, string value)
		{
			if (this.currentKeyFieldConsumers != null)
			{
				while (this.currentKeyFieldConsumers.Count > 0)
				{
					XsdKeyEntryField xsdKeyEntryField = this.currentKeyFieldConsumers[0] as XsdKeyEntryField;
					if (xsdKeyEntryField.Identity != null)
					{
						this.HandleError("Two or more identical field was found. Former value is '" + xsdKeyEntryField.Identity + "' .");
					}
					object obj = null;
					if (dt != null)
					{
						try
						{
							obj = dt.ParseValue(value, this.nameTable, this.nsResolver);
						}
						catch (Exception innerException)
						{
							this.HandleError("Identity value is invalid against its data type " + dt.TokenizedType, innerException);
						}
					}
					if (obj == null)
					{
						obj = value;
					}
					if (!xsdKeyEntryField.SetIdentityField(obj, this.depth == this.xsiNilDepth, dt as XsdAnySimpleType, this.depth, this.lineInfo))
					{
						this.HandleError("Two or more identical key value was found: '" + value + "' .");
					}
					this.currentKeyFieldConsumers.RemoveAt(0);
				}
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00077048 File Offset: 0x00075248
		private object GetXsiType(string name)
		{
			XmlQualifiedName xmlQualifiedName = XmlQualifiedName.Parse(name, this.nsResolver, true);
			object result;
			if (xmlQualifiedName == XmlSchemaComplexType.AnyTypeName)
			{
				result = XmlSchemaComplexType.AnyType;
			}
			else if (XmlSchemaUtil.IsBuiltInDatatypeName(xmlQualifiedName))
			{
				result = XmlSchemaDatatype.FromName(xmlQualifiedName);
			}
			else
			{
				result = this.FindType(xmlQualifiedName);
			}
			return result;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x000770A0 File Offset: 0x000752A0
		private void HandleXsiType(string typename)
		{
			XmlSchemaElement element = this.Context.Element;
			object xsiType = this.GetXsiType(typename);
			if (xsiType == null)
			{
				this.HandleError("The instance type was not found: " + typename);
				return;
			}
			XmlSchemaType xmlSchemaType = xsiType as XmlSchemaType;
			if (xmlSchemaType != null && this.Context.Element != null)
			{
				XmlSchemaType xmlSchemaType2 = element.ElementType as XmlSchemaType;
				if (xmlSchemaType2 != null && (xmlSchemaType.DerivedBy & xmlSchemaType2.FinalResolved) != XmlSchemaDerivationMethod.Empty)
				{
					this.HandleError("The instance type is prohibited by the type of the context element.");
				}
				if (xmlSchemaType2 != xsiType && (xmlSchemaType.DerivedBy & element.BlockResolved) != XmlSchemaDerivationMethod.Empty)
				{
					this.HandleError("The instance type is prohibited by the context element.");
				}
			}
			XmlSchemaComplexType xmlSchemaComplexType = xsiType as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null && xmlSchemaComplexType.IsAbstract)
			{
				this.HandleError("The instance type is abstract: " + typename);
			}
			else
			{
				if (element != null)
				{
					this.AssessLocalTypeDerivationOK(xsiType, element.ElementType, element.BlockResolved);
				}
				this.Context.XsiType = xsiType;
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x000771A0 File Offset: 0x000753A0
		private void AssessLocalTypeDerivationOK(object xsiType, object baseType, XmlSchemaDerivationMethod flag)
		{
			XmlSchemaType xmlSchemaType = xsiType as XmlSchemaType;
			XmlSchemaComplexType xmlSchemaComplexType = baseType as XmlSchemaComplexType;
			XmlSchemaComplexType xmlSchemaComplexType2 = xmlSchemaType as XmlSchemaComplexType;
			if (xsiType != baseType)
			{
				if (xmlSchemaComplexType != null)
				{
					flag |= xmlSchemaComplexType.BlockResolved;
				}
				if (flag == XmlSchemaDerivationMethod.All)
				{
					this.HandleError("Prohibited element type substitution.");
					return;
				}
				if (xmlSchemaType != null && (flag & xmlSchemaType.DerivedBy) != XmlSchemaDerivationMethod.Empty)
				{
					this.HandleError("Prohibited element type substitution.");
					return;
				}
			}
			if (xmlSchemaComplexType2 != null)
			{
				try
				{
					xmlSchemaComplexType2.ValidateTypeDerivationOK(baseType, null, null);
				}
				catch (XmlSchemaValidationException exception)
				{
					this.HandleError(exception);
				}
			}
			else
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = xsiType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null)
				{
					try
					{
						xmlSchemaSimpleType.ValidateTypeDerivationOK(baseType, null, null, true);
					}
					catch (XmlSchemaValidationException exception2)
					{
						this.HandleError(exception2);
					}
				}
				else if (!(xsiType is XmlSchemaDatatype))
				{
					this.HandleError("Primitive data type cannot be derived type using xsi:type specification.");
				}
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x000772B8 File Offset: 0x000754B8
		private void HandleXsiNil(string value, XmlSchemaInfo info)
		{
			XmlSchemaElement element = this.Context.Element;
			if (!element.ActualIsNillable)
			{
				this.HandleError(string.Format("Current element '{0}' is not nillable and thus does not allow occurence of 'nil' attribute.", this.Context.Element.QualifiedName));
				return;
			}
			value = value.Trim(XmlChar.WhitespaceChars);
			if (value == "true")
			{
				if (element.ValidatedFixedValue != null)
				{
					this.HandleError("Schema instance nil was specified, where the element declaration for " + element.QualifiedName + "has fixed value constraints.");
				}
				this.xsiNilDepth = this.depth;
				if (info != null)
				{
					info.IsNil = true;
				}
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0007735C File Offset: 0x0007555C
		private XmlSchema ReadExternalSchema(string uri)
		{
			Uri uri2 = new Uri(this.SourceUri, uri.Trim(XmlChar.WhitespaceChars));
			XmlTextReader xmlTextReader = null;
			XmlSchema result;
			try
			{
				xmlTextReader = new XmlTextReader(uri2.ToString(), (Stream)this.xmlResolver.GetEntity(uri2, null, typeof(Stream)), this.nameTable);
				result = XmlSchema.Read(xmlTextReader, this.ValidationEventHandler);
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
			return result;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000773F4 File Offset: 0x000755F4
		private void HandleSchemaLocation(string schemaLocation)
		{
			if (this.xmlResolver == null)
			{
				return;
			}
			XmlSchema xmlSchema = null;
			bool flag = false;
			string[] array = null;
			try
			{
				schemaLocation = (XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Token).Datatype.ParseValue(schemaLocation, null, null) as string);
				array = schemaLocation.Split(XmlChar.WhitespaceChars);
			}
			catch (Exception innerException)
			{
				this.HandleError("Invalid schemaLocation attribute format.", innerException, true);
				array = new string[0];
			}
			if (array.Length % 2 != 0)
			{
				this.HandleError("Invalid schemaLocation attribute format.");
			}
			int i = 0;
			while (i < array.Length)
			{
				try
				{
					xmlSchema = this.ReadExternalSchema(array[i + 1]);
				}
				catch (Exception innerException2)
				{
					this.HandleError("Could not resolve schema location URI: " + schemaLocation, innerException2, true);
					goto IL_10B;
				}
				goto IL_A7;
				IL_10B:
				i += 2;
				continue;
				IL_A7:
				if (xmlSchema.TargetNamespace == null)
				{
					xmlSchema.TargetNamespace = array[i];
				}
				else if (xmlSchema.TargetNamespace != array[i])
				{
					this.HandleError("Specified schema has different target namespace.");
				}
				if (xmlSchema != null && !this.schemas.Contains(xmlSchema.TargetNamespace))
				{
					flag = true;
					this.schemas.Add(xmlSchema);
					goto IL_10B;
				}
				goto IL_10B;
			}
			if (flag)
			{
				this.schemas.Compile();
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00077564 File Offset: 0x00075764
		private void HandleNoNSSchemaLocation(string noNsSchemaLocation)
		{
			if (this.xmlResolver == null)
			{
				return;
			}
			XmlSchema xmlSchema = null;
			bool flag = false;
			try
			{
				xmlSchema = this.ReadExternalSchema(noNsSchemaLocation);
			}
			catch (Exception innerException)
			{
				this.HandleError("Could not resolve schema location URI: " + noNsSchemaLocation, innerException, true);
			}
			if (xmlSchema != null && xmlSchema.TargetNamespace != null)
			{
				this.HandleError("Specified schema has different target namespace.");
			}
			if (xmlSchema != null && !this.schemas.Contains(xmlSchema.TargetNamespace))
			{
				flag = true;
				this.schemas.Add(xmlSchema);
			}
			if (flag)
			{
				this.schemas.Compile();
			}
		}

		// Token: 0x04000983 RID: 2435
		private static readonly XmlSchemaAttribute[] emptyAttributeArray = new XmlSchemaAttribute[0];

		// Token: 0x04000984 RID: 2436
		private object nominalEventSender;

		// Token: 0x04000985 RID: 2437
		private IXmlLineInfo lineInfo;

		// Token: 0x04000986 RID: 2438
		private IXmlNamespaceResolver nsResolver;

		// Token: 0x04000987 RID: 2439
		private Uri sourceUri;

		// Token: 0x04000988 RID: 2440
		private XmlNameTable nameTable;

		// Token: 0x04000989 RID: 2441
		private XmlSchemaSet schemas;

		// Token: 0x0400098A RID: 2442
		private XmlResolver xmlResolver = new XmlUrlResolver();

		// Token: 0x0400098B RID: 2443
		private XmlSchemaObject startType;

		// Token: 0x0400098C RID: 2444
		private XmlSchemaValidationFlags options;

		// Token: 0x0400098D RID: 2445
		private XmlSchemaValidator.Transition transition;

		// Token: 0x0400098E RID: 2446
		private XsdParticleStateManager state;

		// Token: 0x0400098F RID: 2447
		private ArrayList occuredAtts = new ArrayList();

		// Token: 0x04000990 RID: 2448
		private XmlSchemaAttribute[] defaultAttributes = XmlSchemaValidator.emptyAttributeArray;

		// Token: 0x04000991 RID: 2449
		private ArrayList defaultAttributesCache = new ArrayList();

		// Token: 0x04000992 RID: 2450
		private XsdIDManager idManager = new XsdIDManager();

		// Token: 0x04000993 RID: 2451
		private ArrayList keyTables = new ArrayList();

		// Token: 0x04000994 RID: 2452
		private ArrayList currentKeyFieldConsumers = new ArrayList();

		// Token: 0x04000995 RID: 2453
		private ArrayList tmpKeyrefPool;

		// Token: 0x04000996 RID: 2454
		private ArrayList elementQNameStack = new ArrayList();

		// Token: 0x04000997 RID: 2455
		private StringBuilder storedCharacters = new StringBuilder();

		// Token: 0x04000998 RID: 2456
		private bool shouldValidateCharacters;

		// Token: 0x04000999 RID: 2457
		private int depth;

		// Token: 0x0400099A RID: 2458
		private int xsiNilDepth = -1;

		// Token: 0x0400099B RID: 2459
		private int skipValidationDepth = -1;

		// Token: 0x0400099C RID: 2460
		internal XmlSchemaDatatype CurrentAttributeType;

		// Token: 0x02000247 RID: 583
		private enum Transition
		{
			// Token: 0x0400099F RID: 2463
			None,
			// Token: 0x040009A0 RID: 2464
			Content,
			// Token: 0x040009A1 RID: 2465
			StartTag,
			// Token: 0x040009A2 RID: 2466
			Finished
		}
	}
}
