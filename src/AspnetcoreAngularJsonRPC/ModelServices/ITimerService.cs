using AspnetcoreAngularJsonRPC.Models.Shared;

namespace AspnetcoreAngularJsonRPC.ModelServices
{
    public interface ITimerService
    {
        TimeResponseVM GetTimeResponseServiceCall(TimeRequestVM timeRequest);
    }
}
