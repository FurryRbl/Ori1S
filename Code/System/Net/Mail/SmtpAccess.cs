using System;

namespace System.Net.Mail
{
	/// <summary>Specifies the level of access allowed to a Simple Mail Transport Protocol (SMTP) server.</summary>
	// Token: 0x02000340 RID: 832
	public enum SmtpAccess
	{
		/// <summary>No access to an SMTP host.</summary>
		// Token: 0x04001254 RID: 4692
		None,
		/// <summary>Connection to an SMTP host on the default port (port 25).</summary>
		// Token: 0x04001255 RID: 4693
		Connect,
		/// <summary>Connection to an SMTP host on any port.</summary>
		// Token: 0x04001256 RID: 4694
		ConnectToUnrestrictedPort
	}
}
