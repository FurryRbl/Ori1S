using System;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020004D2 RID: 1234
	internal class SoapHelper
	{
		// Token: 0x060031C1 RID: 12737 RVA: 0x000A2840 File Offset: 0x000A0A40
		public static Exception GetException(ISoapXsd type, string msg)
		{
			return new RemotingException("Soap Parse error, xsd:type xsd:" + type.GetXsdType() + " " + msg);
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000A2860 File Offset: 0x000A0A60
		public static string Normalize(string s)
		{
			return s;
		}
	}
}
