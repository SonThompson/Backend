
namespace BackendHelper.Helpers;

using BackendEntities.RequestModels.EntityModels;
using BackendEntities.Entities;
using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CategoryModel, Category>();
        CreateMap<Category, CategoryModel>();

        CreateMap<CustomerModel, Customer>();
        CreateMap<Customer, CustomerModel>();

        CreateMap<DeliveryModel, Delivery>();
        CreateMap<Delivery, DeliveryModel>();

        CreateMap<ManufactureModel, Manufacture>();
        CreateMap<Manufacture, ManufactureModel>();

        CreateMap<PriceChangeModel, PriceChange>();
        CreateMap<PriceChange, PriceChangeModel>();

        CreateMap<ProductModel, Product>();
        CreateMap<Product, ProductModel>();

        CreateMap<PurchaseItemModel, PurchaseItem>();
        CreateMap<PurchaseItem, PurchaseItemModel>();

        CreateMap<PurchaseModel, Purchase>();
        CreateMap<Purchase, PurchaseModel>();

        CreateMap<StoreModel, Store>();
        CreateMap<Store, StoreModel>();
    }
}

