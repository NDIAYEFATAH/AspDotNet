namespace ApiAspNet.Models.Reservations
{
    public class UpdateReservationRequest
    {
        public DateTime? DateReservation { get; set; }

        public float? MontantReservation { get; set; }

        public string StatutReservation { get; set; }

        public int? ClientId { get; set; }
        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional 
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
