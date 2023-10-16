namespace WebApplication1.Models
{
    public class UpdateCoupon
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }
    }
}
