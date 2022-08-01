using TestProject.Models;

namespace TestProject.ViewModels {
    public class CarViewModel : BaseViewModel<CarModel> {
        public ColorViewModel Color { get; set; }
        public string BrandName { get; set; }
    }
}
