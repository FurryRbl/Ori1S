using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Represents a clause in a structured exception-handling block.</summary>
	// Token: 0x0200028D RID: 653
	[ComVisible(true)]
	public sealed class ExceptionHandlingClause
	{
		// Token: 0x06002166 RID: 8550 RVA: 0x0007A1CC File Offset: 0x000783CC
		internal ExceptionHandlingClause()
		{
		}

		/// <summary>Gets the type of exception handled by this clause.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents that type of exception handled by this clause, or null if the <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> property is <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" /> or <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Finally" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Invalid use of property for the object's current state.</exception>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x0007A1D4 File Offset: 0x000783D4
		public Type CatchType
		{
			get
			{
				return this.catch_type;
			}
		}

		/// <summary>Gets the offset within the method body, in bytes, of the user-supplied filter code.</summary>
		/// <returns>The offset within the method body, in bytes, of the user-supplied filter code. The value of this property has no meaning if the <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> property has any value other than <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Cannot get the offset because the exception handling clause is not a filter.</exception>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x0007A1DC File Offset: 0x000783DC
		public int FilterOffset
		{
			get
			{
				return this.filter_offset;
			}
		}

		/// <summary>Gets a value indicating whether this exception-handling clause is a finally clause, a type-filtered clause, or a user-filtered clause.</summary>
		/// <returns>An <see cref="T:System.Reflection.ExceptionHandlingClauseOptions" /> value that indicates what kind of action this clause performs.</returns>
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x0007A1E4 File Offset: 0x000783E4
		public ExceptionHandlingClauseOptions Flags
		{
			get
			{
				return this.flags;
			}
		}

		/// <summary>Gets the length, in bytes, of the body of this exception-handling clause.</summary>
		/// <returns>An integer that represents the length, in bytes, of the MSIL that forms the body of this exception-handling clause.</returns>
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x0007A1EC File Offset: 0x000783EC
		public int HandlerLength
		{
			get
			{
				return this.handler_length;
			}
		}

		/// <summary>Gets the offset within the method body, in bytes, of this exception-handling clause.</summary>
		/// <returns>An integer that represents the offset within the method body, in bytes, of this exception-handling clause.</returns>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600216B RID: 8555 RVA: 0x0007A1F4 File Offset: 0x000783F4
		public int HandlerOffset
		{
			get
			{
				return this.handler_offset;
			}
		}

		/// <summary>The total length, in bytes, of the try block that includes this exception-handling clause.</summary>
		/// <returns>The total length, in bytes, of the try block that includes this exception-handling clause.</returns>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x0007A1FC File Offset: 0x000783FC
		public int TryLength
		{
			get
			{
				return this.try_length;
			}
		}

		/// <summary>The offset within the method, in bytes, of the try block that includes this exception-handling clause.</summary>
		/// <returns>An integer that represents the offset within the method, in bytes, of the try block that includes this exception-handling clause.</returns>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x0007A204 File Offset: 0x00078404
		public int TryOffset
		{
			get
			{
				return this.try_offset;
			}
		}

		/// <summary>A string representation of the exception-handling clause.</summary>
		/// <returns>A string that lists appropriate property values for the filter clause type.</returns>
		// Token: 0x0600216E RID: 8558 RVA: 0x0007A20C File Offset: 0x0007840C
		public override string ToString()
		{
			string text = string.Format("Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}", new object[]
			{
				this.flags,
				this.try_offset,
				this.try_length,
				this.handler_offset,
				this.handler_length
			});
			if (this.catch_type != null)
			{
				text = string.Format("{0}, CatchType={1}", text, this.catch_type);
			}
			if (this.flags == ExceptionHandlingClauseOptions.Filter)
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}, FilterOffset={1}", new object[]
				{
					text,
					this.filter_offset
				});
			}
			return text;
		}

		// Token: 0x04000C4D RID: 3149
		internal Type catch_type;

		// Token: 0x04000C4E RID: 3150
		internal int filter_offset;

		// Token: 0x04000C4F RID: 3151
		internal ExceptionHandlingClauseOptions flags;

		// Token: 0x04000C50 RID: 3152
		internal int try_offset;

		// Token: 0x04000C51 RID: 3153
		internal int try_length;

		// Token: 0x04000C52 RID: 3154
		internal int handler_offset;

		// Token: 0x04000C53 RID: 3155
		internal int handler_length;
	}
}
