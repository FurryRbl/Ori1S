using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a code checksum pragma code entity.  </summary>
	// Token: 0x02000030 RID: 48
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeChecksumPragma : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class. </summary>
		// Token: 0x060001AC RID: 428 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public CodeChecksumPragma()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class using a file name, a GUID representing the checksum algorithm, and a byte stream representing the checksum data.</summary>
		/// <param name="fileName">The path to the checksum file.</param>
		/// <param name="checksumAlgorithmId">A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</param>
		/// <param name="checksumData">A byte array that contains the checksum data.</param>
		// Token: 0x060001AD RID: 429 RVA: 0x0000AD54 File Offset: 0x00008F54
		public CodeChecksumPragma(string fileName, Guid checksumAlgorithmId, byte[] checksumData)
		{
			this.fileName = fileName;
			this.checksumAlgorithmId = checksumAlgorithmId;
			this.checksumData = checksumData;
		}

		/// <summary>Gets or sets a GUID that identifies the checksum algorithm to use.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000AD74 File Offset: 0x00008F74
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000AD7C File Offset: 0x00008F7C
		public Guid ChecksumAlgorithmId
		{
			get
			{
				return this.checksumAlgorithmId;
			}
			set
			{
				this.checksumAlgorithmId = value;
			}
		}

		/// <summary>Gets or sets the value of the data for the checksum calculation.</summary>
		/// <returns>A byte array that contains the data for the checksum calculation.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000AD88 File Offset: 0x00008F88
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000AD90 File Offset: 0x00008F90
		public byte[] ChecksumData
		{
			get
			{
				return this.checksumData;
			}
			set
			{
				this.checksumData = value;
			}
		}

		/// <summary>Gets or sets the path to the checksum file.</summary>
		/// <returns>The path to the checksum file.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000AD9C File Offset: 0x00008F9C
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		public string FileName
		{
			get
			{
				if (this.fileName == null)
				{
					return string.Empty;
				}
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x04000089 RID: 137
		private string fileName;

		// Token: 0x0400008A RID: 138
		private Guid checksumAlgorithmId;

		// Token: 0x0400008B RID: 139
		private byte[] checksumData;
	}
}
