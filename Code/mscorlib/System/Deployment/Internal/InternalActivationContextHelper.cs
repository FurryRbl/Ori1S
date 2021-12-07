using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal
{
	/// <summary>Provides access to data from an <see cref="T:System.ActivationContext" /> object.</summary>
	// Token: 0x020001D8 RID: 472
	[ComVisible(false)]
	public static class InternalActivationContextHelper
	{
		/// <summary>Gets the contents of the application manifest from an <see cref="T:System.ActivationContext" /> object.</summary>
		/// <returns>The application manifest that is contained by the <see cref="T:System.ActivationContext" /> object.</returns>
		/// <param name="appInfo">The object containing the manifest.</param>
		// Token: 0x0600184A RID: 6218 RVA: 0x0005D488 File Offset: 0x0005B688
		[MonoTODO]
		public static object GetActivationContextData(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the manifest of the last deployment component in an <see cref="T:System.ActivationContext" /> object.</summary>
		/// <returns>The manifest of the last deployment component in the <see cref="T:System.ActivationContext" /> object.</returns>
		/// <param name="appInfo">The object containing the manifest.</param>
		// Token: 0x0600184B RID: 6219 RVA: 0x0005D490 File Offset: 0x0005B690
		[MonoTODO]
		public static object GetApplicationComponentManifest(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a byte array containing the raw content of the application manifest..</summary>
		/// <returns>An array containing the application manifest as raw data.</returns>
		/// <param name="appInfo">The object to get bytes from.</param>
		// Token: 0x0600184C RID: 6220 RVA: 0x0005D498 File Offset: 0x0005B698
		[MonoTODO("2.0 SP1 member")]
		public static byte[] GetApplicationManifestBytes(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the manifest of the first deployment component in an <see cref="T:System.ActivationContext" /> object.</summary>
		/// <returns>The manifest of the first deployment component in the <see cref="T:System.ActivationContext" /> object.</returns>
		/// <param name="appInfo">The object containing the manifest.</param>
		// Token: 0x0600184D RID: 6221 RVA: 0x0005D4A0 File Offset: 0x0005B6A0
		[MonoTODO]
		public static object GetDeploymentComponentManifest(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a byte array containing the raw content of the deployment manifest.</summary>
		/// <returns>An array containing the deployment manifest as raw data.</returns>
		/// <param name="appInfo">The object to get bytes from.</param>
		// Token: 0x0600184E RID: 6222 RVA: 0x0005D4A8 File Offset: 0x0005B6A8
		[MonoTODO("2.0 SP1 member")]
		public static byte[] GetDeploymentManifestBytes(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether this is the first time this <see cref="T:System.ActivationContext" /> object has been run.</summary>
		/// <returns>true if the <see cref="T:System.ActivationContext" /> indicates it is running for the first time; otherwise, false.</returns>
		/// <param name="appInfo">The object to examine.</param>
		// Token: 0x0600184F RID: 6223 RVA: 0x0005D4B0 File Offset: 0x0005B6B0
		[MonoTODO]
		public static bool IsFirstRun(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Informs an <see cref="T:System.ActivationContext" /> to get ready to be run.</summary>
		/// <param name="appInfo">The object to inform.</param>
		// Token: 0x06001850 RID: 6224 RVA: 0x0005D4B8 File Offset: 0x0005B6B8
		[MonoTODO]
		public static void PrepareForExecution(ActivationContext appInfo)
		{
			throw new NotImplementedException();
		}
	}
}
