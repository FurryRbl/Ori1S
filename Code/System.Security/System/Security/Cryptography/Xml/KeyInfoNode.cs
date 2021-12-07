using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Handles <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> subelements that do not have specific implementations or handlers registered on the machine.</summary>
	// Token: 0x02000046 RID: 70
	public class KeyInfoNode : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" /> class.</summary>
		// Token: 0x060001ED RID: 493 RVA: 0x0000954C File Offset: 0x0000774C
		public KeyInfoNode()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" /> class with content taken from the specified <see cref="T:System.Xml.XmlElement" />.</summary>
		/// <param name="node">An XML element from which to take the content used to create the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />. </param>
		// Token: 0x060001EE RID: 494 RVA: 0x00009554 File Offset: 0x00007754
		public KeyInfoNode(XmlElement node)
		{
			this.LoadXml(node);
		}

		/// <summary>Gets or sets the XML content of the current <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</summary>
		/// <returns>The XML content of the current <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00009564 File Offset: 0x00007764
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000956C File Offset: 0x0000776C
		public XmlElement Value
		{
			get
			{
				return this.Node;
			}
			set
			{
				this.Node = value;
			}
		}

		/// <summary>Returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001F1 RID: 497 RVA: 0x00009578 File Offset: 0x00007778
		public override XmlElement GetXml()
		{
			return this.Node;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" /> to match.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlElement" /> that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />. </param>
		// Token: 0x060001F2 RID: 498 RVA: 0x00009580 File Offset: 0x00007780
		public override void LoadXml(XmlElement value)
		{
			this.Node = value;
		}

		// Token: 0x040000EB RID: 235
		private XmlElement Node;
	}
}
