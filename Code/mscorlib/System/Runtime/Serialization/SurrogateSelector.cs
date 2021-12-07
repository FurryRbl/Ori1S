using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Assists formatters in selection of the serialization surrogate to delegate the serialization or deserialization process to.</summary>
	// Token: 0x0200050F RID: 1295
	[ComVisible(true)]
	public class SurrogateSelector : ISurrogateSelector
	{
		/// <summary>Adds a surrogate to the list of checked surrogates.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> for which the surrogate is required.</param>
		/// <param name="context">The context-specific data. </param>
		/// <param name="surrogate">The surrogate to call for this type. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> or <paramref name="surrogate" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">A surrogate already exists for this type and context. </exception>
		// Token: 0x0600338A RID: 13194 RVA: 0x000A6B1C File Offset: 0x000A4D1C
		public virtual void AddSurrogate(Type type, StreamingContext context, ISerializationSurrogate surrogate)
		{
			if (type == null || surrogate == null)
			{
				throw new ArgumentNullException("Null reference.");
			}
			string key = type.FullName + "#" + context.ToString();
			if (this.Surrogates.ContainsKey(key))
			{
				throw new ArgumentException("A surrogate for " + type.FullName + " already exists.");
			}
			this.Surrogates.Add(key, surrogate);
		}

		/// <summary>Adds the specified <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> that can handle a particular object type to the list of surrogates.</summary>
		/// <param name="selector">The surrogate selector to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="selector" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The selector is already on the list of selectors. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600338B RID: 13195 RVA: 0x000A6B98 File Offset: 0x000A4D98
		public virtual void ChainSelector(ISurrogateSelector selector)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("Selector is null.");
			}
			if (this.nextSelector != null)
			{
				selector.ChainSelector(this.nextSelector);
			}
			this.nextSelector = selector;
		}

		/// <summary>Returns the next selector on the chain of selectors.</summary>
		/// <returns>The next <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> on the chain of selectors.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600338C RID: 13196 RVA: 0x000A6BCC File Offset: 0x000A4DCC
		public virtual ISurrogateSelector GetNextSelector()
		{
			return this.nextSelector;
		}

		/// <summary>Returns the surrogate for a particular type.</summary>
		/// <returns>The surrogate for a particular type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> for which the surrogate is requested. </param>
		/// <param name="context">The streaming context. </param>
		/// <param name="selector">The surrogate to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x0600338D RID: 13197 RVA: 0x000A6BD4 File Offset: 0x000A4DD4
		public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type is null.");
			}
			string key = type.FullName + "#" + context.ToString();
			ISerializationSurrogate serializationSurrogate = (ISerializationSurrogate)this.Surrogates[key];
			if (serializationSurrogate != null)
			{
				selector = this;
				return serializationSurrogate;
			}
			if (this.nextSelector != null)
			{
				return this.nextSelector.GetSurrogate(type, context, out selector);
			}
			selector = null;
			return null;
		}

		/// <summary>Removes the surrogate associated with a given type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> for which to remove the surrogate. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> for the current surrogate. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		// Token: 0x0600338E RID: 13198 RVA: 0x000A6C4C File Offset: 0x000A4E4C
		public virtual void RemoveSurrogate(Type type, StreamingContext context)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type is null.");
			}
			string key = type.FullName + "#" + context.ToString();
			this.Surrogates.Remove(key);
		}

		// Token: 0x04001573 RID: 5491
		private Hashtable Surrogates = new Hashtable();

		// Token: 0x04001574 RID: 5492
		private ISurrogateSelector nextSelector;
	}
}
