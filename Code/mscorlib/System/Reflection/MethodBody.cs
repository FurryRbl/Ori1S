using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides access to the metadata and MSIL for the body of a method.</summary>
	// Token: 0x0200029B RID: 667
	[ComVisible(true)]
	public sealed class MethodBody
	{
		// Token: 0x060021D1 RID: 8657 RVA: 0x0007ACC0 File Offset: 0x00078EC0
		internal MethodBody()
		{
		}

		/// <summary>Gets a list that includes all the exception-handling clauses in the method body.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.ExceptionHandlingClause" /> objects representing the exception-handling clauses in the body of the method.</returns>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0007ACC8 File Offset: 0x00078EC8
		public IList<ExceptionHandlingClause> ExceptionHandlingClauses
		{
			get
			{
				return Array.AsReadOnly<ExceptionHandlingClause>(this.clauses);
			}
		}

		/// <summary>Gets the list of local variables declared in the method body.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.LocalVariableInfo" /> objects that describe the local variables declared in the method body.</returns>
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0007ACD8 File Offset: 0x00078ED8
		public IList<LocalVariableInfo> LocalVariables
		{
			get
			{
				return Array.AsReadOnly<LocalVariableInfo>(this.locals);
			}
		}

		/// <summary>Gets a value indicating whether local variables in the method body are initialized to the default values for their types.</summary>
		/// <returns>true if the method body contains code to initialize local variables to null for reference types, or to the zero-initialized value for value types; otherwise, false.</returns>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x0007ACE8 File Offset: 0x00078EE8
		public bool InitLocals
		{
			get
			{
				return this.init_locals;
			}
		}

		/// <summary>Gets a metadata token for the signature that describes the local variables for the method in metadata.</summary>
		/// <returns>An integer that represents the metadata token.</returns>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x0007ACF0 File Offset: 0x00078EF0
		public int LocalSignatureMetadataToken
		{
			get
			{
				return this.sig_token;
			}
		}

		/// <summary>Gets the maximum number of items on the operand stack when the method is executing.</summary>
		/// <returns>The maximum number of items on the operand stack when the method is executing.</returns>
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x0007ACF8 File Offset: 0x00078EF8
		public int MaxStackSize
		{
			get
			{
				return this.max_stack;
			}
		}

		/// <summary>Returns the MSIL for the method body, as an array of bytes.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> that contains the MSIL for the method body. </returns>
		// Token: 0x060021D7 RID: 8663 RVA: 0x0007AD00 File Offset: 0x00078F00
		public byte[] GetILAsByteArray()
		{
			return this.il;
		}

		// Token: 0x04000CAD RID: 3245
		private ExceptionHandlingClause[] clauses;

		// Token: 0x04000CAE RID: 3246
		private LocalVariableInfo[] locals;

		// Token: 0x04000CAF RID: 3247
		private byte[] il;

		// Token: 0x04000CB0 RID: 3248
		private bool init_locals;

		// Token: 0x04000CB1 RID: 3249
		private int sig_token;

		// Token: 0x04000CB2 RID: 3250
		private int max_stack;
	}
}
