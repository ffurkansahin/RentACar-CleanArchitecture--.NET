using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
	public void Configure(EntityTypeBuilder<Model> builder)
	{
		builder.ToTable("Models").HasKey(m => m.Id);
		builder.Property(m=>m.Id).HasColumnName("Id").IsRequired();
		builder.Property(m=>m.Name).HasColumnName("Name").IsRequired();
		builder.Property(m => m.BrandId).HasColumnName("BrandId").IsRequired();
		builder.Property(m => m.FuelTypeId).HasColumnName("FuelTypeId").IsRequired();
		builder.Property(m => m.TransmissionId).HasColumnName("TransmissionId").IsRequired();
		builder.Property(m => m.DailyPrice).HasColumnName("DailyPrice").IsRequired();
		builder.Property(m => m.ImageUrl).HasColumnName("ImageUrl").IsRequired();

		builder.Property(m => m.CreatedDate).HasColumnName("CreatedDate").IsRequired();
		builder.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
		builder.Property(t => t.DeletedDate).HasColumnName("DeletedDated");

		builder.HasIndex(indexExpression: m => m.Name, name: "UK_Model_Name").IsUnique();
		builder.HasOne(m => m.Brand);
		builder.HasOne(m => m.FuelType);
		builder.HasOne(m=>m.Transmission);

		builder.HasMany(m => m.Cars);
		builder.HasQueryFilter(t => !t.DeletedDate.HasValue);
	}
}
