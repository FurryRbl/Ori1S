using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the exception thrown when a component cannot be granted a license.</summary>
	// Token: 0x02000175 RID: 373
	[Serializable]
	public class LicenseException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type of component that was denied a license. </summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license. </param>
		// Token: 0x06000CE7 RID: 3303 RVA: 0x00020588 File Offset: 0x0001E788
		public LicenseException(Type type) : this(type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type and the instance of the component that was denied a license.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license. </param>
		/// <param name="instance">The instance of the component that was not granted a license. </param>
		// Token: 0x06000CE8 RID: 3304 RVA: 0x00020594 File Offset: 0x0001E794
		public LicenseException(Type type, object instance)
		{
			this.type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type and the instance of the component that was denied a license, along with a message to display.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license. </param>
		/// <param name="instance">The instance of the component that was not granted a license. </param>
		/// <param name="message">The exception message to display. </param>
		// Token: 0x06000CE9 RID: 3305 RVA: 0x000205A4 File Offset: 0x0001E7A4
		public LicenseException(Type type, object instance, string message) : this(type, instance, message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type and the instance of the component that was denied a license, along with a message to display and the original exception thrown.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license. </param>
		/// <param name="instance">The instance of the component that was not granted a license. </param>
		/// <param name="message">The exception message to display. </param>
		/// <param name="innerException">An <see cref="T:System.Exception" /> that represents the original exception. </param>
		// Token: 0x06000CEA RID: 3306 RVA: 0x000205B0 File Offset: 0x0001E7B0
		public LicenseException(Type type, object instance, string message, Exception innerException) : base(message, innerException)
		{
			this.type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06000CEB RID: 3307 RVA: 0x000205C4 File Offset: 0x0001E7C4
		protected LicenseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (Type)info.GetValue("LicensedType", typeof(Type));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06000CEC RID: 3308 RVA: 0x000205FC File Offset: 0x0001E7FC
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("LicensedType", this.type);
			base.GetObjectData(info, context);
		}

		/// <summary>Gets the type of the component that was not granted a license.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of component that was not granted a license.</returns>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00020634 File Offset: 0x0001E834
		public Type LicensedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000382 RID: 898
		private Type type;
	}
}
