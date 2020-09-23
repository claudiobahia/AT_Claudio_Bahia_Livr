using Editora.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Editora.Repository.Mapping
{
    public class AutorMap : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autor");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();
            builder.Property(x => x.nome).IsRequired();
            builder.Property(x => x.sobrenome).IsRequired();
            builder.Property(x => x.email).IsRequired();
            builder.Property(x => x.datanascimento).IsRequired();

            builder.HasMany(x => x.livros).WithOne(x => x.autor);
        }
    }
}
