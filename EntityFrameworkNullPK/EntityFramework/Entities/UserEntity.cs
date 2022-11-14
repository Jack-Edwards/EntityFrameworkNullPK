namespace EntityFrameworkNullPK.EntityFramework.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? CompanyId { get; set; }

        public CompanyEntity? Company { get; set; }
        public DivisionEntity? AsDivision { get; set; } // User entities may be Divisions in disguise
        public ICollection<UserDivisionEntity> UserDivisions { get; set; } = new List<UserDivisionEntity>();
    }
}
