namespace Restaurant.WebUi.Models
{
    public class AboutInfo
    {
        public int Id { get; set; }
        public string Subtitle { get; set; }           // Something new (intro_subtitle)
        public string Title { get; set; }              // An Extraordinery Experience (intro_title)
        public string Description { get; set; }        // Intro text (intro_text)
        public string ImagePath { get; set; }          // Intro image path

        public string ChefsSubtitle { get; set; }      // Chefs subtitle (section_subtitle)
        public string ChefsTitle { get; set; }         // Chefs title (section_title)
        public string ChefsDescription1 { get; set; }  // Left text under chefs section
        public string ChefsDescription2 { get; set; }  // Right text under chefs section
    }

}
