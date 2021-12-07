using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.ComponentModel
{
	/// <summary>The exception thrown when using invalid arguments that are enumerators.</summary>
	// Token: 0x0200016A RID: 362
	[Serializable]
	public class InvalidEnumArgumentException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class without a message.</summary>
		// Token: 0x06000CC3 RID: 3267 RVA: 0x000204F8 File Offset: 0x0001E6F8
		public InvalidEnumArgumentException() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with the specified message.</summary>
		/// <param name="message">The message to display with this exception. </param>
		// Token: 0x06000CC4 RID: 3268 RVA: 0x00020504 File Offset: 0x0001E704
		public InvalidEnumArgumentException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with a message generated from the argument, the invalid value, and an enumeration class.</summary>
		/// <param name="argumentName">The name of the argument that caused the exception. </param>
		/// <param name="invalidValue">The value of the argument that failed. </param>
		/// <param name="enumClass">A <see cref="T:System.Type" /> that represents the enumeration class with the valid values. </param>
		// Token: 0x06000CC5 RID: 3269 RVA: 0x00020510 File Offset: 0x0001E710
		public InvalidEnumArgumentException(string argumentName, int invalidValue, Type enumClass) : base(string.Format(CultureInfo.CurrentCulture, "The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.", new object[]
		{
			argumentName,
			invalidValue,
			enumClass.Name
		}), argumentName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06000CC6 RID: 3270 RVA: 0x00020550 File Offset: 0x0001E750
		public InvalidEnumArgumentException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002055C File Offset: 0x0001E75C
		protected InvalidEnumArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
