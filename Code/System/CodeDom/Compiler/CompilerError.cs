using System;
using System.Globalization;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a compiler error or warning.</summary>
	// Token: 0x02000080 RID: 128
	[Serializable]
	public class CompilerError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class.</summary>
		// Token: 0x0600053C RID: 1340 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		public CompilerError() : this(string.Empty, 0, 0, string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class using the specified file name, line, column, error number, and error text.</summary>
		/// <param name="fileName">The file name of the file that the compiler was compiling when it encountered the error. </param>
		/// <param name="line">The line of the source of the error. </param>
		/// <param name="column">The column of the source of the error. </param>
		/// <param name="errorNumber">The error number of the error. </param>
		/// <param name="errorText">The error message text. </param>
		// Token: 0x0600053D RID: 1341 RVA: 0x0001100C File Offset: 0x0000F20C
		public CompilerError(string fileName, int line, int column, string errorNumber, string errorText)
		{
			this.fileName = fileName;
			this.line = line;
			this.column = column;
			this.errorNumber = errorNumber;
			this.errorText = errorText;
		}

		/// <summary>Provides an implementation of Object's <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string representation of the compiler error.</returns>
		// Token: 0x0600053E RID: 1342 RVA: 0x0001103C File Offset: 0x0000F23C
		public override string ToString()
		{
			string text = (!this.isWarning) ? "error" : "warning";
			return string.Format(CultureInfo.InvariantCulture, "{0}({1},{2}) : {3} {4}: {5}", new object[]
			{
				this.fileName,
				this.line,
				this.column,
				text,
				this.errorNumber,
				this.errorText
			});
		}

		/// <summary>Gets or sets the line number where the source of the error occurs.</summary>
		/// <returns>The line number of the source file where the compiler encountered the error.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000110B4 File Offset: 0x0000F2B4
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x000110BC File Offset: 0x0000F2BC
		public int Line
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		/// <summary>Gets or sets the column number where the source of the error occurs.</summary>
		/// <returns>The column number of the source file where the compiler encountered the error.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x000110C8 File Offset: 0x0000F2C8
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x000110D0 File Offset: 0x0000F2D0
		public int Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		/// <summary>Gets or sets the error number.</summary>
		/// <returns>The error number as a string.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x000110DC File Offset: 0x0000F2DC
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x000110E4 File Offset: 0x0000F2E4
		public string ErrorNumber
		{
			get
			{
				return this.errorNumber;
			}
			set
			{
				this.errorNumber = value;
			}
		}

		/// <summary>Gets or sets the text of the error message.</summary>
		/// <returns>The text of the error message.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000110F0 File Offset: 0x0000F2F0
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x000110F8 File Offset: 0x0000F2F8
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
			set
			{
				this.errorText = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the error is a warning.</summary>
		/// <returns>true if the error is a warning; otherwise, false.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00011104 File Offset: 0x0000F304
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0001110C File Offset: 0x0000F30C
		public bool IsWarning
		{
			get
			{
				return this.isWarning;
			}
			set
			{
				this.isWarning = value;
			}
		}

		/// <summary>Gets or sets the file name of the source file that contains the code which caused the error.</summary>
		/// <returns>The file name of the source file that contains the code which caused the error.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00011118 File Offset: 0x0000F318
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x00011120 File Offset: 0x0000F320
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x04000137 RID: 311
		private string fileName;

		// Token: 0x04000138 RID: 312
		private int line;

		// Token: 0x04000139 RID: 313
		private int column;

		// Token: 0x0400013A RID: 314
		private string errorNumber;

		// Token: 0x0400013B RID: 315
		private string errorText;

		// Token: 0x0400013C RID: 316
		private bool isWarning;
	}
}
