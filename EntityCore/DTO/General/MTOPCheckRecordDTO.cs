﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityCore.Attributte;

namespace EntityCore.DTO.General
{
	[Table("MTOPCheckRecords", Schema = "dbo")]
	
	[Condition("IsDeleted", 0)]
	public class MTOPCheckRecordDTO : BaseEntity
	{
		
		[Column("CheckName"), MaxLength(150)]
		public string CheckName { get; set; }

		
		[Column("Remarks")]
		public string Remarks { get; set; }

		
		[Column("RecordDate")]
		public DateTime? RecordDate { get; set; }

		
		[Column("GroupName")]
		public int? GroupName { get; set; }

		
		[Column("ParentId")]
		public int? ParentId { get; set; }

		
		[Column("IsControlPoint"), Required]
		public bool IsControlPoint { get; set; }

		
		[Column("CalculatedPerformanceSource")]
		public byte[] CalculatedPerformanceSource { get; set; }

		
		[Column("AverageUtilization"), MaxLength(50)]
		public byte[] AverageUtilization { get; set; }

		#region Navigation Property

		
		public MTOPCheckDTO MtopCheckDto { get; set; }

		#endregion
	}
}