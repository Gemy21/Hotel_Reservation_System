namespace Train_Project.Entities
{
    public class VipRoom
    {
        public int Id { get; set; }

        public decimal LivingArea { get; set; }

        public bool LateCheckOutAllowed { get; set; }

        public TimeOnly? LateCheckOutTime { get; set; }

        public decimal LateCheckOutFee { get; set; }

        public virtual Room Room { get; set; } = null!;
    }
}
