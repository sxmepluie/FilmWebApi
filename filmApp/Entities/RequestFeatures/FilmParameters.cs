namespace Entities.RequestFeatures
{
    public class FilmParameters : RequestParameters
	{
		public String? SearchTerm { get; set; }
        public FilmParameters()
        {
            OrderBy = "id";
        }
    }
}
