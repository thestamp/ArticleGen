using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleGen.Core.Models
{
    public class FrontPageModel
    {
        public string Industry { get; set; }
        public class FrontPageCategoryModel
        {
            public string Category { get; set; }
            public string[] CategoryArticleNames { get; set; }
        }

        public FrontPageCategoryModel[] Categories { get; set; }
    }

    
}
