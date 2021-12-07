﻿using System;

namespace System.Xml.Serialization
{
	/// <summary>Establishes a <see cref="P:System.Xml.Serialization.IXmlTextParser.Normalized" /> property for use by the .NET Framework infrastructure.</summary>
	// Token: 0x02000252 RID: 594
	public interface IXmlTextParser
	{
		/// <summary>Gets or sets whether white space and attribute values are normalized.</summary>
		/// <returns>true if white space attributes values are normalized; otherwise, false.</returns>
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001824 RID: 6180
		// (set) Token: 0x06001825 RID: 6181
		bool Normalized { get; set; }

		/// <summary>Gets or sets how white space is handled when parsing XML.</summary>
		/// <returns>A member of the <see cref="T:System.Xml.WhitespaceHandling" /> enumeration that describes how whites pace is handled when parsing XML.</returns>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001826 RID: 6182
		// (set) Token: 0x06001827 RID: 6183
		WhitespaceHandling WhitespaceHandling { get; set; }
	}
}
