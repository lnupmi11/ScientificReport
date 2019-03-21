using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ScientificReport.Models
{
  public class ScientificWork
  {
    public int Id { get; set; }
    public string Cypher { get; set; }
    public string Category { get; set; }
    public string Theme { get; set; }
    public string Content { get; set; }
    public Department Department { get; set; }
  }
}
