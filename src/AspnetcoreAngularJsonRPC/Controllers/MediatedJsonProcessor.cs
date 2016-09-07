using MediatR;
using Newtonsoft.Json;
using System;
using System.Reflection;


namespace AspnetcoreAngularJsonRPC.Controllers
{
    public class MediatedJsonProcessor : IJsonProcessor
    {
        private readonly IMediator _mediator;

        public MediatedJsonProcessor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Object Process(string name, string json)
        {
            var type = FindRequestTypeByName(name);
            var request = CreateRequestFromType(type);

            //Bind Json to Command object
            if (!String.IsNullOrWhiteSpace(json))
            {
                JsonConvert.PopulateObject(json, request);
            }


            Type iface = null;
            foreach (var item in type.GetInterfaces())
            {
                if (item.Name == "IRequest`1")
                {
                    iface = item;
                    break;
                }
            }
            //Invoke
            //  var iface = type.GetInterfaces("IRequest`1");
            var hh = type.GetGenericArguments();
            var method = _mediator.GetType().GetMethod("Send").MakeGenericMethod(iface.GetGenericArguments());
            //  var methodAsync=_mediator.GetType().GetMethod("SendAsync").MakeGenericMethod(iface.GetGenericArguments());           
            return method.Invoke(_mediator, new[] { request });
        }

        private static Type FindRequestTypeByName(string name)
        {
            try
            {
                Type returnType;
                //foreach (var item in ObjectFactory.GetAllInstances(typeof(IRequest<>)))
                //{
                //    if (item.GetType().Name.Contains(name))
                //    {
                //        returnType = item.GetType();
                //        break;
                //    }
                //}

                //  var handlerTypes =
                //ObjectFactory.Container.Model.AllInstances.Where(
                //      i =>
                //      i.PluginType.IsGenericType && i.PluginType.GetGenericTypeDefinition() == typeof(IRequest<>))
                //      //.Select(m => m.)
                //      .ToArray();

                // var jaa = ObjectFactory.Container.Model.
                //var jaba = App_Start.StructuremapMvc.StructureMapDependencyScope.Container.Model.GetAllPossible<>
                //HACK: GetTypes some other way, like finding instances of IRequest on app load
                return Type.GetType(name, true, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to find request: " + name, ex);
            }
        }

        private static Object CreateRequestFromType(Type type)
        {
            try
            {

                return Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to find default constructor for request: " + type, ex);
            }
        }
    };
}
