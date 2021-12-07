using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a floating-point value is positive infinity, negative infinity, or Not-a-Number (NaN).</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015D RID: 349
	[ComVisible(true)]
	[Serializable]
	public class NotFiniteNumberException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class.</summary>
		// Token: 0x06001292 RID: 4754 RVA: 0x000494E0 File Offset: 0x000476E0
		public NotFiniteNumberException() : base(Locale.GetText("The number encountered was not a finite quantity."))
		{
			base.HResult = -2146233048;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with the invalid number.</summary>
		/// <param name="offendingNumber">The value of the argument that caused the exception. </param>
		// Token: 0x06001293 RID: 4755 RVA: 0x00049500 File Offset: 0x00047700
		public NotFiniteNumberException(double offendingNumber)
		{
			this.offending_number = offendingNumber;
			base.HResult = -2146233048;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06001294 RID: 4756 RVA: 0x0004951C File Offset: 0x0004771C
		public NotFiniteNumberException(string message) : base(message)
		{
			base.HResult = -2146233048;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message and the invalid number.</summary>
		/// <param name="message">The message that describes the error. </param>
		/// <param name="offendingNumber">The value of the argument that caused the exception. </param>
		// Token: 0x06001295 RID: 4757 RVA: 0x00049530 File Offset: 0x00047730
		public NotFiniteNumberException(string message, double offendingNumber) : base(message)
		{
			this.offending_number = offendingNumber;
			base.HResult = -2146233048;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message, the invalid number, and a reference to the inner exception that is root cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="offendingNumber">The value of the argument that caused the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001296 RID: 4758 RVA: 0x0004954C File Offset: 0x0004774C
		public NotFiniteNumberException(string message, double offendingNumber, Exception innerException) : base(message, innerException)
		{
			this.offending_number = offendingNumber;
			base.HResult = -2146233048;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001297 RID: 4759 RVA: 0x00049568 File Offset: 0x00047768
		protected NotFiniteNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.offending_number = info.GetDouble("OffendingNumber");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message and a reference to the inner exception that is root cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001298 RID: 4760 RVA: 0x00049584 File Offset: 0x00047784
		public NotFiniteNumberException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233048;
		}

		/// <summary>Gets the invalid number that is a positive infinity, a negative infinity, or Not-a-Number (NaN).</summary>
		/// <returns>The invalid number.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0004959C File Offset: 0x0004779C
		public double OffendingNumber
		{
			get
			{
				return this.offending_number;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the invalid number and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600129A RID: 4762 RVA: 0x000495A4 File Offset: 0x000477A4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("OffendingNumber", this.offending_number);
		}

		// Token: 0x04000549 RID: 1353
		private const int Result = -2146233048;

		// Token: 0x0400054A RID: 1354
		private double offending_number;
	}
}
