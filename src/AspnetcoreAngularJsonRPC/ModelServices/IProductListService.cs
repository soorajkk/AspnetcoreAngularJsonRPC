

using AspnetcoreAngularJsonRPC.Models;
using System.Collections.Generic;


namespace AspnetcoreAngularJsonRPC.ModelServices
{
    public interface IProductListService
    {
        List<Product> GetProductList(int catogoryID);
        int CatogoryChageSet(int catogoryID);
    }
}
