
using AspnetcoreAngularJsonRPC.Models;
using System.Collections.Generic;


namespace AspnetcoreAngularJsonRPC.ModelServices
{
    public class ProductListService : IProductListService
    {
        public int CatogoryChageSet(int catogoryID)
        {
            return catogoryID +1;
        }

        public List<Product> GetProductList(int catogoryID)
        {
            return new List<Product>
            {

                new Product   {
                    ProductId= 1,
                    ProductName= "Leaf Rake",
                    ProductCode= "GDN-0011",
                    ReleaseDate= "March 19, 2016",
                    Description= "Leaf rake with 48-inch wooden handle.",
                    Price= 19.95,
                    StarRating= 3.2,
                    ImageUrl= "http://openclipart.org/image/300px/svg_to_png/26215/Anonymous_Leaf_Rake.png"
                },
                new Product{
                    ProductId= 2,
                    ProductName= "Garden Cart",
                    ProductCode= "GDN-0023",
                    ReleaseDate= "March 18, 2016",
                    Description= "15 gallon capacity rolling garden cart",
                    Price= 32.99,
                    StarRating= 4.2,
                    ImageUrl= "http://openclipart.org/image/300px/svg_to_png/58471/garden_cart.png"
                },
               new Product {
                    ProductId= 5,
                    ProductName= "Hammer",
                    ProductCode= "TBX-0048",
                    ReleaseDate= "May 21, 2016",
                    Description= "Curved claw steel hammer",
                    Price= 8.9,
                    StarRating= 4.8,
                    ImageUrl= "http://openclipart.org/image/300px/svg_to_png/73/rejon_Hammer.png"
                },
               new Product {
                    ProductId= 8,
                    ProductName= "Saw",
                    ProductCode= "TBX-0022",
                    ReleaseDate= "May 15, 2016",
                    Description= "15-inch steel blade hand saw",
                    Price= 11.55,
                    StarRating= 3.7,
                    ImageUrl= "http://openclipart.org/image/300px/svg_to_png/27070/egore911_saw.png"
                },
                 new Product{
                   ProductId= 10,
                    ProductName= "Video Game Controller",
                    ProductCode= "GMG-0042",
                    ReleaseDate= "October 15, 2015",
                    Description= "Standard two-button video game controller",
                    Price= 35.95,
                    StarRating= 4.6,
                    ImageUrl= "http://openclipart.org/image/300px/svg_to_png/120337/xbox-controller_01.png"
                }
            };
        }
    }
}