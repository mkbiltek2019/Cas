using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityCore.Attributte;
using EntityCore.DTO.General;
using Newtonsoft.Json;

namespace EntityCore.DTO.Dictionaries
{
	[Table("AircraftOtherParameters", Schema = "Dictionaries")]
	
	[Condition("IsDeleted", 0)]
	public class AircraftOtherParameterDTO : BaseEntity, IBaseDictionary
	{
		
		[Column("Name"), MaxLength(50)]
		public string Name { get; set; }

		
		[Column("FullName"), MaxLength(256)]
		public string FullName { get; set; }


		#region Navigation Property

		[JsonIgnore]
		public ICollection<AircraftEquipmentDTO> AircraftEquipmentDtos { get; set; }

		#endregion
	}
}
