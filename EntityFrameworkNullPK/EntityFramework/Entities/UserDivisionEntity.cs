namespace EntityFrameworkNullPK.EntityFramework.Entities
{
    public class UserDivisionEntity
    {
        public int UserId { get; set; }
        public int DivisionId { get; set; }

        public UserEntity User { get; set; } = null!;
        public DivisionEntity Division { get; set; } = null!;
        public ICollection<UserDivisionEntity> UserDivisions { get; set; } = new List<UserDivisionEntity>();
    }
}
