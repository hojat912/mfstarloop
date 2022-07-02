using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using System;


namespace DependencyInjection
{
    public class ServiceLocator : IServiceLocator
    {
        public  Dictionary<object, object> servicecontainer = null;
        public ServiceLocator()
        {
            servicecontainer = new Dictionary<object, object>();    
        }

        public void AddService(Type type, object instance)
        {
            servicecontainer.Add(type, instance);
        }

        public void ReplaceService(Type type, object instance)
        {
            servicecontainer[type] = instance;        
        }

        public T GetService<T>()
        {
            try
            {
                return (T)servicecontainer[typeof(T)];
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Service not available.");
            }
        }


    }
}