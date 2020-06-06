using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace projectit.Models
{
    public class PostItViewModel : DefaultViewModel
    {
        public string header { get; set; }
        public string body { get; set; }
        public string doing_by { get; set; }
        public string status { get; set; }
            
    }
}
