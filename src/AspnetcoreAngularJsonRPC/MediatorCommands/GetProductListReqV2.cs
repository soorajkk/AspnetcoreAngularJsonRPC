using AspnetcoreAngularJsonRPC.Models;
using AspnetcoreAngularJsonRPC.ModelServices;
using MediatR;
using System.Collections.Generic;


namespace AspnetcoreAngularJsonRPC.MediatorCommands
{
    public class GetProductListReqV2:IRequest<GetProductListReqV2.Response>
    {
        public int CatogoryID { get; set; }

        public class Response
        {          
            public List<Product> ProductList { get; set; }
        };

        public class Handler : IRequestHandler<GetProductListReqV2, Response>
        {
            IProductListService _IProductListService;
            public Handler(IProductListService iProductListService)
            {
                _IProductListService = iProductListService;
            }
            public Response Handle(GetProductListReqV2 request)
            {
                return new Response
                {
                    ProductList = _IProductListService.GetProductList(request.CatogoryID)
                };
            }
        }
    }
}