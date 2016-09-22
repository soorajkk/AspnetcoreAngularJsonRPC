using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using MediatR;

namespace AspnetcoreAngularJsonRPC.Misc
{
    public interface IamSingleton
    {
        List<Type> GetAvaiableCommandList();
    }
    public class IamSingletonDefault : IamSingleton
    {
        static List<Type> lstAvailbeCommands = new List<Type>();
        static IamSingletonDefault()
        {
            LoadAvaiableCommandList();
        }

        private static void LoadAvaiableCommandList()
        {
            Assembly s = null;
            foreach (var compilationLibrary in DependencyContext.Default.RuntimeLibraries)
            {
                if (compilationLibrary.Name.Contains("AspnetcoreAngularJsonRPC"))
                {
                    s = Assembly.Load(new AssemblyName(compilationLibrary.Name));
                    break;
                }
            }

            var dict = new Dictionary<string, Type>();

            var iRequest = typeof(IRequest<>);
            var iAsyncRequest = typeof(IAsyncRequest<>);

            foreach (var type in s.GetTypes())
            {
                if (!type.GetTypeInfo().IsAbstract && (InheritsOrImplements(type, iRequest) || InheritsOrImplements(type, iAsyncRequest)))
                    //dict[type.Name] = type;
                    lstAvailbeCommands.Add(type);
            }
        }

        //REF: http://stackoverflow.com/a/4897426/366559
        public static bool InheritsOrImplements(Type child, Type parent)
        {
            parent = ResolveGenericTypeDefinition(parent);

            var currentChild = child.GetTypeInfo().IsGenericType
                ? child.GetGenericTypeDefinition()
                : child;

            while (currentChild != typeof(object))
            {
                if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
                    return true;

                currentChild = currentChild.GetTypeInfo().BaseType != null
                               && currentChild.GetTypeInfo().BaseType.GetTypeInfo().IsGenericType
                    ? currentChild.GetTypeInfo().BaseType.GetGenericTypeDefinition()
                    : currentChild.GetTypeInfo().BaseType;

                if (currentChild == null)
                    return false;
            }
            return false;
        }

        private static bool HasAnyInterfaces(Type parent, Type child)
        {
            return child.GetInterfaces()
                .Any(childInterface =>
                {
                    var currentInterface = childInterface.GetTypeInfo().IsGenericType
                        ? childInterface.GetGenericTypeDefinition()
                        : childInterface;

                    return currentInterface == parent;
                });
        }

        private static Type ResolveGenericTypeDefinition(Type parent)
        {
            var shouldUseGenericType = parent.GetTypeInfo().IsGenericType && parent.GetGenericTypeDefinition() != parent;

            if (parent.GetTypeInfo().IsGenericType && shouldUseGenericType)
                parent = parent.GetGenericTypeDefinition();

            return parent;
        }

        public List<Type> GetAvaiableCommandList()
        {
            return lstAvailbeCommands;
        }
    }
}
