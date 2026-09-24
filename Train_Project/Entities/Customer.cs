using Train_Project.Authentication.AuthEntity;

namespace Train_Project.Entities
{
    public partial class Customer
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string Location { get; set; } = null!;

        public string Email { get; set; } = null!;

        public long? Phone { get; set; }

        public int UserId { get; set; }

        public virtual Users User { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations { get; set; }
            = new List<Reservation>();
    }
}