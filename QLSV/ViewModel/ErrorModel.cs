using System.Collections.Generic;

namespace QLSV.ViewModel
{
    public class ErrorModel
    {
        public List<string> errors { set; get; }

        public ErrorModel()
        {
            errors = new List<string>();
        }

        public void Add(string error)
        {
            errors.Add(error);
        }
    }
}