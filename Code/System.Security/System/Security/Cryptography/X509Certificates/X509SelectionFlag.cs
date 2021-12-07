using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the type of selection requested using the <see cref="Overload:System.Security.Cryptography.X509Certificates.X509Certificate2UI.SelectFromCollection" /> method.</summary>
	// Token: 0x02000033 RID: 51
	public enum X509SelectionFlag
	{
		/// <summary>A single selection. The UI allows the user to select one X.509 certificate.</summary>
		// Token: 0x040000A6 RID: 166
		SingleSelection,
		/// <summary>A multiple selection. The user can use the SHIFT or CRTL keys to select more than one X.509 certificate.</summary>
		// Token: 0x040000A7 RID: 167
		MultiSelection
	}
}
