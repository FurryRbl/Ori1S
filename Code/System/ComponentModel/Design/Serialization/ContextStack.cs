using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a stack object that can be used by a serializer to make information available to nested serializers.</summary>
	// Token: 0x02000129 RID: 297
	public sealed class ContextStack
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ContextStack" /> class. </summary>
		// Token: 0x06000B56 RID: 2902 RVA: 0x0001DEF4 File Offset: 0x0001C0F4
		public ContextStack()
		{
			this._contextList = new ArrayList();
		}

		/// <summary>Gets the current object on the stack.</summary>
		/// <returns>The current object on the stack, or null if no objects were pushed.</returns>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0001DF08 File Offset: 0x0001C108
		public object Current
		{
			get
			{
				int count = this._contextList.Count;
				if (count > 0)
				{
					return this._contextList[count - 1];
				}
				return null;
			}
		}

		/// <summary>Gets the first object on the stack that inherits from or implements the specified type.</summary>
		/// <returns>The first object on the stack that inherits from or implements the specified type, or null if no object on the stack implements the type.</returns>
		/// <param name="type">A type to retrieve from the context stack. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x17000289 RID: 649
		public object this[Type type]
		{
			get
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				for (int i = this._contextList.Count - 1; i >= 0; i--)
				{
					object obj = this._contextList[i];
					if (type.IsInstanceOfType(obj))
					{
						return obj;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the object on the stack at the specified level.</summary>
		/// <returns>The object on the stack at the specified level, or null if no object exists at that level.</returns>
		/// <param name="level">The level of the object to retrieve on the stack. Level 0 is the top of the stack, level 1 is the next down, and so on. This level must be 0 or greater. If level is greater than the number of levels on the stack, it returns null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="level" /> is less than 0.</exception>
		// Token: 0x1700028A RID: 650
		public object this[int level]
		{
			get
			{
				if (level < 0)
				{
					throw new ArgumentOutOfRangeException("level");
				}
				int count = this._contextList.Count;
				if (count > 0 && count > level)
				{
					return this._contextList[count - 1 - level];
				}
				return null;
			}
		}

		/// <summary>Removes the current object off of the stack, returning its value.</summary>
		/// <returns>The object removed from the stack; null if no objects are on the stack.</returns>
		// Token: 0x06000B5A RID: 2906 RVA: 0x0001DFDC File Offset: 0x0001C1DC
		public object Pop()
		{
			object result = null;
			int count = this._contextList.Count;
			if (count > 0)
			{
				int index = count - 1;
				result = this._contextList[index];
				this._contextList.RemoveAt(index);
			}
			return result;
		}

		/// <summary>Pushes, or places, the specified object onto the stack.</summary>
		/// <param name="context">The context object to push onto the stack. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		// Token: 0x06000B5B RID: 2907 RVA: 0x0001E01C File Offset: 0x0001C21C
		public void Push(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this._contextList.Add(context);
		}

		/// <summary>Appends an object to the end of the stack, rather than pushing it onto the top of the stack.</summary>
		/// <param name="context">A context object to append to the stack.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		// Token: 0x06000B5C RID: 2908 RVA: 0x0001E03C File Offset: 0x0001C23C
		public void Append(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this._contextList.Insert(0, context);
		}

		// Token: 0x040002FE RID: 766
		private ArrayList _contextList;
	}
}
