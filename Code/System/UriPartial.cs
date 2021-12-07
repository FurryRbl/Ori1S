using System;

namespace System
{
	/// <summary>Defines the parts of a URI for the <see cref="M:System.Uri.GetLeftPart(System.UriPartial)" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004BA RID: 1210
	public enum UriPartial
	{
		/// <summary>The scheme segment of the URI.</summary>
		// Token: 0x04001B82 RID: 7042
		Scheme,
		/// <summary>The scheme and authority segments of the URI.</summary>
		// Token: 0x04001B83 RID: 7043
		Authority,
		/// <summary>The scheme, authority, and path segments of the URI.</summary>
		// Token: 0x04001B84 RID: 7044
		Path,
		/// <summary>The scheme, authority, path, and query segments of the URI.</summary>
		// Token: 0x04001B85 RID: 7045
		Query
	}
}
