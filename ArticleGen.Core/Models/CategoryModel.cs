using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGen.Core.Models
{
    public class CategoryModel
    {
        public string Industry { get; set; }
        public string CategoryName { get; set; }
        public ArticleModel[] CategoryArticles { get; set; }

        public class ArticleModel
        {
            public string ArticleName { get; set; }
            public string ArticleHeadline { get; set; }
        }
    }


}
