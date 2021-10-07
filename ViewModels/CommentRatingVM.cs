using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.ViewModels
{
    public class CommentRatingVM
    {

        public class Row
        {
            public int id { get; set; }
            public string Text { get; set; }
            public int Rating { get; set; }
            public string User { get; set; }
            public string EventName { get; set; }
        }

        public string q { get; set; }

        public List<Row> commentRatings { get; set; }

        public int total { get; set; }

    }
}
