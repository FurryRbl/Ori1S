using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a simple implementation of the <see cref="T:System.ComponentModel.Design.IServiceContainer" /> interface. This class cannot be inherited.</summary>
	// Token: 0x0200013A RID: 314
	public class ServiceContainer : IDisposable, IServiceProvider, IServiceContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class.</summary>
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001E79C File Offset: 0x0001C99C
		public ServiceContainer() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class using the specified parent service provider.</summary>
		/// <param name="parentProvider">A parent service provider. </param>
		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		public ServiceContainer(IServiceProvider parentProvider)
		{
			this.parentProvider = parentProvider;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0001E7B8 File Offset: 0x0001C9B8
		private Hashtable Services
		{
			get
			{
				if (this.services == null)
				{
					this.services = new Hashtable();
				}
				return this.services;
			}
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06000BB8 RID: 3000 RVA: 0x0001E7D8 File Offset: 0x0001C9D8
		public void AddService(Type serviceType, object serviceInstance)
		{
			this.AddService(serviceType, serviceInstance, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001E7E4 File Offset: 0x0001C9E4
		public void AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			this.AddService(serviceType, callback, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		/// <param name="promote">true if this service should be added to any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06000BBA RID: 3002 RVA: 0x0001E7F0 File Offset: 0x0001C9F0
		public virtual void AddService(Type serviceType, object serviceInstance, bool promote)
		{
			if (promote && this.parentProvider != null)
			{
				IServiceContainer serviceContainer = (IServiceContainer)this.parentProvider.GetService(typeof(IServiceContainer));
				serviceContainer.AddService(serviceType, serviceInstance, promote);
				return;
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (serviceInstance == null)
			{
				throw new ArgumentNullException("serviceInstance");
			}
			if (this.Services.Contains(serviceType))
			{
				throw new ArgumentException(string.Format("The service {0} already exists in the service container.", serviceType.ToString()), "serviceType");
			}
			this.Services.Add(serviceType, serviceInstance);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested. </param>
		/// <param name="promote">true if this service should be added to any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06000BBB RID: 3003 RVA: 0x0001E890 File Offset: 0x0001CA90
		public virtual void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			if (promote && this.parentProvider != null)
			{
				IServiceContainer serviceContainer = (IServiceContainer)this.parentProvider.GetService(typeof(IServiceContainer));
				serviceContainer.AddService(serviceType, callback, promote);
				return;
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (this.Services.Contains(serviceType))
			{
				throw new ArgumentException(string.Format("The service {0} already exists in the service container.", serviceType.ToString()), "serviceType");
			}
			this.Services.Add(serviceType, callback);
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is null.</exception>
		// Token: 0x06000BBC RID: 3004 RVA: 0x0001E930 File Offset: 0x0001CB30
		public void RemoveService(Type serviceType)
		{
			this.RemoveService(serviceType, false);
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		/// <param name="promote">true if this service should be removed from any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is null.</exception>
		// Token: 0x06000BBD RID: 3005 RVA: 0x0001E93C File Offset: 0x0001CB3C
		public virtual void RemoveService(Type serviceType, bool promote)
		{
			if (promote && this.parentProvider != null)
			{
				IServiceContainer serviceContainer = (IServiceContainer)this.parentProvider.GetService(typeof(IServiceContainer));
				serviceContainer.RemoveService(serviceType, promote);
				return;
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			this.Services.Remove(serviceType);
		}

		/// <summary>Gets the requested service.</summary>
		/// <returns>An instance of the service if it could be found, or null if it could not be found.</returns>
		/// <param name="serviceType">The type of service to retrieve. </param>
		// Token: 0x06000BBE RID: 3006 RVA: 0x0001E99C File Offset: 0x0001CB9C
		public virtual object GetService(Type serviceType)
		{
			object obj = null;
			Type[] defaultServices = this.DefaultServices;
			for (int i = 0; i < defaultServices.Length; i++)
			{
				if (defaultServices[i] == serviceType)
				{
					obj = this;
					break;
				}
			}
			if (obj == null)
			{
				obj = this.Services[serviceType];
			}
			if (obj == null && this.parentProvider != null)
			{
				obj = this.parentProvider.GetService(serviceType);
			}
			if (obj != null)
			{
				ServiceCreatorCallback serviceCreatorCallback = obj as ServiceCreatorCallback;
				if (serviceCreatorCallback != null)
				{
					obj = serviceCreatorCallback(this, serviceType);
					this.Services[serviceType] = obj;
				}
			}
			return obj;
		}

		/// <summary>Gets the default services implemented directly by <see cref="T:System.ComponentModel.Design.ServiceContainer" />.</summary>
		/// <returns>The default services.</returns>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0001EA34 File Offset: 0x0001CC34
		protected virtual Type[] DefaultServices
		{
			get
			{
				return new Type[]
				{
					typeof(IServiceContainer),
					typeof(ServiceContainer)
				};
			}
		}

		/// <summary>Disposes this service container.</summary>
		// Token: 0x06000BC0 RID: 3008 RVA: 0x0001EA64 File Offset: 0x0001CC64
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Disposes this service container.</summary>
		/// <param name="disposing">true if the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> is in the process of being disposed of; otherwise, false.</param>
		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001EA74 File Offset: 0x0001CC74
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing && this.services != null)
				{
					foreach (object obj in this.services)
					{
						if (obj is IDisposable)
						{
							((IDisposable)obj).Dispose();
						}
					}
					this.services = null;
				}
				this._disposed = true;
			}
		}

		// Token: 0x04000310 RID: 784
		private IServiceProvider parentProvider;

		// Token: 0x04000311 RID: 785
		private Hashtable services;

		// Token: 0x04000312 RID: 786
		private bool _disposed;
	}
}
