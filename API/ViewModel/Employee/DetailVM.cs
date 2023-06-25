using API.Utilities;

namespace API.ViewModel.Employee
{
    public class DetailVM
    {
        public Guid Guid { get; set; }
        public string NIK { get; set; }
        public string Fullname { get; set; }
        public GenderLevel Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid? ManagerID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string SubjectReport { get; set; }
        public string DescriptionReport { get; set; }
        public string FileName { get; set; }
        public float RatingValue { get; set; }
        public string Comment { get; set; }
    }
}
