using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal
{
	/// <summary>Provides access to internal properties of an <see cref="T:System.ApplicationIdentity" /> object.</summary>
	// Token: 0x020001D9 RID: 473
	[ComVisible(false)]
	public static class InternalApplicationIdentityHelper
	{
		// Token: 0x06001851 RID: 6225 RVA: 0x0005D4C0 File Offset: 0x0005B6C0
		[MonoTODO("2.0 SP1 member")]
		public static object GetActivationContextData(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an IDefinitionAppId Interface representing the unique identifier of an <see cref="T:System.ApplicationIdentity" /> object.</summary>
		/// <returns>The unique identifier held by the <see cref="T:System.ApplicationIdentity" /> object.</returns>
		/// <param name="id">The object from which to extract the identifier.</param>
		// Token: 0x06001852 RID: 6226 RVA: 0x0005D4C8 File Offset: 0x0005B6C8
		[MonoTODO]
		public static object GetInternalAppId(ApplicationIdentity id)
		{
			throw new NotImplementedException();
		}
	}
}
