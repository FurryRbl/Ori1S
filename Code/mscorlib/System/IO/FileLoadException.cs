using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
	/// <summary>The exception that is thrown when a managed assembly is found but cannot be loaded.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200023E RID: 574
	[ComVisible(true)]
	[Serializable]
	public class FileLoadException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "Could not load the specified file." This message takes into account the current system culture.</summary>
		// Token: 0x06001DD0 RID: 7632 RVA: 0x0006E1B8 File Offset: 0x0006C3B8
		public FileLoadException() : base(Locale.GetText("I/O Error"))
		{
			base.HResult = -2147024894;
			this.msg = Locale.GetText("I/O Error");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06001DD1 RID: 7633 RVA: 0x0006E1F0 File Offset: 0x0006C3F0
		public FileLoadException(string message) : base(message)
		{
			base.HResult = -2147024894;
			this.msg = message;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with a specified error message and the name of the file that could not be loaded.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="fileName">A <see cref="T:System.String" /> containing the name of the file that was not loaded. </param>
		// Token: 0x06001DD2 RID: 7634 RVA: 0x0006E20C File Offset: 0x0006C40C
		public FileLoadException(string message, string fileName) : base(message)
		{
			base.HResult = -2147024894;
			this.msg = message;
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001DD3 RID: 7635 RVA: 0x0006E23C File Offset: 0x0006C43C
		public FileLoadException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147024894;
			this.msg = message;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with a specified error message, the name of the file that could not be loaded, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="fileName">A <see cref="T:System.String" /> containing the name of the file that was not loaded. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001DD4 RID: 7636 RVA: 0x0006E258 File Offset: 0x0006C458
		public FileLoadException(string message, string fileName, Exception inner) : base(message, inner)
		{
			base.HResult = -2147024894;
			this.msg = message;
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06001DD5 RID: 7637 RVA: 0x0006E27C File Offset: 0x0006C47C
		protected FileLoadException(SerializationInfo info, StreamingContext context)
		{
			this.fileName = info.GetString("FileLoad_FileName");
			this.fusionLog = info.GetString("FileLoad_FusionLog");
		}

		/// <summary>Gets the error message and the name of the file that caused this exception.</summary>
		/// <returns>A string containing the error message and the name of the file that caused this exception.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x0006E2B4 File Offset: 0x0006C4B4
		public override string Message
		{
			get
			{
				return this.msg;
			}
		}

		/// <summary>Gets the name of the file that causes this exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the file with the invalid image, or a null reference if no file name was passed to the constructor for the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0006E2BC File Offset: 0x0006C4BC
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		/// <summary>Gets the log file that describes why an assembly load failed.</summary>
		/// <returns>A string containing errors reported by the assembly cache.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x0006E2C4 File Offset: 0x0006C4C4
		public string FusionLog
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this.fusionLog;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the file name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001DD9 RID: 7641 RVA: 0x0006E2CC File Offset: 0x0006C4CC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileLoad_FileName", this.fileName);
			info.AddValue("FileLoad_FusionLog", this.fusionLog);
		}

		/// <summary>Returns the fully qualified name of the current exception, and possibly the error message, the name of the inner exception, and the stack trace.</summary>
		/// <returns>A string containing the fully qualified name of this exception, and possibly the error message, the name of the inner exception, and the stack trace, depending on which <see cref="T:System.IO.FileLoadException" /> constructor is used.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001DDA RID: 7642 RVA: 0x0006E304 File Offset: 0x0006C504
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.GetType().FullName);
			stringBuilder.AppendFormat(": {0}", this.msg);
			if (this.fileName != null)
			{
				stringBuilder.AppendFormat(" : {0}", this.fileName);
			}
			if (this.InnerException != null)
			{
				stringBuilder.AppendFormat(" ----> {0}", this.InnerException);
			}
			if (this.StackTrace != null)
			{
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(this.StackTrace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000B16 RID: 2838
		private const int Result = -2147024894;

		// Token: 0x04000B17 RID: 2839
		private string msg;

		// Token: 0x04000B18 RID: 2840
		private string fileName;

		// Token: 0x04000B19 RID: 2841
		private string fusionLog;
	}
}
