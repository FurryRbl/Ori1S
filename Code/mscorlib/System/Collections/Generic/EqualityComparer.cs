using System;

namespace System.Collections.Generic
{
	/// <summary>Provides a base class for implementations of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface.</summary>
	/// <typeparam name="T">The type of objects to compare.</typeparam>
	// Token: 0x020006C8 RID: 1736
	[Serializable]
	public abstract class EqualityComparer<T> : IEqualityComparer<T>, IEqualityComparer
	{
		// Token: 0x06004278 RID: 17016 RVA: 0x000E3C28 File Offset: 0x000E1E28
		static EqualityComparer()
		{
			if (typeof(IEquatable<T>).IsAssignableFrom(typeof(T)))
			{
				EqualityComparer<T>._default = (EqualityComparer<T>)Activator.CreateInstance(typeof(GenericEqualityComparer<>).MakeGenericType(new Type[]
				{
					typeof(T)
				}));
			}
			else
			{
				EqualityComparer<T>._default = new EqualityComparer<T>.DefaultComparer();
			}
		}

		/// <summary>Returns a hash code for the specified object.</summary>
		/// <returns>A hash code for the specified object.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.-or-<paramref name="obj" /> is of a type that cannot be cast to type <paramref name="T" />.</exception>
		// Token: 0x06004279 RID: 17017 RVA: 0x000E3C94 File Offset: 0x000E1E94
		int IEqualityComparer.GetHashCode(object obj)
		{
			return this.GetHashCode((T)((object)obj));
		}

		/// <summary>Determines whether the specified objects are equal.</summary>
		/// <returns>true if the specified objects are equal; otherwise, false.</returns>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="x" /> or <paramref name="y" /> is of a type that cannot be cast to type <paramref name="T" />.</exception>
		// Token: 0x0600427A RID: 17018 RVA: 0x000E3CA4 File Offset: 0x000E1EA4
		bool IEqualityComparer.Equals(object x, object y)
		{
			return this.Equals((T)((object)x), (T)((object)y));
		}

		/// <summary>When overridden in a derived class, serves as a hash function for the specified object for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the specified object.</returns>
		/// <param name="obj">The object for which to get a hash code.</param>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
		// Token: 0x0600427B RID: 17019
		public abstract int GetHashCode(T obj);

		/// <summary>When overridden in a derived class, determines whether two objects of type <paramref name="T" /> are equal.</summary>
		/// <returns>true if the specified objects are equal; otherwise, false.</returns>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		// Token: 0x0600427C RID: 17020
		public abstract bool Equals(T x, T y);

		/// <summary>Returns a default equality comparer for the type specified by the generic argument.</summary>
		/// <returns>The default instance of the <see cref="T:System.Collections.Generic.EqualityComparer`1" /> class for type <paramref name="T" />.</returns>
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x000E3CB8 File Offset: 0x000E1EB8
		public static EqualityComparer<T> Default
		{
			get
			{
				return EqualityComparer<T>._default;
			}
		}

		// Token: 0x04001C47 RID: 7239
		private static readonly EqualityComparer<T> _default;

		// Token: 0x020006C9 RID: 1737
		[Serializable]
		private sealed class DefaultComparer : EqualityComparer<T>
		{
			// Token: 0x0600427F RID: 17023 RVA: 0x000E3CC8 File Offset: 0x000E1EC8
			public override int GetHashCode(T obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.GetHashCode();
			}

			// Token: 0x06004280 RID: 17024 RVA: 0x000E3CE4 File Offset: 0x000E1EE4
			public override bool Equals(T x, T y)
			{
				if (x == null)
				{
					return y == null;
				}
				return x.Equals(y);
			}
		}
	}
}
