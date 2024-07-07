using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetCore.MvcApp2.Models;

[Table("Tbl_Blog")]
public class BlogModel
{
    // Have to use this above the that is supposed to be a primary key in the table
    [Key]
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
}

public class MessageModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}
