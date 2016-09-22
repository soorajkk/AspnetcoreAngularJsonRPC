using AspnetcoreAngularJsonRPC.Misc;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace AspnetcoreAngularJsonRPC.Controllers
{
    public class MediatedJsonProcessor : IJsonProcessor
    {
        private readonly IMediator _mediator;
        private readonly IamSingleton _iamSingleton;
        static List<Type> lstMediatorTypes = new List<Type>();
        public MediatedJsonProcessor(IMediator mediator, IamSingleton iamSingleton)
        {
            _mediator = mediator;
            _iamSingleton = iamSingleton;
        }

        public static void Initialize(params Assembly[] assemblies)
        {
            string s = "hadsasdsd";
            //_commandTypes = LoadAllRequestTypes(assemblies.Union(new[] { typeof(Commander).Assembly }));
        }

        public Object Process(string name, string json)
        {
            var type = FindRequestTypeByName(name, _iamSingleton.GetAvaiableCommandList());
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

        private static Type FindRequestTypeByName(string name, List<Type> avaiableCommands)
        {
            try
            {
                if (name.Contains("."))
                {

                    //HACK: GetTypes some other way, like finding instances of IRequest on app load
                    return Type.GetType(name, true, true);
                }
                return avaiableCommands.Where(p => p.Name.Trim() == name.Trim()).FirstOrDefault();

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
