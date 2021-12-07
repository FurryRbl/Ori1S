using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a collection of temporary files.</summary>
	// Token: 0x0200008F RID: 143
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	[Serializable]
	public class TempFileCollection : IDisposable, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class with default values.</summary>
		// Token: 0x060005DC RID: 1500 RVA: 0x00011F20 File Offset: 0x00010120
		public TempFileCollection() : this(string.Empty, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class using the specified temporary directory that is set to delete the temporary files after their generation and use, by default.</summary>
		/// <param name="tempDir">A path to the temporary directory to use for storing the temporary files. </param>
		// Token: 0x060005DD RID: 1501 RVA: 0x00011F30 File Offset: 0x00010130
		public TempFileCollection(string tempDir) : this(tempDir, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> class using the specified temporary directory and specified value indicating whether to keep or delete the temporary files after their generation and use, by default.</summary>
		/// <param name="tempDir">A path to the temporary directory to use for storing the temporary files. </param>
		/// <param name="keepFiles">true if the temporary files should be kept after use; false if the temporary files should be deleted. </param>
		// Token: 0x060005DE RID: 1502 RVA: 0x00011F3C File Offset: 0x0001013C
		public TempFileCollection(string tempDir, bool keepFiles)
		{
			this.filehash = new Hashtable();
			this.tempdir = ((tempDir != null) ? tempDir : string.Empty);
			this.keepfiles = keepFiles;
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00011F70 File Offset: 0x00010170
		int ICollection.Count
		{
			get
			{
				return this.filehash.Count;
			}
		}

		/// <summary>Copies the elements of the collection to an array, starting at the specified index of the target array. </summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="start">The zero-based index in array at which copying begins.</param>
		// Token: 0x060005E0 RID: 1504 RVA: 0x00011F80 File Offset: 0x00010180
		void ICollection.CopyTo(Array array, int start)
		{
			this.filehash.Keys.CopyTo(array, start);
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00011F94 File Offset: 0x00010194
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00011F98 File Offset: 0x00010198
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. </summary>
		// Token: 0x060005E3 RID: 1507 RVA: 0x00011F9C File Offset: 0x0001019C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Returns an enumerator that iterates through a collection. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060005E4 RID: 1508 RVA: 0x00011FA8 File Offset: 0x000101A8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.filehash.Keys.GetEnumerator();
		}

		/// <summary>Gets the full path to the base file name, without a file name extension, on the temporary directory path, that is used to generate temporary file names for the collection.</summary>
		/// <returns>The full path to the base file name, without a file name extension, on the temporary directory path, that is used to generate temporary file names for the collection.</returns>
		/// <exception cref="T:System.Security.SecurityException">If the <see cref="P:System.CodeDom.Compiler.TempFileCollection.BasePath" /> property has not been set or is set to null, and <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" /> is not granted for the temporary directory indicated by the <see cref="P:System.CodeDom.Compiler.TempFileCollection.TempDir" /> property. </exception>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00011FBC File Offset: 0x000101BC
		public string BasePath
		{
			get
			{
				if (this.basepath == null)
				{
					if (this.rnd == null)
					{
						this.rnd = new Random();
					}
					string text = this.tempdir;
					if (text.Length == 0)
					{
						text = this.GetOwnTempDir();
					}
					FileStream fileStream = null;
					do
					{
						int num = this.rnd.Next();
						this.basepath = Path.Combine(text, (num + 1).ToString("x"));
						string path = this.basepath + ".tmp";
						try
						{
							fileStream = new FileStream(path, FileMode.CreateNew);
						}
						catch (IOException)
						{
							fileStream = null;
						}
						catch
						{
							throw;
						}
					}
					while (fileStream == null);
					fileStream.Close();
					if (SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.basepath).Demand();
					}
				}
				return this.basepath;
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000120C4 File Offset: 0x000102C4
		private string GetOwnTempDir()
		{
			if (this.ownTempDir != null)
			{
				return this.ownTempDir;
			}
			string tempPath = Path.GetTempPath();
			int num = -1;
			bool flag = false;
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.Win32NT:
			case PlatformID.WinCE:
				flag = true;
				num = 0;
				break;
			}
			for (;;)
			{
				int num2 = this.rnd.Next();
				this.ownTempDir = Path.Combine(tempPath, (num2 + 1).ToString("x"));
				if (!Directory.Exists(this.ownTempDir))
				{
					if (flag)
					{
						Directory.CreateDirectory(this.ownTempDir);
					}
					else
					{
						num = TempFileCollection.mkdir(this.ownTempDir, 448U);
					}
					if (num != 0 && !Directory.Exists(this.ownTempDir))
					{
						break;
					}
				}
				if (num == 0)
				{
					goto Block_7;
				}
			}
			throw new IOException();
			Block_7:
			return this.ownTempDir;
		}

		/// <summary>Gets the number of files in the collection.</summary>
		/// <returns>The number of files in the collection.</returns>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000121AC File Offset: 0x000103AC
		public int Count
		{
			get
			{
				return this.filehash.Count;
			}
		}

		/// <summary>Gets or sets a value indicating whether to keep the files, by default, when the <see cref="M:System.CodeDom.Compiler.TempFileCollection.Delete" /> method is called or the collection is disposed.</summary>
		/// <returns>true if the files should be kept; otherwise, false.</returns>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000121BC File Offset: 0x000103BC
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x000121C4 File Offset: 0x000103C4
		public bool KeepFiles
		{
			get
			{
				return this.keepfiles;
			}
			set
			{
				this.keepfiles = value;
			}
		}

		/// <summary>Gets the temporary directory to store the temporary files in.</summary>
		/// <returns>The temporary directory to store the temporary files in.</returns>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x000121D0 File Offset: 0x000103D0
		public string TempDir
		{
			get
			{
				return this.tempdir;
			}
		}

		/// <summary>Adds a file name with the specified file name extension to the collection.</summary>
		/// <returns>A file name with the specified extension that was just added to the collection.</returns>
		/// <param name="fileExtension">The file name extension for the auto-generated temporary file name to add to the collection. </param>
		// Token: 0x060005EB RID: 1515 RVA: 0x000121D8 File Offset: 0x000103D8
		public string AddExtension(string fileExtension)
		{
			return this.AddExtension(fileExtension, this.keepfiles);
		}

		/// <summary>Adds a file name with the specified file name extension to the collection, using the specified value indicating whether the file should be deleted or retained.</summary>
		/// <returns>A file name with the specified extension that was just added to the collection.</returns>
		/// <param name="fileExtension">The file name extension for the auto-generated temporary file name to add to the collection. </param>
		/// <param name="keepFile">true if the file should be kept after use; false if the file should be deleted. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileExtension" /> is null or an empty string.</exception>
		// Token: 0x060005EC RID: 1516 RVA: 0x000121E8 File Offset: 0x000103E8
		public string AddExtension(string fileExtension, bool keepFile)
		{
			string text = this.BasePath + "." + fileExtension;
			this.AddFile(text, keepFile);
			return text;
		}

		/// <summary>Adds the specified file to the collection, using the specified value indicating whether to keep the file after the collection is disposed or when the <see cref="M:System.CodeDom.Compiler.TempFileCollection.Delete" /> method is called.</summary>
		/// <param name="fileName">The name of the file to add to the collection. </param>
		/// <param name="keepFile">true if the file should be kept after use; false if the file should be deleted. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileName" /> is null or an empty string.-or-<paramref name="fileName" /> is a duplicate.</exception>
		// Token: 0x060005ED RID: 1517 RVA: 0x00012210 File Offset: 0x00010410
		public void AddFile(string fileName, bool keepFile)
		{
			this.filehash.Add(fileName, keepFile);
		}

		/// <summary>Copies the members of the collection to the specified string, beginning at the specified index.</summary>
		/// <param name="fileNames">The array of strings to copy to. </param>
		/// <param name="start">The index of the array to begin copying to. </param>
		// Token: 0x060005EE RID: 1518 RVA: 0x00012224 File Offset: 0x00010424
		public void CopyTo(string[] fileNames, int start)
		{
			this.filehash.Keys.CopyTo(fileNames, start);
		}

		/// <summary>Deletes the temporary files within this collection that were not marked to be kept.</summary>
		// Token: 0x060005EF RID: 1519 RVA: 0x00012238 File Offset: 0x00010438
		public void Delete()
		{
			bool flag = true;
			string[] array = new string[this.filehash.Count];
			this.filehash.Keys.CopyTo(array, 0);
			foreach (string text in array)
			{
				if (!(bool)this.filehash[text])
				{
					File.Delete(text);
					this.filehash.Remove(text);
				}
				else
				{
					flag = false;
				}
			}
			if (this.basepath != null)
			{
				string path = this.basepath + ".tmp";
				File.Delete(path);
				this.basepath = null;
			}
			if (flag && this.ownTempDir != null)
			{
				Directory.Delete(this.ownTempDir, true);
				this.ownTempDir = null;
			}
		}

		/// <summary>Gets an enumerator that can enumerate the members of the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that contains the collection's members.</returns>
		// Token: 0x060005F0 RID: 1520 RVA: 0x00012308 File Offset: 0x00010508
		public IEnumerator GetEnumerator()
		{
			return this.filehash.Keys.GetEnumerator();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060005F1 RID: 1521 RVA: 0x0001231C File Offset: 0x0001051C
		protected virtual void Dispose(bool disposing)
		{
			this.Delete();
			if (disposing)
			{
				GC.SuppressFinalize(true);
			}
		}

		/// <summary>Attempts to delete the temporary files before this object is reclaimed by garbage collection.</summary>
		// Token: 0x060005F2 RID: 1522 RVA: 0x00012338 File Offset: 0x00010538
		~TempFileCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x060005F3 RID: 1523
		[DllImport("libc")]
		private static extern int mkdir(string olpath, uint mode);

		// Token: 0x04000186 RID: 390
		private Hashtable filehash;

		// Token: 0x04000187 RID: 391
		private string tempdir;

		// Token: 0x04000188 RID: 392
		private bool keepfiles;

		// Token: 0x04000189 RID: 393
		private string basepath;

		// Token: 0x0400018A RID: 394
		private Random rnd;

		// Token: 0x0400018B RID: 395
		private string ownTempDir;
	}
}
