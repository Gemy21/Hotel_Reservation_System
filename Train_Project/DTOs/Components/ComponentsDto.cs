using System.ComponentModel.DataAnnotations;

namespace Train_Project.DTOs.Components
{
    public class ComponentsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";
    }

    public class CreateComponentsDto
    {
        [Required]
        public string Name { get; set; } = "";
    }
}
