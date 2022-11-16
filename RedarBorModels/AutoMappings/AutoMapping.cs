namespace RedarBorModels.AutoMappings
{
    using AutoMapper;
    using RedarBorModels.Entities;
    using RedarBorModels.Requests;
    using RedarBorModels.Responses;

    public  class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RedarBorEmployeeRequest, RedarBorEmployeesEntity>();
            CreateMap<RedarBorEmployUpdateRequest, RedarBorEmployeesEntity>();
        }
    }
}
