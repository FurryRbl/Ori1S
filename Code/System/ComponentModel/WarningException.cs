using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Specifies an exception that is handled as a warning instead of an error.</summary>
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public class WarningException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message and no Help file.</summary>
		/// <param name="message">The message to display to the end user. </param>
		// Token: 0x06000FCB RID: 4043 RVA: 0x000297E8 File Offset: 0x000279E8
		public WarningException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message, and with access to the specified Help file.</summary>
		/// <param name="message">The message to display to the end user. </param>
		/// <param name="helpUrl">The Help file to display if the user requests help. </param>
		// Token: 0x06000FCC RID: 4044 RVA: 0x000297F4 File Offset: 0x000279F4
		public WarningException(string message, string helpUrl) : base(message)
		{
			this.helpUrl = helpUrl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message, and with access to the specified Help file and topic.</summary>
		/// <param name="message">The message to display to the end user. </param>
		/// <param name="helpUrl">The Help file to display if the user requests help. </param>
		/// <param name="helpTopic">The Help topic to display if the user requests help. </param>
		// Token: 0x06000FCD RID: 4045 RVA: 0x00029804 File Offset: 0x00027A04
		public WarningException(string message, string helpUrl, string helpTopic) : base(message)
		{
			this.helpUrl = helpUrl;
			this.helpTopic = helpTopic;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class. </summary>
		// Token: 0x06000FCE RID: 4046 RVA: 0x0002981C File Offset: 0x00027A1C
		public WarningException() : base(Locale.GetText("Warning"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06000FCF RID: 4047 RVA: 0x00029830 File Offset: 0x00027A30
		public WarningException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06000FD0 RID: 4048 RVA: 0x0002983C File Offset: 0x00027A3C
		protected WarningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			try
			{
				this.helpTopic = info.GetString("helpTopic");
				this.helpUrl = info.GetString("helpUrl");
			}
			catch (SerializationException)
			{
				this.helpTopic = info.GetString("HelpTopic");
				this.helpUrl = info.GetString("HelpUrl");
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the parameter name and additional exception information.</summary>
		/// <param name="info">Stores the data that was being used to serialize or deserialize the object that the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> was serializing or deserializing. </param>
		/// <param name="context">Describes the source and destination of the stream that generated the exception, as well as a means for serialization to retain that context and an additional caller-defined context. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06000FD1 RID: 4049 RVA: 0x000298BC File Offset: 0x00027ABC
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("helpTopic", this.helpTopic);
			info.AddValue("helpUrl", this.helpUrl);
		}

		/// <summary>Gets the Help topic associated with the warning.</summary>
		/// <returns>The Help topic associated with the warning.</returns>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x000298FC File Offset: 0x00027AFC
		public string HelpTopic
		{
			get
			{
				return this.helpTopic;
			}
		}

		/// <summary>Gets the Help file associated with the warning.</summary>
		/// <returns>The Help file associated with the warning.</returns>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00029904 File Offset: 0x00027B04
		public string HelpUrl
		{
			get
			{
				return this.helpUrl;
			}
		}

		// Token: 0x04000468 RID: 1128
		private string helpUrl;

		// Token: 0x04000469 RID: 1129
		private string helpTopic;
	}
}
