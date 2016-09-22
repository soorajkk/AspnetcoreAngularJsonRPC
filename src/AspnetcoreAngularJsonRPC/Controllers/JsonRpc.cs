using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace AspnetcoreAngularJsonRPC.Controllers
{

    public interface IJsonRpc
    {
        Object Execute(string data);
    }
    public class JsonRpc : IJsonRpc
    {
        private readonly IJsonProcessor _processor;

        public JsonRpc(IJsonProcessor processor)
        {
            _processor = processor;
        }

        public Object Execute(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return null;

            if (data.TrimStart().StartsWith("[")) //batch is an array instead of object
            {
                var requests = JsonConvert.DeserializeObject<JsonRpcRequest[]>(data);
                var responses = new List<JsonRpcResponse>();
                foreach (var request in requests)
                {
                    responses.Add(ExecuteCommand(request));
                }
                return responses;
            }
            else
            {
                var request = JsonConvert.DeserializeObject<JsonRpcRequest>(data);
                var response = ExecuteCommand(request);
                return response;
            }
        }

        private JsonRpcResponse ExecuteCommand(JsonRpcRequest request)
        {
            try
            {
                var json = request.@params == null ? null : request.@params.ToString();
                var result = _processor.Process(request.method, json);
                return new JsonRpcResponse
                {
                    jsonrpc = request.jsonrpc,
                    result = result,
                    error = null,
                    id = request.id,
                };
            }
            catch (Exception ex)
            {
                return new JsonRpcResponse
                {
                    jsonrpc = request.jsonrpc,
                    result = null,
                    error = new JsonRpcError
                    {
                        code = 1,
                        message = ex.GetBaseException().Message,
                        data = ex,
                    },
                    id = request.id,
                };
            }
        }
    }
    // [Authorize]
    [Route("api/[controller]")]
    public class JsonRpcController : Controller
    {
        private readonly IJsonRpc _jsonRpc;

        public JsonRpcController(IJsonRpc jsonRpc)
        {
            _jsonRpc = jsonRpc;
        }

        [Route("")]
        [HttpPost]
        public dynamic Execute()
        {
            var json = GetRequestAsString();
            return _jsonRpc.Execute(json);
        }


        [Route("{*tags}")]
        [HttpGet]
        public dynamic ExecuteGet(string tags)
        {

            //TODO: Do your thing here...

            return _jsonRpc.Execute(GetRequestJsonString(tags.Split('/')));
        }


        [Route("{*tags}")]
        [HttpHead]
        public HttpResponseMessage ExecuteHead(string tags)
        {

            //TODO: Do your thing here...
            var outputObject = (JsonRpcResponse)_jsonRpc.Execute(GetRequestJsonString(tags.Split('/')));
            Type myType = outputObject.result.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);

            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(outputObject.result, null) != null)
                    message.Headers.Add(prop.Name, prop.GetValue(outputObject.result, null).ToString());

            }
            return message;
        }


        private static string GetRequestJsonString(string[] tags)
        {
            StringBuilder param = new StringBuilder();
            for (int i = 1; i < tags.Length; i++)
            {
                param.Append(tags[i]);
            }
            var jsonStr = "{ \"jsonrpc\":\"2.0\",\"method\":" + "\"" + tags[0] + "\"" + ",\"params\":{" + ConvertStringArrayToString(tags) + "},\"id\":\"f8441312-5929-49dc-bd8b-467488fa7b64\"}";
            return jsonStr;
        }
        static string ConvertStringArrayToString(string[] array)
        {
            //
            // Concatenate all the elements into a StringBuilder.
            //
            StringBuilder builder = new StringBuilder();
            //if (array[1].Contains(":"))
            //{

            //    for (int i = 1; i < array.Length; i++)
            //    {
            //        var split = array[i].Split(':');
            //        builder.Append("\"" + split[0] + "\"" + ":" + "\"" + split[1] + "\"");
            //        if (i != array.Length - 1)
            //            builder.Append(',');
            //    }
            //}
            //else
            //{
            for (int i = 1; i < array.Length; i = i + 2)
            {
                builder.Append("\"" + array[i] + "\"" + ":" + "\"" + array[i + 1] + "\"");
                if (i != array.Length - 2)
                    builder.Append(',');
            }
            // }

            return builder.ToString();
        }

        private string GetRequestAsString()
        {

            var input = new StreamReader(Request.Body).ReadToEnd();
            return input.ToString();
            //Request.Body.re
            //var task = Request.Body.rea
            //task.Wait();
            //return task.Result;
        }
    };

    //public class JsonRpcHub : Hub
    //{
    //    private readonly JsonRpc _jsonRpc;

    //    public JsonRpcHub(JsonRpc jsonRpc)
    //    {
    //        _jsonRpc = jsonRpc;
    //    }

    //    public dynamic Execute(JObject obj)
    //    {
    //        var json = obj.ToString();
    //        var result = _jsonRpc.Execute(json);
    //        return result;
    //    }
    //};

    public class JsonRpcRequest
    {
        public string jsonrpc { get; set; }
        public string method { get; set; }
        public JObject @params { get; set; }
        public string id { get; set; }
    };

    public class JsonRpcResponse
    {
        public string jsonrpc { get; set; }
        public Object result { get; set; }
        public JsonRpcError error { get; set; }
        public string id { get; set; }
    }

    public class JsonRpcError
    {
        public int code { get; set; }
        public string message { get; set; }
        public Object data { get; set; }
    }

    public interface IJsonProcessor
    {
        Object Process(string name, string json);
    }
}
