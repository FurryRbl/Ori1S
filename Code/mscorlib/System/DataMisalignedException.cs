using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>The exception that is thrown when a unit of data is read from or written to an address that is not a multiple of the data size. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011D RID: 285
	[ComVisible(true)]
	[Serializable]
	public sealed class DataMisalignedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DataMisalignedException" /> class. </summary>
		// Token: 0x06000FDD RID: 4061 RVA: 0x0003F938 File Offset: 0x0003DB38
		public DataMisalignedException() : base(Locale.GetText("A datatype misalignment was detected in a load or store instruction."))
		{
			base.HResult = -2146233023;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DataMisalignedException" /> class using the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> object that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06000FDE RID: 4062 RVA: 0x0003F958 File Offset: 0x0003DB58
		public DataMisalignedException(string message) : base(message)
		{
			base.HResult = -2146233023;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DataMisalignedException" /> class using the specified error message and underlying exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> object that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current <see cref="T:System.DataMisalignedException" />. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000FDF RID: 4063 RVA: 0x0003F96C File Offset: 0x0003DB6C
		public DataMisalignedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233023;
		}

		// Token: 0x04000493 RID: 1171
		private const int Result = -2146233023;
	}
}
