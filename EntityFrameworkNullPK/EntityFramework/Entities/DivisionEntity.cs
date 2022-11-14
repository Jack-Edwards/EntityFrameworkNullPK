namespace EntityFrameworkNullPK.EntityFramework.Entities
{
    public class DivisionEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CompanyId { get; set; }

        public CompanyEntity? Company { get; set; }
        public UserEntity AsUser { get; set; } = null!; // Division entities are also user entities is disguise
        public List<UserDivisionEntity> DivisionUsers = new List<UserDivisionEntity>();
    }
}
