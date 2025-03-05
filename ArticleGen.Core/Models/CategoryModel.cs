using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGen.Core.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public ArticleModel[] Articles { get; set; }

        public class ArticleModel
        {
            public string Name { get; set; }
            public string Headline { get; set; }
        }
    }


}
