using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a field that does not exist.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000151 RID: 337
	[ComVisible(true)]
	[Serializable]
	public class MissingFieldException : MissingMemberException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class.</summary>
		// Token: 0x06001220 RID: 4640 RVA: 0x00047CA8 File Offset: 0x00045EA8
		public MissingFieldException() : base(Locale.GetText("Cannot find requested field."))
		{
			base.HResult = -2146233071;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06001221 RID: 4641 RVA: 0x00047CC8 File Offset: 0x00045EC8
		public MissingFieldException(string message) : base(message)
		{
			base.HResult = -2146233071;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001222 RID: 4642 RVA: 0x00047CDC File Offset: 0x00045EDC
		protected MissingFieldException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001223 RID: 4643 RVA: 0x00047CE8 File Offset: 0x00045EE8
		public MissingFieldException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233071;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingFieldException" /> class with the specified class name and field name.</summary>
		/// <param name="className">The name of the class in which access to a nonexistent field was attempted. </param>
		/// <param name="fieldName">The name of the field that cannot be accessed. </param>
		// Token: 0x06001224 RID: 4644 RVA: 0x00047D00 File Offset: 0x00045F00
		public MissingFieldException(string className, string fieldName) : base(className, fieldName)
		{
			base.HResult = -2146233071;
		}

		/// <summary>Gets the text string showing the signature of the missing field, the class name, and the field name. This property is read-only.</summary>
		/// <returns>The error message string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00047D18 File Offset: 0x00045F18
		public override string Message
		{
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				string text = Locale.GetText("Field '{0}.{1}' not found.");
				return string.Format(text, this.ClassName, this.MemberName);
			}
		}

		// Token: 0x04000532 RID: 1330
		private const int Result = -2146233071;
	}
}
