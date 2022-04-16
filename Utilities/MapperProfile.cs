using AutoMapper;
using GraduationProject.DTOs;
using GraduationProject.DTOs.Case;
using GraduationProject.DTOs.Mediator;
using GraduationProject.Models;
using GraduationProject.Models.Location;
using GraduationProject.Models.Reviews;

namespace GraduationProject.Utilities
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<GeoLocationDto, GeoLocation>();
			CreateMap<RegisterDto, Mediator>();
			CreateMap<NewCaseDto, Case>().ForMember(d => d.NationalIdImage, act => act.Ignore());
			CreateMap<ReviewDto, CaseReview>();
		}
	}
}
