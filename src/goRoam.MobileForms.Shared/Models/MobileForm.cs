using System;
using System.Collections.Generic;
using System.Linq;
using HandlebarsDotNet;
using Newtonsoft.Json;

namespace goRoam.MobileForms.Models
{
    public class MobileForm
    {
        public static MobileForm Create(string formJson, object context = null)
        {
            var resultJson = formJson;
            if (context != null)
            {
                var formTemplate = Handlebars.Compile(formJson);

                resultJson = formTemplate(context);
            }

            var form = JsonConvert.DeserializeObject<MobileForm>(resultJson);

            return form;
        }

        public string Name { get; set; }
        public List<MobileFormPage> Pages { get; set; }

        private bool _isReadonly = false;
        [JsonIgnore]
        public bool IsReadonly
        {
            get => _isReadonly;
            set
            {
                _isReadonly = value;

                if (_isReadonly)
                {
                    Pages.ForEach(p =>
                    {
                        p.Questions.ForEach(q =>
                        {
                            q.IsReadOnly = true;
                        });
                    });
                }
            }
        }

        [JsonIgnore]
        public bool IsValid
        {
            get => Pages.All(p => p.IsValid);
        }

        public MobileForm()
        {
            Pages = new List<MobileFormPage>();
        }
    }
}
