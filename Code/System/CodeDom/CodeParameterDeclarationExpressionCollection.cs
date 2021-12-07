﻿using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> objects.</summary>
	// Token: 0x02000055 RID: 85
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeParameterDeclarationExpressionCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> class.</summary>
		// Token: 0x060002D8 RID: 728 RVA: 0x0000C758 File Offset: 0x0000A958
		public CodeParameterDeclarationExpressionCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> objects with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">one or more objects in the array are null.</exception>
		// Token: 0x060002D9 RID: 729 RVA: 0x0000C760 File Offset: 0x0000A960
		public CodeParameterDeclarationExpressionCollection(CodeParameterDeclarationExpression[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060002DA RID: 730 RVA: 0x0000C770 File Offset: 0x0000A970
		public CodeParameterDeclarationExpressionCollection(CodeParameterDeclarationExpressionCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x17000085 RID: 133
		public CodeParameterDeclarationExpression this[int index]
		{
			get
			{
				return (CodeParameterDeclarationExpression)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to add. </param>
		// Token: 0x060002DD RID: 733 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		public int Add(CodeParameterDeclarationExpression value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> containing the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060002DE RID: 734 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public void AddRange(CodeParameterDeclarationExpression[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Adds the contents of another <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> containing the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060002DF RID: 735 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		public void AddRange(CodeParameterDeclarationExpressionCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Gets a value indicating whether the collection contains the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" />.</summary>
		/// <returns>true if the collection contains the specified object; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to search for in the collection. </param>
		// Token: 0x060002E0 RID: 736 RVA: 0x0000C838 File Offset: 0x0000AA38
		public bool Contains(CodeParameterDeclarationExpression value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to begin inserting. </param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index. </exception>
		// Token: 0x060002E1 RID: 737 RVA: 0x0000C848 File Offset: 0x0000AA48
		public void CopyTo(CodeParameterDeclarationExpression[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" />, if it exists in the collection.</summary>
		/// <returns>The index in the collection of the specified object, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to locate in the collection. </param>
		// Token: 0x060002E2 RID: 738 RVA: 0x0000C858 File Offset: 0x0000AA58
		public int IndexOf(CodeParameterDeclarationExpression value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to insert. </param>
		// Token: 0x060002E3 RID: 739 RVA: 0x0000C868 File Offset: 0x0000AA68
		public void Insert(int index, CodeParameterDeclarationExpression value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x060002E4 RID: 740 RVA: 0x0000C878 File Offset: 0x0000AA78
		public void Remove(CodeParameterDeclarationExpression value)
		{
			base.List.Remove(value);
		}
	}
}
