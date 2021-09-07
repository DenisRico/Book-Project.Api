using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Common.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Models
{
    public class Book
    {
        //public Book()
        //{

        //}
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }

    }

    //internal class BookMap : IEntityTypeConfiguration<Book>
    //{
    //    public static BookMap Instance = new BookMap();

    //    public void Configure(EntityTypeBuilder<Book> builder)
    //    {
    //        builder.HasKey(item => item.Id);

    //        builder.Property(item => item.Name).IsRequired().HasMaxLength(10);

    //        builder.Property(item => item.Author).IsRequired();

    //    }
    //}
}
