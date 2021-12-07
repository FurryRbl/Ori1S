using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace SimpleJson
{
	// Token: 0x0200025D RID: 605
	[GeneratedCode("simple-json", "1.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class JsonObject : IEnumerable, IDictionary<string, object>, IEnumerable<KeyValuePair<string, object>>, ICollection<KeyValuePair<string, object>>
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x0002DCAC File Offset: 0x0002BEAC
		public JsonObject()
		{
			this._members = new Dictionary<string, object>();
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x0002DCC0 File Offset: 0x0002BEC0
		public JsonObject(IEqualityComparer<string> comparer)
		{
			this._members = new Dictionary<string, object>(comparer);
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x0002DCD4 File Offset: 0x0002BED4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._members.GetEnumerator();
		}

		// Token: 0x1700090F RID: 2319
		public object this[int index]
		{
			get
			{
				return JsonObject.GetAtIndex(this._members, index);
			}
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x0002DCF8 File Offset: 0x0002BEF8
		internal static object GetAtIndex(IDictionary<string, object> obj, int index)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (index >= obj.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = 0;
			foreach (KeyValuePair<string, object> keyValuePair in obj)
			{
				if (num++ == index)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x0002DD94 File Offset: 0x0002BF94
		public void Add(string key, object value)
		{
			this._members.Add(key, value);
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x0002DDA4 File Offset: 0x0002BFA4
		public bool ContainsKey(string key)
		{
			return this._members.ContainsKey(key);
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x0002DDB4 File Offset: 0x0002BFB4
		public ICollection<string> Keys
		{
			get
			{
				return this._members.Keys;
			}
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x0002DDC4 File Offset: 0x0002BFC4
		public bool Remove(string key)
		{
			return this._members.Remove(key);
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x0002DDD4 File Offset: 0x0002BFD4
		public bool TryGetValue(string key, out object value)
		{
			return this._members.TryGetValue(key, out value);
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x0002DDE4 File Offset: 0x0002BFE4
		public ICollection<object> Values
		{
			get
			{
				return this._members.Values;
			}
		}

		// Token: 0x17000912 RID: 2322
		public object this[string key]
		{
			get
			{
				return this._members[key];
			}
			set
			{
				this._members[key] = value;
			}
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x0002DE14 File Offset: 0x0002C014
		public void Add(KeyValuePair<string, object> item)
		{
			this._members.Add(item.Key, item.Value);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x0002DE30 File Offset: 0x0002C030
		public void Clear()
		{
			this._members.Clear();
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x0002DE40 File Offset: 0x0002C040
		public bool Contains(KeyValuePair<string, object> item)
		{
			return this._members.ContainsKey(item.Key) && this._members[item.Key] == item.Value;
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x0002DE84 File Offset: 0x0002C084
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = this.Count;
			foreach (KeyValuePair<string, object> keyValuePair in this)
			{
				array[arrayIndex++] = keyValuePair;
				if (--num <= 0)
				{
					break;
				}
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002431 RID: 9265 RVA: 0x0002DF18 File Offset: 0x0002C118
		public int Count
		{
			get
			{
				return this._members.Count;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x0002DF28 File Offset: 0x0002C128
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x0002DF2C File Offset: 0x0002C12C
		public bool Remove(KeyValuePair<string, object> item)
		{
			return this._members.Remove(item.Key);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x0002DF40 File Offset: 0x0002C140
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return this._members.GetEnumerator();
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x0002DF54 File Offset: 0x0002C154
		public override string ToString()
		{
			return SimpleJson.SerializeObject(this);
		}

		// Token: 0x04000993 RID: 2451
		private readonly Dictionary<string, object> _members;
	}
}
