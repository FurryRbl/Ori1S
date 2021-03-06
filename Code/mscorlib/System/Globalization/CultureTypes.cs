using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the types of culture lists that can be retrieved using <see cref="M:System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes)" />.</summary>
	// Token: 0x0200020F RID: 527
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum CultureTypes
	{
		/// <summary>Cultures that are associated with a language but are not specific to a country/region. The names of .NET Framework cultures consist of the lowercase two-letter code derived from ISO 639-1. For example: "en" (English) is a neutral culture. </summary>
		// Token: 0x040009BD RID: 2493
		NeutralCultures = 1,
		/// <summary>Cultures that are specific to a country/region. The names of these cultures follow RFC 4646 (Windows Vista and later). The format is "&lt;languagecode2&gt;-&lt;country/regioncode2&gt;", where &lt;languagecode2&gt; is a lowercase two-letter code derived from ISO 639-1 and &lt;country/regioncode2&gt; is an uppercase two-letter code derived from ISO 3166. For example, "en-US" for English (United States) is a specific culture.</summary>
		// Token: 0x040009BE RID: 2494
		SpecificCultures = 2,
		/// <summary>All cultures that are installed in the Windows operating system. Note that not all cultures supported by the .NET Framework are installed in the operating system.</summary>
		// Token: 0x040009BF RID: 2495
		InstalledWin32Cultures = 4,
		/// <summary>All cultures that ship with the .NET Framework, including neutral and specific cultures, cultures installed in the Windows operating system, and custom cultures created by the user.</summary>
		// Token: 0x040009C0 RID: 2496
		AllCultures = 7,
		/// <summary>Custom cultures created by the user.</summary>
		// Token: 0x040009C1 RID: 2497
		UserCustomCulture = 8,
		/// <summary>Custom cultures created by the user that replace cultures shipped with the .NET Framework.</summary>
		// Token: 0x040009C2 RID: 2498
		ReplacementCultures = 16,
		/// <summary>Cultures installed in the Windows operating system but not the .NET Framework.</summary>
		// Token: 0x040009C3 RID: 2499
		WindowsOnlyCultures = 32,
		/// <summary>Neutral and specific cultures shipped with the .NET Framework.</summary>
		// Token: 0x040009C4 RID: 2500
		FrameworkCultures = 64
	}
}
