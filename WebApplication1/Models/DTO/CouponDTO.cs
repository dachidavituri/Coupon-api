using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
    public class CouponDTO
    {
        
        public string Name { get; set; }
        public int Percent { get; set; }
        public bool IsActive { get; set; }
    }
}
