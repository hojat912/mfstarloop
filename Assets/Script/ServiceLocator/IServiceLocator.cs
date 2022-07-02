using EventSystem;
using Gameplay.BoardManagment;
using System;

namespace DependencyInjection
{
    public interface IServiceLocator
    {
        void AddService(Type interfaceItem, object instance);
        void ReplaceService(Type type, object board);
        T GetService<T>();

    }
}