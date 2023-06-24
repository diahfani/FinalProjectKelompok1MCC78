namespace API.ViewModel.Rating
{
    public class RatingVM
    {
        public Guid Guid { get; set; }
        public float RatingValue { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
