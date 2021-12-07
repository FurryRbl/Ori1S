﻿using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all asymmetric key exchange formatters derive.</summary>
	// Token: 0x02000596 RID: 1430
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeFormatter
	{
		/// <summary>When overridden in a derived class, gets the parameters for the asymmetric key exchange.</summary>
		/// <returns>A string in XML format containing the parameters of the asymmetric key exchange operation.</returns>
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06003745 RID: 14149
		public abstract string Parameters { get; }

		/// <summary>When overridden in a derived class, creates the encrypted key exchange data from the specified input data.</summary>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		/// <param name="data">The secret information to be passed in the key exchange. </param>
		// Token: 0x06003746 RID: 14150
		public abstract byte[] CreateKeyExchange(byte[] data);

		/// <summary>When overridden in a derived class, creates the encrypted key exchange data from the specified input data.</summary>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		/// <param name="data">The secret information to be passed in the key exchange. </param>
		/// <param name="symAlgType">This parameter is not used in the current version. </param>
		// Token: 0x06003747 RID: 14151
		public abstract byte[] CreateKeyExchange(byte[] data, Type symAlgType);

		/// <summary>When overridden in a derived class, sets the public key to use for encrypting the secret information.</summary>
		/// <param name="key">The instance of the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the public key. </param>
		// Token: 0x06003748 RID: 14152
		public abstract void SetKey(AsymmetricAlgorithm key);
	}
}
