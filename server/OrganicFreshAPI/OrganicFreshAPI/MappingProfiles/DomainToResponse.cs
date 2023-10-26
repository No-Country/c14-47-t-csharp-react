using AutoMapper;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.MappingProfiles;

public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
        {
            CreateMap<Product, GetProductResponse>()
                .ForMember(dest => dest.categoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Category, GetProductResponse>()
                .ForMember(dest => dest.price, opt => opt.Ignore()) // Ignore properties that don't apply to Category
                .ForMember(dest => dest.categoryId, opt => opt.Ignore())
                .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.weightUnit, opt => opt.Ignore())
                .ForMember(dest => dest.stock, opt => opt.Ignore());

            // CreateMap<Sale, GetSaleResponse>()
            //     .ForMember(dest => dest.productId, opt => opt.MapFrom(src => src.CheckoutsDetails.FirstOrDefault()?.ProductId))
            //     .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.CheckoutsDetails.FirstOrDefault()?.Product.Name))
            //     .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.CheckoutsDetails.FirstOrDefault()?.Quantity))
            //     .ForMember(dest => dest.total, opt => opt.MapFrom(src => src.CheckoutsDetails.FirstOrDefault()?.Total));

        }
    }
}