using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Generates IDs for objects.</summary>
	// Token: 0x020004F7 RID: 1271
	[MonoTODO("Serialization format not compatible with.NET")]
	[ComVisible(true)]
	[Serializable]
	public class ObjectIDGenerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> class.</summary>
		// Token: 0x060032FC RID: 13052 RVA: 0x000A4D4C File Offset: 0x000A2F4C
		public ObjectIDGenerator()
		{
			this.table = new Hashtable(ObjectIDGenerator.comparer, ObjectIDGenerator.comparer);
			this.current = 1L;
		}

		/// <summary>Returns the ID for the specified object, generating a new ID if the specified object has not already been identified by the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />.</summary>
		/// <returns>The object's ID is used for serialization. <paramref name="firstTime" /> is set to true if this is the first time the object has been identified; otherwise, it is set to false.</returns>
		/// <param name="obj">The object you want an ID for. </param>
		/// <param name="firstTime">true if <paramref name="obj" /> was not previously known to the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> has been asked to keep track of too many objects. </exception>
		// Token: 0x060032FE RID: 13054 RVA: 0x000A4D80 File Offset: 0x000A2F80
		public virtual long GetId(object obj, out bool firstTime)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			object obj2 = this.table[obj];
			if (obj2 != null)
			{
				firstTime = false;
				return (long)obj2;
			}
			firstTime = true;
			this.table.Add(obj, this.current);
			long result;
			this.current = (result = this.current) + 1L;
			return result;
		}

		/// <summary>Determines whether an object has already been assigned an ID.</summary>
		/// <returns>The object ID of <paramref name="obj" /> if previously known to the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />; otherwise, zero.</returns>
		/// <param name="obj">The object you are asking for. </param>
		/// <param name="firstTime">true if <paramref name="obj" /> was not previously known to the <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		// Token: 0x060032FF RID: 13055 RVA: 0x000A4DE8 File Offset: 0x000A2FE8
		public virtual long HasId(object obj, out bool firstTime)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			object obj2 = this.table[obj];
			if (obj2 != null)
			{
				firstTime = false;
				return (long)obj2;
			}
			firstTime = true;
			return 0L;
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x000A4E28 File Offset: 0x000A3028
		internal long NextId
		{
			get
			{
				long result;
				this.current = (result = this.current) + 1L;
				return result;
			}
		}

		// Token: 0x0400152D RID: 5421
		private Hashtable table;

		// Token: 0x0400152E RID: 5422
		private long current;

		// Token: 0x0400152F RID: 5423
		private static ObjectIDGenerator.InstanceComparer comparer = new ObjectIDGenerator.InstanceComparer();

		// Token: 0x020004F8 RID: 1272
		private class InstanceComparer : IComparer, IHashCodeProvider
		{
			// Token: 0x06003302 RID: 13058 RVA: 0x000A4E50 File Offset: 0x000A3050
			int IComparer.Compare(object o1, object o2)
			{
				if (o1 is string)
				{
					return (!o1.Equals(o2)) ? 1 : 0;
				}
				return (o1 != o2) ? 1 : 0;
			}

			// Token: 0x06003303 RID: 13059 RVA: 0x000A4E80 File Offset: 0x000A3080
			int IHashCodeProvider.GetHashCode(object o)
			{
				return object.InternalGetHashCode(o);
			}
		}
	}
}
