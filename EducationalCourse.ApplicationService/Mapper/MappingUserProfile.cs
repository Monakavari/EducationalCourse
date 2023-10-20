using AutoMapper;
using EducationalCourse.Common.Dtos.User;
using EducationalCourse.Common.Extensions;
using EducationalCourse.Common.Utilities.Generator;
using EducationalCourse.Common.Utilities.Security;
using EducationalCourse.Domain.Models.Account;

namespace EducationalCourse.ApplicationService.Mapper
{
    public class MappingUserProfile : Profile
    {
        public MappingUserProfile()
        {
            CreateMap<User, SignUpDto>();

            CreateMap<SignUpDto, User>()
                .ForMember(des => des.Password, opt => opt.MapFrom(src => PasswordHelper.EncodePasswordMd5(src.Password)))
                .ForMember(des => des.ActiveCode, opt => opt.MapFrom(src => NameGenerator.GenerateUniqCode()))
                .ForMember(des => des.IsDelete, opt => opt.MapFrom(src => false));


            //CreateMap<User, UserAccountInfoDto>()
            //    .ForMember(des => des.CreateDate, opt => opt.MapFrom(src => src.CreateDate));

            //CreateMap<UserAccountInfoDto, User>();
        }
    }
}
