using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace goRoam.MobileForms.Models
{
    public class MobileFormQuestion
    {
        public MobileFormQuestion()
        {
            IsRequired = false;
            IsValid = true;
            MinValue = double.MinValue;
            MaxValue = double.MaxValue;
        }

        public QuestionTypes QuestionType { get; set; }
        public string Prompt { get; set; }
        public string HelperText { get; set; }
        public string ID { get; set; }
        public long ReferenceNumber { get; set; }
        public object Value { get; set; }
        public object DefaultValue { get; set; }
        public bool IsReadOnly { get; set; }
        public List<string> Options { get; set; }
        public bool IsRequired { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        [JsonIgnore]
        public bool IsValid { get; set; }
    }

    public enum QuestionTypes
    {
        Text = 0,
        Email,
        Website,
        PhoneNumber,
        Number,
        Checkbox,
        Picker,
        Picture,
        Signature,
        Label,
        DateTime
    }
}
