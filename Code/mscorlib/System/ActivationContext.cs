using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Identifies the activation context for the current application. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000F4 RID: 244
	[ComVisible(false)]
	[Serializable]
	public sealed class ActivationContext : IDisposable, ISerializable
	{
		// Token: 0x06000C5E RID: 3166 RVA: 0x000384F0 File Offset: 0x000366F0
		private ActivationContext(ApplicationIdentity identity)
		{
			this._appid = identity;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" />  with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" />  to populate with data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure for this serialization.</param>
		// Token: 0x06000C5F RID: 3167 RVA: 0x00038500 File Offset: 0x00036700
		[MonoTODO("Missing serialization support")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00038514 File Offset: 0x00036714
		~ActivationContext()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the form, or store context, for the current application. </summary>
		/// <returns>One of the <see cref="T:System.ActivationContext.ContextForm" /> values. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00038550 File Offset: 0x00036750
		public ActivationContext.ContextForm Form
		{
			get
			{
				return this._form;
			}
		}

		/// <summary>Gets the application identity for the current application.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> object that identifies the current application.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00038558 File Offset: 0x00036758
		public ApplicationIdentity Identity
		{
			get
			{
				return this._appid;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ActivationContext" /> class using the specified application identity.</summary>
		/// <returns>An <see cref="T:System.ActivationContext" /> object.</returns>
		/// <param name="identity">An <see cref="T:System.ApplicationIdentity" /> object that identifies an application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">No deployment or application identity is specified in <paramref name="identity" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000C63 RID: 3171 RVA: 0x00038560 File Offset: 0x00036760
		[MonoTODO("Missing validation")]
		public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			return new ActivationContext(identity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ActivationContext" /> class using the specified application identity and array of manifest paths.</summary>
		/// <returns>An <see cref="T:System.ActivationContext" /> object.</returns>
		/// <param name="identity">An <see cref="T:System.ApplicationIdentity" /> object that identifies an application.</param>
		/// <param name="manifestPaths">A string array of manifest paths for the application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null. -or-<paramref name="manifestPaths" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">No deployment or application identity is specified in <paramref name="identity" />.-or-<paramref name="identity" /> does not match the identity in the manifests.-or-<paramref name="identity" /> does not have the same number of components as the manifest paths.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000C64 RID: 3172 RVA: 0x0003857C File Offset: 0x0003677C
		[MonoTODO("Missing validation")]
		public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity, string[] manifestPaths)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (manifestPaths == null)
			{
				throw new ArgumentNullException("manifestPaths");
			}
			return new ActivationContext(identity);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ActivationContext" />. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C65 RID: 3173 RVA: 0x000385B4 File Offset: 0x000367B4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x000385C4 File Offset: 0x000367C4
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				if (disposing)
				{
				}
				this._disposed = true;
			}
		}

		// Token: 0x0400035D RID: 861
		private ActivationContext.ContextForm _form;

		// Token: 0x0400035E RID: 862
		private ApplicationIdentity _appid;

		// Token: 0x0400035F RID: 863
		private bool _disposed;

		/// <summary>Indicates the context for a manifest-activated application.</summary>
		// Token: 0x020000F5 RID: 245
		public enum ContextForm
		{
			/// <summary>The application is not in the ClickOnce store.</summary>
			// Token: 0x04000361 RID: 865
			Loose,
			/// <summary>The application is contained in the ClickOnce store.</summary>
			// Token: 0x04000362 RID: 866
			StoreBounded
		}
	}
}
