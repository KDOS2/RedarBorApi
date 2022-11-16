namespace RedarBorModels.Entities
{
    public class RedarBorEmployeesEntity
    {
        public long id { get; set; }
        public long CompanyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public string? Name { get; set; }
        public DateTime? Lastlogin { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public long PortalId { get; set; }
        public long RoleId { get; set; }
        public long StatusId { get; set; }
        public string? Telephone { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
