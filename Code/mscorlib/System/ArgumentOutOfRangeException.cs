using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the value of an argument is outside the allowable range of values as defined by the invoked method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000102 RID: 258
	[ComVisible(true)]
	[Serializable]
	public class ArgumentOutOfRangeException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class.</summary>
		// Token: 0x06000D79 RID: 3449 RVA: 0x0003AD88 File Offset: 0x00038F88
		public ArgumentOutOfRangeException() : base(Locale.GetText("Argument is out of range."))
		{
			base.HResult = -2146233086;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that causes this exception. </param>
		// Token: 0x06000D7A RID: 3450 RVA: 0x0003ADA8 File Offset: 0x00038FA8
		public ArgumentOutOfRangeException(string paramName) : base(Locale.GetText("Argument is out of range."), paramName)
		{
			base.HResult = -2146233086;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000D7B RID: 3451 RVA: 0x0003ADC8 File Offset: 0x00038FC8
		public ArgumentOutOfRangeException(string paramName, string message) : base(message, paramName)
		{
			base.HResult = -2146233086;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with a specified error message, the parameter name, and the value of the argument.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		/// <param name="actualValue">The value of the argument that causes this exception. </param>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000D7C RID: 3452 RVA: 0x0003ADE0 File Offset: 0x00038FE0
		public ArgumentOutOfRangeException(string paramName, object actualValue, string message) : base(message, paramName)
		{
			this.actual_value = actualValue;
			base.HResult = -2146233086;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">An object that describes the source or destination of the serialized data. </param>
		// Token: 0x06000D7D RID: 3453 RVA: 0x0003ADFC File Offset: 0x00038FFC
		protected ArgumentOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actual_value = info.GetString("ActualValue");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with a specified error message and the exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for this exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000D7E RID: 3454 RVA: 0x0003AE18 File Offset: 0x00039018
		public ArgumentOutOfRangeException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233086;
		}

		/// <summary>Gets the argument value that causes this exception.</summary>
		/// <returns>An Object that contains the value of the parameter that caused the current <see cref="T:System.Exception" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x0003AE30 File Offset: 0x00039030
		public virtual object ActualValue
		{
			get
			{
				return this.actual_value;
			}
		}

		/// <summary>Gets the error message and the string representation of the invalid argument value, or only the error message if the argument value is null.</summary>
		/// <returns>The text message for this exception. The value of this property takes one of two forms, as follows.Condition Value The <paramref name="actualValue" /> is null. The <paramref name="message" /> string passed to the constructor. The <paramref name="actualValue" /> is not null. The <paramref name="message" /> string appended with the string representation of the invalid argument value. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0003AE38 File Offset: 0x00039038
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (this.actual_value == null)
				{
					return message;
				}
				return message + Environment.NewLine + this.actual_value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the invalid argument value and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">An object that describes the source or destination of the serialized data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000D81 RID: 3457 RVA: 0x0003AE6C File Offset: 0x0003906C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ActualValue", this.actual_value);
		}

		// Token: 0x040003A1 RID: 929
		private const int Result = -2146233086;

		// Token: 0x040003A2 RID: 930
		private object actual_value;
	}
}
