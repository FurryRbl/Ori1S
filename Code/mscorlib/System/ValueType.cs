using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides the base class for value types.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000003 RID: 3
	[ComVisible(true)]
	[Serializable]
	public abstract class ValueType
	{
		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalEquals(object o1, object o2, out object[] fields);

		// Token: 0x06000010 RID: 16 RVA: 0x0000214C File Offset: 0x0000034C
		internal static bool DefaultEquals(object o1, object o2)
		{
			if (o2 == null)
			{
				return false;
			}
			object[] array;
			bool result = ValueType.InternalEquals(o1, o2, out array);
			if (array == null)
			{
				return result;
			}
			for (int i = 0; i < array.Length; i += 2)
			{
				object obj = array[i];
				object obj2 = array[i + 1];
				if (obj == null)
				{
					if (obj2 != null)
					{
						return false;
					}
				}
				else if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000011 RID: 17 RVA: 0x000021B8 File Offset: 0x000003B8
		public override bool Equals(object obj)
		{
			return ValueType.DefaultEquals(this, obj);
		}

		// Token: 0x06000012 RID: 18
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalGetHashCode(object o, out object[] fields);

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000013 RID: 19 RVA: 0x000021C4 File Offset: 0x000003C4
		public override int GetHashCode()
		{
			object[] array;
			int num = ValueType.InternalGetHashCode(this, out array);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						num ^= array[i].GetHashCode();
					}
				}
			}
			return num;
		}

		/// <summary>Returns the fully qualified type name of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000014 RID: 20 RVA: 0x00002208 File Offset: 0x00000408
		public override string ToString()
		{
			return base.GetType().FullName;
		}
	}
}
