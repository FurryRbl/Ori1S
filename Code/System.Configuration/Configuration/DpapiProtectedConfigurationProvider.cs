using System;
using System.Collections.Specialized;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides a <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object that uses the Windows data protection API (DPAPI) to encrypt and decrypt configuration data.</summary>
	// Token: 0x02000044 RID: 68
	public sealed class DpapiProtectedConfigurationProvider : ProtectedConfigurationProvider
	{
		/// <summary>Decrypts the passed <see cref="T:System.Xml.XmlNode" /> object.</summary>
		/// <returns>A decrypted <see cref="T:System.Xml.XmlNode" /> object.</returns>
		/// <param name="encryptedNode">The <see cref="T:System.Xml.XmlNode" /> object to decrypt. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="encryptedNode" /> does not have <see cref="P:System.Xml.XmlNode.Name" /> set to "EncryptedData" and <see cref="T:System.Xml.XmlNodeType" /> set to <see cref="F:System.Xml.XmlNodeType.Element" />.- or -<paramref name="encryptedNode" /> does not have a child node named "CipherData" with a child node named "CipherValue".- or -The child node named "CipherData" is an empty node.</exception>
		// Token: 0x06000286 RID: 646 RVA: 0x00008114 File Offset: 0x00006314
		[MonoNotSupported("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.")]
		public override XmlNode Decrypt(XmlNode encrypted_node)
		{
			throw new NotSupportedException("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.");
		}

		/// <summary>Encrypts the passed <see cref="T:System.Xml.XmlNode" /> object.</summary>
		/// <returns>An encrypted <see cref="T:System.Xml.XmlNode" /> object.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object to encrypt. </param>
		// Token: 0x06000287 RID: 647 RVA: 0x00008120 File Offset: 0x00006320
		[MonoNotSupported("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.")]
		public override XmlNode Encrypt(XmlNode node)
		{
			throw new NotSupportedException("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.");
		}

		/// <summary>Initializes the provider with default settings.</summary>
		/// <param name="name">The provider name to use for the object.</param>
		/// <param name="configurationValues">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> collection of values to use when initializing the object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="configurationValues" /> contains an unrecognized configuration setting.</exception>
		// Token: 0x06000288 RID: 648 RVA: 0x0000812C File Offset: 0x0000632C
		[MonoTODO]
		public override void Initialize(string name, NameValueCollection configurationValues)
		{
			base.Initialize(name, configurationValues);
			string text = configurationValues["useMachineProtection"];
			if (text != null && text.ToLowerInvariant() == "true")
			{
				this.useMachineProtection = true;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Configuration.DpapiProtectedConfigurationProvider" /> object is using machine-specific or user-account-specific protection.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.DpapiProtectedConfigurationProvider" /> is using machine-specific protection; false if it is using user-account-specific protection.</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00008170 File Offset: 0x00006370
		public bool UseMachineProtection
		{
			get
			{
				return this.useMachineProtection;
			}
		}

		// Token: 0x040000CF RID: 207
		private const string NotSupportedReason = "DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.";

		// Token: 0x040000D0 RID: 208
		private bool useMachineProtection;
	}
}
