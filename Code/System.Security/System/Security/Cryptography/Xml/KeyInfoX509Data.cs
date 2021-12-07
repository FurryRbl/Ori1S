using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Mono.Security.X509;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents an &lt;X509Data&gt; subelement of an XMLDSIG or XML Encryption &lt;KeyInfo&gt; element.</summary>
	// Token: 0x02000048 RID: 72
	public class KeyInfoX509Data : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class.</summary>
		// Token: 0x060001FC RID: 508 RVA: 0x00009704 File Offset: 0x00007904
		public KeyInfoX509Data()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified ASN.1 DER encoding of an X.509v3 certificate.</summary>
		/// <param name="rgbCert">The ASN.1 DER encoding of an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		// Token: 0x060001FD RID: 509 RVA: 0x0000970C File Offset: 0x0000790C
		public KeyInfoX509Data(byte[] rgbCert)
		{
			this.AddCertificate(new System.Security.Cryptography.X509Certificates.X509Certificate(rgbCert));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified X.509v3 certificate.</summary>
		/// <param name="cert">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is null.</exception>
		// Token: 0x060001FE RID: 510 RVA: 0x00009720 File Offset: 0x00007920
		public KeyInfoX509Data(System.Security.Cryptography.X509Certificates.X509Certificate cert)
		{
			this.AddCertificate(cert);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified X.509v3 certificate.</summary>
		/// <param name="cert">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		/// <param name="includeOption">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> values that specifies how much of the certificate chain to include.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate has only a partial certificate chain.</exception>
		// Token: 0x060001FF RID: 511 RVA: 0x00009730 File Offset: 0x00007930
		public KeyInfoX509Data(System.Security.Cryptography.X509Certificates.X509Certificate cert, X509IncludeOption includeOption)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			switch (includeOption)
			{
			case X509IncludeOption.None:
			case X509IncludeOption.EndCertOnly:
				this.AddCertificate(cert);
				break;
			case X509IncludeOption.ExcludeRoot:
				this.AddCertificatesChainFrom(cert, false);
				break;
			case X509IncludeOption.WholeChain:
				this.AddCertificatesChainFrom(cert, true);
				break;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009798 File Offset: 0x00007998
		private void AddCertificatesChainFrom(System.Security.Cryptography.X509Certificates.X509Certificate cert, bool root)
		{
			System.Security.Cryptography.X509Certificates.X509Chain x509Chain = new System.Security.Cryptography.X509Certificates.X509Chain();
			x509Chain.Build(new X509Certificate2(cert));
			foreach (X509ChainElement x509ChainElement in x509Chain.ChainElements)
			{
				byte[] array = x509ChainElement.Certificate.RawData;
				if (!root)
				{
					Mono.Security.X509.X509Certificate x509Certificate = new Mono.Security.X509.X509Certificate(array);
					if (x509Certificate.IsSelfSigned)
					{
						array = null;
					}
				}
				if (array != null)
				{
					this.AddCertificate(new System.Security.Cryptography.X509Certificates.X509Certificate(array));
				}
			}
		}

		/// <summary>Gets a list of the X.509v3 certificates contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the X.509 certificates contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00009818 File Offset: 0x00007A18
		public ArrayList Certificates
		{
			get
			{
				return this.X509CertificateList;
			}
		}

		/// <summary>Gets or sets the Certificate Revocation List (CRL) contained within the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>The Certificate Revocation List (CRL) contained within the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00009820 File Offset: 0x00007A20
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00009828 File Offset: 0x00007A28
		public byte[] CRL
		{
			get
			{
				return this.x509crl;
			}
			set
			{
				this.x509crl = value;
			}
		}

		/// <summary>Gets a list of <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> structures that represent an issuer name and serial number pair.</summary>
		/// <returns>A list of <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> structures that represent an issuer name and serial number pair.</returns>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00009834 File Offset: 0x00007A34
		public ArrayList IssuerSerials
		{
			get
			{
				return this.IssuerSerialList;
			}
		}

		/// <summary>Gets a list of the subject key identifiers (SKIs) contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the subject key identifiers (SKIs) contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000983C File Offset: 0x00007A3C
		public ArrayList SubjectKeyIds
		{
			get
			{
				return this.SubjectKeyIdList;
			}
		}

		/// <summary>Gets a list of the subject names of the entities contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the subject names of the entities contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009844 File Offset: 0x00007A44
		public ArrayList SubjectNames
		{
			get
			{
				return this.SubjectNameList;
			}
		}

		/// <summary>Adds the specified X.509v3 certificate to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" />.</summary>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is null.</exception>
		// Token: 0x06000207 RID: 519 RVA: 0x0000984C File Offset: 0x00007A4C
		public void AddCertificate(System.Security.Cryptography.X509Certificates.X509Certificate certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (this.X509CertificateList == null)
			{
				this.X509CertificateList = new ArrayList();
			}
			this.X509CertificateList.Add(certificate);
		}

		/// <summary>Adds the specified issuer name and serial number pair to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="issuerName">The issuer name portion of the pair to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		/// <param name="serialNumber">The serial number portion of the pair to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		// Token: 0x06000208 RID: 520 RVA: 0x00009890 File Offset: 0x00007A90
		public void AddIssuerSerial(string issuerName, string serialNumber)
		{
			if (issuerName == null)
			{
				throw new ArgumentException("issuerName");
			}
			if (this.IssuerSerialList == null)
			{
				this.IssuerSerialList = new ArrayList();
			}
			X509IssuerSerial x509IssuerSerial = new X509IssuerSerial(issuerName, serialNumber);
			this.IssuerSerialList.Add(x509IssuerSerial);
		}

		/// <summary>Adds the specified subject key identifier (SKI) byte array to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectKeyId">A byte array that represents the subject key identifier (SKI) to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		// Token: 0x06000209 RID: 521 RVA: 0x000098E0 File Offset: 0x00007AE0
		public void AddSubjectKeyId(byte[] subjectKeyId)
		{
			if (this.SubjectKeyIdList == null)
			{
				this.SubjectKeyIdList = new ArrayList();
			}
			this.SubjectKeyIdList.Add(subjectKeyId);
		}

		/// <summary>Adds the specified subject key identifier (SKI) string to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectKeyId">A string that represents the subject key identifier (SKI) to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		// Token: 0x0600020A RID: 522 RVA: 0x00009908 File Offset: 0x00007B08
		[ComVisible(false)]
		public void AddSubjectKeyId(string subjectKeyId)
		{
			if (this.SubjectKeyIdList == null)
			{
				this.SubjectKeyIdList = new ArrayList();
			}
			byte[] value = null;
			if (subjectKeyId != null)
			{
				value = Convert.FromBase64String(subjectKeyId);
			}
			this.SubjectKeyIdList.Add(value);
		}

		/// <summary>Adds the subject name of the entity that was issued an X.509v3 certificate to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectName">The name of the entity that was issued an X.509 certificate to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		// Token: 0x0600020B RID: 523 RVA: 0x00009948 File Offset: 0x00007B48
		public void AddSubjectName(string subjectName)
		{
			if (this.SubjectNameList == null)
			{
				this.SubjectNameList = new ArrayList();
			}
			this.SubjectNameList.Add(subjectName);
		}

		/// <summary>Returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600020C RID: 524 RVA: 0x00009970 File Offset: 0x00007B70
		public override XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("X509Data", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.SetAttribute("xmlns", "http://www.w3.org/2000/09/xmldsig#");
			if (this.IssuerSerialList != null && this.IssuerSerialList.Count > 0)
			{
				foreach (object obj in this.IssuerSerialList)
				{
					X509IssuerSerial x509IssuerSerial = (X509IssuerSerial)obj;
					XmlElement xmlElement2 = xmlDocument.CreateElement("X509IssuerSerial", "http://www.w3.org/2000/09/xmldsig#");
					XmlElement xmlElement3 = xmlDocument.CreateElement("X509IssuerName", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement3.InnerText = x509IssuerSerial.IssuerName;
					xmlElement2.AppendChild(xmlElement3);
					XmlElement xmlElement4 = xmlDocument.CreateElement("X509SerialNumber", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement4.InnerText = x509IssuerSerial.SerialNumber;
					xmlElement2.AppendChild(xmlElement4);
					xmlElement.AppendChild(xmlElement2);
				}
			}
			if (this.SubjectKeyIdList != null && this.SubjectKeyIdList.Count > 0)
			{
				foreach (object obj2 in this.SubjectKeyIdList)
				{
					byte[] inArray = (byte[])obj2;
					XmlElement xmlElement5 = xmlDocument.CreateElement("X509SKI", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement5.InnerText = Convert.ToBase64String(inArray);
					xmlElement.AppendChild(xmlElement5);
				}
			}
			if (this.SubjectNameList != null && this.SubjectNameList.Count > 0)
			{
				foreach (object obj3 in this.SubjectNameList)
				{
					string innerText = (string)obj3;
					XmlElement xmlElement6 = xmlDocument.CreateElement("X509SubjectName", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement6.InnerText = innerText;
					xmlElement.AppendChild(xmlElement6);
				}
			}
			if (this.X509CertificateList != null && this.X509CertificateList.Count > 0)
			{
				foreach (object obj4 in this.X509CertificateList)
				{
					System.Security.Cryptography.X509Certificates.X509Certificate x509Certificate = (System.Security.Cryptography.X509Certificates.X509Certificate)obj4;
					XmlElement xmlElement7 = xmlDocument.CreateElement("X509Certificate", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement7.InnerText = Convert.ToBase64String(x509Certificate.GetRawCertData());
					xmlElement.AppendChild(xmlElement7);
				}
			}
			if (this.x509crl != null)
			{
				XmlElement xmlElement8 = xmlDocument.CreateElement("X509CRL", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement8.InnerText = Convert.ToBase64String(this.x509crl);
				xmlElement.AppendChild(xmlElement8);
			}
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object to match.</summary>
		/// <param name="element">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="element" /> parameter does not contain an &lt;X509IssuerName&gt; node.-or-The <paramref name="element" /> parameter does not contain an &lt;X509SerialNumber&gt; node.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x0600020D RID: 525 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public override void LoadXml(XmlElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (this.IssuerSerialList != null)
			{
				this.IssuerSerialList.Clear();
			}
			if (this.SubjectKeyIdList != null)
			{
				this.SubjectKeyIdList.Clear();
			}
			if (this.SubjectNameList != null)
			{
				this.SubjectNameList.Clear();
			}
			if (this.X509CertificateList != null)
			{
				this.X509CertificateList.Clear();
			}
			this.x509crl = null;
			if (element.LocalName != "X509Data" || element.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException("element");
			}
			XmlElement[] childElements = XmlSignature.GetChildElements(element, "X509IssuerSerial");
			if (childElements != null)
			{
				foreach (XmlElement xel in childElements)
				{
					XmlElement childElement = XmlSignature.GetChildElement(xel, "X509IssuerName", "http://www.w3.org/2000/09/xmldsig#");
					XmlElement childElement2 = XmlSignature.GetChildElement(xel, "X509SerialNumber", "http://www.w3.org/2000/09/xmldsig#");
					this.AddIssuerSerial(childElement.InnerText, childElement2.InnerText);
				}
			}
			childElements = XmlSignature.GetChildElements(element, "X509SKI");
			if (childElements != null)
			{
				for (int j = 0; j < childElements.Length; j++)
				{
					byte[] subjectKeyId = Convert.FromBase64String(childElements[j].InnerXml);
					this.AddSubjectKeyId(subjectKeyId);
				}
			}
			childElements = XmlSignature.GetChildElements(element, "X509SubjectName");
			if (childElements != null)
			{
				for (int k = 0; k < childElements.Length; k++)
				{
					this.AddSubjectName(childElements[k].InnerXml);
				}
			}
			childElements = XmlSignature.GetChildElements(element, "X509Certificate");
			if (childElements != null)
			{
				for (int l = 0; l < childElements.Length; l++)
				{
					byte[] data = Convert.FromBase64String(childElements[l].InnerXml);
					this.AddCertificate(new System.Security.Cryptography.X509Certificates.X509Certificate(data));
				}
			}
			XmlElement childElement3 = XmlSignature.GetChildElement(element, "X509CRL", "http://www.w3.org/2000/09/xmldsig#");
			if (childElement3 != null)
			{
				this.x509crl = Convert.FromBase64String(childElement3.InnerXml);
			}
		}

		// Token: 0x040000EF RID: 239
		private byte[] x509crl;

		// Token: 0x040000F0 RID: 240
		private ArrayList IssuerSerialList;

		// Token: 0x040000F1 RID: 241
		private ArrayList SubjectKeyIdList;

		// Token: 0x040000F2 RID: 242
		private ArrayList SubjectNameList;

		// Token: 0x040000F3 RID: 243
		private ArrayList X509CertificateList;
	}
}
