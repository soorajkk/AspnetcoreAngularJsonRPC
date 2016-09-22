using AspnetcoreAngularJsonRPC.ModelServices;
using MediatR;



namespace AspnetcoreAngularJsonRPC.MediatorCommands
{
    public class IsProductListChangedReqV1: IRequest<IsProductListChangedReqV1.IsProductListChangedResp>
    {
        public int CatogoryID { get; set; }

        public class IsProductListChangedResp
        {
            public int Balha { get; set; }
        };

        public class IsProductListChangedReqHandler : IRequestHandler<IsProductListChangedReqV1, IsProductListChangedResp>
        {
            IProductListService _IProductListService;
            public IsProductListChangedReqHandler(IProductListService iProductListService)
            {
                _IProductListService = iProductListService;
            }
            public IsProductListChangedResp Handle(IsProductListChangedReqV1 request)
            {
                return new IsProductListChangedResp
                {
                    Balha = _IProductListService.CatogoryChageSet(request.CatogoryID)
                };
            }
        }
    }
}