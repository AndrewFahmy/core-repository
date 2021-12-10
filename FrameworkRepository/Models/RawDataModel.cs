using System.Collections.Generic;

namespace FrameworkRepository.Models
{
    public class RawDataModel
    {
        public RawDataModel()
        {
            Rows = new List<object[]>(10);
        }

        public List<string> Headers { get; set; }

        public List<object[]> Rows { get; set; }
    }
}
