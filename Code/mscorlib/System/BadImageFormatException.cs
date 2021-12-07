using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System
{
	/// <summary>The exception that is thrown when the file image of a dynamic link library (DLL) or an executable program is invalid. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000107 RID: 263
	[ComVisible(true)]
	[Serializable]
	public class BadImageFormatException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class.</summary>
		// Token: 0x06000D8C RID: 3468 RVA: 0x0003AF50 File Offset: 0x00039150
		public BadImageFormatException() : base(Locale.GetText("Format of the executable (.exe) or library (.dll) is invalid."))
		{
			base.HResult = -2147024885;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000D8D RID: 3469 RVA: 0x0003AF70 File Offset: 0x00039170
		public BadImageFormatException(string message) : base(message)
		{
			base.HResult = -2147024885;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06000D8E RID: 3470 RVA: 0x0003AF84 File Offset: 0x00039184
		protected BadImageFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = info.GetString("BadImageFormat_FileName");
			this.fusionLog = info.GetString("BadImageFormat_FusionLog");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000D8F RID: 3471 RVA: 0x0003AFBC File Offset: 0x000391BC
		public BadImageFormatException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147024885;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message and file name.</summary>
		/// <param name="message">A message that describes the error. </param>
		/// <param name="fileName">The full name of the file with the invalid image. </param>
		// Token: 0x06000D90 RID: 3472 RVA: 0x0003AFD4 File Offset: 0x000391D4
		public BadImageFormatException(string message, string fileName) : base(message)
		{
			this.fileName = fileName;
			base.HResult = -2147024885;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="fileName">The full name of the file with the invalid image. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000D91 RID: 3473 RVA: 0x0003AFF0 File Offset: 0x000391F0
		public BadImageFormatException(string message, string fileName, Exception inner) : base(message, inner)
		{
			this.fileName = fileName;
			base.HResult = -2147024885;
		}

		/// <summary>Gets the error message and the name of the file that caused this exception.</summary>
		/// <returns>A string containing the error message and the name of the file that caused this exception.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0003B00C File Offset: 0x0003920C
		public override string Message
		{
			get
			{
				if (this.message == null)
				{
					return string.Format(CultureInfo.CurrentCulture, "Could not load file or assembly '{0}' or one of its dependencies. An attempt was made to load a program with an incorrect format.", new object[]
					{
						this.fileName
					});
				}
				return base.Message;
			}
		}

		/// <summary>Gets the name of the file that causes this exception.</summary>
		/// <returns>The name of the file with the invalid image, or a null reference if no file name was passed to the constructor for the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0003B04C File Offset: 0x0003924C
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		/// <summary>Gets the log file that describes why an assembly load failed.</summary>
		/// <returns>A String containing errors reported by the assembly cache.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0003B054 File Offset: 0x00039254
		[MonoTODO("Probably not entirely correct. fusionLog needs to be set somehow (we are probably missing internal constuctor)")]
		public string FusionLog
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this.fusionLog;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name, assembly cache log, and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06000D95 RID: 3477 RVA: 0x0003B05C File Offset: 0x0003925C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("BadImageFormat_FileName", this.fileName);
			info.AddValue("BadImageFormat_FusionLog", this.fusionLog);
		}

		/// <summary>Returns the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</summary>
		/// <returns>A string containing the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06000D96 RID: 3478 RVA: 0x0003B094 File Offset: 0x00039294
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.GetType().FullName);
			stringBuilder.AppendFormat(": {0}", this.Message);
			if (this.fileName != null && this.fileName.Length > 0)
			{
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.AppendFormat("File name: '{0}'", this.fileName);
			}
			if (this.InnerException != null)
			{
				stringBuilder.AppendFormat(" ---> {0}", this.InnerException);
			}
			if (this.StackTrace != null)
			{
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(this.StackTrace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040003B7 RID: 951
		private const int Result = -2147024885;

		// Token: 0x040003B8 RID: 952
		private string fileName;

		// Token: 0x040003B9 RID: 953
		private string fusionLog;
	}
}
