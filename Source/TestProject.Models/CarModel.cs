using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Models {
    public class CarModel : BaseModel {
        public int ColorId { get; set; }
        public string BrandName { get; set; }

        [ForeignKey(nameof(ColorId))]
        public ColorModel Color { get; set; }
    }
}
