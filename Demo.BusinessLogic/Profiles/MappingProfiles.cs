using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.DataAccess.models.EmployeeModel;
using Microsoft.Extensions.Options;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(des => des.EmpGender, Options => Options.MapFrom(Src => Src.Gender))
                .ForMember(des => des.EmpType, Options => Options.MapFrom(Src => Src.EmployeeType));

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(des => des.Gender, Options => Options.MapFrom(Src => Src.Gender))
                .ForMember(des => des.EmployeeType, Options => Options.MapFrom(Src => Src.EmployeeType))
               .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)));

            CreateMap<CreatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

        }

    }
}
