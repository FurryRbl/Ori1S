using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Provides access to the XML Schema definition language (XSD) of a SOAP type.</summary>
	// Token: 0x020004C6 RID: 1222
	[ComVisible(true)]
	public interface ISoapXsd
	{
		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600315A RID: 12634
		string GetXsdType();
	}
}
