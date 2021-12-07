using System;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200001E RID: 30
	public struct UnixPipes : IEquatable<UnixPipes>
	{
		// Token: 0x0600017B RID: 379 RVA: 0x0000708C File Offset: 0x0000528C
		public UnixPipes(UnixStream reading, UnixStream writing)
		{
			this.Reading = reading;
			this.Writing = writing;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000709C File Offset: 0x0000529C
		public static UnixPipes CreatePipes()
		{
			int fileDescriptor;
			int fileDescriptor2;
			int retval = Syscall.pipe(out fileDescriptor, out fileDescriptor2);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			return new UnixPipes(new UnixStream(fileDescriptor), new UnixStream(fileDescriptor2));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000070CC File Offset: 0x000052CC
		public override bool Equals(object value)
		{
			if (value == null || value.GetType() != base.GetType())
			{
				return false;
			}
			UnixPipes unixPipes = (UnixPipes)value;
			return this.Reading.Handle == unixPipes.Reading.Handle && this.Writing.Handle == unixPipes.Writing.Handle;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000713C File Offset: 0x0000533C
		public bool Equals(UnixPipes value)
		{
			return this.Reading.Handle == value.Reading.Handle && this.Writing.Handle == value.Writing.Handle;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007184 File Offset: 0x00005384
		public override int GetHashCode()
		{
			return this.Reading.Handle.GetHashCode() ^ this.Writing.Handle.GetHashCode();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000071B8 File Offset: 0x000053B8
		public static bool operator ==(UnixPipes lhs, UnixPipes rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000071C4 File Offset: 0x000053C4
		public static bool operator !=(UnixPipes lhs, UnixPipes rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000073 RID: 115
		public UnixStream Reading;

		// Token: 0x04000074 RID: 116
		public UnixStream Writing;
	}
}
