using System;
using System.Configuration.Provider;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Is the base class to create providers for encrypting and decrypting protected-configuration data.</summary>
	// Token: 0x02000063 RID: 99
	public abstract class ProtectedConfigurationProvider : ProviderBase
	{
		/// <summary>Decrypts the passed <see cref="T:System.Xml.XmlNode" /> object from a configuration file.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> object containing decrypted data.</returns>
		/// <param name="encryptedNode">The <see cref="T:System.Xml.XmlNode" /> object to decrypt.</param>
		// Token: 0x0600036A RID: 874
		public abstract XmlNode Decrypt(XmlNode encrypted_node);

		/// <summary>Encrypts the passed <see cref="T:System.Xml.XmlNode" /> object from a configuration file.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> object containing encrypted data.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object to encrypt.</param>
		// Token: 0x0600036B RID: 875
		public abstract XmlNode Encrypt(XmlNode node);
	}
}
