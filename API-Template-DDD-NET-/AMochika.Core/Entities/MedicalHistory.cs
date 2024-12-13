namespace AMochika.Core.Entities;
    public class MedicalHistory
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Diagnosis { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string Notes { get; set; }

        // Relationship with Client
        public Client Client { get; set; }
    }