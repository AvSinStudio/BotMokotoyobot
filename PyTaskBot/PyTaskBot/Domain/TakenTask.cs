using System;
using System.Globalization;
using Newtonsoft.Json;

namespace PyTaskBot.Domain
{
    public class TakenTask
    {
        [JsonProperty("student")]
        private string StudentRaw { get; set; }

        [JsonProperty("first_date")]
        private string TakenDateRaw { get; set; }

        [JsonProperty("second_date")]
        private string LastSubmitDateRaw { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        public Student Student
        {
            get
            {
                var name = StudentRaw.Split(' ');
                return new Student() {FirstName = name[0], LastName = name[1]};
            }
        }
       

        public DateTime TakenDate => DateTime.Parse(TakenDateRaw);
        public DateTime LastSubmitDate => DateTime.Parse(LastSubmitDateRaw);

      
    }
}