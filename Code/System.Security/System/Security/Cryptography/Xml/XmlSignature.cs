using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000065 RID: 101
	internal class XmlSignature
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0000E868 File Offset: 0x0000CA68
		public static XmlElement GetChildElement(XmlElement xel, string element, string ns)
		{
			for (int i = 0; i < xel.ChildNodes.Count; i++)
			{
				XmlNode xmlNode = xel.ChildNodes[i];
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.LocalName == element && xmlNode.NamespaceURI == ns)
				{
					return xmlNode as XmlElement;
				}
			}
			return null;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000E8D4 File Offset: 0x0000CAD4
		public static string GetAttributeFromElement(XmlElement xel, string attribute, string element)
		{
			XmlElement childElement = XmlSignature.GetChildElement(xel, element, "http://www.w3.org/2000/09/xmldsig#");
			return (childElement == null) ? null : childElement.GetAttribute(attribute);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000E904 File Offset: 0x0000CB04
		public static XmlElement[] GetChildElements(XmlElement xel, string element)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < xel.ChildNodes.Count; i++)
			{
				XmlNode xmlNode = xel.ChildNodes[i];
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.LocalName == element && xmlNode.NamespaceURI == "http://www.w3.org/2000/09/xmldsig#")
				{
					arrayList.Add(xmlNode);
				}
			}
			return arrayList.ToArray(typeof(XmlElement)) as XmlElement[];
		}

		// Token: 0x04000170 RID: 368
		public const string NamespaceURI = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x04000171 RID: 369
		public const string Prefix = "ds";

		// Token: 0x02000066 RID: 102
		public class ElementNames
		{
			// Token: 0x04000172 RID: 370
			public const string CanonicalizationMethod = "CanonicalizationMethod";

			// Token: 0x04000173 RID: 371
			public const string DigestMethod = "DigestMethod";

			// Token: 0x04000174 RID: 372
			public const string DigestValue = "DigestValue";

			// Token: 0x04000175 RID: 373
			public const string DSAKeyValue = "DSAKeyValue";

			// Token: 0x04000176 RID: 374
			public const string EncryptedKey = "EncryptedKey";

			// Token: 0x04000177 RID: 375
			public const string HMACOutputLength = "HMACOutputLength";

			// Token: 0x04000178 RID: 376
			public const string KeyInfo = "KeyInfo";

			// Token: 0x04000179 RID: 377
			public const string KeyName = "KeyName";

			// Token: 0x0400017A RID: 378
			public const string KeyValue = "KeyValue";

			// Token: 0x0400017B RID: 379
			public const string Manifest = "Manifest";

			// Token: 0x0400017C RID: 380
			public const string Object = "Object";

			// Token: 0x0400017D RID: 381
			public const string Reference = "Reference";

			// Token: 0x0400017E RID: 382
			public const string RetrievalMethod = "RetrievalMethod";

			// Token: 0x0400017F RID: 383
			public const string RSAKeyValue = "RSAKeyValue";

			// Token: 0x04000180 RID: 384
			public const string Signature = "Signature";

			// Token: 0x04000181 RID: 385
			public const string SignatureMethod = "SignatureMethod";

			// Token: 0x04000182 RID: 386
			public const string SignatureValue = "SignatureValue";

			// Token: 0x04000183 RID: 387
			public const string SignedInfo = "SignedInfo";

			// Token: 0x04000184 RID: 388
			public const string Transform = "Transform";

			// Token: 0x04000185 RID: 389
			public const string Transforms = "Transforms";

			// Token: 0x04000186 RID: 390
			public const string X509Data = "X509Data";

			// Token: 0x04000187 RID: 391
			public const string X509IssuerSerial = "X509IssuerSerial";

			// Token: 0x04000188 RID: 392
			public const string X509IssuerName = "X509IssuerName";

			// Token: 0x04000189 RID: 393
			public const string X509SerialNumber = "X509SerialNumber";

			// Token: 0x0400018A RID: 394
			public const string X509SKI = "X509SKI";

			// Token: 0x0400018B RID: 395
			public const string X509SubjectName = "X509SubjectName";

			// Token: 0x0400018C RID: 396
			public const string X509Certificate = "X509Certificate";

			// Token: 0x0400018D RID: 397
			public const string X509CRL = "X509CRL";
		}

		// Token: 0x02000067 RID: 103
		public class AttributeNames
		{
			// Token: 0x0400018E RID: 398
			public const string Algorithm = "Algorithm";

			// Token: 0x0400018F RID: 399
			public const string Encoding = "Encoding";

			// Token: 0x04000190 RID: 400
			public const string Id = "Id";

			// Token: 0x04000191 RID: 401
			public const string MimeType = "MimeType";

			// Token: 0x04000192 RID: 402
			public const string Type = "Type";

			// Token: 0x04000193 RID: 403
			public const string URI = "URI";
		}

		// Token: 0x02000068 RID: 104
		public class AlgorithmNamespaces
		{
			// Token: 0x04000194 RID: 404
			public const string XmlDsigBase64Transform = "http://www.w3.org/2000/09/xmldsig#base64";

			// Token: 0x04000195 RID: 405
			public const string XmlDsigC14NTransform = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

			// Token: 0x04000196 RID: 406
			public const string XmlDsigC14NWithCommentsTransform = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

			// Token: 0x04000197 RID: 407
			public const string XmlDsigEnvelopedSignatureTransform = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

			// Token: 0x04000198 RID: 408
			public const string XmlDsigXPathTransform = "http://www.w3.org/TR/1999/REC-xpath-19991116";

			// Token: 0x04000199 RID: 409
			public const string XmlDsigXsltTransform = "http://www.w3.org/TR/1999/REC-xslt-19991116";

			// Token: 0x0400019A RID: 410
			public const string XmlDsigExcC14NTransform = "http://www.w3.org/2001/10/xml-exc-c14n#";

			// Token: 0x0400019B RID: 411
			public const string XmlDsigExcC14NWithCommentsTransform = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

			// Token: 0x0400019C RID: 412
			public const string XmlDecryptionTransform = "http://www.w3.org/2002/07/decrypt#XML";

			// Token: 0x0400019D RID: 413
			public const string XmlLicenseTransform = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";
		}

		// Token: 0x02000069 RID: 105
		public class Uri
		{
			// Token: 0x0400019E RID: 414
			public const string Manifest = "http://www.w3.org/2000/09/xmldsig#Manifest";
		}
	}
}
