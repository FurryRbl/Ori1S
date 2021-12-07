using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Contains information about the canonicalization algorithm and signature algorithm used for the XML signature.</summary>
	// Token: 0x0200004F RID: 79
	public class SignedInfo : IEnumerable, ICollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> class.</summary>
		// Token: 0x06000259 RID: 601 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		public SignedInfo()
		{
			this.references = new ArrayList();
			this.c14nMethod = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
		}

		/// <summary>Gets or sets the canonicalization algorithm that is used before signing for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The canonicalization algorithm used before signing for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000AD10 File Offset: 0x00008F10
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000AD18 File Offset: 0x00008F18
		public string CanonicalizationMethod
		{
			get
			{
				return this.c14nMethod;
			}
			set
			{
				this.c14nMethod = value;
				this.element = null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object used for canonicalization.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.Transform" /> object used for canonicalization.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <see cref="T:System.Security.Cryptography.Xml.Transform" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000AD28 File Offset: 0x00008F28
		[ComVisible(false)]
		[MonoTODO]
		public Transform CanonicalizationMethodObject
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of references in the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The number of references in the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000AD30 File Offset: 0x00008F30
		public int Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000AD38 File Offset: 0x00008F38
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000AD40 File Offset: 0x00008F40
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.element = null;
				this.id = value;
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000AD50 File Offset: 0x00008F50
		public bool IsReadOnly
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the collection is synchronized.</summary>
		/// <returns>true if the collection is synchronized; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000AD58 File Offset: 0x00008F58
		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a list of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> objects of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>A list of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> elements of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000AD60 File Offset: 0x00008F60
		public ArrayList References
		{
			get
			{
				return this.references;
			}
		}

		/// <summary>Gets or sets the length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000AD68 File Offset: 0x00008F68
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000AD70 File Offset: 0x00008F70
		public string SignatureLength
		{
			get
			{
				return this.signatureLength;
			}
			set
			{
				this.element = null;
				this.signatureLength = value;
			}
		}

		/// <summary>Gets or sets the name of the algorithm used for signature generation and validation for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The name of the algorithm used for signature generation and validation for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000AD80 File Offset: 0x00008F80
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000AD88 File Offset: 0x00008F88
		public string SignatureMethod
		{
			get
			{
				return this.signatureMethod;
			}
			set
			{
				this.element = null;
				this.signatureMethod = value;
			}
		}

		/// <summary>Gets an object to use for synchronization.</summary>
		/// <returns>An object to use for synchronization.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000AD98 File Offset: 0x00008F98
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Reference" /> object to the list of references to digest and sign.</summary>
		/// <param name="reference">The reference to add to the list of references. </param>
		/// <exception cref="T:System.ArgumentNullException">The reference parameter is null.</exception>
		// Token: 0x06000268 RID: 616 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		public void AddReference(Reference reference)
		{
			this.references.Add(reference);
		}

		/// <summary>Copies the elements of this instance into an <see cref="T:System.Array" /> object, starting at a specified index in the array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object that holds the collection's elements. </param>
		/// <param name="index">The beginning index in the array where the elements are copied. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06000269 RID: 617 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		public void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an enumerator that iterates through the collection of references.</summary>
		/// <returns>An enumerator that iterates through the collection of references.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x0600026A RID: 618 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		public IEnumerator GetEnumerator()
		{
			return this.references.GetEnumerator();
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> instance.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedInfo.SignatureMethod" /> property is null.-or- The <see cref="P:System.Security.Cryptography.Xml.SignedInfo.References" /> property is empty. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600026B RID: 619 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		public XmlElement GetXml()
		{
			if (this.element != null)
			{
				return this.element;
			}
			if (this.signatureMethod == null)
			{
				throw new CryptographicException("SignatureMethod");
			}
			if (this.references.Count == 0)
			{
				throw new CryptographicException("References empty");
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("SignedInfo", "http://www.w3.org/2000/09/xmldsig#");
			if (this.id != null)
			{
				xmlElement.SetAttribute("Id", this.id);
			}
			if (this.c14nMethod != null)
			{
				XmlElement xmlElement2 = xmlDocument.CreateElement("CanonicalizationMethod", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement2.SetAttribute("Algorithm", this.c14nMethod);
				xmlElement.AppendChild(xmlElement2);
			}
			if (this.signatureMethod != null)
			{
				XmlElement xmlElement3 = xmlDocument.CreateElement("SignatureMethod", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement3.SetAttribute("Algorithm", this.signatureMethod);
				if (this.signatureLength != null)
				{
					XmlElement xmlElement4 = xmlDocument.CreateElement("HMACOutputLength", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement4.InnerText = this.signatureLength;
					xmlElement3.AppendChild(xmlElement4);
				}
				xmlElement.AppendChild(xmlElement3);
			}
			if (this.references.Count == 0)
			{
				throw new CryptographicException("At least one Reference element is required in SignedInfo.");
			}
			foreach (object obj in this.references)
			{
				Reference reference = (Reference)obj;
				XmlNode xml = reference.GetXml();
				XmlNode newChild = xmlDocument.ImportNode(xml, true);
				xmlElement.AppendChild(newChild);
			}
			return xmlElement;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000AF84 File Offset: 0x00009184
		private string GetAttribute(XmlElement xel, string attribute)
		{
			XmlAttribute xmlAttribute = xel.Attributes[attribute];
			return (xmlAttribute == null) ? null : xmlAttribute.InnerText;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> state. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> element.-or- The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedInfo.CanonicalizationMethod" /> property.-or- The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedInfo.SignatureMethod" /> property.</exception>
		// Token: 0x0600026D RID: 621 RVA: 0x0000AFB0 File Offset: 0x000091B0
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "SignedInfo" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException();
			}
			this.id = this.GetAttribute(value, "Id");
			this.c14nMethod = XmlSignature.GetAttributeFromElement(value, "Algorithm", "CanonicalizationMethod");
			XmlElement childElement = XmlSignature.GetChildElement(value, "SignatureMethod", "http://www.w3.org/2000/09/xmldsig#");
			if (childElement != null)
			{
				this.signatureMethod = childElement.GetAttribute("Algorithm");
				XmlElement childElement2 = XmlSignature.GetChildElement(childElement, "HMACOutputLength", "http://www.w3.org/2000/09/xmldsig#");
				if (childElement2 != null)
				{
					this.signatureLength = childElement2.InnerText;
				}
			}
			for (int i = 0; i < value.ChildNodes.Count; i++)
			{
				XmlNode xmlNode = value.ChildNodes[i];
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.LocalName == "Reference" && xmlNode.NamespaceURI == "http://www.w3.org/2000/09/xmldsig#")
				{
					Reference reference = new Reference();
					reference.LoadXml((XmlElement)xmlNode);
					this.AddReference(reference);
				}
			}
			this.element = value;
		}

		// Token: 0x04000108 RID: 264
		private ArrayList references;

		// Token: 0x04000109 RID: 265
		private string c14nMethod;

		// Token: 0x0400010A RID: 266
		private string id;

		// Token: 0x0400010B RID: 267
		private string signatureMethod;

		// Token: 0x0400010C RID: 268
		private string signatureLength;

		// Token: 0x0400010D RID: 269
		private XmlElement element;
	}
}
