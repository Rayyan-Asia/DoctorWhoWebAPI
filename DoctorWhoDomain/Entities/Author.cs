using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorWhoDomain
{
    public class Author
    {
        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public List<Episode>? Episodes { get; set; }

        public Author() {
            Episodes = new List<Episode>();
        }
    }
}