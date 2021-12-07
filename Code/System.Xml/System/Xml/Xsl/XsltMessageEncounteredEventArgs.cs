using System;

namespace System.Xml.Xsl
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Xsl.XsltArgumentList.XsltMessageEncountered" /> event. </summary>
	// Token: 0x020001B7 RID: 439
	public abstract class XsltMessageEncounteredEventArgs : EventArgs
	{
		/// <summary>Gets the contents of the xsl:message element.</summary>
		/// <returns>The contents of the xsl:message element.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060011FD RID: 4605
		public abstract string Message { get; }
	}
}
