using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System
{
	/// <summary>Represents errors that occur during application execution.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000039 RID: 57
	[ComDefaultInterface(typeof(_Exception))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public class Exception : ISerializable, _Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class.</summary>
		// Token: 0x06000613 RID: 1555 RVA: 0x00013E34 File Offset: 0x00012034
		public Exception()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000614 RID: 1556 RVA: 0x00013E48 File Offset: 0x00012048
		public Exception(string message)
		{
			this.message = message;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0). </exception>
		// Token: 0x06000615 RID: 1557 RVA: 0x00013E64 File Offset: 0x00012064
		protected Exception(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.class_name = info.GetString("ClassName");
			this.message = info.GetString("Message");
			this.help_link = info.GetString("HelpURL");
			this.stack_trace = info.GetString("StackTraceString");
			this._remoteStackTraceString = info.GetString("RemoteStackTraceString");
			this.remote_stack_index = info.GetInt32("RemoteStackIndex");
			this.hresult = info.GetInt32("HResult");
			this.source = info.GetString("Source");
			this.inner_exception = (Exception)info.GetValue("InnerException", typeof(Exception));
			try
			{
				this._data = (IDictionary)info.GetValue("Data", typeof(IDictionary));
			}
			catch (SerializationException)
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000616 RID: 1558 RVA: 0x00013F84 File Offset: 0x00012184
		public Exception(string message, Exception innerException)
		{
			this.inner_exception = innerException;
			this.message = message;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> instance that caused the current exception.</summary>
		/// <returns>An instance of Exception that describes the error that caused the current exception. The InnerException property returns the same value as was passed into the constructor, or a null reference (Nothing in Visual Basic) if the inner exception value was not supplied to the constructor. This property is read-only.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00013FA8 File Offset: 0x000121A8
		public Exception InnerException
		{
			get
			{
				return this.inner_exception;
			}
		}

		/// <summary>Gets or sets a link to the help file associated with this exception.</summary>
		/// <returns>The Uniform Resource Name (URN) or Uniform Resource Locator (URL).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00013FB0 File Offset: 0x000121B0
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00013FB8 File Offset: 0x000121B8
		public virtual string HelpLink
		{
			get
			{
				return this.help_link;
			}
			set
			{
				this.help_link = value;
			}
		}

		/// <summary>Gets or sets HRESULT, a coded numerical value that is assigned to a specific exception.</summary>
		/// <returns>The HRESULT value.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00013FC4 File Offset: 0x000121C4
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00013FCC File Offset: 0x000121CC
		protected int HResult
		{
			get
			{
				return this.hresult;
			}
			set
			{
				this.hresult = value;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00013FD8 File Offset: 0x000121D8
		internal void SetMessage(string s)
		{
			this.message = s;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00013FE4 File Offset: 0x000121E4
		internal void SetStackTrace(string s)
		{
			this.stack_trace = s;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00013FF0 File Offset: 0x000121F0
		private string ClassName
		{
			get
			{
				if (this.class_name == null)
				{
					this.class_name = this.GetType().ToString();
				}
				return this.class_name;
			}
		}

		/// <summary>Gets a message that describes the current exception.</summary>
		/// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00014020 File Offset: 0x00012220
		public virtual string Message
		{
			get
			{
				if (this.message == null)
				{
					this.message = string.Format(Locale.GetText("Exception of type '{0}' was thrown."), this.ClassName);
				}
				return this.message;
			}
		}

		/// <summary>Gets or sets the name of the application or the object that causes the error.</summary>
		/// <returns>The name of the application or the object that causes the error.</returns>
		/// <exception cref="T:System.ArgumentException">The object must be a runtime <see cref="N:System.Reflection" /> object.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001405C File Offset: 0x0001225C
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x000140C4 File Offset: 0x000122C4
		public virtual string Source
		{
			get
			{
				if (this.source == null)
				{
					StackTrace stackTrace = new StackTrace(this, true);
					if (stackTrace.FrameCount > 0)
					{
						StackFrame frame = stackTrace.GetFrame(0);
						if (stackTrace != null)
						{
							MethodBase method = frame.GetMethod();
							if (method != null)
							{
								this.source = method.DeclaringType.Assembly.UnprotectedGetName().Name;
							}
						}
					}
				}
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		/// <summary>Gets a string representation of the frames on the call stack at the time the current exception was thrown.</summary>
		/// <returns>A string that describes the immediate frames of the call stack.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x000140D0 File Offset: 0x000122D0
		public virtual string StackTrace
		{
			get
			{
				if (this.stack_trace == null)
				{
					if (this.trace_ips == null)
					{
						return null;
					}
					StackTrace stackTrace = new StackTrace(this, 0, true, true);
					StringBuilder stringBuilder = new StringBuilder();
					string value = string.Format("{0}  {1} ", Environment.NewLine, Locale.GetText("at"));
					string text = Locale.GetText("<unknown method>");
					for (int i = 0; i < stackTrace.FrameCount; i++)
					{
						StackFrame frame = stackTrace.GetFrame(i);
						if (i == 0)
						{
							stringBuilder.AppendFormat("  {0} ", Locale.GetText("at"));
						}
						else
						{
							stringBuilder.Append(value);
						}
						if (frame.GetMethod() == null)
						{
							string internalMethodName = frame.GetInternalMethodName();
							if (internalMethodName != null)
							{
								stringBuilder.Append(internalMethodName);
							}
							else
							{
								stringBuilder.AppendFormat("<0x{0:x5}> {1}", frame.GetNativeOffset(), text);
							}
						}
						else
						{
							this.GetFullNameForStackTrace(stringBuilder, frame.GetMethod());
							if (frame.GetILOffset() == -1)
							{
								stringBuilder.AppendFormat(" <0x{0:x5}> ", frame.GetNativeOffset());
							}
							else
							{
								stringBuilder.AppendFormat(" [0x{0:x5}] ", frame.GetILOffset());
							}
							stringBuilder.AppendFormat("in {0}:{1} ", frame.GetSecureFileName(), frame.GetFileLineNumber());
						}
					}
					this.stack_trace = stringBuilder.ToString();
				}
				return this.stack_trace;
			}
		}

		/// <summary>Gets the method that throws the current exception.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> that threw the current exception.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00014244 File Offset: 0x00012444
		public MethodBase TargetSite
		{
			get
			{
				StackTrace stackTrace = new StackTrace(this, true);
				if (stackTrace.FrameCount > 0)
				{
					return stackTrace.GetFrame(0).GetMethod();
				}
				return null;
			}
		}

		/// <summary>Gets a collection of key/value pairs that provide additional user-defined information about the exception.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IDictionary" /> interface and contains a collection of user-defined key/value pairs. The default is an empty collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00014274 File Offset: 0x00012474
		public virtual IDictionary Data
		{
			get
			{
				if (this._data == null)
				{
					this._data = new Hashtable();
				}
				return this._data;
			}
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Exception" /> that is the root cause of one or more subsequent exceptions.</summary>
		/// <returns>The first exception thrown in a chain of exceptions. If the <see cref="P:System.Exception.InnerException" /> property of the current exception is a null reference (Nothing in Visual Basic), this property returns the current exception.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000625 RID: 1573 RVA: 0x00014294 File Offset: 0x00012494
		public virtual Exception GetBaseException()
		{
			for (Exception innerException = this.inner_exception; innerException != null; innerException = innerException.InnerException)
			{
				if (innerException.InnerException == null)
				{
					return innerException;
				}
			}
			return this;
		}

		/// <summary>When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic). </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06000626 RID: 1574 RVA: 0x000142D0 File Offset: 0x000124D0
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ClassName", this.ClassName);
			info.AddValue("Message", this.message);
			info.AddValue("InnerException", this.inner_exception);
			info.AddValue("HelpURL", this.help_link);
			info.AddValue("StackTraceString", this.StackTrace);
			info.AddValue("RemoteStackTraceString", this._remoteStackTraceString);
			info.AddValue("RemoteStackIndex", this.remote_stack_index);
			info.AddValue("HResult", this.hresult);
			info.AddValue("Source", this.Source);
			info.AddValue("ExceptionMethod", null);
			info.AddValue("Data", this._data, typeof(IDictionary));
		}

		/// <summary>Creates and returns a string representation of the current exception.</summary>
		/// <returns>A string representation of the current exception.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000627 RID: 1575 RVA: 0x000143B0 File Offset: 0x000125B0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.ClassName);
			stringBuilder.Append(": ").Append(this.Message);
			if (this._remoteStackTraceString != null)
			{
				stringBuilder.Append(this._remoteStackTraceString);
			}
			if (this.inner_exception != null)
			{
				stringBuilder.Append(" ---> ").Append(this.inner_exception.ToString());
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Locale.GetText("  --- End of inner exception stack trace ---"));
			}
			if (this.StackTrace != null)
			{
				stringBuilder.Append(Environment.NewLine).Append(this.StackTrace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00014464 File Offset: 0x00012664
		internal Exception FixRemotingException()
		{
			string format = (this.remote_stack_index != 0) ? Locale.GetText("{1}{0}{0}Exception rethrown at [{2}]: {0}") : Locale.GetText("{0}{0}Server stack trace: {0}{1}{0}{0}Exception rethrown at [{2}]: {0}");
			string remoteStackTraceString = string.Format(format, Environment.NewLine, this.StackTrace, this.remote_stack_index);
			this._remoteStackTraceString = remoteStackTraceString;
			this.remote_stack_index++;
			this.stack_trace = null;
			return this;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000144D0 File Offset: 0x000126D0
		internal void GetFullNameForStackTrace(StringBuilder sb, MethodBase mi)
		{
			ParameterInfo[] parameters = mi.GetParameters();
			sb.Append(mi.DeclaringType.ToString());
			sb.Append(".");
			sb.Append(mi.Name);
			if (mi.IsGenericMethod)
			{
				Type[] genericArguments = mi.GetGenericArguments();
				sb.Append("[");
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (i > 0)
					{
						sb.Append(",");
					}
					sb.Append(genericArguments[i].Name);
				}
				sb.Append("]");
			}
			sb.Append(" (");
			for (int j = 0; j < parameters.Length; j++)
			{
				if (j > 0)
				{
					sb.Append(", ");
				}
				Type parameterType = parameters[j].ParameterType;
				if (parameterType.IsClass && parameterType.Namespace != string.Empty)
				{
					sb.Append(parameterType.Namespace);
					sb.Append(".");
				}
				sb.Append(parameterType.Name);
				if (parameters[j].Name != null)
				{
					sb.Append(" ");
					sb.Append(parameters[j].Name);
				}
			}
			sb.Append(")");
		}

		/// <summary>Gets the runtime type of the current instance.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the exact runtime type of the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600062A RID: 1578 RVA: 0x0001462C File Offset: 0x0001282C
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000076 RID: 118
		private IntPtr[] trace_ips;

		// Token: 0x04000077 RID: 119
		private Exception inner_exception;

		// Token: 0x04000078 RID: 120
		internal string message;

		// Token: 0x04000079 RID: 121
		private string help_link;

		// Token: 0x0400007A RID: 122
		private string class_name;

		// Token: 0x0400007B RID: 123
		private string stack_trace;

		// Token: 0x0400007C RID: 124
		private string _remoteStackTraceString;

		// Token: 0x0400007D RID: 125
		private int remote_stack_index;

		// Token: 0x0400007E RID: 126
		internal int hresult = -2146233088;

		// Token: 0x0400007F RID: 127
		private string source;

		// Token: 0x04000080 RID: 128
		private IDictionary _data;
	}
}
