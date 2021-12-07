using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Provides the ability to uniquely identify a manifest-activated application. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000FF RID: 255
	[ComVisible(false)]
	[Serializable]
	public sealed class ApplicationIdentity : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationIdentity" /> class. </summary>
		/// <param name="applicationIdentityFullName">The full name of the application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentityFullName" /> is null.</exception>
		// Token: 0x06000D66 RID: 3430 RVA: 0x0003AB6C File Offset: 0x00038D6C
		public ApplicationIdentity(string applicationIdentityFullName)
		{
			if (applicationIdentityFullName == null)
			{
				throw new ArgumentNullException("applicationIdentityFullName");
			}
			if (applicationIdentityFullName.IndexOf(", Culture=") == -1)
			{
				this._fullName = applicationIdentityFullName + ", Culture=neutral";
			}
			else
			{
				this._fullName = applicationIdentityFullName;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" />) structure for the serialization.</param>
		// Token: 0x06000D67 RID: 3431 RVA: 0x0003ABC0 File Offset: 0x00038DC0
		[MonoTODO("Missing serialization")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		/// <summary>Gets the location of the deployment manifest as a URL.</summary>
		/// <returns>The URL of the deployment manifest.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0003ABD4 File Offset: 0x00038DD4
		public string CodeBase
		{
			get
			{
				return this._codeBase;
			}
		}

		/// <summary>Gets the full name of the application.</summary>
		/// <returns>The full name of the application, also known as the display name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0003ABDC File Offset: 0x00038DDC
		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		/// <summary>Returns the full name of the manifest-activated application.</summary>
		/// <returns>The full name of the manifest-activated application.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000D6A RID: 3434 RVA: 0x0003ABE4 File Offset: 0x00038DE4
		public override string ToString()
		{
			return this._fullName;
		}

		// Token: 0x0400039C RID: 924
		private string _fullName;

		// Token: 0x0400039D RID: 925
		private string _codeBase;
	}
}
