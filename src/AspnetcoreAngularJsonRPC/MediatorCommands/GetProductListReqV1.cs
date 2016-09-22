using AspnetcoreAngularJsonRPC.Models;
using AspnetcoreAngularJsonRPC.ModelServices;
using MediatR;

using System.Collections.Generic;


namespace AspnetcoreAngularJsonRPC.MediatorCommands
{
    public class GetProductListReqV1:IRequest<GetProductListReqV1.GetProductListResp>
    {
        public int CatogoryID { get; set; }    

        public class GetProductListResp
        {
            public string Message { get; set; }
            public List<Product> ProductList { get; set; }
        };

        public class GetProductListReqHandler : IRequestHandler<GetProductListReqV1, GetProductListResp>
        {
            IProductListService _IProductListService;
            public GetProductListReqHandler(IProductListService iProductListService)
            {
                _IProductListService = iProductListService;
            }
            public GetProductListResp Handle(GetProductListReqV1 request)
            {
                return new GetProductListResp
                {
                    Message = "Hello ",
                    ProductList = _IProductListService.GetProductList(request.CatogoryID)
                };
            }
        }
    }
}