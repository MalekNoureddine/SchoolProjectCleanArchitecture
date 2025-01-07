using CleanArchProject.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchProject.Data.Entities.Identities;

namespace CleanArchProject.Infrastracture.Configurations
{
    public class ResetPasswordConfiguration : IEntityTypeConfiguration<ResetPassword>
    {
        public void Configure(EntityTypeBuilder<ResetPassword> builder)
        {
            builder.ToTable("ResetPasswords");
            builder.HasKey(x => x.Token).IsClustered();

            builder.Property(rp => rp.ExpirationDate)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(rp => rp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
