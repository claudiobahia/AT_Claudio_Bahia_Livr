using Editora.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Editora.Repository.Mapping
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Lvro");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();
            builder.Property(x => x.ano).IsRequired();
            builder.Property(x => x.isbn).IsRequired();
            builder.Property(x => x.titulo).IsRequired();

            builder.Property(x => x.autorid).IsRequired();
        }
    }
}
