using System;
using System.Text;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000023 RID: 35
	public sealed class UnixSymbolicLinkInfo : UnixFileSystemInfo
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00007FC4 File Offset: 0x000061C4
		public UnixSymbolicLinkInfo(string path) : base(path)
		{
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007FD0 File Offset: 0x000061D0
		internal UnixSymbolicLinkInfo(string path, Stat stat) : base(path, stat)
		{
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00007FDC File Offset: 0x000061DC
		public override string Name
		{
			get
			{
				return UnixPath.GetFileName(base.FullPath);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00007FEC File Offset: 0x000061EC
		[Obsolete("Use GetContents()")]
		public UnixFileSystemInfo Contents
		{
			get
			{
				return this.GetContents();
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007FF4 File Offset: 0x000061F4
		public string ContentsPath
		{
			get
			{
				return this.ReadLink();
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007FFC File Offset: 0x000061FC
		public bool HasContents
		{
			get
			{
				return this.TryReadLink() != null;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000800C File Offset: 0x0000620C
		public UnixFileSystemInfo GetContents()
		{
			string text = this.ReadLink();
			return UnixFileSystemInfo.GetFileSystemEntry(UnixPath.Combine(UnixPath.GetDirectoryName(base.FullPath), new string[]
			{
				this.ContentsPath
			}));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008044 File Offset: 0x00006244
		public void CreateSymbolicLinkTo(string path)
		{
			int retval = Syscall.symlink(path, this.FullName);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008064 File Offset: 0x00006264
		public void CreateSymbolicLinkTo(UnixFileSystemInfo path)
		{
			int retval = Syscall.symlink(path.FullName, this.FullName);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000808C File Offset: 0x0000628C
		public override void Delete()
		{
			int retval = Syscall.unlink(base.FullPath);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			base.Refresh();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000080B4 File Offset: 0x000062B4
		public override void SetOwner(long owner, long group)
		{
			int retval = Syscall.lchown(base.FullPath, Convert.ToUInt32(owner), Convert.ToUInt32(group));
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000080E0 File Offset: 0x000062E0
		protected override bool GetFileStatus(string path, out Stat stat)
		{
			return Syscall.lstat(path, out stat) == 0;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000080EC File Offset: 0x000062EC
		private string ReadLink()
		{
			string text = this.TryReadLink();
			if (text == null)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return text;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000810C File Offset: 0x0000630C
		private string TryReadLink()
		{
			StringBuilder stringBuilder = new StringBuilder((int)base.Length + 1);
			int num = Syscall.readlink(base.FullPath, stringBuilder);
			if (num == -1)
			{
				return null;
			}
			return stringBuilder.ToString(0, num);
		}
	}
}
