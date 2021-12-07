using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Configuration
{
	/// <summary>The exception that is thrown when a configuration system error has occurred.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CE RID: 462
	[Serializable]
	public class ConfigurationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		// Token: 0x0600101E RID: 4126 RVA: 0x0002AB68 File Offset: 0x00028D68
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException() : this(null)
		{
			this.filename = null;
			this.line = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		// Token: 0x0600101F RID: 4127 RVA: 0x0002AB80 File Offset: 0x00028D80
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		// Token: 0x06001020 RID: 4128 RVA: 0x0002AB8C File Offset: 0x00028D8C
		protected ConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filename = info.GetString("filename");
			this.line = info.GetInt32("line");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown, if any.</param>
		// Token: 0x06001021 RID: 4129 RVA: 0x0002ABC4 File Offset: 0x00028DC4
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		// Token: 0x06001022 RID: 4130 RVA: 0x0002ABD0 File Offset: 0x00028DD0
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, XmlNode node) : base(message)
		{
			this.filename = ConfigurationException.GetXmlNodeFilename(node);
			this.line = ConfigurationException.GetXmlNodeLineNumber(node);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown, if any.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		// Token: 0x06001023 RID: 4131 RVA: 0x0002ABF4 File Offset: 0x00028DF4
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner, XmlNode node) : base(message, inner)
		{
			this.filename = ConfigurationException.GetXmlNodeFilename(node);
			this.line = ConfigurationException.GetXmlNodeLineNumber(node);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationException" /> was thrown.</param>
		// Token: 0x06001024 RID: 4132 RVA: 0x0002AC24 File Offset: 0x00028E24
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, string filename, int line) : base(message)
		{
			this.filename = filename;
			this.line = line;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown, if any.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationException" /> was thrown.</param>
		// Token: 0x06001025 RID: 4133 RVA: 0x0002AC3C File Offset: 0x00028E3C
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner, string filename, int line) : base(message, inner)
		{
			this.filename = filename;
			this.line = line;
		}

		/// <summary>Gets a description of why this configuration exception was thrown.</summary>
		/// <returns>A description of why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0002AC58 File Offset: 0x00028E58
		public virtual string BareMessage
		{
			get
			{
				return base.Message;
			}
		}

		/// <summary>Gets the path to the configuration file that caused this configuration exception to be thrown.</summary>
		/// <returns>The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationException" /> exception to be thrown.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0002AC60 File Offset: 0x00028E60
		public virtual string Filename
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>Gets the line number within the configuration file at which this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0002AC68 File Offset: 0x00028E68
		public virtual int Line
		{
			get
			{
				return this.line;
			}
		}

		/// <summary>Gets an extended description of why this configuration exception was thrown.</summary>
		/// <returns>An extended description of why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0002AC70 File Offset: 0x00028E70
		public override string Message
		{
			get
			{
				string result;
				if (this.filename != null && this.filename.Length != 0)
				{
					if (this.line != 0)
					{
						result = string.Concat(new object[]
						{
							this.BareMessage,
							" (",
							this.filename,
							" line ",
							this.line,
							")"
						});
					}
					else
					{
						result = this.BareMessage + " (" + this.filename + ")";
					}
				}
				else if (this.line != 0)
				{
					result = string.Concat(new object[]
					{
						this.BareMessage,
						" (line ",
						this.line,
						")"
					});
				}
				else
				{
					result = this.BareMessage;
				}
				return result;
			}
		}

		/// <summary>Gets the path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown.</summary>
		/// <returns>A string representing the node file name.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> exception to be thrown.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600102A RID: 4138 RVA: 0x0002AD58 File Offset: 0x00028F58
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public static string GetXmlNodeFilename(XmlNode node)
		{
			if (!(node is IConfigXmlNode))
			{
				return string.Empty;
			}
			return ((IConfigXmlNode)node).Filename;
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlNode" /> object represented when this configuration exception was thrown.</summary>
		/// <returns>An int representing the node line number.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> exception to be thrown.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600102B RID: 4139 RVA: 0x0002AD78 File Offset: 0x00028F78
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public static int GetXmlNodeLineNumber(XmlNode node)
		{
			if (!(node is IConfigXmlNode))
			{
				return 0;
			}
			return ((IConfigXmlNode)node).LineNumber;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and line number at which this configuration exception occurred.</summary>
		/// <param name="info">The object that holds the information to be serialized.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600102C RID: 4140 RVA: 0x0002AD94 File Offset: 0x00028F94
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filename", this.filename);
			info.AddValue("line", this.line);
		}

		// Token: 0x0400047E RID: 1150
		private readonly string filename;

		// Token: 0x0400047F RID: 1151
		private readonly int line;
	}
}
