using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents an XML digital signature or XML encryption &lt;KeyInfo&gt; element.</summary>
	// Token: 0x02000043 RID: 67
	public class KeyInfo : IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> class.</summary>
		// Token: 0x060001D7 RID: 471 RVA: 0x00008FE0 File Offset: 0x000071E0
		public KeyInfo()
		{
			this.Info = new ArrayList();
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects contained within the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects contained within the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008FF4 File Offset: 0x000071F4
		public int Count
		{
			get
			{
				return this.Info.Count;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> ID.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> ID.</returns>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00009004 File Offset: 0x00007204
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000900C File Offset: 0x0000720C
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> that represents a particular type of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> information to the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <param name="clause">The <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object. </param>
		// Token: 0x060001DB RID: 475 RVA: 0x00009018 File Offset: 0x00007218
		public void AddClause(KeyInfoClause clause)
		{
			this.Info.Add(clause);
		}

		/// <summary>Returns an enumerator of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060001DC RID: 476 RVA: 0x00009028 File Offset: 0x00007228
		public IEnumerator GetEnumerator()
		{
			return this.Info.GetEnumerator();
		}

		/// <summary>Returns an enumerator of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects of the specified type in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> that can be used to iterate through the collection.</returns>
		/// <param name="requestedObjectType">The type of object to enumerate. </param>
		// Token: 0x060001DD RID: 477 RVA: 0x00009038 File Offset: 0x00007238
		public IEnumerator GetEnumerator(Type requestedObjectType)
		{
			ArrayList arrayList = new ArrayList();
			IEnumerator enumerator = this.Info.GetEnumerator();
			do
			{
				if (enumerator.Current.GetType().Equals(requestedObjectType))
				{
					arrayList.Add(enumerator.Current);
				}
			}
			while (enumerator.MoveNext());
			return arrayList.GetEnumerator();
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001DE RID: 478 RVA: 0x00009098 File Offset: 0x00007298
		public XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#");
			foreach (object obj in this.Info)
			{
				KeyInfoClause keyInfoClause = (KeyInfoClause)obj;
				XmlNode xml = keyInfoClause.GetXml();
				XmlNode newChild = xmlDocument.ImportNode(xml, true);
				xmlElement.AppendChild(newChild);
			}
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> state. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060001DF RID: 479 RVA: 0x00009138 File Offset: 0x00007338
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Id = ((value.Attributes["Id"] == null) ? null : value.GetAttribute("Id"));
			if (value.LocalName == "KeyInfo" && value.NamespaceURI == "http://www.w3.org/2000/09/xmldsig#")
			{
				foreach (object obj in value.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						KeyInfoClause keyInfoClause = null;
						string localName = xmlNode.LocalName;
						if (localName == null)
						{
							goto IL_255;
						}
						if (KeyInfo.<>f__switch$mapC == null)
						{
							KeyInfo.<>f__switch$mapC = new Dictionary<string, int>(6)
							{
								{
									"KeyValue",
									0
								},
								{
									"KeyName",
									1
								},
								{
									"RetrievalMethod",
									2
								},
								{
									"X509Data",
									3
								},
								{
									"RSAKeyValue",
									4
								},
								{
									"EncryptedKey",
									5
								}
							};
						}
						int num;
						if (!KeyInfo.<>f__switch$mapC.TryGetValue(localName, out num))
						{
							goto IL_255;
						}
						switch (num)
						{
						case 0:
						{
							XmlNodeList childNodes = xmlNode.ChildNodes;
							if (childNodes.Count > 0)
							{
								foreach (object obj2 in childNodes)
								{
									XmlNode xmlNode2 = (XmlNode)obj2;
									string localName2 = xmlNode2.LocalName;
									if (localName2 != null)
									{
										if (KeyInfo.<>f__switch$mapB == null)
										{
											KeyInfo.<>f__switch$mapB = new Dictionary<string, int>(2)
											{
												{
													"DSAKeyValue",
													0
												},
												{
													"RSAKeyValue",
													1
												}
											};
										}
										int num2;
										if (KeyInfo.<>f__switch$mapB.TryGetValue(localName2, out num2))
										{
											if (num2 != 0)
											{
												if (num2 == 1)
												{
													keyInfoClause = new RSAKeyValue();
												}
											}
											else
											{
												keyInfoClause = new DSAKeyValue();
											}
										}
									}
								}
							}
							break;
						}
						case 1:
							keyInfoClause = new KeyInfoName();
							break;
						case 2:
							keyInfoClause = new KeyInfoRetrievalMethod();
							break;
						case 3:
							keyInfoClause = new KeyInfoX509Data();
							break;
						case 4:
							keyInfoClause = new RSAKeyValue();
							break;
						case 5:
							keyInfoClause = new KeyInfoEncryptedKey();
							break;
						default:
							goto IL_255;
						}
						IL_260:
						if (keyInfoClause != null)
						{
							keyInfoClause.LoadXml((XmlElement)xmlNode);
							this.AddClause(keyInfoClause);
							continue;
						}
						continue;
						IL_255:
						keyInfoClause = new KeyInfoNode();
						goto IL_260;
					}
				}
			}
		}

		// Token: 0x040000E5 RID: 229
		private ArrayList Info;

		// Token: 0x040000E6 RID: 230
		private string id;
	}
}
