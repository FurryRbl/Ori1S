using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown as a wrapper around the exception thrown by the class initializer. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018E RID: 398
	[ComVisible(true)]
	[Serializable]
	public sealed class TypeInitializationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TypeInitializationException" /> class with the default error message, the specified type name, and a reference to the inner exception that is the root cause of this exception.</summary>
		/// <param name="fullTypeName">The fully qualified name of the type that fails to initialize. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600145C RID: 5212 RVA: 0x00051F74 File Offset: 0x00050174
		public TypeInitializationException(string fullTypeName, Exception innerException) : base(Locale.GetText("An exception was thrown by the type initializer for ") + fullTypeName, innerException)
		{
			this.type_name = fullTypeName;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00051F94 File Offset: 0x00050194
		internal TypeInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type_name = info.GetString("TypeName");
		}

		/// <summary>Gets the fully qualified name of the type that fails to initialize.</summary>
		/// <returns>The fully qualified name of the type that fails to initialize.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00051FB0 File Offset: 0x000501B0
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the type name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600145F RID: 5215 RVA: 0x00051FB8 File Offset: 0x000501B8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("TypeName", this.type_name);
		}

		// Token: 0x040007FC RID: 2044
		private string type_name;
	}
}
