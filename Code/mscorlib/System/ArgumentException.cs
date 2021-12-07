using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when one of the arguments provided to a method is not valid.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000100 RID: 256
	[ComVisible(true)]
	[Serializable]
	public class ArgumentException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class.</summary>
		// Token: 0x06000D6B RID: 3435 RVA: 0x0003ABEC File Offset: 0x00038DEC
		public ArgumentException() : base(Locale.GetText("Value does not fall within the expected range."))
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06000D6C RID: 3436 RVA: 0x0003AC0C File Offset: 0x00038E0C
		public ArgumentException(string message) : base(message)
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000D6D RID: 3437 RVA: 0x0003AC20 File Offset: 0x00038E20
		public ArgumentException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="paramName">The name of the parameter that caused the current exception. </param>
		// Token: 0x06000D6E RID: 3438 RVA: 0x0003AC38 File Offset: 0x00038E38
		public ArgumentException(string message, string paramName) : base(message)
		{
			this.param_name = paramName;
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message, the parameter name, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="paramName">The name of the parameter that caused the current exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000D6F RID: 3439 RVA: 0x0003AC54 File Offset: 0x00038E54
		public ArgumentException(string message, string paramName, Exception innerException) : base(message, innerException)
		{
			this.param_name = paramName;
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000D70 RID: 3440 RVA: 0x0003AC70 File Offset: 0x00038E70
		protected ArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.param_name = info.GetString("ParamName");
		}

		/// <summary>Gets the name of the parameter that causes this exception.</summary>
		/// <returns>The parameter name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0003AC8C File Offset: 0x00038E8C
		public virtual string ParamName
		{
			get
			{
				return this.param_name;
			}
		}

		/// <summary>Gets the error message and the parameter name, or only the error message if no parameter name is set.</summary>
		/// <returns>A text string describing the details of the exception. The value of this property takes one of two forms: Condition Value The <paramref name="paramName" /> is a null reference (Nothing in Visual Basic) or of zero length. The <paramref name="message" /> string passed to the constructor. The <paramref name="paramName" /> is not null reference (Nothing in Visual Basic) and it has a length greater than zero. The <paramref name="message" /> string appended with the name of the invalid parameter. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0003AC94 File Offset: 0x00038E94
		public override string Message
		{
			get
			{
				if (this.ParamName != null && this.ParamName.Length != 0)
				{
					return base.Message + Environment.NewLine + Locale.GetText("Parameter name: ") + this.ParamName;
				}
				return base.Message;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is a null reference (Nothing in Visual Basic). </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000D73 RID: 3443 RVA: 0x0003ACE4 File Offset: 0x00038EE4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ParamName", this.ParamName);
		}

		// Token: 0x0400039E RID: 926
		private const int Result = -2147024809;

		// Token: 0x0400039F RID: 927
		private string param_name;
	}
}
