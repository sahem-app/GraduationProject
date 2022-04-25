using System.Linq;
using AutoMapper;
using GraduationProject.DTOs;
using GraduationProject.DTOs.Case;
using GraduationProject.DTOs.Mediator;
using GraduationProject.Models;
using GraduationProject.Models.CaseProperties;
using GraduationProject.Models.Location;
using GraduationProject.Models.Reviews;

namespace GraduationProject.Utilities
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<GeoLocationDto, GeoLocation>();

			CreateMap<RegisterDto, Mediator>()
				.ForMember(d => d.NationalIdImage, act => act.MapFrom(src => FormFileHandler.ConvertToBytes(src.NationalIdImage)))
				.ForMember(d => d.ProfileImage, act => act.MapFrom(src => FormFileHandler.ConvertToBytes(src.ProfileImage)));

			CreateMap<NewCaseDto, Case>()
				.ForMember(d => d.NationalIdImage, act => act.MapFrom(src => FormFileHandler.ConvertToBytes(src.NationalIdImage)))
				.ForMember(d => d.Images, act => act.MapFrom(src => src.OptionalImages.Select(i => new Image(FormFileHandler.ConvertToBytes(i)))));

			CreateMap<ReviewDto, MediatorReview>();
			CreateMap<ReviewDto, CaseReview>()
				.ForMember(d => d.CaseId, act => act.MapFrom(src => src.RevieweeId));
		}
	}
}
