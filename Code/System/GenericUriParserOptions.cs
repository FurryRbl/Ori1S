using System;

namespace System
{
	/// <summary>Specifies options for a <see cref="T:System.UriParser" />.</summary>
	// Token: 0x0200026F RID: 623
	[Flags]
	public enum GenericUriParserOptions
	{
		/// <summary>The parser:</summary>
		// Token: 0x040006DA RID: 1754
		Default = 0,
		/// <summary>The parser allows a registry-based authority.</summary>
		// Token: 0x040006DB RID: 1755
		GenericAuthority = 1,
		/// <summary>The parser allows a URI with no authority.</summary>
		// Token: 0x040006DC RID: 1756
		AllowEmptyAuthority = 2,
		/// <summary>The scheme does not define a user information part.</summary>
		// Token: 0x040006DD RID: 1757
		NoUserInfo = 4,
		/// <summary>The scheme does not define a port.</summary>
		// Token: 0x040006DE RID: 1758
		NoPort = 8,
		/// <summary>The scheme does not define a query part.</summary>
		// Token: 0x040006DF RID: 1759
		NoQuery = 16,
		/// <summary>The scheme does not define a fragment part.</summary>
		// Token: 0x040006E0 RID: 1760
		NoFragment = 32,
		/// <summary>The parser does not convert back slashes into forward slashes.</summary>
		// Token: 0x040006E1 RID: 1761
		DontConvertPathBackslashes = 64,
		/// <summary>The parser does not canonicalize the URI.</summary>
		// Token: 0x040006E2 RID: 1762
		DontCompressPath = 128,
		/// <summary>The parser does not unescape path dots, forward slashes, or back slashes.</summary>
		// Token: 0x040006E3 RID: 1763
		DontUnescapePathDotsAndSlashes = 256,
		/// <summary>The parser supports Internationalized Domain Name (IDN) parsing (IDN) of host names. Whether IDN is used is dictated by configuration values. See the Remarks for more information.</summary>
		// Token: 0x040006E4 RID: 1764
		Idn = 512,
		/// <summary>The parser supports the parsing rules specified in RFC 3987 for International Resource Identifiers (IRI). Whether IRI is used is dictated by configuration values. See the Remarks for more information.</summary>
		// Token: 0x040006E5 RID: 1765
		IriParsing = 1024
	}
}
