using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an operation is performed on a disposed object.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000165 RID: 357
	[ComVisible(true)]
	[Serializable]
	public class ObjectDisposedException : InvalidOperationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with a string containing the name of the disposed object.</summary>
		/// <param name="objectName">A string containing the name of the disposed object. </param>
		// Token: 0x0600133A RID: 4922 RVA: 0x0004D324 File Offset: 0x0004B524
		public ObjectDisposedException(string objectName) : base(Locale.GetText("The object was used after being disposed."))
		{
			this.obj_name = objectName;
			this.msg = Locale.GetText("The object was used after being disposed.");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with the specified object name and message.</summary>
		/// <param name="objectName">The name of the disposed object. </param>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x0600133B RID: 4923 RVA: 0x0004D350 File Offset: 0x0004B550
		public ObjectDisposedException(string objectName, string message) : base(message)
		{
			this.obj_name = objectName;
			this.msg = message;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If <paramref name="innerException" /> is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x0600133C RID: 4924 RVA: 0x0004D368 File Offset: 0x0004B568
		public ObjectDisposedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x0600133D RID: 4925 RVA: 0x0004D374 File Offset: 0x0004B574
		protected ObjectDisposedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.obj_name = info.GetString("ObjectName");
		}

		/// <summary>Gets the message that describes the error.</summary>
		/// <returns>A string that describes the error.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0004D390 File Offset: 0x0004B590
		public override string Message
		{
			get
			{
				return this.msg;
			}
		}

		/// <summary>Gets the name of the disposed object.</summary>
		/// <returns>A string containing the name of the disposed object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0004D398 File Offset: 0x0004B598
		public string ObjectName
		{
			get
			{
				return this.obj_name;
			}
		}

		/// <summary>Retrieves the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001340 RID: 4928 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ObjectName", this.obj_name);
		}

		// Token: 0x0400058D RID: 1421
		private string obj_name;

		// Token: 0x0400058E RID: 1422
		private string msg;
	}
}
