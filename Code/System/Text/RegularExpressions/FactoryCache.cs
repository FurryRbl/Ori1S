using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000468 RID: 1128
	internal class FactoryCache
	{
		// Token: 0x06002859 RID: 10329 RVA: 0x00080084 File Offset: 0x0007E284
		public FactoryCache(int capacity)
		{
			this.capacity = capacity;
			this.factories = new Hashtable(capacity);
			this.mru_list = new MRUList();
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000800B8 File Offset: 0x0007E2B8
		public void Add(string pattern, RegexOptions options, IMachineFactory factory)
		{
			lock (this)
			{
				FactoryCache.Key key = new FactoryCache.Key(pattern, options);
				this.Cleanup();
				this.factories[key] = factory;
				this.mru_list.Use(key);
			}
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x0008011C File Offset: 0x0007E31C
		private void Cleanup()
		{
			while (this.factories.Count >= this.capacity && this.capacity > 0)
			{
				object obj = this.mru_list.Evict();
				if (obj != null)
				{
					this.factories.Remove((FactoryCache.Key)obj);
				}
			}
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x00080174 File Offset: 0x0007E374
		public IMachineFactory Lookup(string pattern, RegexOptions options)
		{
			lock (this)
			{
				FactoryCache.Key key = new FactoryCache.Key(pattern, options);
				if (this.factories.Contains(key))
				{
					this.mru_list.Use(key);
					return (IMachineFactory)this.factories[key];
				}
			}
			return null;
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x000801F0 File Offset: 0x0007E3F0
		// (set) Token: 0x0600285E RID: 10334 RVA: 0x000801F8 File Offset: 0x0007E3F8
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
			set
			{
				lock (this)
				{
					this.capacity = value;
					this.Cleanup();
				}
			}
		}

		// Token: 0x040018FD RID: 6397
		private int capacity;

		// Token: 0x040018FE RID: 6398
		private Hashtable factories;

		// Token: 0x040018FF RID: 6399
		private MRUList mru_list;

		// Token: 0x02000469 RID: 1129
		private class Key
		{
			// Token: 0x0600285F RID: 10335 RVA: 0x00080244 File Offset: 0x0007E444
			public Key(string pattern, RegexOptions options)
			{
				this.pattern = pattern;
				this.options = options;
			}

			// Token: 0x06002860 RID: 10336 RVA: 0x0008025C File Offset: 0x0007E45C
			public override int GetHashCode()
			{
				return this.pattern.GetHashCode() ^ (int)this.options;
			}

			// Token: 0x06002861 RID: 10337 RVA: 0x00080270 File Offset: 0x0007E470
			public override bool Equals(object o)
			{
				if (o == null || !(o is FactoryCache.Key))
				{
					return false;
				}
				FactoryCache.Key key = (FactoryCache.Key)o;
				return this.options == key.options && this.pattern.Equals(key.pattern);
			}

			// Token: 0x06002862 RID: 10338 RVA: 0x000802BC File Offset: 0x0007E4BC
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					"('",
					this.pattern,
					"', [",
					this.options,
					"])"
				});
			}

			// Token: 0x04001900 RID: 6400
			public string pattern;

			// Token: 0x04001901 RID: 6401
			public RegexOptions options;
		}
	}
}
