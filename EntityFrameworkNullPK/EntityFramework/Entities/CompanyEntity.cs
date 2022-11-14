namespace EntityFrameworkNullPK.EntityFramework.Entities
{
    public class CompanyEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<DivisionEntity> Divisions = new List<DivisionEntity>();
        public List<UserEntity> Users = new List<UserEntity>();
    }
}
