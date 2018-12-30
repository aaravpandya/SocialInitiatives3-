using System.ComponentModel.DataAnnotations;

namespace SocialInitiatives3.Infrastructure
{
    public class Image
    {
        public int ImageId { get; set; }

        [Required] public string Name { get; set; }

        [Required] public byte[] Data { get; set; }

        [Required] public int Length { get; set; }

        [Required] public int Width { get; set; }

        [Required] public int Height { get; set; }

        [Required] public string ContentType { get; set; }
    }
}