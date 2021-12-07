using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Xml.Serialization
{
	/// <summary>Abstract class used for controlling serialization by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class. </summary>
	// Token: 0x020002AD RID: 685
	public abstract class XmlSerializationWriter : XmlSerializationGeneratedCode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> class. </summary>
		// Token: 0x06001C5E RID: 7262 RVA: 0x00097528 File Offset: 0x00095728
		protected XmlSerializationWriter()
		{
			this.qnameCount = 0;
			this.serializedObjects = new Hashtable();
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00097544 File Offset: 0x00095744
		internal void Initialize(XmlWriter writer, XmlSerializerNamespaces nss)
		{
			this.writer = writer;
			if (nss != null)
			{
				this.namespaces = new ArrayList();
				foreach (XmlQualifiedName xmlQualifiedName in nss.ToArray())
				{
					if (xmlQualifiedName.Name != string.Empty && xmlQualifiedName.Namespace != string.Empty)
					{
						this.namespaces.Add(xmlQualifiedName);
					}
				}
			}
		}

		/// <summary>Gets or sets a list of XML qualified name objects that contain the namespaces and prefixes used to produce qualified names in XML documents. </summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the namespaces and prefix pairs.</returns>
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x000975C0 File Offset: 0x000957C0
		// (set) Token: 0x06001C61 RID: 7265 RVA: 0x000975C8 File Offset: 0x000957C8
		protected ArrayList Namespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlWriter" /> that is being used by the <see cref="T:System.Xml.Serialization.XmlSerializationWriter" />. </summary>
		/// <returns>The <see cref="T:System.Xml.XmlWriter" /> used by the class instance.</returns>
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x000975D4 File Offset: 0x000957D4
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x000975DC File Offset: 0x000957DC
		protected XmlWriter Writer
		{
			get
			{
				return this.writer;
			}
			set
			{
				this.writer = value;
			}
		}

		/// <summary>Stores an implementation of the <see cref="T:System.Xml.Serialization.XmlSerializationWriteCallback" /> delegate and the type it applies to, for a later invocation. </summary>
		/// <param name="type">The <see cref="T:System.Type" /> of objects that are serialized.</param>
		/// <param name="typeName">The name of the type of objects that are serialized.</param>
		/// <param name="typeNs">The namespace of the type of objects that are serialized.</param>
		/// <param name="callback">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializationWriteCallback" /> delegate.</param>
		// Token: 0x06001C64 RID: 7268 RVA: 0x000975E8 File Offset: 0x000957E8
		protected void AddWriteCallback(Type type, string typeName, string typeNs, XmlSerializationWriteCallback callback)
		{
			XmlSerializationWriter.WriteCallbackInfo writeCallbackInfo = new XmlSerializationWriter.WriteCallbackInfo();
			writeCallbackInfo.Type = type;
			writeCallbackInfo.TypeName = typeName;
			writeCallbackInfo.TypeNs = typeNs;
			writeCallbackInfo.Callback = callback;
			if (this.callbacks == null)
			{
				this.callbacks = new Hashtable();
			}
			this.callbacks.Add(type, writeCallbackInfo);
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates an unexpected name for an element that adheres to an XML Schema choice element declaration.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="value">The name that is not valid.</param>
		/// <param name="identifier">The choice element declaration that the name belongs to.</param>
		/// <param name="name">The expected local name of an element.</param>
		/// <param name="ns">The expected namespace of an element.</param>
		// Token: 0x06001C65 RID: 7269 RVA: 0x0009763C File Offset: 0x0009583C
		protected Exception CreateChoiceIdentifierValueException(string value, string identifier, string name, string ns)
		{
			string message = string.Format("Value '{0}' of the choice identifier '{1}' does not match element '{2}' from namespace '{3}'.", new object[]
			{
				value,
				identifier,
				name,
				ns
			});
			return new InvalidOperationException(message);
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates a failure while writing an array where an XML Schema choice element declaration is applied.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="type">The type being serialized.</param>
		/// <param name="identifier">A name for the choice element declaration.</param>
		// Token: 0x06001C66 RID: 7270 RVA: 0x00097674 File Offset: 0x00095874
		protected Exception CreateInvalidChoiceIdentifierValueException(string type, string identifier)
		{
			string message = string.Format("Invalid or missing choice identifier '{0}' of type '{1}'.", identifier, type);
			return new InvalidOperationException(message);
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a value for an XML element does not match an enumeration type.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="value">The value that is not valid.</param>
		/// <param name="elementName">The name of the XML element with an invalid value.</param>
		/// <param name="enumValue">The valid value.</param>
		// Token: 0x06001C67 RID: 7271 RVA: 0x00097694 File Offset: 0x00095894
		protected Exception CreateMismatchChoiceException(string value, string elementName, string enumValue)
		{
			string message = string.Format("Value of {0} mismatches the type of {1}, you need to set it to {2}.", elementName, value, enumValue);
			return new InvalidOperationException(message);
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an XML element that should adhere to the XML Schema any element declaration cannot be processed.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="name">The XML element that cannot be processed.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		// Token: 0x06001C68 RID: 7272 RVA: 0x000976B8 File Offset: 0x000958B8
		protected Exception CreateUnknownAnyElementException(string name, string ns)
		{
			string message = string.Format("The XML element named '{0}' from namespace '{1}' was not expected. The XML element name and namespace must match those provided via XmlAnyElementAttribute(s).", name, ns);
			return new InvalidOperationException(message);
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a type being serialized is not being used in a valid manner or is unexpectedly encountered. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="o">The object whose type cannot be serialized.</param>
		// Token: 0x06001C69 RID: 7273 RVA: 0x000976D8 File Offset: 0x000958D8
		protected Exception CreateUnknownTypeException(object o)
		{
			return this.CreateUnknownTypeException(o.GetType());
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a type being serialized is not being used in a valid manner or is unexpectedly encountered. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="type">The type that cannot be serialized.</param>
		// Token: 0x06001C6A RID: 7274 RVA: 0x000976E8 File Offset: 0x000958E8
		protected Exception CreateUnknownTypeException(Type type)
		{
			string message = string.Format("The type {0} may not be used in this context.", type);
			return new InvalidOperationException(message);
		}

		/// <summary>Processes a base-64 byte array.</summary>
		/// <returns>The same byte array that was passed in as an argument.</returns>
		/// <param name="value">A base-64 <see cref="T:System.Byte" /> array.</param>
		// Token: 0x06001C6B RID: 7275 RVA: 0x00097708 File Offset: 0x00095908
		protected static byte[] FromByteArrayBase64(byte[] value)
		{
			return value;
		}

		/// <summary>Produces a string from an input hexadecimal byte array.</summary>
		/// <returns>The byte array value converted to a string.</returns>
		/// <param name="value">A hexadecimal byte array to translate to a string.</param>
		// Token: 0x06001C6C RID: 7276 RVA: 0x0009770C File Offset: 0x0009590C
		protected static string FromByteArrayHex(byte[] value)
		{
			return XmlCustomFormatter.FromByteArrayHex(value);
		}

		/// <summary>Produces a string from an input <see cref="T:System.Char" />.</summary>
		/// <returns>The <see cref="T:System.Char" /> value converted to a string.</returns>
		/// <param name="value">A <see cref="T:System.Char" /> to translate to a string.</param>
		// Token: 0x06001C6D RID: 7277 RVA: 0x00097714 File Offset: 0x00095914
		protected static string FromChar(char value)
		{
			return XmlCustomFormatter.FromChar(value);
		}

		/// <summary>Produces a string from a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A string representation of the <see cref="T:System.DateTime" /> that shows the date but no time.</returns>
		/// <param name="value">A <see cref="T:System.DateTime" /> to translate to a string.</param>
		// Token: 0x06001C6E RID: 7278 RVA: 0x0009771C File Offset: 0x0009591C
		protected static string FromDate(DateTime value)
		{
			return XmlCustomFormatter.FromDate(value);
		}

		/// <summary>Produces a string from an input <see cref="T:System.DateTime" />.</summary>
		/// <returns>A string representation of the <see cref="T:System.DateTime" /> that shows the date and time.</returns>
		/// <param name="value">A <see cref="T:System.DateTime" /> to translate to a string.</param>
		// Token: 0x06001C6F RID: 7279 RVA: 0x00097724 File Offset: 0x00095924
		protected static string FromDateTime(DateTime value)
		{
			return XmlCustomFormatter.FromDateTime(value);
		}

		/// <summary>Produces a string that consists of delimited identifiers that represent the enumeration members that have been set.</summary>
		/// <returns>A string that consists of delimited identifiers, where each represents a member from the set enumerator list.</returns>
		/// <param name="value">The enumeration value as a series of bitwise OR operations.</param>
		/// <param name="values">The enumeration's name values.</param>
		/// <param name="ids">The enumeration's constant values.</param>
		// Token: 0x06001C70 RID: 7280 RVA: 0x0009772C File Offset: 0x0009592C
		protected static string FromEnum(long value, string[] values, long[] ids)
		{
			return XmlCustomFormatter.FromEnum(value, values, ids);
		}

		/// <summary>Produces a string from a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A string representation of the <see cref="T:System.DateTime" /> object that shows the time but no date.</returns>
		/// <param name="value">A <see cref="T:System.DateTime" /> that is translated to a string.</param>
		// Token: 0x06001C71 RID: 7281 RVA: 0x00097738 File Offset: 0x00095938
		protected static string FromTime(DateTime value)
		{
			return XmlCustomFormatter.FromTime(value);
		}

		/// <summary>Encodes a valid XML name by replacing characters that are not valid with escape sequences.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="name">A string to be used as an XML name.</param>
		// Token: 0x06001C72 RID: 7282 RVA: 0x00097740 File Offset: 0x00095940
		protected static string FromXmlName(string name)
		{
			return XmlCustomFormatter.FromXmlName(name);
		}

		/// <summary>Encodes a valid XML local name by replacing characters that are not valid with escape sequences.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="ncName">A string to be used as a local (unqualified) XML name.</param>
		// Token: 0x06001C73 RID: 7283 RVA: 0x00097748 File Offset: 0x00095948
		protected static string FromXmlNCName(string ncName)
		{
			return XmlCustomFormatter.FromXmlNCName(ncName);
		}

		/// <summary>Encodes an XML name.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="nmToken">An XML name to be encoded.</param>
		// Token: 0x06001C74 RID: 7284 RVA: 0x00097750 File Offset: 0x00095950
		protected static string FromXmlNmToken(string nmToken)
		{
			return XmlCustomFormatter.FromXmlNmToken(nmToken);
		}

		/// <summary>Encodes a space-delimited sequence of XML names into a single XML name.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="nmTokens">A space-delimited sequence of XML names to be encoded.</param>
		// Token: 0x06001C75 RID: 7285 RVA: 0x00097758 File Offset: 0x00095958
		protected static string FromXmlNmTokens(string nmTokens)
		{
			return XmlCustomFormatter.FromXmlNmTokens(nmTokens);
		}

		/// <summary>Returns an XML qualified name, with invalid characters replaced by escape sequences. </summary>
		/// <returns>An XML qualified name, with invalid characters replaced by escape sequences.</returns>
		/// <param name="xmlQualifiedName">An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the XML to be written.</param>
		// Token: 0x06001C76 RID: 7286 RVA: 0x00097760 File Offset: 0x00095960
		protected string FromXmlQualifiedName(XmlQualifiedName xmlQualifiedName)
		{
			if (xmlQualifiedName == null || xmlQualifiedName == XmlQualifiedName.Empty)
			{
				return null;
			}
			return this.GetQualifiedName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000977A0 File Offset: 0x000959A0
		private string GetId(object o, bool addToReferencesList)
		{
			if (this.idGenerator == null)
			{
				this.idGenerator = new ObjectIDGenerator();
			}
			bool flag;
			long id = this.idGenerator.GetId(o, out flag);
			return string.Format(CultureInfo.InvariantCulture, "id{0}", new object[]
			{
				id
			});
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x000977F0 File Offset: 0x000959F0
		private bool AlreadyQueued(object ob)
		{
			if (this.idGenerator == null)
			{
				return false;
			}
			bool flag;
			this.idGenerator.HasId(ob, out flag);
			return !flag;
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00097820 File Offset: 0x00095A20
		private string GetNamespacePrefix(string ns)
		{
			string text = this.Writer.LookupPrefix(ns);
			if (text == null)
			{
				text = string.Format(CultureInfo.InvariantCulture, "q{0}", new object[]
				{
					++this.qnameCount
				});
				this.WriteAttribute("xmlns", text, null, ns);
			}
			return text;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00097880 File Offset: 0x00095A80
		private string GetQualifiedName(string name, string ns)
		{
			if (ns == string.Empty)
			{
				return name;
			}
			string namespacePrefix = this.GetNamespacePrefix(ns);
			if (namespacePrefix == string.Empty)
			{
				return name;
			}
			return string.Format("{0}:{1}", namespacePrefix, name);
		}

		/// <summary>Initializes instances of the <see cref="T:System.Xml.Serialization.XmlSerializationWriteCallback" /> delegate to serialize SOAP-encoded XML data. </summary>
		// Token: 0x06001C7B RID: 7291
		protected abstract void InitCallbacks();

		/// <summary>Initializes object references only while serializing a SOAP-encoded SOAP message.</summary>
		// Token: 0x06001C7C RID: 7292 RVA: 0x000978C8 File Offset: 0x00095AC8
		protected void TopLevelElement()
		{
			this.topLevelElement = true;
		}

		/// <summary>Instructs an <see cref="T:System.Xml.XmlWriter" /> object to write an XML attribute that has no namespace specified for its name.</summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a byte array.</param>
		// Token: 0x06001C7D RID: 7293 RVA: 0x000978D4 File Offset: 0x00095AD4
		protected void WriteAttribute(string localName, byte[] value)
		{
			this.WriteAttribute(localName, string.Empty, value);
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlWriter" /> to write an XML attribute that has no namespace specified for its name. </summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a string.</param>
		// Token: 0x06001C7E RID: 7294 RVA: 0x000978E4 File Offset: 0x00095AE4
		protected void WriteAttribute(string localName, string value)
		{
			this.WriteAttribute(string.Empty, localName, string.Empty, value);
		}

		/// <summary>Instructs an <see cref="T:System.Xml.XmlWriter" /> object to write an XML attribute.</summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="ns">The namespace of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a byte array.</param>
		// Token: 0x06001C7F RID: 7295 RVA: 0x000978F8 File Offset: 0x00095AF8
		protected void WriteAttribute(string localName, string ns, byte[] value)
		{
			if (value == null)
			{
				return;
			}
			this.Writer.WriteStartAttribute(localName, ns);
			this.WriteValue(value);
			this.Writer.WriteEndAttribute();
		}

		/// <summary>Writes an XML attribute. </summary>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="ns">The namespace of the XML attribute.</param>
		/// <param name="value">The value of the XML attribute as a string.</param>
		// Token: 0x06001C80 RID: 7296 RVA: 0x0009792C File Offset: 0x00095B2C
		protected void WriteAttribute(string localName, string ns, string value)
		{
			this.WriteAttribute(null, localName, ns, value);
		}

		/// <summary>Writes an XML attribute where the namespace prefix is provided manually. </summary>
		/// <param name="prefix">The namespace prefix to write.</param>
		/// <param name="localName">The local name of the XML attribute.</param>
		/// <param name="ns">The namespace represented by the prefix.</param>
		/// <param name="value">The value of the XML attribute as a string.</param>
		// Token: 0x06001C81 RID: 7297 RVA: 0x00097938 File Offset: 0x00095B38
		protected void WriteAttribute(string prefix, string localName, string ns, string value)
		{
			if (value == null)
			{
				return;
			}
			this.Writer.WriteAttributeString(prefix, localName, ns, value);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00097954 File Offset: 0x00095B54
		private void WriteXmlNode(XmlNode node)
		{
			if (node is XmlDocument)
			{
				node = ((XmlDocument)node).DocumentElement;
			}
			node.WriteTo(this.Writer);
		}

		/// <summary>Writes an XML node object within the body of a named XML element.</summary>
		/// <param name="node">The XML node to write, possibly a child XML element.</param>
		/// <param name="name">The local name of the parent XML element to write.</param>
		/// <param name="ns">The namespace of the parent XML element to write.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the object to serialize is null; otherwise, false.</param>
		/// <param name="any">true to indicate that the node, if an XML element, adheres to an XML Schema any element declaration; otherwise, false.</param>
		// Token: 0x06001C83 RID: 7299 RVA: 0x00097988 File Offset: 0x00095B88
		protected void WriteElementEncoded(XmlNode node, string name, string ns, bool isNullable, bool any)
		{
			if (name != string.Empty)
			{
				if (node == null)
				{
					if (isNullable)
					{
						this.WriteNullTagEncoded(name, ns);
					}
				}
				else
				{
					this.Writer.WriteStartElement(name, ns);
					this.WriteXmlNode(node);
					this.Writer.WriteEndElement();
				}
			}
			else
			{
				this.WriteXmlNode(node);
			}
		}

		/// <summary>Instructs an <see cref="T:System.Xml.XmlWriter" /> object to write an <see cref="T:System.Xml.XmlNode" /> object within the body of a named XML element.</summary>
		/// <param name="node">The XML node to write, possibly a child XML element.</param>
		/// <param name="name">The local name of the parent XML element to write.</param>
		/// <param name="ns">The namespace of the parent XML element to write.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the object to serialize is null; otherwise, false.</param>
		/// <param name="any">true to indicate that the node, if an XML element, adheres to an XML Schema any element declaration; otherwise, false.</param>
		// Token: 0x06001C84 RID: 7300 RVA: 0x000979EC File Offset: 0x00095BEC
		protected void WriteElementLiteral(XmlNode node, string name, string ns, bool isNullable, bool any)
		{
			if (name != string.Empty)
			{
				if (node == null)
				{
					if (isNullable)
					{
						this.WriteNullTagLiteral(name, ns);
					}
				}
				else
				{
					this.Writer.WriteStartElement(name, ns);
					this.WriteXmlNode(node);
					this.Writer.WriteEndElement();
				}
			}
			else
			{
				this.WriteXmlNode(node);
			}
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		// Token: 0x06001C85 RID: 7301 RVA: 0x00097A50 File Offset: 0x00095C50
		protected void WriteElementQualifiedName(string localName, XmlQualifiedName value)
		{
			this.WriteElementQualifiedName(localName, string.Empty, value, null);
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		// Token: 0x06001C86 RID: 7302 RVA: 0x00097A60 File Offset: 0x00095C60
		protected void WriteElementQualifiedName(string localName, string ns, XmlQualifiedName value)
		{
			this.WriteElementQualifiedName(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C87 RID: 7303 RVA: 0x00097A6C File Offset: 0x00095C6C
		protected void WriteElementQualifiedName(string localName, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			this.WriteElementQualifiedName(localName, string.Empty, value, xsiType);
		}

		/// <summary>Writes an XML element with a specified qualified name in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The name to write, using its prefix if namespace-qualified, in the element text.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C88 RID: 7304 RVA: 0x00097A7C File Offset: 0x00095C7C
		protected void WriteElementQualifiedName(string localName, string ns, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			localName = XmlCustomFormatter.FromXmlNCName(localName);
			this.WriteStartElement(localName, ns);
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			this.Writer.WriteString(this.FromXmlQualifiedName(value));
			this.WriteEndElement();
		}

		/// <summary>Writes an XML element with a specified value in its body. </summary>
		/// <param name="localName">The local name of the XML element to be written without namespace qualification.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001C89 RID: 7305 RVA: 0x00097AD4 File Offset: 0x00095CD4
		protected void WriteElementString(string localName, string value)
		{
			this.WriteElementString(localName, string.Empty, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body. </summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001C8A RID: 7306 RVA: 0x00097AE4 File Offset: 0x00095CE4
		protected void WriteElementString(string localName, string ns, string value)
		{
			this.WriteElementString(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C8B RID: 7307 RVA: 0x00097AF0 File Offset: 0x00095CF0
		protected void WriteElementString(string localName, string value, XmlQualifiedName xsiType)
		{
			this.WriteElementString(localName, string.Empty, value, xsiType);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C8C RID: 7308 RVA: 0x00097B00 File Offset: 0x00095D00
		protected void WriteElementString(string localName, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			if (xsiType != null)
			{
				localName = XmlCustomFormatter.FromXmlNCName(localName);
				this.WriteStartElement(localName, ns);
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
				this.Writer.WriteString(value);
				this.WriteEndElement();
			}
			else
			{
				this.Writer.WriteElementString(localName, ns, value);
			}
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001C8D RID: 7309 RVA: 0x00097B6C File Offset: 0x00095D6C
		protected void WriteElementStringRaw(string localName, byte[] value)
		{
			this.WriteElementStringRaw(localName, string.Empty, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001C8E RID: 7310 RVA: 0x00097B7C File Offset: 0x00095D7C
		protected void WriteElementStringRaw(string localName, string value)
		{
			this.WriteElementStringRaw(localName, string.Empty, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C8F RID: 7311 RVA: 0x00097B8C File Offset: 0x00095D8C
		protected void WriteElementStringRaw(string localName, byte[] value, XmlQualifiedName xsiType)
		{
			this.WriteElementStringRaw(localName, string.Empty, value, xsiType);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001C90 RID: 7312 RVA: 0x00097B9C File Offset: 0x00095D9C
		protected void WriteElementStringRaw(string localName, string ns, byte[] value)
		{
			this.WriteElementStringRaw(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		// Token: 0x06001C91 RID: 7313 RVA: 0x00097BA8 File Offset: 0x00095DA8
		protected void WriteElementStringRaw(string localName, string ns, string value)
		{
			this.WriteElementStringRaw(localName, ns, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C92 RID: 7314 RVA: 0x00097BB4 File Offset: 0x00095DB4
		protected void WriteElementStringRaw(string localName, string value, XmlQualifiedName xsiType)
		{
			this.WriteElementStringRaw(localName, string.Empty, value, null);
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C93 RID: 7315 RVA: 0x00097BC4 File Offset: 0x00095DC4
		protected void WriteElementStringRaw(string localName, string ns, byte[] value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			this.WriteStartElement(localName, ns);
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			if (value.Length > 0)
			{
				this.Writer.WriteBase64(value, 0, value.Length);
			}
			this.WriteEndElement();
		}

		/// <summary>Writes an XML element with a specified value in its body.</summary>
		/// <param name="localName">The local name of the XML element.</param>
		/// <param name="ns">The namespace of the XML element.</param>
		/// <param name="value">The text value of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C94 RID: 7316 RVA: 0x00097C20 File Offset: 0x00095E20
		protected void WriteElementStringRaw(string localName, string ns, string value, XmlQualifiedName xsiType)
		{
			localName = XmlCustomFormatter.FromXmlNCName(localName);
			this.WriteStartElement(localName, ns);
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			this.Writer.WriteRaw(value);
			this.WriteEndElement();
		}

		/// <summary>Writes an XML element whose body is empty. </summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x06001C95 RID: 7317 RVA: 0x00097C70 File Offset: 0x00095E70
		protected void WriteEmptyTag(string name)
		{
			this.WriteEmptyTag(name, string.Empty);
		}

		/// <summary>Writes an XML element whose body is empty.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x06001C96 RID: 7318 RVA: 0x00097C80 File Offset: 0x00095E80
		protected void WriteEmptyTag(string name, string ns)
		{
			name = XmlCustomFormatter.FromXmlName(name);
			this.WriteStartElement(name, ns);
			this.WriteEndElement();
		}

		/// <summary>Writes a &lt;closing&gt; element tag.</summary>
		// Token: 0x06001C97 RID: 7319 RVA: 0x00097C98 File Offset: 0x00095E98
		protected void WriteEndElement()
		{
			this.WriteEndElement(null);
		}

		/// <summary>Writes a closing element tag.</summary>
		/// <param name="o">The object being serialized.</param>
		// Token: 0x06001C98 RID: 7320 RVA: 0x00097CA4 File Offset: 0x00095EA4
		protected void WriteEndElement(object o)
		{
			if (o != null)
			{
				this.serializedObjects.Remove(o);
			}
			this.Writer.WriteEndElement();
		}

		/// <summary>Writes an id attribute that appears in a SOAP-encoded multiRef element. </summary>
		/// <param name="o">The object being serialized.</param>
		// Token: 0x06001C99 RID: 7321 RVA: 0x00097CC4 File Offset: 0x00095EC4
		protected void WriteId(object o)
		{
			this.WriteAttribute("id", this.GetId(o, true));
		}

		/// <summary>Writes namespace declaration attributes.</summary>
		/// <param name="xmlns">The XML namespaces to declare.</param>
		// Token: 0x06001C9A RID: 7322 RVA: 0x00097CDC File Offset: 0x00095EDC
		protected void WriteNamespaceDeclarations(XmlSerializerNamespaces ns)
		{
			if (ns == null)
			{
				return;
			}
			ICollection values = ns.Namespaces.Values;
			foreach (object obj in values)
			{
				XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
				if (xmlQualifiedName.Namespace != string.Empty && this.Writer.LookupPrefix(xmlQualifiedName.Namespace) != xmlQualifiedName.Name)
				{
					this.WriteAttribute("xmlns", xmlQualifiedName.Name, "http://www.w3.org/2000/xmlns/", xmlQualifiedName.Namespace);
				}
			}
		}

		/// <summary>Writes an XML element whose body contains a valid XML qualified name. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The XML qualified name to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C9B RID: 7323 RVA: 0x00097DA4 File Offset: 0x00095FA4
		protected void WriteNullableQualifiedNameEncoded(string name, string ns, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			if (value != null)
			{
				this.WriteElementQualifiedName(name, ns, value, xsiType);
			}
			else
			{
				this.WriteNullTagEncoded(name, ns);
			}
		}

		/// <summary>Writes an XML element whose body contains a valid XML qualified name. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The XML qualified name to write in the body of the XML element.</param>
		// Token: 0x06001C9C RID: 7324 RVA: 0x00097DD8 File Offset: 0x00095FD8
		protected void WriteNullableQualifiedNameLiteral(string name, string ns, XmlQualifiedName value)
		{
			if (value != null)
			{
				this.WriteElementQualifiedName(name, ns, value);
			}
			else
			{
				this.WriteNullTagLiteral(name, ns);
			}
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C9D RID: 7325 RVA: 0x00097E08 File Offset: 0x00096008
		protected void WriteNullableStringEncoded(string name, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value != null)
			{
				this.WriteElementString(name, ns, value, xsiType);
			}
			else
			{
				this.WriteNullTagEncoded(name, ns);
			}
		}

		/// <summary>Writes a byte array as the body of an XML element. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The byte array to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C9E RID: 7326 RVA: 0x00097E28 File Offset: 0x00096028
		protected void WriteNullableStringEncodedRaw(string name, string ns, byte[] value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
			}
			else
			{
				this.WriteElementStringRaw(name, ns, value, xsiType);
			}
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		/// <param name="xsiType">The name of the XML Schema data type to be written to the xsi:type attribute.</param>
		// Token: 0x06001C9F RID: 7327 RVA: 0x00097E48 File Offset: 0x00096048
		protected void WriteNullableStringEncodedRaw(string name, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
			}
			else
			{
				this.WriteElementStringRaw(name, ns, value, xsiType);
			}
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		// Token: 0x06001CA0 RID: 7328 RVA: 0x00097E68 File Offset: 0x00096068
		protected void WriteNullableStringLiteral(string name, string ns, string value)
		{
			if (value != null)
			{
				this.WriteElementString(name, ns, value, null);
			}
			else
			{
				this.WriteNullTagLiteral(name, ns);
			}
		}

		/// <summary>Writes a byte array as the body of an XML element. <see cref="T:System.Xml.XmlWriter" /> inserts an xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The byte array to write in the body of the XML element.</param>
		// Token: 0x06001CA1 RID: 7329 RVA: 0x00097E88 File Offset: 0x00096088
		protected void WriteNullableStringLiteralRaw(string name, string ns, byte[] value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
			}
			else
			{
				this.WriteElementStringRaw(name, ns, value);
			}
		}

		/// <summary>Writes an XML element that contains a string as the body. <see cref="T:System.Xml.XmlWriter" /> inserts a xsi:nil='true' attribute if the string's value is null.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="value">The string to write in the body of the XML element.</param>
		// Token: 0x06001CA2 RID: 7330 RVA: 0x00097EA8 File Offset: 0x000960A8
		protected void WriteNullableStringLiteralRaw(string name, string ns, string value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
			}
			else
			{
				this.WriteElementStringRaw(name, ns, value);
			}
		}

		/// <summary>Writes an XML element with an xsi:nil='true' attribute.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x06001CA3 RID: 7331 RVA: 0x00097EC8 File Offset: 0x000960C8
		protected void WriteNullTagEncoded(string name)
		{
			this.WriteNullTagEncoded(name, string.Empty);
		}

		/// <summary>Writes an XML element with an xsi:nil='true' attribute.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x06001CA4 RID: 7332 RVA: 0x00097ED8 File Offset: 0x000960D8
		protected void WriteNullTagEncoded(string name, string ns)
		{
			this.Writer.WriteStartElement(name, ns);
			this.Writer.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			this.Writer.WriteEndElement();
		}

		/// <summary>Writes an XML element with an xsi:nil='true' attribute.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x06001CA5 RID: 7333 RVA: 0x00097F18 File Offset: 0x00096118
		protected void WriteNullTagLiteral(string name)
		{
			this.WriteNullTagLiteral(name, string.Empty);
		}

		/// <summary>Writes an XML element with an xsi:nil='true' attribute. </summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x06001CA6 RID: 7334 RVA: 0x00097F28 File Offset: 0x00096128
		protected void WriteNullTagLiteral(string name, string ns)
		{
			this.WriteStartElement(name, ns);
			this.Writer.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			this.WriteEndElement();
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a &lt;multiRef&gt; XML element for a given object. </summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a multiRef element that is referenced by the current element.</param>
		// Token: 0x06001CA7 RID: 7335 RVA: 0x00097F60 File Offset: 0x00096160
		protected void WritePotentiallyReferencingElement(string n, string ns, object o)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, null, false, false);
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a &lt;multiRef&gt; XML element for a given object. </summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a multiRef element that referenced by the current element.</param>
		/// <param name="ambientType">The type stored in the object's type mapping (as opposed to the object's type found directly through the typeof operation).</param>
		// Token: 0x06001CA8 RID: 7336 RVA: 0x00097F70 File Offset: 0x00096170
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, ambientType, false, false);
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a &lt;multiRef&gt; XML element for a given object.</summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a multiRef element that is referenced by the current element.</param>
		/// <param name="ambientType">The type stored in the object's type mapping (as opposed to the object's type found directly through the typeof operation).</param>
		/// <param name="suppressReference">true to serialize the object directly into the XML element rather than make the element reference another element that contains the data; otherwise, false.</param>
		// Token: 0x06001CA9 RID: 7337 RVA: 0x00097F80 File Offset: 0x00096180
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType, bool suppressReference)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, ambientType, suppressReference, false);
		}

		/// <summary>Writes a SOAP message XML element that can contain a reference to a multiRef XML element for a given object.</summary>
		/// <param name="n">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized either in the current XML element or a multiRef element that referenced by the current element.</param>
		/// <param name="ambientType">The type stored in the object's type mapping (as opposed to the object's type found directly through the typeof operation).</param>
		/// <param name="suppressReference">true to serialize the object directly into the XML element rather than make the element reference another element that contains the data; otherwise, false.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the object to serialize is null; otherwise, false.</param>
		// Token: 0x06001CAA RID: 7338 RVA: 0x00097F90 File Offset: 0x00096190
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType, bool suppressReference, bool isNullable)
		{
			if (o == null)
			{
				if (isNullable)
				{
					this.WriteNullTagEncoded(n, ns);
				}
				return;
			}
			this.WriteStartElement(n, ns, true);
			this.CheckReferenceQueue();
			if (this.callbacks != null && this.callbacks.ContainsKey(o.GetType()))
			{
				XmlSerializationWriter.WriteCallbackInfo writeCallbackInfo = (XmlSerializationWriter.WriteCallbackInfo)this.callbacks[o.GetType()];
				if (o.GetType().IsEnum)
				{
					writeCallbackInfo.Callback(o);
				}
				else if (suppressReference)
				{
					this.Writer.WriteAttributeString("id", this.GetId(o, false));
					if (ambientType != o.GetType())
					{
						this.WriteXsiType(writeCallbackInfo.TypeName, writeCallbackInfo.TypeNs);
					}
					writeCallbackInfo.Callback(o);
				}
				else
				{
					if (!this.AlreadyQueued(o))
					{
						this.referencedElements.Enqueue(o);
					}
					this.Writer.WriteAttributeString("href", "#" + this.GetId(o, true));
				}
			}
			else
			{
				TypeData typeData = TypeTranslator.GetTypeData(o.GetType());
				if (typeData.SchemaType == SchemaTypes.Primitive)
				{
					this.WriteXsiType(typeData.XmlType, "http://www.w3.org/2001/XMLSchema");
					this.Writer.WriteString(XmlCustomFormatter.ToXmlString(typeData, o));
				}
				else
				{
					if (!this.IsPrimitiveArray(typeData))
					{
						throw new InvalidOperationException("Invalid type: " + o.GetType().FullName);
					}
					if (!this.AlreadyQueued(o))
					{
						this.referencedElements.Enqueue(o);
					}
					this.Writer.WriteAttributeString("href", "#" + this.GetId(o, true));
				}
			}
			this.WriteEndElement();
		}

		/// <summary>Serializes objects into SOAP-encoded multiRef XML elements in a SOAP message. </summary>
		// Token: 0x06001CAB RID: 7339 RVA: 0x00098158 File Offset: 0x00096358
		protected void WriteReferencedElements()
		{
			if (this.referencedElements == null)
			{
				return;
			}
			if (this.callbacks == null)
			{
				return;
			}
			while (this.referencedElements.Count > 0)
			{
				object obj = this.referencedElements.Dequeue();
				TypeData typeData = TypeTranslator.GetTypeData(obj.GetType());
				XmlSerializationWriter.WriteCallbackInfo writeCallbackInfo = (XmlSerializationWriter.WriteCallbackInfo)this.callbacks[obj.GetType()];
				if (writeCallbackInfo != null)
				{
					this.WriteStartElement(writeCallbackInfo.TypeName, writeCallbackInfo.TypeNs, true);
					this.Writer.WriteAttributeString("id", this.GetId(obj, false));
					if (typeData.SchemaType != SchemaTypes.Array)
					{
						this.WriteXsiType(writeCallbackInfo.TypeName, writeCallbackInfo.TypeNs);
					}
					writeCallbackInfo.Callback(obj);
					this.WriteEndElement();
				}
				else if (this.IsPrimitiveArray(typeData))
				{
					this.WriteArray(obj, typeData);
				}
			}
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0009823C File Offset: 0x0009643C
		private bool IsPrimitiveArray(TypeData td)
		{
			return td.SchemaType == SchemaTypes.Array && (td.ListItemTypeData.SchemaType == SchemaTypes.Primitive || td.ListItemType == typeof(object) || this.IsPrimitiveArray(td.ListItemTypeData));
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0009828C File Offset: 0x0009648C
		private void WriteArray(object o, TypeData td)
		{
			TypeData typeData = td;
			int num = -1;
			string text;
			do
			{
				typeData = typeData.ListItemTypeData;
				text = typeData.XmlType;
				num++;
			}
			while (typeData.SchemaType == SchemaTypes.Array);
			while (num-- > 0)
			{
				text += "[]";
			}
			this.WriteStartElement("Array", "http://schemas.xmlsoap.org/soap/encoding/", true);
			this.Writer.WriteAttributeString("id", this.GetId(o, false));
			if (td.SchemaType == SchemaTypes.Array)
			{
				Array array = (Array)o;
				int length = array.Length;
				this.Writer.WriteAttributeString("arrayType", "http://schemas.xmlsoap.org/soap/encoding/", this.GetQualifiedName(text, "http://www.w3.org/2001/XMLSchema") + "[" + length.ToString() + "]");
				for (int i = 0; i < length; i++)
				{
					this.WritePotentiallyReferencingElement("Item", string.Empty, array.GetValue(i), td.ListItemType, false, true);
				}
			}
			this.WriteEndElement();
		}

		/// <summary>Writes a SOAP message XML element that contains a reference to a multiRef element for a given object. </summary>
		/// <param name="n">The local name of the referencing element being written.</param>
		/// <param name="ns">The namespace of the referencing element being written.</param>
		/// <param name="o">The object being serialized.</param>
		// Token: 0x06001CAE RID: 7342 RVA: 0x00098390 File Offset: 0x00096590
		protected void WriteReferencingElement(string n, string ns, object o)
		{
			this.WriteReferencingElement(n, ns, o, false);
		}

		/// <summary>Writes a SOAP message XML element that contains a reference to a multiRef element for a given object.</summary>
		/// <param name="n">The local name of the referencing element being written.</param>
		/// <param name="ns">The namespace of the referencing element being written.</param>
		/// <param name="o">The object being serialized.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the object to serialize is null; otherwise, false.</param>
		// Token: 0x06001CAF RID: 7343 RVA: 0x0009839C File Offset: 0x0009659C
		protected void WriteReferencingElement(string n, string ns, object o, bool isNullable)
		{
			if (o == null)
			{
				if (isNullable)
				{
					this.WriteNullTagEncoded(n, ns);
				}
				return;
			}
			this.CheckReferenceQueue();
			if (!this.AlreadyQueued(o))
			{
				this.referencedElements.Enqueue(o);
			}
			this.Writer.WriteStartElement(n, ns);
			this.Writer.WriteAttributeString("href", "#" + this.GetId(o, true));
			this.Writer.WriteEndElement();
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x00098418 File Offset: 0x00096618
		private void CheckReferenceQueue()
		{
			if (this.referencedElements == null)
			{
				this.referencedElements = new Queue();
				this.InitCallbacks();
			}
		}

		/// <summary>Writes a SOAP 1.2 RPC result element with a specified qualified name in its body.</summary>
		/// <param name="name">The local name of the result body.</param>
		/// <param name="ns">The namespace of the result body.</param>
		// Token: 0x06001CB1 RID: 7345 RVA: 0x00098438 File Offset: 0x00096638
		[MonoTODO]
		protected void WriteRpcResult(string name, string ns)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes an object that uses custom XML formatting as an XML element. </summary>
		/// <param name="serializable">An object that implements the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> interface that uses custom XML formatting.</param>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> class object is null; otherwise, false.</param>
		// Token: 0x06001CB2 RID: 7346 RVA: 0x00098440 File Offset: 0x00096640
		protected void WriteSerializable(IXmlSerializable serializable, string name, string ns, bool isNullable)
		{
			this.WriteSerializable(serializable, name, ns, isNullable, true);
		}

		/// <summary>Instructs <see cref="T:System.Xml.XmlNode" /> to write an object that uses custom XML formatting as an XML element. </summary>
		/// <param name="serializable">An object that implements the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> interface that uses custom XML formatting.</param>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="isNullable">true to write an xsi:nil='true' attribute if the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> object is null; otherwise, false.</param>
		/// <param name="wrapped">true to ignore writing the opening element tag; otherwise, false to write the opening element tag.</param>
		// Token: 0x06001CB3 RID: 7347 RVA: 0x00098450 File Offset: 0x00096650
		protected void WriteSerializable(IXmlSerializable serializable, string name, string ns, bool isNullable, bool wrapped)
		{
			if (serializable == null)
			{
				if (isNullable && wrapped)
				{
					this.WriteNullTagLiteral(name, ns);
				}
				return;
			}
			if (wrapped)
			{
				this.Writer.WriteStartElement(name, ns);
			}
			serializable.WriteXml(this.Writer);
			if (wrapped)
			{
				this.Writer.WriteEndElement();
			}
		}

		/// <summary>Writes the XML declaration if the writer is positioned at the start of an XML document. </summary>
		// Token: 0x06001CB4 RID: 7348 RVA: 0x000984AC File Offset: 0x000966AC
		protected void WriteStartDocument()
		{
			if (this.Writer.WriteState == WriteState.Start)
			{
				this.Writer.WriteStartDocument();
			}
		}

		/// <summary>Writes an opening element tag, including any attributes. </summary>
		/// <param name="name">The local name of the XML element to write.</param>
		// Token: 0x06001CB5 RID: 7349 RVA: 0x000984CC File Offset: 0x000966CC
		protected void WriteStartElement(string name)
		{
			this.WriteStartElement(name, string.Empty, null, false);
		}

		/// <summary>Writes an opening element tag, including any attributes. </summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		// Token: 0x06001CB6 RID: 7350 RVA: 0x000984DC File Offset: 0x000966DC
		protected void WriteStartElement(string name, string ns)
		{
			this.WriteStartElement(name, ns, null, false);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="writePrefixed">true to write the element name with a prefix if none is available for the specified namespace; otherwise, false.</param>
		// Token: 0x06001CB7 RID: 7351 RVA: 0x000984E8 File Offset: 0x000966E8
		protected void WriteStartElement(string name, string ns, bool writePrefixed)
		{
			this.WriteStartElement(name, ns, null, writePrefixed);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized as an XML element.</param>
		// Token: 0x06001CB8 RID: 7352 RVA: 0x000984F4 File Offset: 0x000966F4
		protected void WriteStartElement(string name, string ns, object o)
		{
			this.WriteStartElement(name, ns, o, false);
		}

		/// <summary>Writes an opening element tag, including any attributes.</summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized as an XML element.</param>
		/// <param name="writePrefixed">true to write the element name with a prefix if none is available for the specified namespace; otherwise, false.</param>
		// Token: 0x06001CB9 RID: 7353 RVA: 0x00098500 File Offset: 0x00096700
		protected void WriteStartElement(string name, string ns, object o, bool writePrefixed)
		{
			this.WriteStartElement(name, ns, o, writePrefixed, this.namespaces);
		}

		/// <summary>Writes an opening element tag, including any attributes. </summary>
		/// <param name="name">The local name of the XML element to write.</param>
		/// <param name="ns">The namespace of the XML element to write.</param>
		/// <param name="o">The object being serialized as an XML element.</param>
		/// <param name="writePrefixed">true to write the element name with a prefix if none is available for the specified namespace; otherwise, false.</param>
		/// <param name="xmlns">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> class that contains prefix and namespace pairs to be used in the generated XML.</param>
		// Token: 0x06001CBA RID: 7354 RVA: 0x00098514 File Offset: 0x00096714
		protected void WriteStartElement(string name, string ns, object o, bool writePrefixed, XmlSerializerNamespaces xmlns)
		{
			if (xmlns == null)
			{
				throw new ArgumentNullException("xmlns");
			}
			this.WriteStartElement(name, ns, o, writePrefixed, xmlns.ToArray());
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00098548 File Offset: 0x00096748
		private void WriteStartElement(string name, string ns, object o, bool writePrefixed, ICollection namespaces)
		{
			if (o != null)
			{
				if (this.serializedObjects.Contains(o))
				{
					throw new InvalidOperationException("A circular reference was detected while serializing an object of type " + o.GetType().Name);
				}
				this.serializedObjects[o] = o;
			}
			string text = null;
			if (this.topLevelElement && ns != null && ns.Length != 0)
			{
				foreach (object obj in namespaces)
				{
					XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
					if (xmlQualifiedName.Namespace == ns)
					{
						text = xmlQualifiedName.Name;
						writePrefixed = true;
						break;
					}
				}
			}
			if (writePrefixed && ns != string.Empty)
			{
				name = XmlCustomFormatter.FromXmlName(name);
				if (text == null)
				{
					text = this.Writer.LookupPrefix(ns);
				}
				if (text == null || text.Length == 0)
				{
					text = "q" + ++this.qnameCount;
				}
				this.Writer.WriteStartElement(text, name, ns);
			}
			else
			{
				this.Writer.WriteStartElement(name, ns);
			}
			if (this.topLevelElement)
			{
				if (namespaces != null)
				{
					foreach (object obj2 in namespaces)
					{
						XmlQualifiedName xmlQualifiedName2 = (XmlQualifiedName)obj2;
						string text2 = this.Writer.LookupPrefix(xmlQualifiedName2.Namespace);
						if (text2 == null || text2.Length == 0)
						{
							this.WriteAttribute("xmlns", xmlQualifiedName2.Name, "http://www.w3.org/2000/xmlns/", xmlQualifiedName2.Namespace);
						}
					}
				}
				this.topLevelElement = false;
			}
		}

		/// <summary>Writes an XML element whose text body is a value of a simple XML Schema data type. </summary>
		/// <param name="name">The local name of the element to write.</param>
		/// <param name="ns">The namespace of the element to write.</param>
		/// <param name="o">The object to be serialized in the element body.</param>
		/// <param name="xsiType">true if the XML element explicitly specifies the text value's type using the xsi:type attribute; otherwise, false.</param>
		// Token: 0x06001CBC RID: 7356 RVA: 0x0009876C File Offset: 0x0009696C
		protected void WriteTypedPrimitive(string name, string ns, object o, bool xsiType)
		{
			TypeData typeData = TypeTranslator.GetTypeData(o.GetType());
			if (typeData.SchemaType != SchemaTypes.Primitive)
			{
				throw new InvalidOperationException(string.Format("The type of the argument object '{0}' is not primitive.", typeData.FullTypeName));
			}
			if (name == null)
			{
				ns = ((!typeData.IsXsdType) ? "http://microsoft.com/wsdl/types/" : "http://www.w3.org/2001/XMLSchema");
				name = typeData.XmlType;
			}
			else
			{
				name = XmlCustomFormatter.FromXmlName(name);
			}
			this.Writer.WriteStartElement(name, ns);
			string value;
			if (o is XmlQualifiedName)
			{
				value = this.FromXmlQualifiedName((XmlQualifiedName)o);
			}
			else
			{
				value = XmlCustomFormatter.ToXmlString(typeData, o);
			}
			if (xsiType)
			{
				if (typeData.SchemaType != SchemaTypes.Primitive)
				{
					throw new InvalidOperationException(string.Format("The type {0} was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.", o.GetType().FullName));
				}
				this.WriteXsiType(typeData.XmlType, (!typeData.IsXsdType) ? "http://microsoft.com/wsdl/types/" : "http://www.w3.org/2001/XMLSchema");
			}
			this.WriteValue(value);
			this.Writer.WriteEndElement();
		}

		/// <summary>Writes a base-64 byte array.</summary>
		/// <param name="value">The byte array to write.</param>
		// Token: 0x06001CBD RID: 7357 RVA: 0x00098878 File Offset: 0x00096A78
		protected void WriteValue(byte[] value)
		{
			this.Writer.WriteBase64(value, 0, value.Length);
		}

		/// <summary>Writes a specified string.</summary>
		/// <param name="value">The string to write.</param>
		// Token: 0x06001CBE RID: 7358 RVA: 0x0009888C File Offset: 0x00096A8C
		protected void WriteValue(string value)
		{
			if (value != null)
			{
				this.Writer.WriteString(value);
			}
		}

		/// <summary>Writes the specified <see cref="T:System.Xml.XmlNode" /> as an XML attribute.</summary>
		/// <param name="node">An <see cref="T:System.Xml.XmlAttribute" /> object.</param>
		// Token: 0x06001CBF RID: 7359 RVA: 0x000988A0 File Offset: 0x00096AA0
		protected void WriteXmlAttribute(XmlNode node)
		{
			this.WriteXmlAttribute(node, null);
		}

		/// <summary>Writes the specified <see cref="T:System.Xml.XmlNode" /> object as an XML attribute.</summary>
		/// <param name="node">An <see cref="T:System.Xml.XmlNode" /> of <see cref="T:System.Xml.XmlAttribute" /> type.</param>
		/// <param name="container">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> object (or null) used to generate a qualified name value for an arrayType attribute from the Web Services Description Language (WSDL) namespace ("http://schemas.xmlsoap.org/wsdl/").</param>
		// Token: 0x06001CC0 RID: 7360 RVA: 0x000988AC File Offset: 0x00096AAC
		protected void WriteXmlAttribute(XmlNode node, object container)
		{
			XmlAttribute xmlAttribute = node as XmlAttribute;
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException("The node must be either type XmlAttribute or a derived type.");
			}
			if (xmlAttribute.NamespaceURI == "http://schemas.xmlsoap.org/wsdl/" && xmlAttribute.LocalName == "arrayType")
			{
				string str;
				string ns;
				string str2;
				TypeTranslator.ParseArrayType(xmlAttribute.Value, out str, out ns, out str2);
				string qualifiedName = this.GetQualifiedName(str + str2, ns);
				this.WriteAttribute(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, qualifiedName);
				return;
			}
			this.WriteAttribute(xmlAttribute.Prefix, xmlAttribute.LocalName, xmlAttribute.NamespaceURI, xmlAttribute.Value);
		}

		/// <summary>Writes an xsi:type attribute for an XML element that is being serialized into a document. </summary>
		/// <param name="name">The local name of an XML Schema data type.</param>
		/// <param name="ns">The namespace of an XML Schema data type.</param>
		// Token: 0x06001CC1 RID: 7361 RVA: 0x00098958 File Offset: 0x00096B58
		protected void WriteXsiType(string name, string ns)
		{
			if (ns != null && ns != string.Empty)
			{
				this.WriteAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", this.GetQualifiedName(name, ns));
			}
			else
			{
				this.WriteAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", name);
			}
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> has been invalidly applied to a member; only members that are of type <see cref="T:System.Xml.XmlNode" />, or derived from <see cref="T:System.Xml.XmlNode" />, are valid.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="o">The object that represents the invalid member.</param>
		// Token: 0x06001CC2 RID: 7362 RVA: 0x000989AC File Offset: 0x00096BAC
		protected Exception CreateInvalidAnyTypeException(object o)
		{
			if (o == null)
			{
				return new InvalidOperationException("null is invalid as anyType in XmlSerializer");
			}
			return this.CreateInvalidAnyTypeException(o.GetType());
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> has been invalidly applied to a member; only members that are of type <see cref="T:System.Xml.XmlNode" />, or derived from <see cref="T:System.Xml.XmlNode" />, are valid.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> that is invalid.</param>
		// Token: 0x06001CC3 RID: 7363 RVA: 0x000989CC File Offset: 0x00096BCC
		protected Exception CreateInvalidAnyTypeException(Type t)
		{
			return new InvalidOperationException(string.Format("An object of type '{0}' is invalid as anyType in XmlSerializer", t));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> for an invalid enumeration value.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.InvalidEnumArgumentException" />.</returns>
		/// <param name="value">An object that represents the invalid enumeration.</param>
		/// <param name="typeName">The XML type name.</param>
		// Token: 0x06001CC4 RID: 7364 RVA: 0x000989E0 File Offset: 0x00096BE0
		protected Exception CreateInvalidEnumValueException(object value, string typeName)
		{
			return new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "'{0}' is not a valid value for {1}.", new object[]
			{
				value,
				typeName
			}));
		}

		/// <summary>Takes a numeric enumeration value and the names and constants from the enumerator list for the enumeration and returns a string that consists of delimited identifiers that represent the enumeration members that have been set.</summary>
		/// <returns>A string that consists of delimited identifiers, where each item is one of the values set by the bitwise operation.</returns>
		/// <param name="value">The enumeration value as a series of bitwise OR operations.</param>
		/// <param name="values">The values of the enumeration.</param>
		/// <param name="ids">The constants of the enumeration.</param>
		/// <param name="typeName">The name of the type </param>
		// Token: 0x06001CC5 RID: 7365 RVA: 0x00098A10 File Offset: 0x00096C10
		protected static string FromEnum(long value, string[] values, long[] ids, string typeName)
		{
			return XmlCustomFormatter.FromEnum(value, values, ids, typeName);
		}

		/// <summary>Produces a string that can be written as an XML qualified name, with invalid characters replaced by escape sequences. </summary>
		/// <returns>An XML qualified name, with invalid characters replaced by escape sequences.</returns>
		/// <param name="xmlQualifiedName">An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the XML to be written.</param>
		/// <param name="ignoreEmpty">true to ignore empty spaces in the string; otherwise, false.</param>
		// Token: 0x06001CC6 RID: 7366 RVA: 0x00098A1C File Offset: 0x00096C1C
		[MonoTODO]
		protected string FromXmlQualifiedName(XmlQualifiedName xmlQualifiedName, bool ignoreEmpty)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a dynamically generated assembly by name.</summary>
		/// <returns>A dynamically generated assembly.</returns>
		/// <param name="assemblyFullName">The full name of the assembly.</param>
		// Token: 0x06001CC7 RID: 7367 RVA: 0x00098A24 File Offset: 0x00096C24
		[MonoTODO]
		protected static Assembly ResolveDynamicAssembly(string assemblyFullName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="M:System.Xml.XmlConvert.EncodeName(System.String)" /> method is used to write valid XML.</summary>
		/// <returns>true if the <see cref="M:System.Xml.Serialization.XmlSerializationWriter.FromXmlQualifiedName(System.Xml.XmlQualifiedName)" /> method returns an encoded name; otherwise, false.</returns>
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00098A2C File Offset: 0x00096C2C
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x00098A34 File Offset: 0x00096C34
		[MonoTODO]
		protected bool EscapeName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000B6E RID: 2926
		private const string xmlNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000B6F RID: 2927
		private const string unexpectedTypeError = "The type {0} was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.";

		// Token: 0x04000B70 RID: 2928
		private ObjectIDGenerator idGenerator;

		// Token: 0x04000B71 RID: 2929
		private int qnameCount;

		// Token: 0x04000B72 RID: 2930
		private bool topLevelElement;

		// Token: 0x04000B73 RID: 2931
		private ArrayList namespaces;

		// Token: 0x04000B74 RID: 2932
		private XmlWriter writer;

		// Token: 0x04000B75 RID: 2933
		private Queue referencedElements;

		// Token: 0x04000B76 RID: 2934
		private Hashtable callbacks;

		// Token: 0x04000B77 RID: 2935
		private Hashtable serializedObjects;

		// Token: 0x020002AE RID: 686
		private class WriteCallbackInfo
		{
			// Token: 0x04000B78 RID: 2936
			public Type Type;

			// Token: 0x04000B79 RID: 2937
			public string TypeName;

			// Token: 0x04000B7A RID: 2938
			public string TypeNs;

			// Token: 0x04000B7B RID: 2939
			public XmlSerializationWriteCallback Callback;
		}
	}
}
