namespace EnglishWords.Core;

public class Word
{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Meaning { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Repetitions { get; set; } = 0;  // Veces que se ha repasado
        public double EaseFactor { get; set; } = 2.5; // Factor de facilidad
        public int Interval { get; set; } = 1; // Intervalo en días
        public DateTime NextReviewDate { get; set; } = DateTime.UtcNow;
        public int ClientId { get; set; }
}
