using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004AA RID: 1194
	[Serializable]
	internal class MethodDictionary : IEnumerable, ICollection, IDictionary
	{
		// Token: 0x06003070 RID: 12400 RVA: 0x0009F6EC File Offset: 0x0009D8EC
		public MethodDictionary(IMethodMessage message)
		{
			this._message = message;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x0009F6FC File Offset: 0x0009D8FC
		public MethodDictionary(string[] keys)
		{
			this._methodKeys = keys;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x0009F70C File Offset: 0x0009D90C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MethodDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06003073 RID: 12403 RVA: 0x0009F714 File Offset: 0x0009D914
		internal bool HasInternalProperties
		{
			get
			{
				if (this._internalProperties == null)
				{
					return false;
				}
				if (this._internalProperties is MethodDictionary)
				{
					return ((MethodDictionary)this._internalProperties).HasInternalProperties;
				}
				return this._internalProperties.Count > 0;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x0009F760 File Offset: 0x0009D960
		internal IDictionary InternalProperties
		{
			get
			{
				if (this._internalProperties != null && this._internalProperties is MethodDictionary)
				{
					return ((MethodDictionary)this._internalProperties).InternalProperties;
				}
				return this._internalProperties;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x0009F7A0 File Offset: 0x0009D9A0
		// (set) Token: 0x06003076 RID: 12406 RVA: 0x0009F7A8 File Offset: 0x0009D9A8
		public string[] MethodKeys
		{
			get
			{
				return this._methodKeys;
			}
			set
			{
				this._methodKeys = value;
			}
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x0009F7B4 File Offset: 0x0009D9B4
		protected virtual IDictionary AllocInternalProperties()
		{
			this._ownProperties = true;
			return new Hashtable();
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x0009F7C4 File Offset: 0x0009D9C4
		public IDictionary GetInternalProperties()
		{
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			return this._internalProperties;
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x0009F7E4 File Offset: 0x0009D9E4
		private bool IsOverridenKey(string key)
		{
			if (this._ownProperties)
			{
				return false;
			}
			foreach (string b in this._methodKeys)
			{
				if (key == b)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x0009F82C File Offset: 0x0009DA2C
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x0009F830 File Offset: 0x0009DA30
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008F3 RID: 2291
		public object this[object key]
		{
			get
			{
				string text = (string)key;
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					if (this._methodKeys[i] == text)
					{
						return this.GetMethodProperty(text);
					}
				}
				if (this._internalProperties != null)
				{
					return this._internalProperties[key];
				}
				return null;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x0009F8A4 File Offset: 0x0009DAA4
		protected virtual object GetMethodProperty(string key)
		{
			switch (key)
			{
			case "__Uri":
				return this._message.Uri;
			case "__MethodName":
				return this._message.MethodName;
			case "__TypeName":
				return this._message.TypeName;
			case "__MethodSignature":
				return this._message.MethodSignature;
			case "__CallContext":
				return this._message.LogicalCallContext;
			case "__Args":
				return this._message.Args;
			case "__OutArgs":
				return ((IMethodReturnMessage)this._message).OutArgs;
			case "__Return":
				return ((IMethodReturnMessage)this._message).ReturnValue;
			}
			return null;
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x0009F9D8 File Offset: 0x0009DBD8
		protected virtual void SetMethodProperty(string key, object value)
		{
			switch (key)
			{
			case "__CallContext":
			case "__OutArgs":
			case "__Return":
				return;
			case "__MethodName":
			case "__TypeName":
			case "__MethodSignature":
			case "__Args":
				throw new ArgumentException("key was invalid");
			case "__Uri":
				((IInternalMessage)this._message).Uri = (string)value;
				return;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x0009FAB0 File Offset: 0x0009DCB0
		public ICollection Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this._methodKeys[i]);
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties.Keys)
					{
						string text = (string)obj;
						if (!this.IsOverridenKey(text))
						{
							arrayList.Add(text);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x0009FB70 File Offset: 0x0009DD70
		public ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this.GetMethodProperty(this._methodKeys[i]));
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (!this.IsOverridenKey((string)dictionaryEntry.Key))
						{
							arrayList.Add(dictionaryEntry.Value);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x0009FC40 File Offset: 0x0009DE40
		public void Add(object key, object value)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					this.SetMethodProperty(text, value);
					return;
				}
			}
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			this._internalProperties[key] = value;
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x0009FCB0 File Offset: 0x0009DEB0
		public void Clear()
		{
			if (this._internalProperties != null)
			{
				this._internalProperties.Clear();
			}
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x0009FCC8 File Offset: 0x0009DEC8
		public bool Contains(object key)
		{
			string b = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == b)
				{
					return true;
				}
			}
			return this._internalProperties != null && this._internalProperties.Contains(key);
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x0009FD24 File Offset: 0x0009DF24
		public void Remove(object key)
		{
			string b = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == b)
				{
					throw new ArgumentException("key was invalid");
				}
			}
			if (this._internalProperties != null)
			{
				this._internalProperties.Remove(key);
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x0009FD88 File Offset: 0x0009DF88
		public int Count
		{
			get
			{
				if (this._internalProperties != null)
				{
					return this._internalProperties.Count + this._methodKeys.Length;
				}
				return this._methodKeys.Length;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06003087 RID: 12423 RVA: 0x0009FDC0 File Offset: 0x0009DFC0
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x0009FDC4 File Offset: 0x0009DFC4
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x0009FDC8 File Offset: 0x0009DFC8
		public void CopyTo(Array array, int index)
		{
			this.Values.CopyTo(array, index);
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x0009FDD8 File Offset: 0x0009DFD8
		public IDictionaryEnumerator GetEnumerator()
		{
			return new MethodDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x04001484 RID: 5252
		private IDictionary _internalProperties;

		// Token: 0x04001485 RID: 5253
		protected IMethodMessage _message;

		// Token: 0x04001486 RID: 5254
		private string[] _methodKeys;

		// Token: 0x04001487 RID: 5255
		private bool _ownProperties;

		// Token: 0x020004AB RID: 1195
		private class DictionaryEnumerator : IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x0600308B RID: 12427 RVA: 0x0009FDE0 File Offset: 0x0009DFE0
			public DictionaryEnumerator(MethodDictionary methodDictionary)
			{
				this._methodDictionary = methodDictionary;
				IDictionaryEnumerator hashtableEnum;
				if (this._methodDictionary._internalProperties != null)
				{
					IDictionaryEnumerator enumerator = this._methodDictionary._internalProperties.GetEnumerator();
					hashtableEnum = enumerator;
				}
				else
				{
					hashtableEnum = null;
				}
				this._hashtableEnum = hashtableEnum;
				this._posMethod = -1;
			}

			// Token: 0x170008F9 RID: 2297
			// (get) Token: 0x0600308C RID: 12428 RVA: 0x0009FE30 File Offset: 0x0009E030
			public object Current
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x0600308D RID: 12429 RVA: 0x0009FE4C File Offset: 0x0009E04C
			public bool MoveNext()
			{
				if (this._posMethod != -2)
				{
					this._posMethod++;
					if (this._posMethod < this._methodDictionary._methodKeys.Length)
					{
						return true;
					}
					this._posMethod = -2;
				}
				if (this._hashtableEnum == null)
				{
					return false;
				}
				while (this._hashtableEnum.MoveNext())
				{
					if (!this._methodDictionary.IsOverridenKey((string)this._hashtableEnum.Key))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600308E RID: 12430 RVA: 0x0009FEDC File Offset: 0x0009E0DC
			public void Reset()
			{
				this._posMethod = -1;
				this._hashtableEnum.Reset();
			}

			// Token: 0x170008FA RID: 2298
			// (get) Token: 0x0600308F RID: 12431 RVA: 0x0009FEF0 File Offset: 0x0009E0F0
			public DictionaryEntry Entry
			{
				get
				{
					if (this._posMethod >= 0)
					{
						return new DictionaryEntry(this._methodDictionary._methodKeys[this._posMethod], this._methodDictionary.GetMethodProperty(this._methodDictionary._methodKeys[this._posMethod]));
					}
					if (this._posMethod == -1 || this._hashtableEnum == null)
					{
						throw new InvalidOperationException("The enumerator is positioned before the first element of the collection or after the last element");
					}
					return this._hashtableEnum.Entry;
				}
			}

			// Token: 0x170008FB RID: 2299
			// (get) Token: 0x06003090 RID: 12432 RVA: 0x0009FF6C File Offset: 0x0009E16C
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x170008FC RID: 2300
			// (get) Token: 0x06003091 RID: 12433 RVA: 0x0009FF88 File Offset: 0x0009E188
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x0400148A RID: 5258
			private MethodDictionary _methodDictionary;

			// Token: 0x0400148B RID: 5259
			private IDictionaryEnumerator _hashtableEnum;

			// Token: 0x0400148C RID: 5260
			private int _posMethod;
		}
	}
}
