using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Collections.Generic
{
	// Token: 0x02000051 RID: 81
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	[Serializable]
	public class HashSet<T> : IEnumerable, ISerializable, IDeserializationCallback, ICollection<T>, IEnumerable<T>
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x00014D04 File Offset: 0x00012F04
		public HashSet()
		{
			this.Init(10, null);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00014D18 File Offset: 0x00012F18
		public HashSet(IEqualityComparer<T> comparer)
		{
			this.Init(10, comparer);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00014D2C File Offset: 0x00012F2C
		public HashSet(IEnumerable<T> collection) : this(collection, null)
		{
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00014D38 File Offset: 0x00012F38
		public HashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			int capacity = 0;
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				capacity = collection2.Count;
			}
			this.Init(capacity, comparer);
			foreach (T item in collection)
			{
				this.Add(item);
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00014DCC File Offset: 0x00012FCC
		protected HashSet(SerializationInfo info, StreamingContext context)
		{
			this.si = info;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00014DDC File Offset: 0x00012FDC
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00014DEC File Offset: 0x00012FEC
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00014DF0 File Offset: 0x00012FF0
		void ICollection<!0>.CopyTo(T[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00014DFC File Offset: 0x00012FFC
		void ICollection<!0>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00014E08 File Offset: 0x00013008
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00014E18 File Offset: 0x00013018
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00014E20 File Offset: 0x00013020
		private void Init(int capacity, IEqualityComparer<T> comparer)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.comparer = (comparer ?? EqualityComparer<T>.Default);
			if (capacity == 0)
			{
				capacity = 10;
			}
			capacity = (int)((float)capacity / 0.9f) + 1;
			this.InitArrays(capacity);
			this.generation = 0;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00014E78 File Offset: 0x00013078
		private void InitArrays(int size)
		{
			this.table = new int[size];
			this.links = new HashSet<T>.Link[size];
			this.empty_slot = -1;
			this.slots = new T[size];
			this.touched = 0;
			this.threshold = (int)((float)this.table.Length * 0.9f);
			if (this.threshold == 0 && this.table.Length > 0)
			{
				this.threshold = 1;
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00014EF0 File Offset: 0x000130F0
		private bool SlotsContainsAt(int index, int hash, T item)
		{
			HashSet<T>.Link link;
			for (int num = this.table[index] - 1; num != -1; num = link.Next)
			{
				link = this.links[num];
				if (link.HashCode == hash && ((hash != -2147483648 || (item != null && this.slots[num] != null)) ? this.comparer.Equals(item, this.slots[num]) : (item == null && null == this.slots[num])))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00014FB0 File Offset: 0x000131B0
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this.count);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00014FC0 File Offset: 0x000131C0
		public void CopyTo(T[] array, int index)
		{
			this.CopyTo(array, index, this.count);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00014FD0 File Offset: 0x000131D0
		public void CopyTo(T[] array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index > array.Length)
			{
				throw new ArgumentException("index larger than largest valid index of array");
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException("Destination array cannot hold the requested elements!");
			}
			int num = 0;
			int num2 = 0;
			while (num < this.touched && num2 < count)
			{
				if (this.GetLinkHashCode(num) != 0)
				{
					array[index++] = this.slots[num];
				}
				num++;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00015070 File Offset: 0x00013270
		private void Resize()
		{
			int num = HashSet<T>.PrimeHelper.ToPrime(this.table.Length << 1 | 1);
			int[] array = new int[num];
			HashSet<T>.Link[] array2 = new HashSet<T>.Link[num];
			for (int i = 0; i < this.table.Length; i++)
			{
				for (int num2 = this.table[i] - 1; num2 != -1; num2 = this.links[num2].Next)
				{
					int num3 = array2[num2].HashCode = this.GetItemHashCode(this.slots[num2]);
					int num4 = (num3 & int.MaxValue) % num;
					array2[num2].Next = array[num4] - 1;
					array[num4] = num2 + 1;
				}
			}
			this.table = array;
			this.links = array2;
			T[] destinationArray = new T[num];
			Array.Copy(this.slots, 0, destinationArray, 0, this.touched);
			this.slots = destinationArray;
			this.threshold = (int)((float)num * 0.9f);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00015174 File Offset: 0x00013374
		private int GetLinkHashCode(int index)
		{
			return this.links[index].HashCode & int.MinValue;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00015190 File Offset: 0x00013390
		private int GetItemHashCode(T item)
		{
			if (item == null)
			{
				return int.MinValue;
			}
			return this.comparer.GetHashCode(item) | int.MinValue;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000151B8 File Offset: 0x000133B8
		public bool Add(T item)
		{
			int itemHashCode = this.GetItemHashCode(item);
			int num = (itemHashCode & int.MaxValue) % this.table.Length;
			if (this.SlotsContainsAt(num, itemHashCode, item))
			{
				return false;
			}
			if (++this.count > this.threshold)
			{
				this.Resize();
				num = (itemHashCode & int.MaxValue) % this.table.Length;
			}
			int num2 = this.empty_slot;
			if (num2 == -1)
			{
				num2 = this.touched++;
			}
			else
			{
				this.empty_slot = this.links[num2].Next;
			}
			this.links[num2].HashCode = itemHashCode;
			this.links[num2].Next = this.table[num] - 1;
			this.table[num] = num2 + 1;
			this.slots[num2] = item;
			this.generation++;
			return true;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x000152B4 File Offset: 0x000134B4
		public IEqualityComparer<T> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000152BC File Offset: 0x000134BC
		public void Clear()
		{
			this.count = 0;
			Array.Clear(this.table, 0, this.table.Length);
			Array.Clear(this.slots, 0, this.slots.Length);
			Array.Clear(this.links, 0, this.links.Length);
			this.empty_slot = -1;
			this.touched = 0;
			this.generation++;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00015328 File Offset: 0x00013528
		public bool Contains(T item)
		{
			int itemHashCode = this.GetItemHashCode(item);
			int index = (itemHashCode & int.MaxValue) % this.table.Length;
			return this.SlotsContainsAt(index, itemHashCode, item);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00015358 File Offset: 0x00013558
		public bool Remove(T item)
		{
			int itemHashCode = this.GetItemHashCode(item);
			int num = (itemHashCode & int.MaxValue) % this.table.Length;
			int num2 = this.table[num] - 1;
			if (num2 == -1)
			{
				return false;
			}
			int num3 = -1;
			do
			{
				HashSet<T>.Link link = this.links[num2];
				if (link.HashCode == itemHashCode && ((itemHashCode != -2147483648 || (item != null && this.slots[num2] != null)) ? this.comparer.Equals(this.slots[num2], item) : (item == null && null == this.slots[num2])))
				{
					break;
				}
				num3 = num2;
				num2 = link.Next;
			}
			while (num2 != -1);
			if (num2 == -1)
			{
				return false;
			}
			this.count--;
			if (num3 == -1)
			{
				this.table[num] = this.links[num2].Next + 1;
			}
			else
			{
				this.links[num3].Next = this.links[num2].Next;
			}
			this.links[num2].Next = this.empty_slot;
			this.empty_slot = num2;
			this.links[num2].HashCode = 0;
			this.slots[num2] = default(T);
			this.generation++;
			return true;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000154F0 File Offset: 0x000136F0
		public int RemoveWhere(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			int num = 0;
			T[] array = new T[this.count];
			this.CopyTo(array, 0);
			foreach (T t in array)
			{
				if (predicate(t))
				{
					this.Remove(t);
					num++;
				}
			}
			return num;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00015560 File Offset: 0x00013760
		public void TrimExcess()
		{
			this.Resize();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00015568 File Offset: 0x00013768
		public void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			T[] array = new T[this.count];
			this.CopyTo(array, 0);
			foreach (T t in array)
			{
				if (!other.Contains(t))
				{
					this.Remove(t);
				}
			}
			foreach (T item in other)
			{
				if (!this.Contains(item))
				{
					this.Remove(item);
				}
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00015634 File Offset: 0x00013834
		public void ExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (T item in other)
			{
				this.Remove(item);
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000156A4 File Offset: 0x000138A4
		public bool Overlaps(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (T item in other)
			{
				if (this.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00015724 File Offset: 0x00013924
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.count != other.Count<T>())
			{
				return false;
			}
			foreach (T value in this)
			{
				if (!other.Contains(value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000157B8 File Offset: 0x000139B8
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (T item in other)
			{
				if (this.Contains(item))
				{
					this.Remove(item);
				}
				else
				{
					this.Add(item);
				}
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00015844 File Offset: 0x00013A44
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (T item in other)
			{
				this.Add(item);
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000158B4 File Offset: 0x00013AB4
		private bool CheckIsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (T value in this)
			{
				if (!other.Contains(value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00015938 File Offset: 0x00013B38
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			return this.count == 0 || (this.count <= other.Count<T>() && this.CheckIsSubsetOf(other));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00015980 File Offset: 0x00013B80
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			return this.count == 0 || (this.count < other.Count<T>() && this.CheckIsSubsetOf(other));
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000159C8 File Offset: 0x00013BC8
		private bool CheckIsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (T item in other)
			{
				if (!this.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00015A48 File Offset: 0x00013C48
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			return this.count >= other.Count<T>() && this.CheckIsSupersetOf(other);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00015A78 File Offset: 0x00013C78
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			return this.count > other.Count<T>() && this.CheckIsSupersetOf(other);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00015AA8 File Offset: 0x00013CA8
		[MonoTODO]
		public static IEqualityComparer<HashSet<T>> CreateSetComparer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00015AB0 File Offset: 0x00013CB0
		[MonoTODO]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00015AB8 File Offset: 0x00013CB8
		[MonoTODO]
		public virtual void OnDeserialization(object sender)
		{
			if (this.si == null)
			{
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00015ACC File Offset: 0x00013CCC
		public HashSet<T>.Enumerator GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x0400011E RID: 286
		private const int INITIAL_SIZE = 10;

		// Token: 0x0400011F RID: 287
		private const float DEFAULT_LOAD_FACTOR = 0.9f;

		// Token: 0x04000120 RID: 288
		private const int NO_SLOT = -1;

		// Token: 0x04000121 RID: 289
		private const int HASH_FLAG = -2147483648;

		// Token: 0x04000122 RID: 290
		private int[] table;

		// Token: 0x04000123 RID: 291
		private HashSet<T>.Link[] links;

		// Token: 0x04000124 RID: 292
		private T[] slots;

		// Token: 0x04000125 RID: 293
		private int touched;

		// Token: 0x04000126 RID: 294
		private int empty_slot;

		// Token: 0x04000127 RID: 295
		private int count;

		// Token: 0x04000128 RID: 296
		private int threshold;

		// Token: 0x04000129 RID: 297
		private IEqualityComparer<T> comparer;

		// Token: 0x0400012A RID: 298
		private SerializationInfo si;

		// Token: 0x0400012B RID: 299
		private int generation;

		// Token: 0x02000052 RID: 82
		private struct Link
		{
			// Token: 0x0400012C RID: 300
			public int HashCode;

			// Token: 0x0400012D RID: 301
			public int Next;
		}

		// Token: 0x02000053 RID: 83
		[Serializable]
		public struct Enumerator : IEnumerator, IDisposable, IEnumerator<T>
		{
			// Token: 0x060004DB RID: 1243 RVA: 0x00015AD4 File Offset: 0x00013CD4
			internal Enumerator(HashSet<T> hashset)
			{
				this.hashset = hashset;
				this.stamp = hashset.generation;
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060004DC RID: 1244 RVA: 0x00015AEC File Offset: 0x00013CEC
			object IEnumerator.Current
			{
				get
				{
					this.CheckState();
					if (this.next <= 0)
					{
						throw new InvalidOperationException("Current is not valid");
					}
					return this.current;
				}
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x00015B24 File Offset: 0x00013D24
			void IEnumerator.Reset()
			{
				this.CheckState();
				this.next = 0;
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x00015B34 File Offset: 0x00013D34
			public bool MoveNext()
			{
				this.CheckState();
				if (this.next < 0)
				{
					return false;
				}
				while (this.next < this.hashset.touched)
				{
					int num = this.next++;
					if (this.hashset.GetLinkHashCode(num) != 0)
					{
						this.current = this.hashset.slots[num];
						return true;
					}
				}
				this.next = -1;
				return false;
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x060004DF RID: 1247 RVA: 0x00015BB4 File Offset: 0x00013DB4
			public T Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x00015BBC File Offset: 0x00013DBC
			public void Dispose()
			{
				this.hashset = null;
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x00015BC8 File Offset: 0x00013DC8
			private void CheckState()
			{
				if (this.hashset == null)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.hashset.generation != this.stamp)
				{
					throw new InvalidOperationException("HashSet have been modified while it was iterated over");
				}
			}

			// Token: 0x0400012E RID: 302
			private HashSet<T> hashset;

			// Token: 0x0400012F RID: 303
			private int next;

			// Token: 0x04000130 RID: 304
			private int stamp;

			// Token: 0x04000131 RID: 305
			private T current;
		}

		// Token: 0x02000054 RID: 84
		private static class PrimeHelper
		{
			// Token: 0x060004E3 RID: 1251 RVA: 0x00015C1C File Offset: 0x00013E1C
			private static bool TestPrime(int x)
			{
				if ((x & 1) != 0)
				{
					int num = (int)Math.Sqrt((double)x);
					for (int i = 3; i < num; i += 2)
					{
						if (x % i == 0)
						{
							return false;
						}
					}
					return true;
				}
				return x == 2;
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x00015C5C File Offset: 0x00013E5C
			private static int CalcPrime(int x)
			{
				for (int i = (x & -2) - 1; i < 2147483647; i += 2)
				{
					if (HashSet<T>.PrimeHelper.TestPrime(i))
					{
						return i;
					}
				}
				return x;
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x00015C94 File Offset: 0x00013E94
			public static int ToPrime(int x)
			{
				for (int i = 0; i < HashSet<T>.PrimeHelper.primes_table.Length; i++)
				{
					if (x <= HashSet<T>.PrimeHelper.primes_table[i])
					{
						return HashSet<T>.PrimeHelper.primes_table[i];
					}
				}
				return HashSet<T>.PrimeHelper.CalcPrime(x);
			}

			// Token: 0x04000132 RID: 306
			private static readonly int[] primes_table = new int[]
			{
				11,
				19,
				37,
				73,
				109,
				163,
				251,
				367,
				557,
				823,
				1237,
				1861,
				2777,
				4177,
				6247,
				9371,
				14057,
				21089,
				31627,
				47431,
				71143,
				106721,
				160073,
				240101,
				360163,
				540217,
				810343,
				1215497,
				1823231,
				2734867,
				4102283,
				6153409,
				9230113,
				13845163
			};
		}
	}
}
