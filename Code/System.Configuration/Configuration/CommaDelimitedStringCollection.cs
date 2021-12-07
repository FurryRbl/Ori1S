using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	/// <summary>Represents a collection of string elements separated by commas. This class cannot be inherited.</summary>
	// Token: 0x02000018 RID: 24
	public sealed class CommaDelimitedStringCollection : StringCollection
	{
		/// <summary>Gets a value that specifies whether the collection has been modified. </summary>
		/// <returns>true if the <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> has been modified; otherwise, false.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002B00 File Offset: 0x00000D00
		public bool IsModified
		{
			get
			{
				return this.modified;
			}
		}

		/// <summary>Gets a value indicating whether the collection object is read-only.</summary>
		/// <returns>true if the specified string element in the <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002B08 File Offset: 0x00000D08
		public new bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets or sets a string element in the collection based on the index.</summary>
		/// <returns>A string element in the collection.</returns>
		/// <param name="index">The index of the string element in the collection.</param>
		// Token: 0x17000031 RID: 49
		public new string this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				if (this.readOnly)
				{
					throw new ConfigurationErrorsException("The configuration is read only");
				}
				base[index] = value;
				this.modified = true;
			}
		}

		/// <summary>Adds a string to the comma-delimited collection.</summary>
		/// <param name="value">A string value.</param>
		// Token: 0x060000BF RID: 191 RVA: 0x00002B44 File Offset: 0x00000D44
		public new void Add(string value)
		{
			if (this.readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Add(value);
			this.modified = true;
		}

		/// <summary>Adds all the strings in a string array to the collection.</summary>
		/// <param name="range">An array of strings to add to the collection.</param>
		// Token: 0x060000C0 RID: 192 RVA: 0x00002B6C File Offset: 0x00000D6C
		public new void AddRange(string[] range)
		{
			if (this.readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.AddRange(range);
			this.modified = true;
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x060000C1 RID: 193 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public new void Clear()
		{
			if (this.readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Clear();
			this.modified = true;
		}

		/// <summary>Creates a copy of the collection.</summary>
		/// <returns>A <see cref="T:System.Configuration.CommaDelimitedStringCollection" />.</returns>
		// Token: 0x060000C2 RID: 194 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public CommaDelimitedStringCollection Clone()
		{
			CommaDelimitedStringCollection commaDelimitedStringCollection = new CommaDelimitedStringCollection();
			string[] array = new string[this.Count];
			base.CopyTo(array, 0);
			commaDelimitedStringCollection.AddRange(array);
			return commaDelimitedStringCollection;
		}

		/// <summary>Adds a string element to the collection at the specified index.</summary>
		/// <param name="index">The index in the collection at which the new element will be added.</param>
		/// <param name="value">The value of the new element to add to the collection.</param>
		// Token: 0x060000C3 RID: 195 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public new void Insert(int index, string value)
		{
			if (this.readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Insert(index, value);
			this.modified = true;
		}

		/// <summary>Removes a string element from the collection.</summary>
		/// <param name="value">The string to remove.</param>
		// Token: 0x060000C4 RID: 196 RVA: 0x00002C20 File Offset: 0x00000E20
		public new void Remove(string value)
		{
			if (this.readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Remove(value);
			this.modified = true;
		}

		/// <summary>Sets the collection object to read-only.</summary>
		// Token: 0x060000C5 RID: 197 RVA: 0x00002C54 File Offset: 0x00000E54
		public void SetReadOnly()
		{
			this.readOnly = true;
		}

		/// <summary>Returns a string representation of the object.</summary>
		/// <returns>A string representation of the object.</returns>
		// Token: 0x060000C6 RID: 198 RVA: 0x00002C60 File Offset: 0x00000E60
		public override string ToString()
		{
			if (this.Count == 0)
			{
				return null;
			}
			string[] array = new string[this.Count];
			base.CopyTo(array, 0);
			return string.Join(",", array);
		}

		// Token: 0x0400002F RID: 47
		private bool modified;

		// Token: 0x04000030 RID: 48
		private bool readOnly;
	}
}
