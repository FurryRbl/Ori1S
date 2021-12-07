using System;
using System.Collections;
using System.Configuration.Internal;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Configuration
{
	/// <summary>The current value is not one of the <see cref="P:System.Web.Configuration.PagesSection.EnableSessionState" /> values.</summary>
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class ConfigurationErrorsException : ConfigurationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		// Token: 0x06000189 RID: 393 RVA: 0x00005C9C File Offset: 0x00003E9C
		public ConfigurationErrorsException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		// Token: 0x0600018A RID: 394 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public ConfigurationErrorsException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		/// <exception cref="T:System.InvalidOperationException">The current type is not a <see cref="T:System.Configuration.ConfigurationException" /> or a <see cref="T:System.Configuration.ConfigurationErrorsException" />.</exception>
		// Token: 0x0600018B RID: 395 RVA: 0x00005CB0 File Offset: 0x00003EB0
		protected ConfigurationErrorsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filename = info.GetString("ConfigurationErrors_Filename");
			this.line = info.GetInt32("ConfigurationErrors_Line");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x0600018C RID: 396 RVA: 0x00005CE8 File Offset: 0x00003EE8
		public ConfigurationErrorsException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x0600018D RID: 397 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public ConfigurationErrorsException(string message, XmlNode node) : this(message, null, ConfigurationErrorsException.GetFilename(node), ConfigurationErrorsException.GetLineNumber(node))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x0600018E RID: 398 RVA: 0x00005D0C File Offset: 0x00003F0C
		public ConfigurationErrorsException(string message, Exception inner, XmlNode node) : this(message, inner, ConfigurationErrorsException.GetFilename(node), ConfigurationErrorsException.GetLineNumber(node))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x0600018F RID: 399 RVA: 0x00005D24 File Offset: 0x00003F24
		public ConfigurationErrorsException(string message, XmlReader reader) : this(message, null, ConfigurationErrorsException.GetFilename(reader), ConfigurationErrorsException.GetLineNumber(reader))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x06000190 RID: 400 RVA: 0x00005D3C File Offset: 0x00003F3C
		public ConfigurationErrorsException(string message, Exception inner, XmlReader reader) : this(message, inner, ConfigurationErrorsException.GetFilename(reader), ConfigurationErrorsException.GetLineNumber(reader))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		// Token: 0x06000191 RID: 401 RVA: 0x00005D54 File Offset: 0x00003F54
		public ConfigurationErrorsException(string message, string filename, int line) : this(message, null, filename, line)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		// Token: 0x06000192 RID: 402 RVA: 0x00005D60 File Offset: 0x00003F60
		public ConfigurationErrorsException(string message, Exception inner, string filename, int line) : base(message, inner)
		{
			this.filename = filename;
			this.line = line;
		}

		/// <summary>Gets a description of why this configuration exception was thrown.</summary>
		/// <returns>A description of why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> was thrown.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005D7C File Offset: 0x00003F7C
		public override string BareMessage
		{
			get
			{
				return base.BareMessage;
			}
		}

		/// <summary>Gets a collection of errors that detail the reasons this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object that contains errors that identify the reasons this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00005D84 File Offset: 0x00003F84
		public ICollection Errors
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the path to the configuration file that caused this configuration exception to be thrown.</summary>
		/// <returns>The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> to be thrown.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005D8C File Offset: 0x00003F8C
		public override string Filename
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>Gets the line number within the configuration file at which this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00005D94 File Offset: 0x00003F94
		public override int Line
		{
			get
			{
				return this.line;
			}
		}

		/// <summary>Gets an extended description of why this configuration exception was thrown.</summary>
		/// <returns>An extended description of why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005D9C File Offset: 0x00003F9C
		public override string Message
		{
			get
			{
				string result;
				if (!string.IsNullOrEmpty(this.filename))
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

		/// <summary>Gets the path to the configuration file that the internal <see cref="T:System.Xml.XmlReader" /> was reading when this configuration exception was thrown.</summary>
		/// <returns>The path of the configuration file the internal <see cref="T:System.Xml.XmlReader" /> object was accessing when the exception occurred.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x06000198 RID: 408 RVA: 0x00005E7C File Offset: 0x0000407C
		public static string GetFilename(XmlReader reader)
		{
			if (reader is IConfigErrorInfo)
			{
				return ((IConfigErrorInfo)reader).Filename;
			}
			return (reader == null) ? null : reader.BaseURI;
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlReader" /> object was processing when this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file that the <see cref="T:System.Xml.XmlReader" /> object was accessing when the exception occurred.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x06000199 RID: 409 RVA: 0x00005EA8 File Offset: 0x000040A8
		public static int GetLineNumber(XmlReader reader)
		{
			if (reader is IConfigErrorInfo)
			{
				return ((IConfigErrorInfo)reader).LineNumber;
			}
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			return (xmlLineInfo == null) ? 0 : xmlLineInfo.LineNumber;
		}

		/// <summary>Gets the path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown.</summary>
		/// <returns>The path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown. </returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x0600019A RID: 410 RVA: 0x00005EE8 File Offset: 0x000040E8
		public static string GetFilename(XmlNode node)
		{
			if (!(node is IConfigErrorInfo))
			{
				return null;
			}
			return ((IConfigErrorInfo)node).Filename;
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlNode" /> object represented when this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file that contains the <see cref="T:System.Xml.XmlNode" /> object being parsed when this configuration exception was thrown.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x0600019B RID: 411 RVA: 0x00005F04 File Offset: 0x00004104
		public static int GetLineNumber(XmlNode node)
		{
			if (!(node is IConfigErrorInfo))
			{
				return 0;
			}
			return ((IConfigErrorInfo)node).LineNumber;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and line number at which this configuration exception occurred.</summary>
		/// <param name="info">The object that holds the information to be serialized.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x0600019C RID: 412 RVA: 0x00005F20 File Offset: 0x00004120
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ConfigurationErrors_Filename", this.filename);
			info.AddValue("ConfigurationErrors_Line", this.line);
		}

		// Token: 0x0400007A RID: 122
		private readonly string filename;

		// Token: 0x0400007B RID: 123
		private readonly int line;
	}
}
