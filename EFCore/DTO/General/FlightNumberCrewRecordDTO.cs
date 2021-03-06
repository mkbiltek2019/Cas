﻿using System.Runtime.Serialization;
using EFCore.Attributte;
using EFCore.DTO.Dictionaries;

namespace EFCore.DTO.General
{
	[DataContract(IsReference = true)]
	[Condition("IsDeleted", 0)]
	public class FlightNumberCrewRecordDTO : BaseEntity
	{
		[DataMember]
		public int? FlightNumberId { get; set; }

		[DataMember]
		public int? SpecializationId { get; set; }

		[DataMember]
		public int? Count { get; set; }

		[DataMember]
		[Include]
		public FlightNumberDTO FlightNumber { get; set; }

		[DataMember]
		[Include]
		public SpecializationDTO Specialization { get; set; }

	}
}