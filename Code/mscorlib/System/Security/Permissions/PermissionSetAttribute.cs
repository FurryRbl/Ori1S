using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using Mono.Security.Cryptography;
using Mono.Xml;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for a <see cref="T:System.Security.PermissionSet" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200060D RID: 1549
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class PermissionSetAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PermissionSetAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003AFC RID: 15100 RVA: 0x000CA20C File Offset: 0x000C840C
		public PermissionSetAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets a file containing the XML representation of a custom permission set to be declared.</summary>
		/// <returns>The physical path to the file containing the XML representation of the permission set.</returns>
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x000CA218 File Offset: 0x000C8418
		// (set) Token: 0x06003AFE RID: 15102 RVA: 0x000CA220 File Offset: 0x000C8420
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				this.file = value;
			}
		}

		/// <summary>Gets or sets the hexadecimal representation of the XML encoded permission set.</summary>
		/// <returns>The hexadecimal representation of the XML encoded permission set.</returns>
		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06003AFF RID: 15103 RVA: 0x000CA22C File Offset: 0x000C842C
		// (set) Token: 0x06003B00 RID: 15104 RVA: 0x000CA234 File Offset: 0x000C8434
		public string Hex
		{
			get
			{
				return this.hex;
			}
			set
			{
				this.hex = value;
			}
		}

		/// <summary>Gets or sets the name of the permission set.</summary>
		/// <returns>The name of an immutable <see cref="T:System.Security.NamedPermissionSet" /> (one of several permission sets that are contained in the default policy and cannot be altered).</returns>
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x000CA240 File Offset: 0x000C8440
		// (set) Token: 0x06003B02 RID: 15106 RVA: 0x000CA248 File Offset: 0x000C8448
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

		/// <summary>Gets or sets a value indicating whether the file specified by <see cref="P:System.Security.Permissions.PermissionSetAttribute.File" /> is Unicode or ASCII encoded.</summary>
		/// <returns>true if the file is Unicode encoded; otherwise, false.</returns>
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x000CA254 File Offset: 0x000C8454
		// (set) Token: 0x06003B04 RID: 15108 RVA: 0x000CA25C File Offset: 0x000C845C
		public bool UnicodeEncoded
		{
			get
			{
				return this.isUnicodeEncoded;
			}
			set
			{
				this.isUnicodeEncoded = value;
			}
		}

		/// <summary>Gets or sets the XML representation of a permission set.</summary>
		/// <returns>The XML representation of a permission set.</returns>
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x000CA268 File Offset: 0x000C8468
		// (set) Token: 0x06003B06 RID: 15110 RVA: 0x000CA270 File Offset: 0x000C8470
		public string XML
		{
			get
			{
				return this.xml;
			}
			set
			{
				this.xml = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A new <see cref="T:System.Security.IPermission" />.</returns>
		// Token: 0x06003B07 RID: 15111 RVA: 0x000CA27C File Offset: 0x000C847C
		public override IPermission CreatePermission()
		{
			return null;
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000CA280 File Offset: 0x000C8480
		private PermissionSet CreateFromXml(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			try
			{
				securityParser.LoadXml(xml);
			}
			catch (SmallXmlParserException ex)
			{
				throw new XmlSyntaxException(ex.Line, ex.ToString());
			}
			SecurityElement securityElement = securityParser.ToXml();
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				return null;
			}
			PermissionState state = PermissionState.None;
			if (CodeAccessPermission.IsUnrestricted(securityElement))
			{
				state = PermissionState.Unrestricted;
			}
			if (text.EndsWith("NamedPermissionSet"))
			{
				NamedPermissionSet namedPermissionSet = new NamedPermissionSet(securityElement.Attribute("Name"), state);
				namedPermissionSet.FromXml(securityElement);
				return namedPermissionSet;
			}
			if (text.EndsWith("PermissionSet"))
			{
				PermissionSet permissionSet = new PermissionSet(state);
				permissionSet.FromXml(securityElement);
				return permissionSet;
			}
			return null;
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A new <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003B09 RID: 15113 RVA: 0x000CA354 File Offset: 0x000C8554
		public PermissionSet CreatePermissionSet()
		{
			PermissionSet result = null;
			if (base.Unrestricted)
			{
				result = new PermissionSet(PermissionState.Unrestricted);
			}
			else
			{
				result = new PermissionSet(PermissionState.None);
				if (this.name != null)
				{
					return PolicyLevel.CreateAppDomainLevel().GetNamedPermissionSet(this.name);
				}
				if (this.file != null)
				{
					Encoding encoding = (!this.isUnicodeEncoded) ? Encoding.ASCII : Encoding.Unicode;
					using (StreamReader streamReader = new StreamReader(this.file, encoding))
					{
						result = this.CreateFromXml(streamReader.ReadToEnd());
					}
				}
				else if (this.xml != null)
				{
					result = this.CreateFromXml(this.xml);
				}
				else if (this.hex != null)
				{
					Encoding ascii = Encoding.ASCII;
					byte[] array = CryptoConvert.FromHex(this.hex);
					result = this.CreateFromXml(ascii.GetString(array, 0, array.Length));
				}
			}
			return result;
		}

		// Token: 0x040019AF RID: 6575
		private string file;

		// Token: 0x040019B0 RID: 6576
		private string name;

		// Token: 0x040019B1 RID: 6577
		private bool isUnicodeEncoded;

		// Token: 0x040019B2 RID: 6578
		private string xml;

		// Token: 0x040019B3 RID: 6579
		private string hex;
	}
}
