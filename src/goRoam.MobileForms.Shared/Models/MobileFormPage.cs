using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace goRoam.MobileForms.Models
{
    public class MobileFormPage
    {
        public List<MobileFormQuestion> Questions { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public long ReferenceNumber { get; set; }

        [JsonIgnore]
        public bool IsValid
        {
            get => Questions.All(q => q.IsValid);
        }

        public MobileFormPage()
        {
            Questions = new List<MobileFormQuestion>();
        }
    }
}
