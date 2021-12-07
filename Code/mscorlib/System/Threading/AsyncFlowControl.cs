using System;
using System.Security;

namespace System.Threading
{
	/// <summary>Provides the functionality to restore the migration, or flow, of the execution context between threads.  </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000699 RID: 1689
	public struct AsyncFlowControl : IDisposable
	{
		// Token: 0x06004055 RID: 16469 RVA: 0x000DEAD4 File Offset: 0x000DCCD4
		internal AsyncFlowControl(Thread t, AsyncFlowControlType type)
		{
			this._t = t;
			this._type = type;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Threading.AsyncFlowControl" />.</summary>
		// Token: 0x06004056 RID: 16470 RVA: 0x000DEAE4 File Offset: 0x000DCCE4
		void IDisposable.Dispose()
		{
			if (this._t != null)
			{
				this.Undo();
				this._t = null;
				this._type = AsyncFlowControlType.None;
			}
		}

		/// <summary>Restores the flow of the execution context between threads.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.AsyncFlowControl" /> structure is not used on the thread where it was created.-or-The <see cref="T:System.Threading.AsyncFlowControl" /> structure has already been used to call <see cref="M:System.Threading.AsyncFlowControl.Undo" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004057 RID: 16471 RVA: 0x000DEB08 File Offset: 0x000DCD08
		public void Undo()
		{
			if (this._t == null)
			{
				throw new InvalidOperationException(Locale.GetText("Can only be called once."));
			}
			AsyncFlowControlType type = this._type;
			if (type != AsyncFlowControlType.Execution)
			{
				if (type == AsyncFlowControlType.Security)
				{
					SecurityContext.RestoreFlow();
				}
			}
			else
			{
				ExecutionContext.RestoreFlow();
			}
			this._t = null;
		}

		/// <summary>Gets a hash code for the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</returns>
		// Token: 0x06004058 RID: 16472 RVA: 0x000DEB68 File Offset: 0x000DCD68
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure. </summary>
		/// <returns>true if <paramref name="obj" /> is an <see cref="T:System.Threading.AsyncFlowControl" /> structure and is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure; otherwise, false. </returns>
		/// <param name="obj">An object to compare with the current structure.</param>
		// Token: 0x06004059 RID: 16473 RVA: 0x000DEB7C File Offset: 0x000DCD7C
		public override bool Equals(object obj)
		{
			return obj is AsyncFlowControl && obj.Equals(this);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Threading.AsyncFlowControl" /> structure is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure; otherwise, false. </returns>
		/// <param name="obj">An <see cref="T:System.Threading.AsyncFlowControl" /> structure to compare with the current structure.</param>
		// Token: 0x0600405A RID: 16474 RVA: 0x000DEB9C File Offset: 0x000DCD9C
		public bool Equals(AsyncFlowControl obj)
		{
			return this._t == obj._t && this._type == obj._type;
		}

		/// <summary>Compares two <see cref="T:System.Threading.AsyncFlowControl" /> structures to determine whether they are equal. </summary>
		/// <returns>true if the two structures are equal; otherwise, false. </returns>
		/// <param name="a">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		/// <param name="b">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		// Token: 0x0600405B RID: 16475 RVA: 0x000DEBC8 File Offset: 0x000DCDC8
		public static bool operator ==(AsyncFlowControl a, AsyncFlowControl b)
		{
			return a.Equals(b);
		}

		/// <summary>Compares two <see cref="T:System.Threading.AsyncFlowControl" /> structures to determine whether they are not equal. </summary>
		/// <returns>true if the structures are not equal; otherwise, false. </returns>
		/// <param name="a">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		/// <param name="b">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		// Token: 0x0600405C RID: 16476 RVA: 0x000DEBD4 File Offset: 0x000DCDD4
		public static bool operator !=(AsyncFlowControl a, AsyncFlowControl b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04001BAA RID: 7082
		private Thread _t;

		// Token: 0x04001BAB RID: 7083
		private AsyncFlowControlType _type;
	}
}
