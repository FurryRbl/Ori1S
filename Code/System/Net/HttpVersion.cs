﻿using System;

namespace System.Net
{
	/// <summary>Defines the HTTP version numbers that are supported by the <see cref="T:System.Net.HttpWebRequest" /> and <see cref="T:System.Net.HttpWebResponse" /> classes.</summary>
	// Token: 0x02000321 RID: 801
	public class HttpVersion
	{
		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.0.</summary>
		// Token: 0x040011BD RID: 4541
		public static readonly Version Version10 = new Version(1, 0);

		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.1.</summary>
		// Token: 0x040011BE RID: 4542
		public static readonly Version Version11 = new Version(1, 1);
	}
}
