#region MIT License

// ==========================================================
// 
// XafEFPlayground project - Copyright (c) 2023 JeePeeTee
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// ===========================================================

#endregion

#region usings

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XafEFPlayground.Module.BusinessObjects;
using XafEFPlayground.Module.Extensions;

#endregion

namespace XafEFPlayground.Module.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book> {
    // public static void BookConfuguration(ModelBuilder modelBuilder) {
    //     modelBuilder.Entity<Book>(builder => {
    //         builder.ToTable(XafEfPlaygroundConsts.DbTablePrefix + "Book", XafEfPlaygroundConsts.DbSchema);
    //         builder.ConfigureMyConventions(); //auto configure base class properties
    //         builder.Property(x => x.BookTitle).IsRequired().HasMaxLength(128);
    //         builder.Property<bool>("IsDeleted");
    //         builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
    //         builder.Property<decimal>("BasePrice").HasDefaultValue(0).HasColumnType("money");
    //         builder.HasIndex(e => e.BookTitle).IsUnique();
    //     });
    // }

    public void Configure(EntityTypeBuilder<Book> builder) {
            builder.ToTable(XafEfPlaygroundConsts.DbTablePrefix + "Book", XafEfPlaygroundConsts.DbSchema);
            builder.ConfigureMyConventions(); //auto configure base class properties
            builder.Property(x => x.BookTitle).IsRequired().HasMaxLength(128);
            builder.Property<bool>("IsDeleted");
            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
            builder.Property<decimal>("BasePrice").HasDefaultValue(0).HasColumnType("money");
            builder.HasIndex(e => e.BookTitle).IsUnique();
        }
}
