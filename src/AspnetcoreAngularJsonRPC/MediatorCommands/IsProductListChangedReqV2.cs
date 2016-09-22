using AspnetcoreAngularJsonRPC.ModelServices;
using MediatR;


namespace AspnetcoreAngularJsonRPC.MediatorCommands
{
    public class IsProductListChangedReqV2: IRequest<IsProductListChangedReqV2.IsProductListChangedRespv2>
    {
        public int CatogoryID { get; set; }

        public class IsProductListChangedRespv2
        {
            public int CatogoryID { get; set; }
        };

        public class IsProductListChangedReqV2Handler : IRequestHandler<IsProductListChangedReqV2, IsProductListChangedRespv2>
        {
            IProductListService _IProductListService;
            public IsProductListChangedReqV2Handler(IProductListService iProductListService)
            {
                _IProductListService = iProductListService;
            }

            public IsProductListChangedRespv2 Handle(IsProductListChangedReqV2 message)
            {
                return new IsProductListChangedRespv2
                {
                    CatogoryID = _IProductListService.CatogoryChageSet(message.CatogoryID)
                };
            }
            
        }
    }
}