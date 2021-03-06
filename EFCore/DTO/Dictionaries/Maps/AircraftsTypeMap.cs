﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EFCore.DTO.Dictionaries;

namespace EFCore.DTO.Maps
{
	public class AircraftsTypeMap : EntityTypeConfiguration<AircraftsTypeDTO>
	{
		public AircraftsTypeMap()
		{
			ToTable("Dictionaries.AircraftsTypes");

			HasKey(i => i.ItemId);
			Property(i => i.ItemId)
				.HasColumnName("ItemId");

			Property(i => i.ShortName)
				.IsRequired()
				.HasMaxLength(50)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("ShortName");

			Property(i => i.FullName)
				.IsRequired()
				.HasMaxLength(150)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("FullName");
		}
	}
}