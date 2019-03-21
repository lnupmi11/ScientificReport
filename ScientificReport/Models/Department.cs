using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
  public class Department
  {
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }
  }
}
