namespace ProductShop
{
    using AutoMapper;
    using ProductShop.DataTransferObjects;
    using ProductShop.Models;
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<User, UserInputModel>().ReverseMap();
            this.CreateMap<Product, UserInputProduct>().ReverseMap();
            this.CreateMap<Category, UserInputCategory>().ReverseMap();
            this.CreateMap<CategoryProduct, UserInputCategoryProduct>().ReverseMap();
        }
    }
}
