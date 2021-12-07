using System;
using System.Collections;

namespace System.IO.IsolatedStorage
{
	// Token: 0x02000269 RID: 617
	internal class IsolatedStorageFileEnumerator : IEnumerator
	{
		// Token: 0x06002028 RID: 8232 RVA: 0x00076BB0 File Offset: 0x00074DB0
		public IsolatedStorageFileEnumerator(IsolatedStorageScope scope, string root)
		{
			this._scope = scope;
			if (Directory.Exists(root))
			{
				this._storages = Directory.GetDirectories(root, "d.*");
			}
			this._pos = -1;
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x00076BF0 File Offset: 0x00074DF0
		public object Current
		{
			get
			{
				if (this._pos < 0 || this._storages == null || this._pos >= this._storages.Length)
				{
					return null;
				}
				return new IsolatedStorageFile(this._scope, this._storages[this._pos]);
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00076C44 File Offset: 0x00074E44
		public bool MoveNext()
		{
			return this._storages != null && ++this._pos < this._storages.Length;
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00076C7C File Offset: 0x00074E7C
		public void Reset()
		{
			this._pos = -1;
		}

		// Token: 0x04000BE7 RID: 3047
		private IsolatedStorageScope _scope;

		// Token: 0x04000BE8 RID: 3048
		private string[] _storages;

		// Token: 0x04000BE9 RID: 3049
		private int _pos;
	}
}
