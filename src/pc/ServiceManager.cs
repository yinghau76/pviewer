using System;
using System.Collections;

namespace PViewer
{
	/// <summary>
	/// Summary description for ServiceManager.
	/// </summary>
	public class ServiceManager
	{
        ArrayList serviceList = new ArrayList();
        Hashtable serviceHashtable = new Hashtable();

        static ServiceManager defaultServiceManager = new ServiceManager();

        public static ServiceManager Services
        {
            get { return defaultServiceManager; }
        }

		private ServiceManager()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        protected void AddService(IService service)
        {
            serviceList.Add(service);
        }

        protected void AddServices(IService[] services)
        {
            foreach (IService service in services)
            {
                serviceList.Add(service);
            }
        }

        public void UnloadAllServices()
        {
            foreach (IService service in serviceList)
            {
                service.Unload();
            }
        }

        public IService GetService(Type serviceType)
        {
            IService s = (IService) serviceHashtable[serviceType];
            if (s != null) 
            {
                return s;
            }

            foreach (IService service in serviceList)
            {
                if (serviceType.IsInstanceOfType(service))
                {
                    serviceHashtable[serviceType] = service;
                    return service;
                }
            }

            return null;
        }
	}
}
