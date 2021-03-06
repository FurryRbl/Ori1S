using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents the Windows user prior to an impersonation operation.</summary>
	// Token: 0x0200066C RID: 1644
	[ComVisible(true)]
	public class WindowsImpersonationContext : IDisposable
	{
		// Token: 0x06003E73 RID: 15987 RVA: 0x000D6504 File Offset: 0x000D4704
		internal WindowsImpersonationContext(IntPtr token)
		{
			this._token = WindowsImpersonationContext.DuplicateToken(token);
			if (!WindowsImpersonationContext.SetCurrentToken(token))
			{
				throw new SecurityException("Couldn't impersonate token.");
			}
			this.undo = false;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Principal.WindowsImpersonationContext" />.</summary>
		// Token: 0x06003E74 RID: 15988 RVA: 0x000D6538 File Offset: 0x000D4738
		[ComVisible(false)]
		public void Dispose()
		{
			if (!this.undo)
			{
				this.Undo();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Principal.WindowsImpersonationContext" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003E75 RID: 15989 RVA: 0x000D654C File Offset: 0x000D474C
		[ComVisible(false)]
		protected virtual void Dispose(bool disposing)
		{
			if (!this.undo)
			{
				this.Undo();
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Reverts the user context to the Windows user represented by this object.</summary>
		/// <exception cref="T:System.Security.SecurityException">An attempt is made to use this method for any purpose other than to revert identity to self. </exception>
		// Token: 0x06003E76 RID: 15990 RVA: 0x000D656C File Offset: 0x000D476C
		public void Undo()
		{
			if (!WindowsImpersonationContext.RevertToSelf())
			{
				WindowsImpersonationContext.CloseToken(this._token);
				throw new SecurityException("Couldn't switch back to original token.");
			}
			WindowsImpersonationContext.CloseToken(this._token);
			this.undo = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003E77 RID: 15991
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CloseToken(IntPtr token);

		// Token: 0x06003E78 RID: 15992
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr DuplicateToken(IntPtr token);

		// Token: 0x06003E79 RID: 15993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetCurrentToken(IntPtr token);

		// Token: 0x06003E7A RID: 15994
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RevertToSelf();

		// Token: 0x04001B26 RID: 6950
		private IntPtr _token;

		// Token: 0x04001B27 RID: 6951
		private bool undo;
	}
}
