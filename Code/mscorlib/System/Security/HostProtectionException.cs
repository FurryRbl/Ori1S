using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security
{
	/// <summary>The exception that is thrown when a denied host resource is detected.</summary>
	// Token: 0x02000533 RID: 1331
	[ComVisible(true)]
	[MonoTODO("Not supported in the runtime")]
	[Serializable]
	public class HostProtectionException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.HostProtectionException" /> class with default values. </summary>
		// Token: 0x0600347B RID: 13435 RVA: 0x000AC554 File Offset: 0x000AA754
		public HostProtectionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.HostProtectionException" /> class with a specified error message. </summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x0600347C RID: 13436 RVA: 0x000AC55C File Offset: 0x000AA75C
		public HostProtectionException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.HostProtectionException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception. </summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="e">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600347D RID: 13437 RVA: 0x000AC568 File Offset: 0x000AA768
		public HostProtectionException(string message, Exception e) : base(message, e)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.HostProtectionException" /> class with a specified error message, the protected host resources, and the host resources that caused the exception to be thrown.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="protectedResources">A bitwise combination of the <see cref="T:System.Security.Permissions.HostProtectionResource" /> values indicating the host resources that are inaccessible to partially trusted code.</param>
		/// <param name="demandedResources">A bitwise combination of the <see cref="T:System.Security.Permissions.HostProtectionResource" /> values indicating the demanded host resources.</param>
		// Token: 0x0600347E RID: 13438 RVA: 0x000AC574 File Offset: 0x000AA774
		public HostProtectionException(string message, HostProtectionResource protectedResources, HostProtectionResource demandedResources) : base(message)
		{
			this._protected = protectedResources;
			this._demanded = demandedResources;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.HostProtectionException" /> class using the provided serialization information and streaming context.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600347F RID: 13439 RVA: 0x000AC58C File Offset: 0x000AA78C
		protected HostProtectionException(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		/// <summary>Gets or sets the demanded host protection resources that caused the exception to be thrown.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.HostProtectionResource" /> values identifying the protection resources causing the exception to be thrown. The default is <see cref="F:System.Security.Permissions.HostProtectionResource.None" />. </returns>
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000AC59C File Offset: 0x000AA79C
		public HostProtectionResource DemandedResources
		{
			get
			{
				return this._demanded;
			}
		}

		/// <summary>Gets or sets the host protection resources that are inaccessible to partially trusted code.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.HostProtectionResource" /> values identifying the inaccessible host protection categories. The default is <see cref="F:System.Security.Permissions.HostProtectionResource.None" />.</returns>
		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06003481 RID: 13441 RVA: 0x000AC5A4 File Offset: 0x000AA7A4
		public HostProtectionResource ProtectedResources
		{
			get
			{
				return this._protected;
			}
		}

		/// <summary>Sets the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the host protection exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06003482 RID: 13442 RVA: 0x000AC5AC File Offset: 0x000AA7AC
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		/// <summary>Returns a string representation of the current host protection exception.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.HostProtectionException" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06003483 RID: 13443 RVA: 0x000AC5C0 File Offset: 0x000AA7C0
		[MonoTODO]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x04001615 RID: 5653
		private HostProtectionResource _protected;

		// Token: 0x04001616 RID: 5654
		private HostProtectionResource _demanded;
	}
}
