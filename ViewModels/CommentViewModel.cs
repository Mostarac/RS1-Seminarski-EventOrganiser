using System;
using System.Collections.Generic;

namespace webapp.ViewModels
{
    public class CommentViewModel
    {
        public int EventId { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public string UserId { get; set; }
            public string Username { get; set; }
            public DateTime CreatedDate { get; set; }
            public int EventId { get; set; }
        }
    }
}
