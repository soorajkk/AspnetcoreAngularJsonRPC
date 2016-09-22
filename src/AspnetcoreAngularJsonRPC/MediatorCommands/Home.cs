using MediatR;


namespace AspnetcoreAngularJsonRPC.MediatorCommands
{
    public class Home : IRequest<Home.Response>
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string jaba { get; set; }

        public class Response
        {
            public string Message { get; set; }
        };

        public class Handler : IRequestHandler<Home, Response>
        {
            public Response Handle(Home request)
            {
                return new Response
                {
                    Message = "Hello " + request.Name + request.ID + request.jaba
                };
            }
        }
    };
}
