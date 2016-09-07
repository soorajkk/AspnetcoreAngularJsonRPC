using MediatR;

namespace AspnetcoreAngularJsonRPC.MediatorCommands
{
    public class ShowTimerResult : IRequest<ShowTimerResult.Response>
    {
        //public bool IsTimeNeeded { get; set; }
        public class Response
        {
            public string Message { get; set; }
            // public TimeResponseVM MyProperty { get; set; }
        };

        //public class ShowTimerResultHandler : IRequestHandler<ShowTimerResult, Response>
        //{
        //    ITimerService _iTimerService;
        //    public ShowTimerResultHandler(ITimerService iITimerService)
        //    {
        //        _iTimerService = iITimerService;
        //    }
        //    public Response Handle(ShowTimerResult request)
        //    {
        //        return new Response
        //        {
        //            Message = "Hello ",
        //            MyProperty = _iTimerService.GetTimeResponseServiceCall(new TimeRequestVM() { IsTimeNeeded = request.IsTimeNeeded })
        //        };
        //    }
        //}
    }
}
