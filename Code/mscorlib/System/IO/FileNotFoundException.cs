using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
	/// <summary>The exception that is thrown when an attempt to access a file that does not exist on disk fails.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000240 RID: 576
	[ComVisible(true)]
	[Serializable]
	public class FileNotFoundException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with its message string set to a system-supplied message and its HRESULT set to COR_E_FILENOTFOUND.</summary>
		// Token: 0x06001DDB RID: 7643 RVA: 0x0006E398 File Offset: 0x0006C598
		public FileNotFoundException() : base(Locale.GetText("Unable to find the specified file."))
		{
			base.HResult = -2146232799;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with its message string set to <paramref name="message" /> and its HRESULT set to COR_E_FILENOTFOUND.</summary>
		/// <param name="message">A description of the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06001DDC RID: 7644 RVA: 0x0006E3B8 File Offset: 0x0006C5B8
		public FileNotFoundException(string message) : base(message)
		{
			base.HResult = -2146232799;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A description of the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001DDD RID: 7645 RVA: 0x0006E3CC File Offset: 0x0006C5CC
		public FileNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232799;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with its message string set to <paramref name="message" />, specifying the file name that cannot be found, and its HRESULT set to COR_E_FILENOTFOUND.</summary>
		/// <param name="message">A description of the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="fileName">The full name of the file with the invalid image. </param>
		// Token: 0x06001DDE RID: 7646 RVA: 0x0006E3E4 File Offset: 0x0006C5E4
		public FileNotFoundException(string message, string fileName) : base(message)
		{
			base.HResult = -2146232799;
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="fileName">The full name of the file with the invalid image. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001DDF RID: 7647 RVA: 0x0006E400 File Offset: 0x0006C600
		public FileNotFoundException(string message, string fileName, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232799;
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileNotFoundException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">An object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">An object that contains contextual information about the source or destination. </param>
		// Token: 0x06001DE0 RID: 7648 RVA: 0x0006E41C File Offset: 0x0006C61C
		protected FileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = info.GetString("FileNotFound_FileName");
			this.fusionLog = info.GetString("FileNotFound_FusionLog");
		}

		/// <summary>Gets the name of the file that cannot be found.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the file, or null if no file name was passed to the constructor for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0006E454 File Offset: 0x0006C654
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		/// <summary>Gets the log file that describes why loading of an assembly failed.</summary>
		/// <returns>A String containing errors reported by the assembly cache.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0006E45C File Offset: 0x0006C65C
		public string FusionLog
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this.fusionLog;
			}
		}

		/// <summary>Gets the error message that explains the reason for the exception.</summary>
		/// <returns>A string containing the error message.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0006E464 File Offset: 0x0006C664
		public override string Message
		{
			get
			{
				if (this.message == null && this.fileName != null)
				{
					return string.Format(CultureInfo.CurrentCulture, "Could not load file or assembly '{0}' or one of its dependencies. The system cannot find the file specified.", new object[]
					{
						this.fileName
					});
				}
				return this.message;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The object that contains contextual information about the source or destination. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001DE4 RID: 7652 RVA: 0x0006E4B0 File Offset: 0x0006C6B0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileNotFound_FileName", this.fileName);
			info.AddValue("FileNotFound_FusionLog", this.fusionLog);
		}

		/// <summary>Returns the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</summary>
		/// <returns>A string containing the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001DE5 RID: 7653 RVA: 0x0006E4E8 File Offset: 0x0006C6E8
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

		// Token: 0x04000B21 RID: 2849
		private const int Result = -2146232799;

		// Token: 0x04000B22 RID: 2850
		private string fileName;

		// Token: 0x04000B23 RID: 2851
		private string fusionLog;
	}
}
