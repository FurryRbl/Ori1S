using System;
using System.Collections;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.Compiler.CompilerError" /> objects.</summary>
	// Token: 0x0200007F RID: 127
	[Serializable]
	public class CompilerErrorCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> class.</summary>
		// Token: 0x0600052D RID: 1325 RVA: 0x00010E08 File Offset: 0x0000F008
		public CompilerErrorCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> class that contains the contents of the specified <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" />.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> object with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x0600052E RID: 1326 RVA: 0x00010E10 File Offset: 0x0000F010
		public CompilerErrorCollection(CompilerErrorCollection value)
		{
			base.InnerList.AddRange(value.InnerList);
		}

		/// <summary>Initializes a new instance of <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> that contains the specified array of <see cref="T:System.CodeDom.Compiler.CompilerError" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.Compiler.CompilerError" /> objects to initialize the collection with. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x0600052F RID: 1327 RVA: 0x00010E34 File Offset: 0x0000F034
		public CompilerErrorCollection(CompilerError[] value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.Compiler.CompilerError" /> object to the error collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.Compiler.CompilerError" /> object to add. </param>
		// Token: 0x06000530 RID: 1328 RVA: 0x00010E48 File Offset: 0x0000F048
		public int Add(CompilerError value)
		{
			return base.InnerList.Add(value);
		}

		/// <summary>Copies the elements of an array to the end of the error collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.Compiler.CompilerError" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000531 RID: 1329 RVA: 0x00010E58 File Offset: 0x0000F058
		public void AddRange(CompilerError[] value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Adds the contents of the specified compiler error collection to the end of the error collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> object that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000532 RID: 1330 RVA: 0x00010E68 File Offset: 0x0000F068
		public void AddRange(CompilerErrorCollection value)
		{
			base.InnerList.AddRange(value.InnerList);
		}

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.Compiler.CompilerError" /> object.</summary>
		/// <returns>true if the <see cref="T:System.CodeDom.Compiler.CompilerError" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.Compiler.CompilerError" /> to locate. </param>
		// Token: 0x06000533 RID: 1331 RVA: 0x00010E7C File Offset: 0x0000F07C
		public bool Contains(CompilerError value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the collection values to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" />. </param>
		/// <param name="index">The index in the array at which to start copying. </param>
		/// <exception cref="T:System.ArgumentException">The array indicated by the <paramref name="array" /> parameter is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> is greater than the available space between the index value of the <paramref name="arrayIndex" /> parameter in the array indicated by the <paramref name="array" /> parameter and the end of the array indicated by the <paramref name="array" /> parameter. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the lowbound of the array indicated by the <paramref name="array" /> parameter. </exception>
		// Token: 0x06000534 RID: 1332 RVA: 0x00010E8C File Offset: 0x0000F08C
		public void CopyTo(CompilerError[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.Compiler.CompilerError" /> object in the collection, if it exists in the collection.</summary>
		/// <returns>The index of the specified <see cref="T:System.CodeDom.Compiler.CompilerError" /> in the <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.Compiler.CompilerError" /> to locate. </param>
		// Token: 0x06000535 RID: 1333 RVA: 0x00010E9C File Offset: 0x0000F09C
		public int IndexOf(CompilerError value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.Compiler.CompilerError" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the compiler error should be inserted. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.Compiler.CompilerError" /> to insert. </param>
		// Token: 0x06000536 RID: 1334 RVA: 0x00010EAC File Offset: 0x0000F0AC
		public void Insert(int index, CompilerError value)
		{
			base.InnerList.Insert(index, value);
		}

		/// <summary>Removes a specific <see cref="T:System.CodeDom.Compiler.CompilerError" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.Compiler.CompilerError" /> to remove from the <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x06000537 RID: 1335 RVA: 0x00010EBC File Offset: 0x0000F0BC
		public void Remove(CompilerError value)
		{
			base.InnerList.Remove(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.Compiler.CompilerError" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerError" /> at each valid index.</returns>
		/// <param name="index">The zero-based index of the entry to locate in the collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index value indicated by the <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x170000EF RID: 239
		public CompilerError this[int index]
		{
			get
			{
				return (CompilerError)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the collection contains errors.</summary>
		/// <returns>true if the collection contains errors; otherwise, false.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		public bool HasErrors
		{
			get
			{
				foreach (object obj in base.InnerList)
				{
					CompilerError compilerError = (CompilerError)obj;
					if (!compilerError.IsWarning)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the collection contains warnings.</summary>
		/// <returns>true if the collection contains warnings; otherwise, false.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00010F70 File Offset: 0x0000F170
		public bool HasWarnings
		{
			get
			{
				foreach (object obj in base.InnerList)
				{
					CompilerError compilerError = (CompilerError)obj;
					if (compilerError.IsWarning)
					{
						return true;
					}
				}
				return false;
			}
		}
	}
}
