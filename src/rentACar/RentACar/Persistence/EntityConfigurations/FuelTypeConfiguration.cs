using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FuelTypeConfiguration : IEntityTypeConfiguration<FuelType>
{
	public void Configure(EntityTypeBuilder<FuelType> builder)
	{
		builder.ToTable("FuelTypes").HasKey(f => f.Id);
		builder.Property(f => f.Id).HasColumnName("Id").IsRequired();
		builder.Property(f => f.Name).HasColumnName("Name").IsRequired();
		builder.Property(f => f.CreatedDate).HasColumnName("CreatedDate").IsRequired();
		builder.Property(f => f.UpdatedDate).HasColumnName("UpdatedDate");
		builder.Property(f => f.DeletedDate).HasColumnName("DeletedDated");

		builder.HasIndex(indexExpression: f => f.Name, name: "UK_FuelTypes_Name").IsUnique();
		builder.HasMany(f =>f.Models);

		builder.HasQueryFilter(f => !f.DeletedDate.HasValue);
	}
}
