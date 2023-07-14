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
using XafEFPlayground.Module.Entities;

#endregion

namespace XafEFPlayground.Module.Extensions;

public static class EntityTypeBuilderExtensions {
    public static void ConfigureMyConventions(this EntityTypeBuilder b) {
        b.ConfigureConcurrencyStamp();
        // b.ConfigureExtraProperties();
        // b.ConfigureObjectExtensions();
        b.ConfigureHaveCreator();
        b.ConfigureSoftDelete();
        b.ConfigureDeletionTime();
        b.ConfigureDeletionAudited();
        b.ConfigureCreationTime();
        b.ConfigureLastModificationTime();
        b.ConfigureModificationAudited();
        // b.ConfigureMultiTenant();
    }

    public static void ConfigureConcurrencyStamp<T>(this EntityTypeBuilder<T> b) where T : class, IHaveConcurrencyStamp {
        b.As<EntityTypeBuilder>().ConfigureConcurrencyStamp();
    }

    private static void ConfigureConcurrencyStamp(this EntityTypeBuilder b) {
        if (b.Metadata.ClrType.IsAssignableTo<IHaveConcurrencyStamp>()) {
            b.Property(nameof(IHaveConcurrencyStamp.Version))
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName(nameof(IHaveConcurrencyStamp.Version));
        }
    }

    public static void ConfigureHaveCreator(this EntityTypeBuilder b) {
        if (b.Metadata.ClrType.IsAssignableTo<IHaveCreator>()) {
            b.Property(nameof(IHaveCreator.CreatedBy))
                .IsRequired(false)
                .HasColumnName(nameof(IHaveCreator.CreatedBy));
        }
    }

    public static void ConfigureSoftDelete(this EntityTypeBuilder b) {
        if (b.Metadata.ClrType.IsAssignableTo<ISoftDelete>()) {
            b.Property(nameof(ISoftDelete.IsDeleted))
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName(nameof(ISoftDelete.IsDeleted));
        }
    }

    public static void ConfigureDeletionTime(this EntityTypeBuilder b) {
        if (b.Metadata.ClrType.IsAssignableTo<IHaveDeletionTime>()) {
            b.ConfigureSoftDelete();

            b.Property(nameof(IHaveDeletionTime.Deleted))
                .IsRequired(false)
                .HasColumnName(nameof(IHaveDeletionTime.Deleted));
        }
    }

    public static void ConfigureDeletionAudited(this EntityTypeBuilder b) {
        if (b.Metadata.ClrType.IsAssignableTo<IDeletionAuditedObject>()) {
            b.ConfigureDeletionTime();

            b.Property(nameof(IDeletionAuditedObject.DeletedBy))
                .IsRequired(false)
                .HasColumnName(nameof(IDeletionAuditedObject.DeletedBy));
        }
    }

    public static void ConfigureCreationTime(this EntityTypeBuilder b) {
        if (b.Metadata.ClrType.IsAssignableTo<IHaveCreationTime>()) {
            b.Property(nameof(IHaveCreationTime.Created))
                .IsRequired()
                .HasColumnName(nameof(IHaveCreationTime.Created));
        }
    }

    public static void ConfigureLastModificationTime(this EntityTypeBuilder b) {
        if (b.Metadata.ClrType.IsAssignableTo<IHaveModificationTime>()) {
            b.Property(nameof(IHaveModificationTime.LastModified))
                .IsRequired(false)
                .HasColumnName(nameof(IHaveModificationTime.LastModified));
        }
    }

    public static void ConfigureModificationAudited(this EntityTypeBuilder b) {
        if (b.Metadata.ClrType.IsAssignableTo<IModificationAuditedObject>()) {
            b.ConfigureLastModificationTime();

            b.Property(nameof(IModificationAuditedObject.LastModifiedBy))
                .IsRequired(false)
                .HasColumnName(nameof(IModificationAuditedObject.LastModifiedBy));
        }
    }
}