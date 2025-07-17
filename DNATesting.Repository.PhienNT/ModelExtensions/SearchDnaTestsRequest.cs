using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATesting.Repository.PhienNT.ModelExtensions
{
    public class SearchDnaTestsRequest
    {
        public string testType { get; set; }
        public bool? isCompleted { get; set; }
         public int page { get; set; }
        public int pageSize {  get; set; }
    }
}
