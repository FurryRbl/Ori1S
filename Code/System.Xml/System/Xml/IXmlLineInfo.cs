using System;

namespace System.Xml
{
	/// <summary>Provides an interface to enable a class to return line and position information.</summary>
	// Token: 0x020000DA RID: 218
	public interface IXmlLineInfo
	{
		/// <summary>Gets the current line number.</summary>
		/// <returns>The current line number or 0 if no line information is available (for example, <see cref="M:System.Xml.IXmlLineInfo.HasLineInfo" /> returns false).</returns>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060007E5 RID: 2021
		int LineNumber { get; }

		/// <summary>Gets the current line position.</summary>
		/// <returns>The current line position or 0 if no line information is available (for example, <see cref="M:System.Xml.IXmlLineInfo.HasLineInfo" /> returns false).</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060007E6 RID: 2022
		int LinePosition { get; }

		/// <summary>Gets a value indicating whether the class can return line information.</summary>
		/// <returns>true if <see cref="P:System.Xml.IXmlLineInfo.LineNumber" /> and <see cref="P:System.Xml.IXmlLineInfo.LinePosition" /> can be provided; otherwise, false.</returns>
		// Token: 0x060007E7 RID: 2023
		bool HasLineInfo();
	}
}
