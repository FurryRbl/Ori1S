using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies characteristics of the X.500 distinguished name.</summary>
	// Token: 0x0200043E RID: 1086
	[Flags]
	public enum X500DistinguishedNameFlags
	{
		/// <summary>The distinguished name has no special characteristics.</summary>
		// Token: 0x04001809 RID: 6153
		None = 0,
		/// <summary>The distinguished name is reversed.</summary>
		// Token: 0x0400180A RID: 6154
		Reversed = 1,
		/// <summary>The distinguished name uses semicolons.</summary>
		// Token: 0x0400180B RID: 6155
		UseSemicolons = 16,
		/// <summary>The distinguished name does not use the plus sign.</summary>
		// Token: 0x0400180C RID: 6156
		DoNotUsePlusSign = 32,
		/// <summary>The distinguished name does not use quotation marks.</summary>
		// Token: 0x0400180D RID: 6157
		DoNotUseQuotes = 64,
		/// <summary>The distinguished name uses commas.</summary>
		// Token: 0x0400180E RID: 6158
		UseCommas = 128,
		/// <summary>The distinguished name uses the new line character.</summary>
		// Token: 0x0400180F RID: 6159
		UseNewLines = 256,
		/// <summary>The distinguished name uses UTF8 encoding.</summary>
		// Token: 0x04001810 RID: 6160
		UseUTF8Encoding = 4096,
		/// <summary>The distinguished name uses T61 encoding.</summary>
		// Token: 0x04001811 RID: 6161
		UseT61Encoding = 8192,
		/// <summary>The distinguished name uses UTF8 encoding.</summary>
		// Token: 0x04001812 RID: 6162
		ForceUTF8Encoding = 16384
	}
}
